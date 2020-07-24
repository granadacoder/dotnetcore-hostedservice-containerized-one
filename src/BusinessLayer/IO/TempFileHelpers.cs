using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.IO
{
    public class TempFileHelpers
    {
        public const string ErrorMessageILoggerFactoryWrapperIsNull = "ILoggerFactoryWrapper is null";
        public const string ErrorMessageIFileSystemIsNull = "IFileSystem is null";

        private readonly ILogger<TempFileHelpers> logger;
        private readonly IFileSystem fileSystem;

        public TempFileHelpers(ILoggerFactory loggerFactory, IFileSystem fileSystem)
        {
            if (null == loggerFactory)
            {
                throw new ArgumentNullException(ErrorMessageILoggerFactoryWrapperIsNull, (Exception)null);
            }

            this.logger = loggerFactory.CreateLogger<TempFileHelpers>();

            this.fileSystem = fileSystem ?? throw new ArgumentNullException(ErrorMessageIFileSystemIsNull, (Exception)null);
        }

        public string WriteToTempFile(string uid, string contents, string extension)
        {
            // Writes text to a temporary file and returns path 
            string fileName = this.GetTempFileNameWithExtension(uid, extension);
            return this.WriteContentsToConcreteFile(contents, fileName);
        }

        private string WriteContentsToConcreteFile(string contents, string fileName)
        {
            this.fileSystem.File.WriteAllText(fileName, contents);
            return fileName;
        }

        private string GetTempFileNameWithExtension(string uid, string extension)
        {
            string fileName = System.IO.Path.GetTempFileName();
            fileName = fileName.Replace(".tmp", extension);
            fileName = System.IO.Path.Combine(this.GetUserTempPath(uid), System.IO.Path.GetFileName(fileName));
            return fileName;
        }

        private string GetUserTempPath(string uid)
        {
            string path = Path.GetTempPath();

            string linuxHasNoSpecificUserTempFolderWorkAround = string.Empty;
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                linuxHasNoSpecificUserTempFolderWorkAround = new string(Environment.UserName.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray());
            }

            path = Path.Combine(path, linuxHasNoSpecificUserTempFolderWorkAround, uid);
            Directory.CreateDirectory(path);

            return path;
        }
    }
}

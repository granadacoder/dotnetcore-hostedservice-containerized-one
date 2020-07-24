using System;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.Demos;
using MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.Demos.Interfaces;
using MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.HostedServices;
using MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.IO;
using MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.Managers;
using MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.Managers.Interfaces;
using MyCompany.MyExamples.WorkerServiceExampleOne.Domain.Dtos;
using MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.EntityFramework;
using MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.EntityFramework.Contexts;
using MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.Interfaces;

using Serilog;

namespace MyCompany.MyExamples.WorkerServiceExampleOne.ConsoleOne
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            /* easy concrete logger that uses a file for demos */
            Serilog.ILogger lgr = new Serilog.LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("MyCompany.MyExamples.WorkerServiceExampleOne.ConsoleOne.log.txt", rollingInterval: Serilog.RollingInterval.Day)
                .CreateLogger();

            try
            {
                /* look at the Project-Properties/Debug(Tab) for this environment variable */
                string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Console.WriteLine(string.Format("ASPNETCORE_ENVIRONMENT='{0}'", environmentName));
                Console.WriteLine(string.Empty);

                Console.WriteLine(string.Format("Environment.UserName='{0}'", Environment.UserName));
                Console.WriteLine(string.Empty);

                string basePath = Directory.GetCurrentDirectory();
                basePath = GetBasePath();

                Console.WriteLine(string.Format("GetBasePath='{0}'", basePath));
                Console.WriteLine(string.Empty);

                // when using single file exe, the hosts config loader defaults to GetCurrentDirectory
                // which is where the exe is, not where the bundle (with appsettings) has been extracted.
                // when running in debug (from output folder) there is effectively no difference
                string realPath = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
                Console.WriteLine(string.Format("realPath='{0}'", realPath));
                Console.WriteLine(string.Empty);

                IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(realPath)
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                        .AddEnvironmentVariables();

                IConfigurationRoot configuration = builder.Build();

                using (IHost host = Host.CreateDefaultBuilder(args)
                      .UseContentRoot(realPath)
                    .UseSystemd()
                    .ConfigureServices((_, services) => AppendDi(services, configuration, lgr)).Build())
                {
                    await host.StartAsync();

                    await host.WaitForShutdownAsync();
                }
            }
            catch (Exception ex)
            {
                string flattenMsg = GenerateFullFlatMessage(ex, true);
                Console.WriteLine(flattenMsg);
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();

            return 0;
        }

        private static string GetBasePath()
        {
            System.Diagnostics.ProcessModule processModule = System.Diagnostics.Process.GetCurrentProcess().MainModule;
            return Path.GetDirectoryName(processModule?.FileName);
        }

        private static string GenerateFullFlatMessage(Exception ex)
        {
            return GenerateFullFlatMessage(ex, false);
        }

        private static void AppendDi(IServiceCollection servColl, IConfiguration configuration, Serilog.ILogger lgr)
        {
            servColl
                .AddSingleton(lgr)
                .AddLogging()
                .AddSingleton<IBoardGameManager, BoardGameManager>()
                .AddSingleton<IBoardGameDataLayer, BoardGameEntityFrameworkDataLayer>()
                .AddSingleton<IMyParentManager, MyParentManager>()
                .AddSingleton<IMyParentDomainData, MyParentEntityFrameworkDomainDataLayer>()
                .AddSingleton<IParentDemos, ParentDemos>()
                .AddSingleton<IMyChildManager, MyChildManager>()
                .AddSingleton<IMyChildDomainData, MyChildEntityFrameworkDomainDataLayer>();

            servColl.AddSingleton<TempFileHelpers, TempFileHelpers>();
            servColl.AddSingleton<IFileSystem, FileSystem>();
            servColl.AddHostedService<TimedHostedService>();

            servColl.Configure<MyConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            servColl.AddLogging(blder =>
            {
                blder.AddConsole().SetMinimumLevel(LogLevel.Trace);
                blder.SetMinimumLevel(LogLevel.Trace);
                blder.AddSerilog(logger: lgr, dispose: true);
            });

            Console.WriteLine("Using UseInMemoryDatabase");
            servColl.AddDbContext<WorkerServiceExampleOneDbContext>(options => options.UseInMemoryDatabase(databaseName: "WorkerServiceExampleOneInMemoryDatabase"));
        }

        private static string GenerateFullFlatMessage(Exception ex, bool showStackTrace)
        {
            string returnValue;

            StringBuilder sb = new StringBuilder();
            Exception nestedEx = ex;

            while (nestedEx != null)
            {
                if (!string.IsNullOrEmpty(nestedEx.Message))
                {
                    sb.Append(nestedEx.Message + System.Environment.NewLine);
                }

                if (showStackTrace && !string.IsNullOrEmpty(nestedEx.StackTrace))
                {
                    sb.Append(nestedEx.StackTrace + System.Environment.NewLine);
                }

                if (ex is AggregateException)
                {
                    AggregateException ae = ex as AggregateException;

                    foreach (Exception aeflatEx in ae.Flatten().InnerExceptions)
                    {
                        if (!string.IsNullOrEmpty(aeflatEx.Message))
                        {
                            sb.Append(aeflatEx.Message + System.Environment.NewLine);
                        }

                        if (showStackTrace && !string.IsNullOrEmpty(aeflatEx.StackTrace))
                        {
                            sb.Append(aeflatEx.StackTrace + System.Environment.NewLine);
                        }
                    }
                }

                nestedEx = nestedEx.InnerException;
            }

            returnValue = sb.ToString();

            return returnValue;
        }
    }
}
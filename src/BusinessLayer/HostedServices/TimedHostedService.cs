using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Systemd;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.IO;
using MyCompany.MyExamples.WorkerServiceExampleOne.Domain.Dtos;

namespace MyCompany.MyExamples.WorkerServiceExampleOne.BusinessLayer.HostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> logger;
        private readonly TempFileHelpers tempFileHelpers;
        private readonly MyConnectionStrings myConnectionStrings;
        private int executionCount = 0;
        private Timer timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, TempFileHelpers tempFileHelpers, IOptionsMonitor<MyConnectionStrings> optionsAccessor)
        {
            this.logger = logger;
            this.tempFileHelpers = tempFileHelpers;

            /* you probably will not use connection strings in this class, BUT a quick way to show it is possible */
            this.myConnectionStrings = optionsAccessor.CurrentValue;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Timed Hosted Service running.");

            this.timer = new Timer(this.DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Timed Hosted Service is stopping.");

            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref this.executionCount);

            string logMessageOne = string.Format("Timed Hosted Service is working. Count: {0}", count);

            this.logger.LogInformation(logMessageOne);

            string logMessageTwo = string.Empty;

            if (SystemdHelpers.IsSystemdService())
            {
                logMessageTwo = string.Format("Running as Linux Daemon.  IsSystemdService is TRUE. Count: {0}", count);
                this.logger.LogInformation(logMessageTwo);
            }
            else
            {
                logMessageTwo = string.Format("NOT Running as Linux Daemon.  IsSystemdService is FALSE. Count: {0}", count);
                this.logger.LogInformation(logMessageTwo);
            }

            string logMessageThree = string.Empty;
            if (null != this.myConnectionStrings)
            {
                foreach (var (key, value) in this.myConnectionStrings)
                {
                    logMessageThree += $"ConnectionString :  [{key}] = {value}" + System.Environment.NewLine;
                }
            }

            this.logger.LogInformation(logMessageThree);

            string fileName = DateTime.Now.ToString("yyyy-MM-dd___HH");
            fileName = fileName.Replace(":", "--");

            string tempFileName = this.tempFileHelpers.WriteToTempFile(fileName, logMessageOne + System.Environment.NewLine + logMessageTwo + System.Environment.NewLine + logMessageThree, ".txt");

            this.logger.LogInformation(string.Format("tempFileName written. tempFileName: {0}", tempFileName));
        }
    }
}
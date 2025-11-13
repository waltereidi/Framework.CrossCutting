using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Quartz.Jobs
{
    public class MeuJob : IJob
    {
        private readonly ILogger<MeuJob> _logger;

        public MeuJob(ILogger<MeuJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Executando MeuJob às {time}", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}

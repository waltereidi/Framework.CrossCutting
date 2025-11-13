using Quartz;


public class QuartzScheduller
{
    public record JobConfiguration(string jobName , CronExpression ce );
    public static IServiceCollectionQuartzConfigurator GetConfiguration<T>(IServiceCollectionQuartzConfigurator q ,JobConfiguration jc ) where T : IJob
    {
        q.AddJob<T>(opts => opts.WithIdentity(jc.jobName));

        q.AddTrigger(opts => opts
            .ForJob(jc.jobName)
            .WithIdentity($"{jc.jobName}-trigger")
            .StartNow()
            .WithCronSchedule(jc.ce.ToString())
        );

        return q;
    }

}
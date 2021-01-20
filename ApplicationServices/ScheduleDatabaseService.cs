using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ApplicationServices
{
    public sealed class ScheduleDatabaseService
    {
        private readonly InitializeDatabaseService _initializeDatabaseService;

        public ScheduleDatabaseService(InitializeDatabaseService initializeDatabaseService)
        {
            _initializeDatabaseService = initializeDatabaseService;
        }

        public async Task Schedule()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<InitializeDatabaseService>()
                .WithIdentity("DatabaseInitialization", "CurrencyExchange")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("DailyInitialization", "CurrencyExchange")
                .StartNow()
                .WithSimpleSchedule(
                    sch => sch.WithIntervalInSeconds(3).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
namespace FinoBank.Cola.Scheduler
{
    internal class Service
    {
        public void OnStart()
        {
            //// construct a scheduler factory
            //ISchedulerFactory schedFact = new StdSchedulerFactory();
            //IScheduler sched = schedFact.GetScheduler().Result;
            //sched.Start();

            //IJobDetail job = JobBuilder.Create<CaseNotifications>()
            //    .WithIdentity("CaseNotifications", "CaseNotificationsGroup")
            //    .Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //  .WithIdentity("CaseNotificationsTrigger", "CaseNotificationsGroup")
            //  .StartNow()
            //  .WithSimpleSchedule(x => x
            //      .WithIntervalInSeconds(600)
            //      .RepeatForever())
            //  .Build();

            //sched.ScheduleJob(job, trigger);
        }

        public void OnStop()
        {
        }
    }
}
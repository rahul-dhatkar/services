using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Threading.Tasks;

namespace FinoBank.Cola.Scheduler
{
    /// <summary>
    /// Quartz Manager
    /// </summary>
    internal class QuartzManager
    {
        /// <summary>
        /// The scheduler
        /// </summary>
        private IScheduler _scheduler;

        /// <summary>
        /// Gets the scheduler.
        /// </summary>
        /// <value>
        /// The scheduler.
        /// </value>
        public static IScheduler Scheduler { get { return Instance._scheduler; } }

        // Singleton
        private static QuartzManager _instance = null;

        /// <summary>
        /// Singleton
        /// </summary>
        public static QuartzManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuartzManager();
                }
                return _instance;
            }
        }

        /// <summary></summary>
        private QuartzManager()
        {
            Init();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private async Task Init()
        {
            _scheduler = await new StdSchedulerFactory().GetScheduler();
        }

        /// <summary>
        /// Uses the job factory.
        /// </summary>
        /// <param name="jobFactory">The job factory.</param>
        /// <returns></returns>
        public IScheduler UseJobFactory(IJobFactory jobFactory)
        {
            Scheduler.JobFactory = jobFactory;
            return Scheduler;
        }

        /// <summary>
        /// Adds the job.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="group">The group.</param>
        /// <param name="interval">The interval.</param>
        /// <returns></returns>
        public async Task AddJob<T>(string name, string group, int interval)
            where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(name + "Trigger", group)
                .StartNow()
                .WithSimpleSchedule(t => t.WithIntervalInSeconds(interval).RepeatForever())
                .Build();

            await Scheduler.ScheduleJob(job, jobTrigger);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns></returns>
        public static Task Start()
        {
            return Scheduler.Start();
        }
    }
}
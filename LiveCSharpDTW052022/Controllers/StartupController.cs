using Mercadona.Utilities;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;

namespace Mercadona.Controllers
{
    public class StartupController : Controller
{
    public StartupController()
    {
            // Configurer le planificateur Quartz
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            scheduler.Start().GetAwaiter().GetResult();

            // Créer une tâche déclenchée tous les jours à 00h01
            IJobDetail job = JobBuilder.Create<MyJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 1))
                .Build();

            // Ajouter la tâche au planificateur
            scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();
        }
}
}
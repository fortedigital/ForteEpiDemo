using System.Threading;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;

namespace EpiDemo.Web.Jobs
{
    [ScheduledPlugIn(DisplayName = "My super job")]
    public class MyJob : ScheduledJobBase
    {
        public override string Execute()
        {
            for (int i = 0; i < 10; i++)
            {
                this.OnStatusChanged("Status changed " + i);
                Thread.Sleep(1000);
            }

            return "Wynik joba";
        }

    }
}
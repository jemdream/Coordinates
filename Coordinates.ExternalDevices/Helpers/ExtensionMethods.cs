using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Coordinates.ExternalDevices.Helpers
{
    public static class ExtensionMethods
    {
        public static double Round(this double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        public static Task TimeoutAfter(this Task task, TimeSpan timeout, IScheduler scheduler)
        {
            return task.ToObservable().Timeout(timeout, scheduler).ToTask();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLab1
{
    internal class BreakThread
    {
        private readonly List<MainThread> threads;

        public BreakThread(List<MainThread> threads)
        {
            this.threads = threads;
        }

        public void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!threads.All(t => t.CanStop))
            {
                long elapsed = stopwatch.ElapsedMilliseconds;

                foreach (var mainThread in threads)
                {
                    if (elapsed >= mainThread.TimeDuration)
                    {
                        mainThread.CanStop = true;
                    }
                }

                Thread.Sleep(10);
            }
            
            Console.WriteLine("Потік для зупинки завершив роботу.");
        }
    }
}

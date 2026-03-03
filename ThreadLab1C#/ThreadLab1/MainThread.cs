using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadLab1
{
    internal class MainThread
    {
        private int id;
        private readonly int step;
        private long sum;
        private long count;

        public int TimeDuration { get; }

        public bool CanStop { get; set; }

        public MainThread(int step, int duration)
        {
            this.step = step;
            TimeDuration = duration;
        }

        public void Run()
        {
            this.id = Thread.CurrentThread.ManagedThreadId;

            long currentNumber = 0;

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (!CanStop)
            {
                sum += currentNumber;
                count++;
                currentNumber += step;
            }

            var elapsedTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Потік {id} завершив роботу. Сума: {sum}, Кількість: {count}, Час: {elapsedTime} мс");
        }
    }
}

using System.Diagnostics;
using System.Threading;

namespace ThreadLab1
{
    internal class ThreadManager
    {
        private readonly short threadCount;

        public ThreadManager(short threadCount)
        {
            this.threadCount = threadCount;
        }

        public void Start()
        {
            List<int> durations = GetThreadDurations(threadCount);

            List<int> steps = GetSteps(threadCount);

            List<MainThread> mainThreads = new List<MainThread>();

            for (int i = 0; i < threadCount; i++)
            {
                var mainThread = new MainThread(steps[i], durations[i]);
                mainThreads.Add(mainThread);

                Thread thread = new Thread(mainThread.Run);
                thread.Start();
            }

            BreakThread breakThread = new BreakThread(mainThreads);
            Thread stopperThread = new Thread(breakThread.Run);

            stopperThread.Start();
        }

        private static List<int> GetThreadDurations(short threadCount)
        {
            while (true)
            {
                Console.WriteLine($"Введіть час роботи в мілісекундах для {threadCount} потоків: ");
                string input = Console.ReadLine() ?? string.Empty;

                string[] strDurations = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (strDurations.Length != threadCount)
                {
                    Console.WriteLine($"Помилка: потрібно ввести {threadCount} чисел.");
                    continue;
                }


                List<int> durations = new List<int>();
                bool allValid = true;

                foreach (string strDuration in strDurations)
                {
                    if (int.TryParse(strDuration, out int duration) && duration > 0)
                    {
                        durations.Add(duration);
                    }
                    else
                    {
                        Console.WriteLine($"Помилка: '{strDuration}' не є валідним цілим числом.");
                        allValid = false;
                        break;
                    }
                }

                if (allValid)
                {
                    return durations;
                }
            }
        }

        private static List<int> GetSteps(int threadCount)
        {
            while (true)
            {
                Console.WriteLine("Введіть кроки для обчислення: ");
                string input = Console.ReadLine() ?? string.Empty;

                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != threadCount)
                {
                    Console.WriteLine($"Помилка: потрібно ввести {threadCount} чисел.");
                    continue;
                }

                List<int> steps = new List<int>();
                bool allValid = true;

                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int step))
                    {
                        steps.Add(step);
                    }
                    else
                    {
                        Console.WriteLine($"Помилка: '{part}' не є валідним цілим числом.");
                        allValid = false;
                        break;
                    }
                }

                if (allValid)
                {
                    return steps;
                }
            }
        }

    }
}

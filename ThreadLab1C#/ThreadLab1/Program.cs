using ThreadLab1;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Введіть кількість потоків для обчислення:");
        short threadCount;
        while (!short.TryParse(Console.ReadLine(), out threadCount) || threadCount <= 0)
        {
            Console.WriteLine("Будь ласка, введіть дійсне додатне число для кількості потоків:");
        }

        var threadManager = new ThreadManager(threadCount);

        threadManager.Start();
    }
}
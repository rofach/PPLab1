import java.util.Scanner;

public class App {
    public static void main(String[] args) throws Exception {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Enter the number of threads to compute:");
        
        short threadCount = 0;
        while (true) {
            String input = scanner.nextLine().trim();
            try {
                threadCount = Short.parseShort(input);
                if (threadCount > 0) {
                    break;
                }
            } catch (NumberFormatException e) {
            }
            System.out.println("Enter a valid positive number for the thread count:");
        }

        ThreadManager threadManager = new ThreadManager(threadCount);
        threadManager.start();

       
    }
}

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class ThreadManager implements Runnable {
    private final short threadCount;
    private Scanner scanner;

    public ThreadManager(short threadCount) {
        this.threadCount = threadCount;
        this.scanner = new Scanner(System.in);
    }

    @Override
    public void run() {
        List<Integer> durations = getDurations(threadCount);

        List<Integer> steps = getSteps(threadCount);

        List<MainThread> mainThreads = new ArrayList<>();

        for (int i = 0; i < threadCount; i++) {
            MainThread mainThread = new MainThread(steps.get(i), durations.get(i));
            mainThreads.add(mainThread);

            Thread thread = new Thread(mainThread);
            thread.start();
        }

        BreakThread breakThread = new BreakThread(mainThreads);
        Thread stopperThread = new Thread(breakThread);

        stopperThread.start();

    }

    private List<Integer> getDurations(short threadCount) {
        while (true) {
            System.out.printf("Enter runtime in milliseconds for %d thread(s): %n", threadCount);
            String input = scanner.nextLine().trim();

            if (input.isEmpty()) continue;

            String[] stringDurations = input.split("\\s+");

            if (stringDurations.length != threadCount) {
                System.out.printf("Error: please enter %d numbers.%n", threadCount);
                continue;
            }

            List<Integer> durations = new ArrayList<>();
            boolean allValid = true;

            for (String stringDuration : stringDurations) {
                try {
                    int duration = Integer.parseInt(stringDuration);
                    if (duration > 0) {
                        durations.add(duration);
                    } else {
                        System.out.printf("Error: '%s' is not a valid integer.%n", stringDuration);
                        allValid = false;
                        break;
                    }
                } catch (NumberFormatException e) {
                    System.out.printf("Error: '%s' is not a valid integer.%n", stringDuration);
                    allValid = false;
                    break;
                }
            }

            if (allValid) {
                return durations;
            }
        }
    }

    private List<Integer> getSteps(short threadCount) {
        while (true) {
            System.out.printf("Enter the step for %d thread(s): %n", threadCount);
            Scanner scanner = new Scanner(System.in);
            String input = scanner.nextLine().trim();

            if (input.isEmpty()) continue;

            String[] parts = input.split("\\s+");

            if (parts.length != threadCount) {
                System.out.printf("Error: please enter %d numbers.%n", threadCount);
                continue;
            }

            List<Integer> steps = new ArrayList<>();
            boolean allValid = true;

            for (String part : parts) {
                try {
                    int step = Integer.parseInt(part);
                    steps.add(step);
                } catch (NumberFormatException e) {
                    System.out.printf("Error: '%s' is not a valid integer.%n", part);
                    allValid = false;
                    break;
                }
            }

            if (allValid) {
                return steps;
            }
        }
    }
}
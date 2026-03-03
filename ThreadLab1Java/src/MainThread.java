public class MainThread {
    private long step;
    private int timeDuration;
    private long sum = 0;
    private volatile boolean canStop = false;

    public MainThread(long step, int timeDuration) {
        this.step = step;
        this.timeDuration = timeDuration;
    }
    
    public int getTimeDuration() {
        return timeDuration;
    }

    public boolean isCanStop() {
        return canStop;
    }

    public void setCanStop(boolean canStop) {
        this.canStop = canStop;
    }

    public void run() {
        long counter = 0;
        long currentTime = System.currentTimeMillis();

        while(!isCanStop()){
            counter++;
            sum += step;
        }

        long endTime = System.currentTimeMillis();

        System.out.printf("Thread %s finished. Sum: %d, Iterations: %d, Execution time: %d ms%n",
                Thread.currentThread().getName(), sum, counter, endTime - currentTime);
    }
}
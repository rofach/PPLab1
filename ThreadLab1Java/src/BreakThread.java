import java.util.List;

public class BreakThread {
    private final List<MainThread> mainThreads;

    public BreakThread(List<MainThread> mainThreads) {
        this.mainThreads = mainThreads;
    }

    public void run() {
        long startTime = System.currentTimeMillis();

        while(!mainThreads.stream().allMatch(mainThread -> mainThread.isCanStop())) {
            long currentTime = System.currentTimeMillis();

            for(MainThread mainThread : mainThreads) {
                if (!mainThread.isCanStop() && currentTime - startTime >= mainThread.getTimeDuration()) {
                    mainThread.setCanStop(true);
                }
            }

            try {
                Thread.sleep(1);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                break;
            }
        }
    }     
}
using System;

public class Utils
{
    public static double CurrentTime() //time since year 2000
    {
        DateTime epochStart = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        double currentEpochTime = (DateTime.UtcNow - epochStart).TotalMilliseconds;

        return currentEpochTime;
    }
}

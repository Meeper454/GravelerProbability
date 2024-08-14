
long rolls = 0;
int maxOnes = 0;

Random random = new Random();
DateTime startTime = DateTime.Now;

List<Task> threads = new List<Task>();
Mutex mut = new Mutex();
long rollcount = 100000000;

var threadFunc = () =>
{
    int[] numbers = [0, 0, 0, 0];
    while (numbers[0] < 177 && rolls < rollcount)
    {
        numbers = [0, 0, 0, 0];
        for (int i = 0; i < 231; i++)
        {
            int roll = random.Next(1, 5);
            numbers[roll - 1] = numbers[roll - 1] + 1;

            if (roll > 1 && numbers[1] + numbers[2] + numbers[3] > 54)
            { break; }

        }
        rolls = rolls + 1;
        if (numbers[0] > maxOnes)
        {
            maxOnes = numbers[0];
        }

        Thread.Yield();
    }
};


var reportFunc = () =>
{
    bool threadsRunning = true;

    while (threadsRunning)
    {
        long currentRolls = rolls;
        if (true)
        {
            DateTime endTime = DateTime.Now;

            double secondsSinceStart = (endTime - startTime).TotalSeconds;

            double rps = currentRolls / secondsSinceStart;
            double scsLeft = rollcount / rps;

            if (mut.WaitOne())
            {
                Console.CursorTop = 0;
                Console.WriteLine("Took " + (endTime - startTime).TotalSeconds + " seconds");
                Console.WriteLine("Rolls: " + currentRolls.ToString("0,0"));

                Console.Write("Time to " + rollcount.ToString("0,0") + " Rolls: " + scsLeft.ToString("0,0") + " seconds         \n");                
                mut.ReleaseMutex();
            }

        }
        threads.ForEach((task) =>
        {
            threadsRunning |= (task.Status == TaskStatus.Running);
        });
        Thread.Yield();

    }
};

threads.Add(Task.Run(reportFunc));
while (threads.Count < 8)
{
    threads.Add(Task.Run(threadFunc));
}

Task.WaitAll(threads.ToArray());
DateTime endTime = DateTime.Now;

Console.WriteLine("Took " + (endTime - startTime).TotalSeconds + " seconds");
Console.WriteLine("Highest Ones Roll:" + maxOnes.ToString());
Console.WriteLine("Number of Roll Sessions: " + rolls.ToString("0,0"));

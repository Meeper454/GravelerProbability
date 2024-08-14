
int[] numbers = [0, 0, 0, 0];
long rolls = 0;
int maxOnes = 0;

Random random = new Random();
DateTime startTime = DateTime.Now;

List<Task> threads = new List<Task>();

long rollcount = 100000000;

var threadFunc = () =>
{

    while (numbers[0] < 177 && rolls < rollcount)
    {
        numbers = [0, 0, 0, 0];
        for (int i = 0; i < 231; i++)
        {
            int roll = random.Next(1, 5);
            numbers[roll - 1] = numbers[roll - 1] + 1;
        }
        rolls = rolls + 1;
        if (numbers[0] > maxOnes)
        {
            maxOnes = numbers[0];
        }

        long currentRolls = rolls;
        if (currentRolls % 100000 == 0)
        {
            DateTime endTime = DateTime.Now;

            double secondsSinceStart = (endTime - startTime).TotalSeconds;

            double rps = secondsSinceStart / currentRolls;
            double scsLeft = rps * rollcount;

            Console.WriteLine("Took " + (endTime - startTime).TotalSeconds + " seconds");
            Console.WriteLine("Rolls: " + currentRolls.ToString("0,0"));

            Console.WriteLine("Time to 1 billion Rolls: " + scsLeft.ToString("0,0") + " seconds");

        }
    }
};

while (threads.Count < 4)
{
    threads.Add(Task.Run(threadFunc));
}

Task.WaitAll(threads.ToArray());

DateTime endTime = DateTime.Now;

Console.WriteLine("Took " + (endTime - startTime).TotalSeconds +" seconds");
Console.WriteLine("Highest Ones Roll:" + maxOnes.ToString());
Console.WriteLine("Number of Roll Sessions: " + rolls.ToString());

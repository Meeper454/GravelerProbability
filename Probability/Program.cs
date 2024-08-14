
int[] numbers = [0, 0, 0, 0];
long rolls = 0;
int maxOnes = 0;

Random random = new Random();
DateTime startTime = DateTime.Now;


while (numbers[0] < 177 && rolls < 1000000000)
{
    numbers = [0, 0, 0, 0];
    for (int i = 0; i < 231; i++)
    {
        int roll = random.Next(1, 5);
        numbers[roll - 1] = numbers[roll - 1] + 1;
        rolls = rolls + 1;
        if (numbers[0] > maxOnes)
        { 
            maxOnes = numbers[0]; 
        }
    }
}
DateTime endTime = DateTime.Now;

Console.WriteLine("Took " + (endTime - startTime).TotalSeconds +" seconds");
Console.WriteLine("Highest Ones Roll:" + maxOnes.ToString());
Console.WriteLine("Number of Roll Sessions: " + rolls.ToString());

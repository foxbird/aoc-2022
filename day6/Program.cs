using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true)
    .AddEnvironmentVariables()
    .Build();

var logger = LoggerFactory.Create(builder => 
    builder.AddConsole()
        .AddConfiguration(config.GetSection("Logging")))
        .CreateLogger("Program");

if (args.Length != 3) {
    Console.WriteLine("Usage: day6 <inputstring> <packet> <message>");
    return;
}

string buffer = args[0];
if (!int.TryParse(args[1], out int sopLen))
{
    Console.WriteLine($"'{args[1]}' is not an integer");
    return;
}
if (sopLen <= 0 || sopLen >= buffer.Length)
{
    Console.WriteLine($"Start of packet length must be between 1 and {buffer.Length}");
    return;
}
if (!int.TryParse(args[2], out int somLen))
{
    Console.WriteLine($"'{args[2]}' is not an integer");
    return;
}
if (somLen <= 0 || somLen >= buffer.Length)
{
    Console.WriteLine($"Start of message length must be between 1 and {buffer.Length}");
    return;
}

int FindUnique(string input, int length)
{
    for (int i = 0; i < input.Length; i++) {
        List<char> seen = new();
        for (int j = i; j < input.Length && j - i < length; j++)
        {
            // Abort if already seen
            var pos = seen.IndexOf(input[j]);
            if (pos != -1) {
                logger.LogDebug($"{i} - {j} - {j - i} == {String.Join("", seen)}{input[j]} == FAIL");
                i = i + pos;
                break;
            }
            logger.LogDebug($"{i} - {j} - {j - i} == {String.Join("", seen)}{input[j]} == PASS");

            // Add this character
            seen.Add(input[j]);
        }

        if (seen.Count == length) {
            logger.LogDebug($"Marker found after {i + length}");
            return i + length;
        }
    }

    return -1;

}

int endOfSopMarker = FindUnique(buffer, sopLen);
if (endOfSopMarker == -1)
{
    Console.WriteLine("Start of packet not found");
    return;
}
Console.WriteLine($"Start of packet after {endOfSopMarker}");

int endOfSomMarker = FindUnique(buffer, somLen);
if (endOfSomMarker == -1)
{
    Console.WriteLine("Start of message not found");
    return;
}
Console.WriteLine($"Start of message after {endOfSomMarker}");

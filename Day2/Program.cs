using AdventofCode2022.Day2;

Console.WriteLine("Part 1");
{
    var totalScore = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day2\input.txt");
    using var streamReader = new StreamReader(fileStream);

    do
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var results = line.Split(' ');
        var myHand = ParseHand(results[1]);
        var theirHand = ParseHand(results[0]);

        var matchResult = CalculateMatchResult(myHand, theirHand);
        var myScore = CalculateMatchResultScore(matchResult) + CalculateHandScore(myHand);

        totalScore += myScore;
    } while (true);

    Console.WriteLine($"TotalScore: {totalScore}");

    static MatchResult CalculateMatchResult(Hand first, Hand second) => (first, second) switch
    {
        (Hand.Rock, Hand.Scissor)
        or (Hand.Paper, Hand.Rock)
        or (Hand.Scissor, Hand.Paper) => MatchResult.Win,
        _ => first == second ? MatchResult.Draw : MatchResult.Loss,
    };

    static Hand ParseHand(string action) => action switch
    {
        "A" or "X" => Hand.Rock,
        "B" or "Y" => Hand.Paper,
        "C" or "Z" => Hand.Scissor,
        _ => throw new InvalidOperationException(),
    };
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    var totalScore = 0;

    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day2\input.txt");
    using var streamReader = new StreamReader(fileStream);

    do
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null) break;

        var results = line.Split(' ');
        var matchResult = ParseMatchResult(results[1]);
        var theirHand = ParseHand(results[0]);
        var myHand = CalculateHand(theirHand, matchResult);
        var myScore = CalculateMatchResultScore(matchResult) + CalculateHandScore(myHand);

        totalScore += myScore;
    } while (true);

    Console.WriteLine($"TotalScore: {totalScore}");

    static Hand CalculateHand(Hand theirHand, MatchResult matchResult) => (theirHand, matchResult) switch
    {
        (_, MatchResult.Draw) => theirHand,
        (Hand.Rock, MatchResult.Win) => Hand.Paper,
        (Hand.Rock, MatchResult.Loss) => Hand.Scissor,
        (Hand.Paper, MatchResult.Win) => Hand.Scissor,
        (Hand.Paper, MatchResult.Loss) => Hand.Rock,
        (Hand.Scissor, MatchResult.Win) => Hand.Rock,
        (Hand.Scissor, MatchResult.Loss) => Hand.Paper,
        _ => throw new InvalidOperationException(),
    };

    static Hand ParseHand(string action) => action switch
    {
        "A" => Hand.Rock,
        "B" => Hand.Paper,
        "C" => Hand.Scissor,
        _ => throw new InvalidOperationException(),
    };

    static MatchResult ParseMatchResult(string matchResult) => matchResult switch
    {
        "X" => MatchResult.Loss,
        "Y" => MatchResult.Draw,
        "Z" => MatchResult.Win,
        _ => throw new InvalidOperationException(),
    };
}

static int CalculateHandScore(Hand hand) => hand switch
{
    Hand.Rock => 1,
    Hand.Paper => 2,
    Hand.Scissor => 3,
    _ => throw new InvalidOperationException(),
};

static int CalculateMatchResultScore(MatchResult matchResult) => matchResult switch
{
    MatchResult.Win => 6,
    MatchResult.Draw => 3,
    MatchResult.Loss => 0,
    _ => throw new InvalidOperationException(),
};

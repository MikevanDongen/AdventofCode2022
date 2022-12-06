Console.WriteLine("Part 1");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day6\input.txt");
    using var streamReader = new StreamReader(fileStream);

    const int size = 4;
    var buffer = new char[size];
    var tempBuffer = new char[size];
    var bufferIndex = 0;
    var count = 1;
    var totalCount = 0;
    do
    {
        var numberOfChars = await streamReader.ReadAsync(buffer, bufferIndex, count);
        if (numberOfChars == 0) break;
        totalCount += numberOfChars;

        var indexOf = -1;
        for (int i = bufferIndex; i < bufferIndex + numberOfChars && i > 0; i++)
        {
            var newIndexOf = Array.LastIndexOf(buffer, buffer[i], i - 1, i);
            indexOf = newIndexOf > indexOf ? newIndexOf : indexOf;
        }

        if (indexOf == -1)
        {
            if ((bufferIndex + numberOfChars) == size)
            {
                break;
            }

            bufferIndex += numberOfChars;
        }
        else
        {
            buffer[(indexOf + 1)..].CopyTo(tempBuffer, 0);
            bufferIndex += numberOfChars - indexOf - 1;
            count = size - bufferIndex;
            (buffer, tempBuffer) = (tempBuffer, buffer);
        }
    } while (true);

    Console.WriteLine(totalCount);
    Console.WriteLine(buffer[..size]);
}

Console.WriteLine();
Console.WriteLine("Part 2");
{
    using var fileStream = File.OpenRead(@"C:\Repos\AdventofCode2022\Day6\input.txt");
    using var streamReader = new StreamReader(fileStream);

    const int size = 14;
    var buffer = new char[size];
    var tempBuffer = new char[size];
    var bufferIndex = 0;
    var count = 1;
    var totalCount = 0;
    do
    {
        var numberOfChars = await streamReader.ReadAsync(buffer, bufferIndex, count);
        if (numberOfChars == 0) break;
        totalCount += numberOfChars;

        var indexOf = -1;
        for (int i = bufferIndex; i < bufferIndex + numberOfChars && i > 0; i++)
        {
            var newIndexOf = Array.LastIndexOf(buffer, buffer[i], i - 1, i);
            indexOf = newIndexOf > indexOf ? newIndexOf : indexOf;
        }

        if (indexOf == -1)
        {
            if ((bufferIndex + numberOfChars) == size)
            {
                break;
            }

            bufferIndex += numberOfChars;
        }
        else
        {
            buffer[(indexOf + 1)..].CopyTo(tempBuffer, 0);
            bufferIndex += numberOfChars - indexOf - 1;
            count = size - bufferIndex;
            (buffer, tempBuffer) = (tempBuffer, buffer);
        }
    } while (true);

    Console.WriteLine(totalCount);
    Console.WriteLine(buffer[..size]);
}

Console.WriteLine();
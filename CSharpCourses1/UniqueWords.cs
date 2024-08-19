using System.Net;
using System.Text.RegularExpressions;

namespace CSharpCourses1;

public class UniqueWords
{
    private readonly string _uniqueDictionaryFileName = "UniqueDictionary.txt";

    public async Task FindWordsAsync(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("No file found!");
            return;
        }

        var textFromFile = await File.ReadAllTextAsync(path);
        var uniqueWords = Regex.Matches(textFromFile, @"\b\w+\b").Select(x => x.Value.ToLower()).Distinct().ToList();

        if (uniqueWords.Any())
        {
            Console.WriteLine($"Unique words in file:\n{string.Join(", ", uniqueWords)}");
            Console.WriteLine($"Unique word count in the file: {uniqueWords.Count}");
            await WriteUniqueWordsAsync(uniqueWords);
        }
        else
        {
            Console.WriteLine("No unique words found!");
        }
    }

    private async Task WriteUniqueWordsAsync(List<string> newWords)
    {
        var existingWords = new List<string>(); 

        if (File.Exists(_uniqueDictionaryFileName))
        {
            var dictionaryContent = await File.ReadAllTextAsync(_uniqueDictionaryFileName);
            existingWords = Regex.Matches(dictionaryContent, @"\b\w+\b").Select(x => x.Value).ToList();
        }
        else
        {
            await using (File.Create(_uniqueDictionaryFileName))
            {
                // Dispose after creation
            }
        }

        var notInFileWords = newWords.Except(existingWords).ToList();
        Console.WriteLine($"New words added to the dictionary: {notInFileWords.Count}");
        await File.AppendAllLinesAsync(_uniqueDictionaryFileName, notInFileWords);
    }
}
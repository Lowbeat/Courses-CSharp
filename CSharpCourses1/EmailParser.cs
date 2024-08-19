using System.Text.RegularExpressions;

namespace CSharpCourses1;

public class EmailParser
{
    public async Task ParseEmailsFromUrl(string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
        {
            var pageContent = await FetchPageContentAsync(uri);

            if (!string.IsNullOrEmpty(pageContent))
            {
                ParseAndDisplayEmails(pageContent);
            }
            else
            {
                Console.WriteLine("Failed to retrieve content from the URL.");
            }
        }
        else
        {
            Console.WriteLine("Invalid URL.");
        }
    }

    private async Task<string> FetchPageContentAsync(Uri uri)
    {
        using var client = new HttpClient();
        try
        {
            return await client.GetStringAsync(uri);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return string.Empty;
        }
    }

    private void ParseAndDisplayEmails(string content)
    {
        var emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
        var matches = Regex.Matches(content, emailPattern);

        if (matches.Count > 0)
        {
            Console.WriteLine("Found the following email addresses:");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        else
        {
            Console.WriteLine("No email addresses found.");
        }
    }
}
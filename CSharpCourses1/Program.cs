namespace CSharpCourses1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Choose a program from the list by number:");
            Console.WriteLine("1. Email parser");
            Console.WriteLine("2. Unique words");
            Console.WriteLine("3. Find file by string");
            var readLine = Console.ReadLine();

            switch (readLine)
            {
                case "1":
                    Console.WriteLine("Enter the URL:");
                    var url = Console.ReadLine();

                    if (url != null)
                        await new EmailParser().ParseEmailsFromUrl(url);
                    break;
                case "2":
                    Console.WriteLine("Enter full file path:");
                    var path = Console.ReadLine();

                    if (path != null)
                        await new UniqueWords().FindWordsAsync(path);
                    break;
                case "3":
                    await new StringFileFinder().SearchFilesAsync();
                    break;
                default:
                    Console.WriteLine("Invalid number!");
                    break;
            }
        }
    }
}
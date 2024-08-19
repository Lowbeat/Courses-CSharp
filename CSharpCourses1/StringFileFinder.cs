namespace CSharpCourses1;

public class StringFileFinder
{
    public async Task SearchFilesAsync()
    {
        Console.Write("Enter the search string: ");
        var searchString = Console.ReadLine();

        var drives = DriveInfo.GetDrives().Where(d => d.IsReady).Select(d => d.RootDirectory.FullName);

        Console.WriteLine($"Searching for files containing '{searchString}' across all drives");

        var tasks = new List<Task>();
        foreach (var drive in drives)
        {
            tasks.Add(Task.Run(() => SearchInDirectory(drive, searchString)));
        }
        await Task.WhenAll(tasks);

        Console.WriteLine("Search complete.");
    }
    private async Task SearchInDirectory(string directory, string searchString)
    {
        try
        {
            // Get all files in the current directory that contain the search string
            var files = Directory.GetFiles(directory).Where(f => Path.GetFileName(f).Contains(searchString, StringComparison.OrdinalIgnoreCase));

            foreach (var file in files)
            {
                Console.WriteLine($"Found: {file}");
                return;
            }

            var subdirectories = Directory.GetDirectories(directory);
            var tasks = new List<Task>();
            foreach (var subdirectory in subdirectories)
            {
                tasks.Add(Task.Run(() => SearchInDirectory(subdirectory, searchString)));
            }
            await Task.WhenAll(tasks);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access denied to directory: {directory}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while searching in directory: {directory}. {e.Message}");
        }
    }
}
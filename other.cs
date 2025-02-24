/* using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "example.txt";
        
        while (true)
        {
            Console.WriteLine("Enter text to store in the file (or type 'exit' to stop):");
            string? userInput = Console.ReadLine();
            
            if (userInput != null && userInput.ToLower() == "exit")
                break;

            // Append user input to file
            AppendToFile(filePath, userInput + Environment.NewLine);
        }

        // Read from file
        string content = ReadFromFile(filePath);
        Console.WriteLine("\nFile content:");
        Console.WriteLine(content);
    }

    static void AppendToFile(string path, string content)
    {
        try
        {
            File.AppendAllText(path, content);
            Console.WriteLine("Data appended to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    static string ReadFromFile(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("File not found.");
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return string.Empty;
        }
    }
}
 */
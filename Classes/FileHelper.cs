/* 
Oppgave 1
Dere kan enten lage en ny text fil, skrive til den via File klassen, eller bruke File klassen til å lage nye filer. 
La programmet også lese fra filen, (her kan det være lurt å lage hjelpemetoder for å både lese fra filen og skrive til den.)
 */
namespace Filbehandling.Classes;
using static Filbehandling.Classes.Pretty;

public static class FileHelper
{
    public static void WriteToFile(string filePath)
    {
        Magenta("\nEnter text and press Enter (as many times as you want) to save to file.");
        Magenta("(type 'exit' to stop):");
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        /* 
        StreamWriter is a built-in C# class used to create, write, or append text to a file.
        The first parameter (filePath) specifies the path of the file to write to.
        The second (append: true) is a named argument:
            If true, new data is appended to the file instead of overwriting it.
            If false, the file is overwritten with new data.
         */
        {
            string userInput;
            while ((userInput = Console.ReadLine()!) != null && userInput != "exit")
            // Console.ReadLine()! = "I am sure this will never be null, so don’t show warnings."
            {
                writer.WriteLine(userInput); //The writer object is an instance of the StreamWriter class
            }
        }
    }

    public static void ReadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            Yellow("\nContents of the file:");
            Console.WriteLine(File.ReadAllText(filePath));
        }
        else
        {
            Red("File does not exist.");
        }
    }
}
﻿// Playing around with menu selection, so each task is easier to access

using static Filbehandling.Classes.Pretty; // to be able to use colors for teminal printouts

using Filbehandling.Classes;

class Program
{
    static async Task Main()
    {
        string menuSelection = "";

        do
        {
            Magenta("\nSelect a task to perform:");
            Console.WriteLine("1. File Handling");
            Console.WriteLine("2. JSON Processing");
            Console.WriteLine("3. REST API Data Retrieval");
            // make differet sub options?
            Console.WriteLine("4. Exit");

            string? input = Console.ReadLine();
            if (input != null)
            {
                menuSelection = input.ToLower();
            }

            switch (menuSelection)
            {
                case "1":
                    string filePath = "UserInput.txt";
                    FileHelper.WriteToFile(filePath);
                    FileHelper.ReadFromFile(filePath);
                    break;

                case "2":
                    string jsonPath = "data.json";
                    JsonHelper.WriteJsonToFile(jsonPath);
                    JsonHelper.ReadJsonFromFile(jsonPath);
                    break;

                case "3":
                    
                    await ApiHelper.Run();
                    break;

                case "4":
                    Red("Exiting program.");
                    break;

                default:
                    Red("Invalid selection. Please try again.");
                    break;
            }
        } while (menuSelection != "4");
    }
}
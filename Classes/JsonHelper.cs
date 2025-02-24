/* 
Oppgave 2
I denne oppgaven kan dere enten lage en ny klasse for å behandle JSON filer eller implementere disse metodene i klassen dere allerede jobber i. Dere kan lese fra JSON enten via File klassen eller via JSONSerializer.
Forsøk å lage noen metoder som leser fra JSON filene og skriver dem ut til terminalen via Console klassen, og forsøk også gjerne å lage en metode som skriver ny JSON data.
 */
using static Filbehandling.Classes.Pretty;
namespace Filbehandling.Classes;
using System.Text.Json;

public static class JsonHelper
{
    public static void WriteJsonToFile(string jsonPath)
    {
        var data = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                { "Name", "Emily" },
                { "Occupation", "Student" },
                { "City", "Bergen" }
            },
            new Dictionary<string, string>
            {
                { "Name", "Daniel" },
                { "Occupation", "Teacher" },
                { "City", "Oslo" }
            }
        };

        string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        /* 
        In our case, "data" is a Dictionary<string, string>
        "JsonSerializerOptions" is a class that allows customization of JSON formatting.
        "WriteIndented = true" makes the JSON output pretty-printed (adds spaces and newlines for readability).
         */
        File.WriteAllText(jsonPath, jsonString);
        /* It writes text to a file.
            If the file exists, it overwrites it.
            If the file does not exist, it creates a new file.     
        */
    }

    public static void ReadJsonFromFile(string jsonPath)
    {
        if (File.Exists(jsonPath))
        {
            string jsonString = File.ReadAllText(jsonPath);
            var data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString);
            Yellow("JSON Data:");
            // // Check if data is not null and contains any items before iterating
            if (data != null)
            {
                foreach (var entry in data)
                {
                    // Ensure each entry (dictionary) is not null
                    if (entry != null)
                    {
                        foreach (var pair in entry)
                        {
                            Console.WriteLine($"{pair.Key}: {pair.Value}");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Red("Encountered a null dictionary entry.");
                    }
                }
            }
            else
            {
                Red("No data found in the JSON file.");
            }
        }
        else
        {
            Red("JSON file does not exist.");
        }
    }
}
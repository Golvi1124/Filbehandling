/* 
Oppgave 3
Bruk enten C# HTTP klassen eller CURL (CLI) via Git BASH terminal til å hente ut data fra en RestAPI på nett
Skriv ut dataen som hentes via C# HTTP eller CURL til terminalen eller til en tekstfil via File klassen
*/

using System.Text.Json;
using static Filbehandling.Classes.Pretty;
public static class ApiHelper
{
    public static HttpClient client = new HttpClient();
    public static CreateFile createFile = new CreateFile();

    public static async Task Run()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("https://wizard-world-api.herokuapp.com/Houses");

            if (!response.IsSuccessStatusCode) // instead of manually checking status codes. Convert.ToInt32(response.StatusCode) != 200
            {
                Red($"A HTTP error occured! {response.Headers}");
                return;
            }

            string content = await response.Content.ReadAsStringAsync();
            /* reads the HTTP response body as a string.
                await ensures we wait for the response to fully download before continuing.
                The response contains JSON data (a string representation of users). */
            List<House>? houses = JsonSerializer.Deserialize<List<House>>(content);
            /* Reads the JSON response (content).
                Converts it into a list of GetResponse objects. 
                 After deserialization, we get a List<GetResponse> containing multiple user objects.*/

            if (houses != null)
            {
                Yellow("Hogwarts Houses:");
                foreach (var house in houses)
                {
                    Console.WriteLine($"Name: {house.name}");
                    Console.WriteLine($"House colours: {house.houseColours}");
                    Console.WriteLine($"Founder: {house.founder}");
                    Console.WriteLine("------");
                }

                string output = JsonSerializer.Serialize(houses, new JsonSerializerOptions { WriteIndented = true });
                createFile.WriteToFile("HPHouses.json", output);
            }
        }
        catch (HttpRequestException error)
        {
            Red($"A HTTP error occured: {error.Message}");
        }
        //to check other errors
        catch (JsonException jsonError)
        {
            Red($"JSON processing error: {jsonError.Message}");
        }
        catch (Exception ex)
        {
            Red($"Unexpected error: {ex.Message}");
        }
    }

    public static async Task SelectHouse()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("https://wizard-world-api.herokuapp.com/Houses");

            if (!response.IsSuccessStatusCode)
            {
                Red($"A HTTP error occurred! {response.Headers}");
                return;
            }

            string content = await response.Content.ReadAsStringAsync();
            List<House>? houses = JsonSerializer.Deserialize<List<House>>(content);

            if (houses == null || houses.Count == 0)
            {
                Red("No house data available.");
                return;
            }

            Magenta("Select a Hogwarts House to learn more:");
            for (int i = 0; i < houses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {houses[i].name}");
            }

            Console.Write("Enter your choice (1-4): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= houses.Count)
            {
                House selectedHouse = houses[choice - 1];
                Yellow($"\nYour selected house: {selectedHouse.name}");
                Console.WriteLine($"\tHouse Colours: {selectedHouse.houseColours}");
                Console.WriteLine($"\tFounder: {selectedHouse.founder}");
                Console.WriteLine($"\tAnimal: {selectedHouse.animal}");
                Console.WriteLine($"\tElement: {selectedHouse.element}");
                Console.WriteLine($"\tGhost: {selectedHouse.ghost}");
                Console.WriteLine($"\tCommon Room: {selectedHouse.commonRoom}");
                Console.WriteLine($"\tHeads:");
                foreach (var head in selectedHouse.heads)
                {
                    Console.WriteLine($"\t\t-{head.firstName} {head.lastName}");
                }
                Console.WriteLine($"\tTraits:");
                foreach (var trait in selectedHouse.traits)
                {
                    Console.WriteLine($"\t\t-{trait.name}");
                }
            }
            else
            {
                Red("Invalid choice. Please select a number between 1 and 4.");
            }
        }
        catch (HttpRequestException error)
        {
            Console.WriteLine($"A HTTP error occurred: {error.Message}");
        }
        catch (JsonException jsonError)
        {
            Console.WriteLine($"JSON processing error: {jsonError.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    public class House
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? houseColours { get; set; }
        public string? founder { get; set; }
        public string? animal { get; set; }
        public string? element { get; set; }
        public string? ghost { get; set; }
        public string? commonRoom { get; set; }
        public List<Head> heads { get; set; } = new List<Head>();
        public List<Trait> traits { get; set; } = new List<Trait>();
    }

    public class Head
    {
        public string? id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
    }

    public class Trait
    {
        public string? id { get; set; }
        public string? name { get; set; }
    }



    public class CreateFile
    {
        public void WriteToFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                Red($"Error writing to file: {ex.Message}");
            }
        }
    }
}

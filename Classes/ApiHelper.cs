/* 
Oppgave 3
Bruk enten C# HTTP klassen eller CURL (CLI) via Git BASH terminal til å hente ut data fra en RestAPI på nett
Skriv ut dataen som hentes via C# HTTP eller CURL til terminalen eller til en tekstfil via File klassen
https://free-apis.github.io/#/categories
using: https://wizard-world-api.herokuapp.com/Houses

 */

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
                Console.WriteLine($"A HTTP error occured! {response.Headers}");
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
    foreach (var house in houses)
    {
        Console.WriteLine($"Name: {house.name}");
        Console.WriteLine($"House Colours: {house.houseColours}");
        Console.WriteLine($"Founder: {house.founder}");
        Console.WriteLine("------");
    }

    string output = JsonSerializer.Serialize(houses, new JsonSerializerOptions { WriteIndented = true });
    createFile.WriteToFile("HPHouses.json", output);
}


        }
        catch (HttpRequestException error)
        {
            Console.WriteLine($"A HTTP error occured: {error.Message}");
        }
        //to check othet errors
        catch (JsonException jsonError)
        {
            Console.WriteLine($"JSON processing error: {jsonError.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

public class GetResponse
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? founder { get; set; }
}

public class House
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? houseColours { get; set; }
    public string? founder { get; set; }
}

public class CreateFile
{
    public void WriteToFile(string filePath, string content)
    {
        try
        {
            File.WriteAllText(filePath, content);
            Console.WriteLine($"File successfully written to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
}
    /* 
    private static readonly string url = "https://wizard-world-api.herokuapp.com/Houses";
    private static readonly string filePath = "HPHouses.txt";

    // Method to fetch data from the API
    public static async Task FetchAndSaveDataAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Send GET request
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseData = await response.Content.ReadAsStringAsync();

                    // Save the data to the file
                    await File.WriteAllTextAsync(filePath, responseData);

                    // Print the data to the terminal
                    PrintDataToTerminal(responseData);

                    Console.WriteLine($"Data has been written to {filePath}");
                }
                else
                {
                    Console.WriteLine("Error: Unable to retrieve data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

    // Method to print data to the terminal
    private static void PrintDataToTerminal(string data)
    {
        Console.WriteLine("Data Retrieved from API:");
        Console.WriteLine(data);
    } */
}

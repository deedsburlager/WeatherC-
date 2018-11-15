using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Weather
{
    public class Program
    {
       
        static void Main(string[] args)
        {
          
            while (true)
            {
                //User Interface
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to the WEATHER APP!");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("A - Temperatures By Zipcode");
                Console.WriteLine("B - Current Humidity By Zipcode");
                string menuChoice = Console.ReadLine().ToUpper();

                if (menuChoice == "A")
                {
                    Console.WriteLine("Enter your ZIP for Temperture Info!");
                    string zip = Console.ReadLine();
                    Console.Clear();

                    //Call API Method
                    CurrentTemperatureCall(zip);
                }
                else if(menuChoice == "B")
                {
                    Console.WriteLine("Enter your ZIP for Humidity Info!");
                    string zip = Console.ReadLine();
                    Console.Clear();

                    //Call API Method
                    CurrentHumidityCall(zip);
                }

            }
     
              
        }
    

        private static void CurrentTemperatureCall(string zip)
        {
            string key = "242de471603105407f10af0002294b02";
            WeatherObj model = null;
            HttpClient client = new HttpClient();
            string url = $"http://api.openweathermap.org/data/2.5/weather?" +
                            $"zip={zip},us&units=imperial&appid={key}";
            var task = client.GetAsync(url)
                .ContinueWith((taskForResponse) =>
                {
                    HttpResponseMessage response = taskForResponse.Result;
                    var processJson = response.Content.ReadAsAsync<WeatherObj>();
                    processJson.Wait();
                    model = processJson.Result;
                });

            task.Wait();
            DisplayTemperatures(model);

        }

        private static void CurrentHumidityCall(string zip)
        {
            string key = "242de471603105407f10af0002294b02";
            WeatherObj model = null;
            HttpClient client = new HttpClient();
            string url = $"http://api.openweathermap.org/data/2.5/weather?" +
                            $"zip={zip},us&units=imperial&appid={key}";
            var task = client.GetAsync(url)
                .ContinueWith((taskForResponse) =>
                {
                    HttpResponseMessage response = taskForResponse.Result;
                    var processJson = response.Content.ReadAsAsync<WeatherObj>();
                    processJson.Wait();
                    model = processJson.Result;
                });

            task.Wait();
            DisplayHumidity(model);

        }

        private static void DisplayTemperatures(WeatherObj model)
        {
       
            Console.WriteLine($"The Temperature for {model.City} is:");
            Console.WriteLine($"Current Temperture: {model.Main.CurrentTemp}");
            Console.WriteLine($"With a high of {model.Main.HighTemp}");
            Console.WriteLine($"And a low of {model.Main.LowTemp}");
            Console.WriteLine("Would you like save? Y/N");
            string saveAnswer = Console.ReadLine().ToUpper();
            if (saveAnswer == "Y")
            {
                SaveTemperatures(model);
            }
         
            Console.Clear();
            Console.WriteLine("Thank you for using the Weather App.");
            Console.WriteLine("Press any key to search again...");
            Console.ReadLine();

        }

        private static void DisplayHumidity(WeatherObj model)
        {

            Console.WriteLine($"The Humidity for {model.City} is:");
            Console.WriteLine($"Current Humidity: {model.Main.Humidity}%");
            Console.WriteLine("Would you like save? Y/N");
            string saveAnswer = Console.ReadLine().ToUpper();
            if (saveAnswer == "Y")
            {
                SaveHumidity(model);
            }

            Console.Clear();
            Console.WriteLine("Thank you for using the Weather App.");
            Console.WriteLine("Press any key to search again...");
            Console.ReadLine();

        }
        private static void SaveTemperatures(WeatherObj model)
        {
          
            
            using (StreamWriter writer = new StreamWriter("Temperature.txt", true))
            {
                writer.WriteLine("******************************************");
                writer.WriteLine();
                writer.WriteLine($"City: {model.City}");
                writer.WriteLine($"Current Temp:{model.Main.CurrentTemp}°F");
                writer.WriteLine($"High Temp:{model.Main.HighTemp}°F");
                writer.WriteLine($"Low Temp:{model.Main.LowTemp}°F");
                writer.WriteLine($"Date Searched: {DateTime.Now}");
                writer.WriteLine();
                writer.WriteLine("******************************************");


            }
        }
        private static void SaveHumidity(WeatherObj model)
        {


            using (StreamWriter writer = new StreamWriter("Humidity.txt", true))
            {
                writer.WriteLine("******************************************");
                writer.WriteLine();
                writer.WriteLine($"City: {model.City}");
                writer.WriteLine($"Current Humidity:{model.Main.Humidity}%");
                writer.WriteLine($"Date Searched: {DateTime.Now}");
                writer.WriteLine();
                writer.WriteLine("******************************************");


            }
        }
    }
}

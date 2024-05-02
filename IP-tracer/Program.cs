using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace IP_tracer
{
    public class Data
    {
        public string country { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string loc { get; set; }
        public string timezone { get; set; }
        public string org { get; set; }
        public string hostname { get; set; }
        public string postal { get; set; }

    }
        
     


    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "IP_tracer";
            Console.Write("Enter IP Adress: ");
            string IP = Console.ReadLine();
            string link = $"https://ipinfo.io/{IP}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(link);
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("Req is made Successfully!");

                    string DATA = await response.Content.ReadAsStringAsync();
                    Data ipInfo = JsonConvert.DeserializeObject<Data>(DATA);

                    Console.Clear();
                    string[] coords = ipInfo.loc.Split(',');
                    Console.WriteLine("Here is your Target's information: ");
                    Console.WriteLine($"Country : {ipInfo.country}");
                    Console.WriteLine($"City : {ipInfo.city}");
                    Console.WriteLine($"Region : {ipInfo.region}");
                    Console.WriteLine($"Coordinates : {ipInfo.loc}");
                    Console.WriteLine($"Google Maps Location: www.google.com/maps/?q={coords[0]},{coords[1]}");
                    Console.WriteLine($"Time Zone : {ipInfo.timezone}");
                    Console.WriteLine($"ASN : {ipInfo.org}");
                    Console.WriteLine($"Hostname : {ipInfo.hostname}");
                    Console.WriteLine($"Postal Code : {ipInfo.postal}");

                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"you got this Error : {ex.Message}");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Amazon.Rekognition.Model;
using System.Threading;
using RealEyes.Amazon;
using System.Configuration;

namespace RealEyes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            
            //ReadAllSettings();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void ReadAllSettings() {
            try  
            {  
                var appSettings = ConfigurationManager.AppSettings;  

                if (appSettings.Count == 0)  
                {  
                    Console.WriteLine("AppSettings is empty.");  
                }  
                else  
                {  
                    foreach (var key in appSettings.AllKeys)  
                    {  
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);  
                    }  
                }  
            }  
            catch (ConfigurationErrorsException)  
            {  
                Console.WriteLine("Error reading app settings");  
            }  
        }
        
    }
}

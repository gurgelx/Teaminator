using System;
using System.Net;
using Microsoft.Owin.Hosting;


namespace Teaminator.WebApi
{
    class ConsoleHost
    {
        static void Main(string[] args)
        {
            var host = "http://*:8080";
            using (WebApp.Start<StartUp>(host))
            {
                
                Console.WriteLine("Server is running @ " + host);
                Console.ReadLine();
            }
        }
    }
}

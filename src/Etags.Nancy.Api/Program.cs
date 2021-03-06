﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Etags.Nancy.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://+:8080";
            if (args.Any())
            {
                url = "http://+:" + args[0];
            }

            Global.BuildSessionFactory();
            // Data.Create();
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}

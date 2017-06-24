using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace HaloLive.Authentication.Application
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.ConfigureKestrelHostWithCommandlinArgs(args) //setups HaloLive specific hosting
				.UseStartup<Startup>()
				.UseApplicationInsights()
				.Build();

			host.Run();
		}
	}
}

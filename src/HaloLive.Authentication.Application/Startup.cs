using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using HaloLive.Hosting;
using HaloLive.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HaloLive.Authentication.Application
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				//Add the authserver config json file
				.AddJsonFile(@"Config/authserverconfig.json", false)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			GeneralConfiguration = builder.Build();
		}

		public IConfigurationRoot GeneralConfiguration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();
			services.AddLogging();
			services.AddOptions();
			services.Configure<AuthenticationServerConfigurationModel>(GeneralConfiguration.GetSection("AuthConfig"));

			//We need to immediately resolve the authserver config options because we need them to regiter openiddict
			IOptions<AuthenticationServerConfigurationModel> authOptions = services.BuildServiceProvider()
				.GetService<IOptions<AuthenticationServerConfigurationModel>>();

			services.AddAuthentication();

			//Below is the OpenIddict registration
			//This is the recommended setup from the official Github: https://github.com/openiddict/openiddict-core
			services.AddIdentity<HaloLiveApplicationUser, HaloLiveApplicationRole>(options =>
				{
					//These disable the ridiculous requirements that the defauly password scheme has
					options.Password.RequireNonAlphanumeric = false;
				})
				.AddEntityFrameworkStores<HaloLiveAuthenticationDbContext, int>()
				.AddDefaultTokenProviders();

			services.AddDbContext<HaloLiveAuthenticationDbContext>(options =>
			{
				//TODO: Setup db options
				options.UseMySql(authOptions.Value.AuthenticationDatabaseString);
				options.UseOpenIddict<int>();
			});

			services.AddOpenIddict<int>(options =>
			{
				// Register the Entity Framework stores.
				options.AddEntityFrameworkCoreStores<HaloLiveAuthenticationDbContext>();

				// Register the ASP.NET Core MVC binder used by OpenIddict.
				// Note: if you don't call this method, you won't be able to
				// bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
				options.AddMvcBinders();

				//This controller endpoint/action was specified in the HaloLive documentation: https://github.com/HaloLive/Documentation
				options.EnableTokenEndpoint(authOptions.Value.AuthenticationControllerEndpoint); // Enable the token endpoint (required to use the password flow).
				options.AllowPasswordFlow(); // Allow client applications to use the grant_type=password flow.
				options.AllowRefreshTokenFlow();
				options.UseJsonWebTokens();
				//Loads the cert from the specified path
				options.AddSigningCertificate(X509Certificate2Loader.Create(authOptions.Value.JwtSigningX509Certificate2Path).Load());
			});

			// Configure Identity to use the same JWT claims as OpenIddict instead
			// of the legacy WS-Federation claims it uses by default (ClaimTypes),
			// which saves you from doing the mapping in your authorization controller.
			services.Configure<IdentityOptions>(options =>
			{
				options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
				options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
				options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(GeneralConfiguration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseIdentity();
			app.UseOpenIddict();

			app.UseMvcWithDefaultRoute();
		}
	}
}

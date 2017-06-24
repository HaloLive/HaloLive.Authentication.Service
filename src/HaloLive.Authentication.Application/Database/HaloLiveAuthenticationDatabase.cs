using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HaloLive.Authentication
{
	/// <summary>
	/// <see cref="DbContext"/> for the <see cref="HaloLiveApplicationUser"/>s for <see cref="OpenIddict"/>.
	/// See Documentation for details: https://github.com/openiddict/openiddict-core
	/// </summary>
	public class HaloLiveAuthenticationDatabase : IdentityDbContext<HaloLiveApplicationUser, HaloLiveApplicationRole, int>
	{
		public HaloLiveAuthenticationDatabase(DbContextOptions<HaloLiveAuthenticationDatabase> options)
			: base(options)
		{

		}
	}
}

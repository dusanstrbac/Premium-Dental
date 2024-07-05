using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PremiumDental.Areas.Identity.Data;

[assembly: HostingStartup(typeof(PremiumDental.Areas.Identity.IdentityHostingStartup))]
namespace PremiumDental.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PremiumDentalContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PremiumDentalContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<PremiumDentalContext>();
            });
        }
    }
}
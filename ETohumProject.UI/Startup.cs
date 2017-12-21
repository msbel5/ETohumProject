using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ETohumProject.UI.Startup))]

namespace ETohumProject.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(
                    "ETProject",
                    new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });

            //hangfire isimli açık kaynak kütüphanesinin kodu queue için bunu kullandım.
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}

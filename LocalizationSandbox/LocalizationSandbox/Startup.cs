using LocalizationSandbox.App.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace LocalizationSandbox
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            //ローカライズ設定(リソースの場所を指定)
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(options => {
               // options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //ローカリゼーションのミドルウェア
            app.UseRequestLocalization(options =>
            {
                CultureInfo[] supportedCultures = LocalizationConstants.SupportedCultures
                                                    .Select(x => new CultureInfo(x))
                                                    .ToArray();

                //cookie名の変更
                CookieRequestCultureProvider cookieCultureProvider = options.RequestCultureProviders.FirstOrDefault(prov => prov is CookieRequestCultureProvider) as CookieRequestCultureProvider;
                cookieCultureProvider.CookieName = LocalizationConstants.CookieName;

                options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                // Formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                options.SupportedUICultures = supportedCultures;
            });


            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

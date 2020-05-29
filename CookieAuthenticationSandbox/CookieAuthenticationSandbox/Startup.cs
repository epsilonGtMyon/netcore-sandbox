using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CookieAuthenticationSandbox
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //UseCookiePolicyで適用されるオプションの設定
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services
                .AddControllersWithViews(options =>
                {

                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressMapClientErrors = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //認証関連のクラス登録
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //option.RequireAuthenticatedSignIn = false;
            })
            //cookie認証を使う
            //スキーマ名を省略した場合は CookieAuthenticationDefaults.AuthenticationSchemが使われているっぽい
            .AddCookie(options => {
                //cookieの名前から「ASP.NET Core を使っている」という情報を知られたくない場合は(セキュリティのため)
                //ここで名前を変更することができる。
                options.Cookie.Name = "authCookie";
            });
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

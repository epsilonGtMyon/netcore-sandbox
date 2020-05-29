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

            //UseCookiePolicy�œK�p�����I�v�V�����̐ݒ�
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

            //�F�؊֘A�̃N���X�o�^
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //option.RequireAuthenticatedSignIn = false;
            })
            //cookie�F�؂��g��
            //�X�L�[�}�����ȗ������ꍇ�� CookieAuthenticationDefaults.AuthenticationSchem���g���Ă�����ۂ�
            .AddCookie(options => {
                //cookie�̖��O����uASP.NET Core ���g���Ă���v�Ƃ�������m��ꂽ���Ȃ��ꍇ��(�Z�L�����e�B�̂���)
                //�����Ŗ��O��ύX���邱�Ƃ��ł���B
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

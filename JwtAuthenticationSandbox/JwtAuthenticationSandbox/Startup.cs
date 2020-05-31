using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;


namespace JwtAuthenticationSandbox
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

            services.
                AddAuthentication(option =>
                {
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {

                    //JwtSecurityTokenHandler.ValidateTokenPayload��ʂ����߂ɂ��낢�낢��݂����B
                    //���؂�Microsoft.IdentityModel.Tokens.Validators���Ă����ÓI�N���X������

                    //audience�̌���
                    option.TokenValidationParameters.ValidAudiences = new string[] { "sandbox" };

                    //issuer�̌���
                    option.TokenValidationParameters.ValidIssuers = new string[] { "epsilon" };

                    //�f�t�H���g��5���ɂȂ��ĂāA�����߂��Ă����΂炭�͌��؂����i���Ă��܂��B
                    option.TokenValidationParameters.ClockSkew = TimeSpan.Zero;

                    //�Ƃ肠�����e�L�g�[�Ȍ�
                    byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
                    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
                    option.TokenValidationParameters.IssuerSigningKey = securityKey;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

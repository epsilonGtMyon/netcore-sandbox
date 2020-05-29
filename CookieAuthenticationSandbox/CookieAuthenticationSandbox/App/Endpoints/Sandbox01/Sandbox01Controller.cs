using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace CookieAuthenticationSandbox.App.Endpoints.Sandbox01
{
    [ApiController]
    [Route("api/sandbox01")]
    public class Sandbox01Controller : ControllerBase
    {
        /// <summary>
        /// 何もしない
        /// </summary>
        /// <returns></returns>
        [HttpGet("do01")]
        public IActionResult Do01()
        {
            return Ok();
        }

        /// <summary>
        /// Authorizeつき
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("do02")]
        public IActionResult Do02()
        {
            var u = User;
            var claimsList = u.Claims.ToList();
            return Ok();
        }

        /// <summary>
        /// 認証ではない通常のCookieを発行してみる。
        /// </summary>
        /// <returns></returns>
        [HttpPost("do-cookie")]
        public IActionResult DoCookie()
        {
            Response.Cookies.Append("aaa", "bbb", new CookieOptions
            {
                MaxAge = TimeSpan.FromSeconds(15)
            });
            return Ok();
        }

        /// <summary>
        /// サインイン
        /// </summary>
        /// <returns></returns>
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn()
        {
            //Claim一覧の準備
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Taro")
            };

            //AuthenticationServiceのSignInAsyncにて
            //Options.RequireAuthenticatedSignInがデフォルト値だとtrueなので
            //principal.Identity.IsAuthenticatedがtrueでないといけない
            //この状態にするにはClaimsIdentityに_authenticationType(スキーマ名)が必要

            //まぁRequireAuthenticatedSignInをfalseにすりゃいいんだが..
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);
            return Ok();
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}

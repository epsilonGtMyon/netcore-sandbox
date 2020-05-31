using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace JwtAuthenticationSandbox.App.Endpoints.Sandbox01
{
    [ApiController]
    [Route("api/sandbox01")]
    public class Sandbox01Controller : ControllerBase
    {
        private readonly ILogger<Sandbox01Controller> _logger;

        public Sandbox01Controller(
            ILogger<Sandbox01Controller> logger
            )
        {
            _logger = logger;
        }

        /// <summary>
        /// なにもなし
        /// </summary>
        /// <returns></returns>
        [HttpGet("do01")]
        public IActionResult Do01()
        {
            return Ok();
        }

        /// <summary>
        /// 認証あり
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("do02")]
        public IActionResult Do02()
        {
            foreach (Claim claim in User.Claims)
            {
                _logger.LogInformation(claim.ToString());
            }

            return Ok();
        }


        /// <summary>
        /// トークンの発行
        /// </summary>
        /// <returns></returns>
        [HttpPost("gen-token")]
        public IActionResult GenToken()
        {
            //payloadに含めるクレーム一覧
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Taro")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

            //署名用のキー
            //TokenValidationParameters.RequireSignedTokensをfalseにすれば、署名なしでも使用可能になるがセキュリティ的にダメ
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials cred = new SigningCredentials(securityKey, "HS256");

            JwtSecurityTokenHandler th = new JwtSecurityTokenHandler();
            JwtSecurityToken jst = th.CreateJwtSecurityToken(
                issuer: "epsilon",
                audience: "sandbox",
                subject: claimsIdentity,
                notBefore: null,//nullのときは現在時刻が使われるようだ
                expires: DateTime.Now.AddMinutes(5),//とりあえず短め
                issuedAt: null,//nullのときは現在時刻が使われるようだ
                signingCredentials: cred
                );
            string token = th.WriteToken(jst);

            var r = new
            {
                token
            };
            return Ok(r);
        }
    }
}

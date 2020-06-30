using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MyLocalizationUsage.App.Endpoints.Sandbox01.Spec;
using System.Linq;

namespace MyLocalizationUsage.App.Endpoints.Sandbox01
{
    [ApiController]
    [Route("api/sandbox01")]
    public class Sandbox01Controller : ControllerBase
    {
        private readonly IStringLocalizer<Dummy> _localizer;

        public Sandbox01Controller(IStringLocalizer<Dummy> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// パラメータあり
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("hello")]
        public IActionResult Hello(HelloRequest request)
        {
            string s = _localizer["M0001", request.MyName];
            return Content(s);
        }

        /// <summary>
        /// パラメータなし
        /// </summary>
        /// <returns></returns>
        [HttpGet("good-morning")]
        public IActionResult GoodMorning()
        {
            string s = _localizer["M0002"];
            return Content(s);
        }

        /// <summary>
        /// 全部の文字列を取得
        /// </summary>
        /// <returns></returns>
        [HttpGet("all-strings")]
        public IActionResult AllStrings()
        {
            string strings = string.Join(", ", _localizer.GetAllStrings().Select(x => (string)x));
            return Content(strings);
        }

        /// <summary>
        /// ロケールの変更
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("change-locale")]
        public IActionResult ChangeLocale([FromBody]ChangeLocaleRequest request)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(request.LocaleName))
            );
            return Ok();
        }

    }
}

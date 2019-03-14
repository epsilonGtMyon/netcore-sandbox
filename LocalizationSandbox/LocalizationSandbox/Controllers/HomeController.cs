using LocalizationSandbox.App.Common;
using LocalizationSandbox.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationSandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<AppMessage> _localizer;

        public HomeController(IStringLocalizer<AppMessage> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Index() => View();

        /// <summary>
        /// ロケール変更
        /// 
        /// Cookieに保存する
        /// </summary>
        /// <param name="req"></param>
        [HttpPost]
        public void ChangeLanguage(HomeRequest req)
        {
            Response.Cookies.Append(LocalizationConstants.CookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(req.Lang)));
        }


        /// <summary>
        /// メッセージの取得
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetMessage()
        {
            return _localizer[AppMessage.Message01];
        }
    }

    public class HomeRequest{
        public string Lang { get; set; }
    }
}

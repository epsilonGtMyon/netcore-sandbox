using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HttpClientSandbox
{
	internal class Sandbox01
	{
		private readonly ILogger<Sandbox01> _logger;
		private readonly HttpClientProvider _httpClientProvider;

		public Sandbox01(
			ILogger<Sandbox01> logger,
			HttpClientProvider httpClientProvider
		)
		{
			_logger = logger;
			_httpClientProvider = httpClientProvider;
		}

		public async Task Execute()
		{
			string userId = "epsilongtmyon";
			string url = $"https://qiita.com/api/v2/users/{userId}";

			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, url);

			// ヘッダー追加したいとき
			// req.Headers.Add("xxx", "uuu");

			// POSTなどでbodyを設定したい場合
			// req.Content = new StringContent("{\"aaa\": 1}", Encoding.UTF8, "application/json");

			// multi-partで送りたい場合はこれを使う
			// rootContentにStreamContentでファイルを渡したり、StirngContentでその他の値を渡す。
			// MultipartFormDataContent rootContent = new MultipartFormDataContent();
			// req.Content = rootContent;

			HttpClient httpClient = _httpClientProvider.Get();
			HttpResponseMessage resp = await httpClient.SendAsync(req);

			// 200番台
			resp.EnsureSuccessStatusCode();

			using (Stream stream = resp.Content.ReadAsStream())
			{
				JsonSerializerOptions options = new JsonSerializerOptions();
				GetUserResponse getUserResponse = JsonSerializer.Deserialize<GetUserResponse>(stream, options)!;

				_logger.LogInformation("レスポンス{getUserResponse}", getUserResponse);
			}
		}
	}
}

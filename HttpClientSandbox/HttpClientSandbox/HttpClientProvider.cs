namespace HttpClientSandbox
{
	internal class HttpClientProvider
	{
		private readonly HttpClient _httpClient;

		public HttpClientProvider()
		{
			_httpClient = new HttpClient();
		}

		public HttpClient Get()
		{
			return _httpClient;
		}
	}
}

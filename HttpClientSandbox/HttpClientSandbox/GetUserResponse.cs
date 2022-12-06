using System.Text.Json.Serialization;

namespace HttpClientSandbox
{
	internal class GetUserResponse
	{
		[JsonPropertyName("description")]
		public string Description { get; set; } = "";

		[JsonPropertyName("facebook_id")]
		public string FacebookId { get; set; } = "";

		[JsonPropertyName("followees_count")]
		public int? FolloweesCount { get; set; }

		[JsonPropertyName("followers_count")]
		public int? FollowersCount { get; set; }


		[JsonPropertyName("github_login_name")]
		public string GithubLoginName { get; set; } = "";


		[JsonPropertyName("id")]
		public string Id { get; set; } = "";


		// 以下省略

		public override string ToString()
		{
			string s = "GetUserResponse[";

			s += $"Description={Description}";
			s += $", FacebookId={FacebookId}";
			s += $", FolloweesCount={FolloweesCount}";
			s += $", FollowersCount={FollowersCount}";
			s += $", GithubLoginName={GithubLoginName}";
			s += $", Id={Id}";

			s += "]";
			return s;
		}

	}
}

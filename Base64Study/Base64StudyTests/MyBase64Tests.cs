using Base64Study;
using System.Text;
using Xunit;

namespace Base64StudyTests
{
    public class MyBase64Tests
    {
        public class EncodeTests
        {
            [Theory]
            [InlineData("", "")]
            [InlineData("f", "Zg==")]
            [InlineData("fo", "Zm8=")]
            [InlineData("foo", "Zm9v")]
            [InlineData("foob", "Zm9vYg==")]
            [InlineData("fooba", "Zm9vYmE=")]
            [InlineData("foobar", "Zm9vYmFy")]
            public void Test(string testData, string expected)
            {
                byte[] b = Encoding.UTF8.GetBytes(testData);

                string encoded = MyBase64.Encode(b);

                Assert.Equal(expected, encoded);
            }
        }

        public class DecodeTests
        {
            [Theory]
            [InlineData("", "")]
            [InlineData("Zg==", "f")]
            [InlineData("Zm8=", "fo")]
            [InlineData("Zm9v", "foo")]
            [InlineData("Zm9vYg==", "foob")]
            [InlineData("Zm9vYmE=", "fooba")]
            [InlineData("Zm9vYmFy", "foobar")]
            public void Test(string testData, string expected)
            {
                byte[] decoded = MyBase64.Decode(testData);

                string decodedString = Encoding.UTF8.GetString(decoded);
                Assert.Equal(expected, decodedString);
            }
        }
    }
}

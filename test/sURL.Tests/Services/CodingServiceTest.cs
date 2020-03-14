using Xunit;
using Moq;

namespace sURL.Services
{
    public class CodingServiceTest
    {
        [Fact]
        public void Encode_正常系()
        {
            const uint input = 1234567890;
            const string ans = "aSwn3joHvs4IOhi5ow690ho67e";

            var hashids = new Mock<HashidsNet.IHashids>();
            hashids
                .Setup(x => x.EncodeHex(It.IsAny<string>()))
                .Returns(ans);

            var hashidsService = new Mock<IHashidsService>();
            hashidsService
                .Setup(x => x.Hashids)
                .Returns(hashids.Object);

            var codingService = new CodingService(
                hashidsService.Object
            );

            var result = codingService.Encode(input);

            Assert.Equal(ans, result);
        }

    }
}
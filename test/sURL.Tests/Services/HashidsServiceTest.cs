using System;
using Xunit;

namespace sURL.Services
{
    public class HashidsServiceTest : HashidsService
    {
        [Fact]
        public void CreateSalt_正常系()
        {
            var hashids = new HashidsServiceSuper();

            var salt = hashids.CreateSalt();

            Assert.False(string.IsNullOrEmpty(salt));
        }
    }

    public class HashidsServiceSuper : HashidsService
    {
        public new string CreateSalt() => base.CreateSalt();
    }
}

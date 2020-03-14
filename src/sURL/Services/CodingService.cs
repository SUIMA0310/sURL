using System;

namespace sURL.Services
{
    public interface ICodingService
    {
        // 数値 ▶ 文字列
        string Encode(uint input);

        // 文字列 ▶ 数値
        uint Decode(string input);
    }

    public class CodingService : ICodingService
    {
        private IHashidsService _HashidsService = null;

        public CodingService(
            IHashidsService hashidsService
        )
        {
            this._HashidsService = hashidsService;
        }

        public uint Decode(string input)
        {
            var hashids = this._HashidsService.Hashids;

            // decode by hashids.
            var hexOutput = hashids.DecodeHex(input);

            // convert from hex string to uint.
            return Convert.ToUInt32(hexOutput, 16);
        }

        public string Encode(uint input)
        {
            var hashids = this._HashidsService.Hashids;

            // convert to hex string.
            var hexInput = Convert.ToString(input, 16);

            // encode by hashids
            return hashids.EncodeHex(hexInput);
        }
    }
}
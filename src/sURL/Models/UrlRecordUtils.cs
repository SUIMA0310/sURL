using System;

namespace sURL.Models
{
    public static class UrlRecoedExtensions
    {

        // AccessCountを繰り上げる。
        public static void AccessCountUp(this UrlRecord record)
        {
            record.AccessCount++;
        }
    }
}
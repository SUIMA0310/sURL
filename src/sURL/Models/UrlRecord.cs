using System;

namespace sURL.Models
{
    public class UrlRecord
    {
        // Strage に保存する際にレコードを一意に識別する
        public virtual uint Id {get; set;}

        // リダイレクト先のURL
        public virtual string Url {get; set;}

        // リダイレクトした回数をカウントする
        public virtual uint AccessCount {get; set;}

        public virtual DateTime CreateTime {get; set;}

        #region コンストラクタ

        public UrlRecord()
        {
            this.Id = 0;
            this.Url = string.Empty;
            this.AccessCount = 0;
            this.CreateTime = DateTime.Now;
        }

        public UrlRecord(
            uint id, 
            string url, 
            uint accsessCount, 
            DateTime createTime)
        {
            this.Id = id;
            this.Url = url;
            this.AccessCount = accsessCount;
            this.CreateTime = createTime;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sURL.Models
{
    public class UrlRecordContext : DbContext
    {
        public UrlRecordContext(DbContextOptions<UrlRecordContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<UrlRecord> Urls {get; set;}
    }
}
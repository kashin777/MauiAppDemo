using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppDemo.Models
{
    public class MauiAppDemoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private string connStr;

        public MauiAppDemoDbContext()
        {
            connStr = Path.Combine(FileSystem.AppDataDirectory, "MauiAppDemo.sq3");
        }

        //デスクトップ上にSQLiteのDBファイルが作成される
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={connStr}");
    }
}

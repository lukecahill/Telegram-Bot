using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Models;

namespace TelegramBot.DAL {
    class TelegramBotContext : DbContext {
        public DbSet<Items> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=my_db;");
    }
}
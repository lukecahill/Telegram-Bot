using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TelegramBot {
    class TelegramBotContext : DbContext {
        public DbSet<Items> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=my_db;");
    }
}
﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TelegramBot;

namespace telegram_bot.Migrations
{
    [DbContext(typeof(TelegramBotContext))]
    partial class TelegramBotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TelegramBot.Items", b =>
                {
                    b.Property<int>("itemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.HasKey("itemId");

                    b.ToTable("Items");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WeddingPlanner.Models;

namespace WeddingPlanner.Migrations
{
    [DbContext(typeof(WeddingContext))]
    [Migration("20171115221609_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("WeddingPlanner.Models.Guest", b =>
                {
                    b.Property<int>("guestId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("userId");

                    b.Property<int>("weddingId");

                    b.HasKey("guestId");

                    b.HasIndex("userId");

                    b.HasIndex("weddingId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("WeddingPlanner.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("email");

                    b.Property<string>("firstName");

                    b.Property<string>("lastName");

                    b.Property<string>("password");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WeddingPlanner.Models.Wedding", b =>
                {
                    b.Property<int>("weddingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address")
                        .IsRequired();

                    b.Property<int>("userId");

                    b.Property<string>("wedderOne")
                        .IsRequired();

                    b.Property<string>("wedderTwo")
                        .IsRequired();

                    b.Property<DateTime>("weddingDate");

                    b.HasKey("weddingId");

                    b.HasIndex("userId");

                    b.ToTable("Weddings");
                });

            modelBuilder.Entity("WeddingPlanner.Models.Guest", b =>
                {
                    b.HasOne("WeddingPlanner.Models.User", "user")
                        .WithMany("guests")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WeddingPlanner.Models.Wedding", "wedding")
                        .WithMany("guests")
                        .HasForeignKey("weddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingPlanner.Models.Wedding", b =>
                {
                    b.HasOne("WeddingPlanner.Models.User", "user")
                        .WithMany("weddings")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

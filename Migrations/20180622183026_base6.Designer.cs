﻿// <auto-generated />
using Belt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Belt.Migrations
{
    [DbContext(typeof(StorageContext))]
    [Migration("20180622183026_base6")]
    partial class base6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Belt.Models.EventActivity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("creatorId");

                    b.Property<DateTime>("date");

                    b.Property<float>("duration");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.HasIndex("creatorId");

                    b.ToTable("activities");
                });

            modelBuilder.Entity("Belt.Models.Participant", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("activityId");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("activityId");

                    b.HasIndex("userId");

                    b.ToTable("participants");
                });

            modelBuilder.Entity("Belt.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_at");

                    b.Property<string>("email");

                    b.Property<string>("first_name");

                    b.Property<string>("last_name");

                    b.Property<string>("password");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Belt.Models.EventActivity", b =>
                {
                    b.HasOne("Belt.Models.User", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Belt.Models.Participant", b =>
                {
                    b.HasOne("Belt.Models.EventActivity", "activity")
                        .WithMany()
                        .HasForeignKey("activityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Belt.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomOccupancy.Persistence;

namespace RoomOccupancy.Persistence.Migrations
{
    [DbContext(typeof(ReservationDbContext))]
    [Migration("20190907000554_AwaitsAcceptance")]
    partial class AwaitsAcceptance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.BuildingFaculty", b =>
                {
                    b.Property<int>("FacultyId");

                    b.Property<int>("BuildingId");

                    b.HasKey("FacultyId", "BuildingId");

                    b.HasIndex("BuildingId");

                    b.ToTable("BuildingFaculties");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Disponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Disponents");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acronym");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.FacultyRoom", b =>
                {
                    b.Property<int>("RoomId");

                    b.Property<int>("FacultyId");

                    b.HasKey("RoomId", "FacultyId");

                    b.HasIndex("FacultyId");

                    b.ToTable("FacultyRooms");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualUse");

                    b.Property<int>("BuildingId");

                    b.Property<string>("Description");

                    b.Property<string>("DesignatedUse");

                    b.Property<int?>("DisponentId");

                    b.Property<string>("FacultyLookup");

                    b.Property<int>("Floor");

                    b.Property<string>("Name");

                    b.Property<string>("Number");

                    b.Property<int?>("Seats");

                    b.Property<float?>("Space");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("DisponentId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.RoomEquipment", b =>
                {
                    b.Property<int>("RoomId");

                    b.Property<int>("EquipmentId");

                    b.Property<int>("Amount");

                    b.Property<bool>("IsAvailable");

                    b.HasKey("RoomId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("RoomEquipment");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId");

                    b.Property<bool>("AwaitsAcceptance");

                    b.Property<DateTime>("End");

                    b.Property<bool>("IsCancelled");

                    b.Property<bool>("IsCyclical");

                    b.Property<int>("ReservationDays");

                    b.Property<int>("RoomId");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId");

                    b.Property<int?>("DisponentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("DisponentId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Users.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("FacultyId");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Users.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.BuildingFaculty", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.FacultyRoom", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Faculty", "Faculty")
                        .WithMany("Rooms")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Room", "Room")
                        .WithMany("Faculties")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Room", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Disponent", "Disponent")
                        .WithMany()
                        .HasForeignKey("DisponentId");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.RoomEquipment", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Equipment", "Equipment")
                        .WithMany("RoomsEquipment")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Room", "Room")
                        .WithMany("Equipment")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Users.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.Lecturer", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Users.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Disponent", "Disponent")
                        .WithMany()
                        .HasForeignKey("DisponentId");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Users.AppUser", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId");
                });
#pragma warning restore 612, 618
        }
    }
}

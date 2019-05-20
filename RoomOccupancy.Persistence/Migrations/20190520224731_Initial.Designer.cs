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
    [Migration("20190520224731_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampusMapPositionId");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.HasIndex("CampusMapPositionId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.BuildingWing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BuildingWings");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.CampusMapPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("XPos");

                    b.Property<int>("YPos");

                    b.HasKey("Id");

                    b.ToTable("CampusMapPosition");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Disponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisponentType");

                    b.Property<string>("Name");

                    b.Property<int?>("RelatedEntityId");

                    b.HasKey("Id");

                    b.ToTable("Disponents");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualUse");

                    b.Property<int>("BuildingId");

                    b.Property<int?>("BuildingWindId");

                    b.Property<int?>("BuildingWingId");

                    b.Property<string>("Description");

                    b.Property<string>("DesignatedUse");

                    b.Property<int?>("FacultyId");

                    b.Property<int>("Floor");

                    b.Property<string>("Name");

                    b.Property<int?>("Seats");

                    b.Property<float?>("Space");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("BuildingWingId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.RoomDisponent", b =>
                {
                    b.Property<int>("RoomId");

                    b.Property<int>("DisponentId");

                    b.Property<int>("Id");

                    b.HasKey("RoomId", "DisponentId");

                    b.HasIndex("DisponentId");

                    b.ToTable("RoomDisponents");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End");

                    b.Property<bool>("IsCyclical");

                    b.Property<int>("RoomId");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LecturerId");

                    b.Property<string>("Name");

                    b.Property<int>("ScheduleId");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.DegreeProgramme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Degree");

                    b.Property<string>("FieldOfStudy");

                    b.Property<string>("Form");

                    b.Property<string>("Specialisation");

                    b.HasKey("Id");

                    b.ToTable("DegreeProgrammes");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DegreeProgrammeId");

                    b.Property<int>("FacultyId");

                    b.Property<string>("Semestrial");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("DegreeProgrammeId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Building", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.CampusMapPosition", "CampusMapPosition")
                        .WithMany()
                        .HasForeignKey("CampusMapPositionId");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Equipment", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Room", "Room")
                        .WithMany("Equipment")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Faculty", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Department", "Department")
                        .WithMany("Faculties")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.Room", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.BuildingWing", "BuildingWing")
                        .WithMany()
                        .HasForeignKey("BuildingWingId");

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId");
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Campus.RoomDisponent", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Disponent", "Disponent")
                        .WithMany("Rooms")
                        .HasForeignKey("DisponentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Room", "Room")
                        .WithMany("Disponents")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.Course", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Schedule.Lecturer", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Schedule.Schedule", "Schedule")
                        .WithMany("Courses")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RoomOccupancy.Domain.Entities.Schedule.Schedule", b =>
                {
                    b.HasOne("RoomOccupancy.Domain.Entities.Schedule.DegreeProgramme", "DegreeProgramme")
                        .WithMany()
                        .HasForeignKey("DegreeProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RoomOccupancy.Domain.Entities.Campus.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

//SqlDbContext.cs → Defines database connections, manages entity relationships, and handles migrations. (You already have this file!)
using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using P1WEBMVC.Models;
using P1WEBMVC.Models.DomainModels;
using P1WEBMVC.Models.Junction_Model;
namespace P1WEBMVC.Data
{




    public class SqlDbContext : DbContext
    {



        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        // Database Entities
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        // public DbSet<Payment> Payments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        




        // Other DbSet properties


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User & DoctorProfile Relationship (One-to-One)
            // modelBuilder.Entity<Appointment>()
            //     .HasOne(a => a.Payment)
            //     .WithOne(p => p.Appointment)
            //     .HasForeignKey<Appointment>(a => a.PaymentId)
            //     .OnDelete(DeleteBehavior.Restrict);


        }

        //     // Patient & Appointment Relationship (One-to-Many)
        //     modelBuilder.Entity<Appointment>()
        //         .HasOne(a => a.Patient)
        //         .WithMany(p => p.Appointments)
        //         .HasForeignKey(a => a.PatientID)
        //         .OnDelete(DeleteBehavior.SetNull);

        //     // Doctor & Appointment Relationship (One-to-Many)
        //     modelBuilder.Entity<Appointment>()
        //         .HasOne(a => a.Doctor)
        //         .WithMany(d => d.Appointments)
        //         .HasForeignKey(a => a.DoctorID)
        //         .OnDelete(DeleteBehavior.SetNull);

        //     // Appointment & Payment Relationship (One-to-One)
        //     // modelBuilder.Entity<Payment>()
        //     //     .HasOne(p => p.Appointment)
        //     //     .WithOne(a => a.Payment)
        //     //     .HasForeignKey<Payment>(p => p.AppointmentID)
        //     //     .OnDelete(DeleteBehavior.Cascade);

        //     // Doctor & Reviews Relationship (One-to-Many)
        //     modelBuilder.Entity<Review>()
        //         .HasOne(r => r.Doctor)
        //         .WithMany(d => d.Reviews)
        //         .HasForeignKey(r => r.DoctorID)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     // Patient & Reviews Relationship (One-to-Many)
        //     modelBuilder.Entity<Review>()
        //         .HasOne(r => r.Patient)
        //         .WithMany(p => p.Reviews)
        //         .HasForeignKey(r => r.PatientID)
        //         .OnDelete(DeleteBehavior.SetNull);








        //    // Doctor and Patient relationship (Many-to-Many)
        //     modelBuilder.Entity<DoctorPatient>()
        //         .ToTable("DoctorPatient");

        //     modelBuilder.Entity<DoctorPatient>()
        //         .HasKey(dp => new { dp.DoctorId, dp.PatientId });

        //     modelBuilder.Entity<DoctorPatient>()
        //         .HasOne(dp => dp.Doctor)
        //         .WithMany(d => d.DoctorPatients)
        //         .HasForeignKey(dp => dp.DoctorId);

        //     modelBuilder.Entity<DoctorPatient>()
        //         .HasOne(dp => dp.Patient)
        //         .WithMany(p => p.DoctorPatients)
        //         .HasForeignKey(dp => dp.PatientId);

        // }







    }
}
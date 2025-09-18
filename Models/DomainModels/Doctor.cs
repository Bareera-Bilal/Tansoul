
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P1WEBMVC.Models
{
    public class Doctor
    {
        public Guid DoctorId { get; set; }

        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Specialization { get; set; }
        public required string Contact { get; set; }

        // public required string MedicalLicense { get; set; }
        // public required string Aadhaar { get; set; }
        // public required string PAN { get; set; }

        public bool IsApproved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Appointment> Appointments { get; set; } = new();
        

    }
}


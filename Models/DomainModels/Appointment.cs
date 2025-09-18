using System;
using System.ComponentModel.DataAnnotations;
using P1WEBMVC.Data;
using P1WEBMVC.Types;
using P1WEBMVC.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;


namespace P1WEBMVC.Models
{
    public class Appointment
    {
        [Key]



         public string PatientName { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public Guid AppointmentId { get; set; } = Guid.NewGuid();

        public required Guid DoctorId { get; set; }

       
        public Doctor? Doctor { get; set; }   // naviagation property 



        public required Guid PatientId { get; set; }

       

        public Patient? Patient { get; set; }  // naviagtaion property to User 



        // public required Guid PaymentId { get; set; }

       

        // public Payment? Payment { get; set; }   // naviagtion property




        [Required]
        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string Reason { get; set; } = string.Empty;

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
        public DateTime AppointmentDate { get; set; } = DateTime.UtcNow.AddDays(1);
    }
}



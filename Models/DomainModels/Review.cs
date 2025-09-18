using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P1WEBMVC.Types;

namespace P1WEBMVC.Models;

public class Review
{
    [Key]
    public Guid ReviewId { get; set; } = Guid.NewGuid();
    public string? Feedback { get; set; }

    public string? Content { get; set; }



    public required Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient? Patient { get; set; }       // naviagation property 



    public required Guid DoctorId { get; set; } // Added DoctorID property

    [ForeignKey("DoctorId")]

    public Doctor? Doctor { get; set; }
}

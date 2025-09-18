namespace P1WEBMVC.Models;
using System.ComponentModel.DataAnnotations;
using P1WEBMVC.Types;




public class Patient
{

    [Key]
    public Guid PatientId { get; set; } = Guid.NewGuid();
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public string? ProfilePicUrl { get; set; }

    public required string PhoneNumber { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<Review>? Reviews { get; set; }

    public bool HasAdminPrivileges { get; set; } = false;
}








using System;

namespace P1WEBMVC.Models.ViewModels;

public class DoctorEditView
{
    public Guid DoctorId { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Specialization { get; set; }
    public required string Contact { get; set; }
}


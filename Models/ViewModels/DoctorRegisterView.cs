using System;

namespace P1WEBMVC.Models.ViewModels;

public class DoctorRegisterView
{
public required string Name { get; set; }

 public required string Username { get; set; }
public required string Email { get; set; }
public required string Password { get; set; } // This should already be encrypted before assignment
public required string Specialization { get; set; }
public required string Contact { get; set; }
}

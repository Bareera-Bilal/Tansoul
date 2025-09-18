using System;

namespace P1WEBMVC.Models.ViewModels;

public class DoctorView
{
    
public Doctor? Doctor { get; set; }    
 public List<Appointment> Appointments { get; set; } = new();
 
}
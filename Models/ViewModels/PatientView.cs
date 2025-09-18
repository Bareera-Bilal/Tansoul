using System;

namespace P1WEBMVC.Models.ViewModels;

public class PatientView
{
public Patient? Patient { get; set; }  
 public List<Appointment> Appointments { get; set; } = new();
}


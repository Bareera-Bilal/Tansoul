using System;

namespace P1WEBMVC.Models.Junction_Model;

public class DoctorPatient
{
    public Guid DoctorId { get; set; }
    public Doctor? Doctor { get; set; }


    
    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }
}
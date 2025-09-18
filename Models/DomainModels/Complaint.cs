
using System.ComponentModel.DataAnnotations;

public class Complaint
{
    [Key]
    public Guid ComplaintId { get; set; }

    [Required]
    [StringLength(500)]
    public required string Description { get; set; }

    public DateTime SubmittedOn { get; set; } = DateTime.Now;
}

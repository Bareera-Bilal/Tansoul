using System.ComponentModel.DataAnnotations;
using P1WEBMVC.Types;

namespace P1WEBMVC.Models;
public class Notification
{
    public Guid NotificationId { get; set; } = Guid.NewGuid();
    public required string Message { get; set; }
    
    public Guid UserId { get; set; }
    public required Patient Patient { get; set; }
}

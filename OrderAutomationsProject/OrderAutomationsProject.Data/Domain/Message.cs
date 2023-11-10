using OrderAutomationsProject.Base.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAutomationsProject.Data.Domain;

public class Message : BaseModel
{
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public string Role { get; set; }

    [ForeignKey("Sender")]
    public int SenderId { get; set; }
    public virtual Dealer Sender { get; set; }

    [ForeignKey("Receiver")]
    public int ReceiverId { get; set; }
    public virtual Dealer Receiver { get; set; }
}

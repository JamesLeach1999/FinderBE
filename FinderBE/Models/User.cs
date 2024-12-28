using System.ComponentModel.DataAnnotations;

namespace FinderBE.Models;

public class User
{
    [Required]
    public Guid UserId = Guid.NewGuid();

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime AccountCreatedDate  = DateTime.Now;
}

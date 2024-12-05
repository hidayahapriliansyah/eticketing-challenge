using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticketing.Models;

[Table(name: "admins")]
public class Admin : Base
{
    [Key]
    [Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "email")]
    public string Email { get; set; } = null!;

    [Column(name: "password")]
    public string Password { get; set; } = null!;
}

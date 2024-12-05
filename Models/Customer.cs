using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticketing.Models;

[Table(name: "customers")]
public class Customer : Base
{
    [Key]
    [Column(name: "id")]
    public Guid Id { get; set; }

    [Column(name: "name")]
    public string Name { get; set; } = null!;

    [Column(name: "email")]
    public string Email { get; set; } = null!;

    [Column(name: "username")]
    public string Username { get; set; } = null!;

    [Column(name: "password")]
    public string Password { get; set; } = null!;

    public List<Ticket> Tickets { get; set; } = [];
}

using System.ComponentModel.DataAnnotations;

public class RegisterCustomerRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string Username { get; set; } = null!;

    [Required]
    [MinLength(8)]
    [MaxLength(30)]
    public string Password { get; set; } = null!;
}

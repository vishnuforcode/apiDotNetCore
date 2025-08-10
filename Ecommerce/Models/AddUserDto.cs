namespace Ecommerce.Models
{
    public class AddUserDto
    {
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Password { get; set; }
    }
}
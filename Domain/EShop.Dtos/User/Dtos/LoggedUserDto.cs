namespace EShop.Dtos.User.Dtos;

public class LoggedUserDto
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public bool IsAdmin { get; set; }
}
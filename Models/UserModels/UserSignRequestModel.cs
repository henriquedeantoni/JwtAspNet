public class UserSignRequestModel
{
    public string FirstName { get; set; }
    public string LastName{ get; set; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password{ get; init; }
    public bool Active { get; set; }
    
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
namespace afi_demo.Models.Requests;

public class RegisterUserRequest
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string ReferenceNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; }

}




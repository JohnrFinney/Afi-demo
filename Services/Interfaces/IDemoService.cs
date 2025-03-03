using afi_demo.Endpoints;
using afi_demo.Models.Requests;

namespace afi_demo.Services.Interfaces;

public interface IDemoService
{
    Task<int?> RegisterNewUser(RegisterUserRequest model, bool validateAll = false);


}

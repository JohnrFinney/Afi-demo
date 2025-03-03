using afi_demo.Models.Requests;
using afi_demo.Services.Interfaces;

namespace afi_demo.Services;

public class DemoService : IDemoService
{
    public Task<int?> RegisterNewUser(RegisterUserRequest model, bool validateAll = false)
    {
        throw new NotImplementedException();
    }
}

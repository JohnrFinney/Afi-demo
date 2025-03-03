using afi_demo.Models.Requests;

namespace afi_demo.Services.Repositories.Interfaces;

public interface IDemoRepository
{
    Task<int?> RegisterNewUser(RegisterUserRequest model);

}

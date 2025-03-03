using afi_demo.Classes.Entities;
using afi_demo.Models.Requests;
using afi_demo.Services.Repositories.Interfaces;

namespace afi_demo.Services.Repositories;

public class DemoRepository : IDemoRepository
{

    private readonly AppDbContext _dbContext;

    public DemoRepository(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<int?> RegisterNewUser(RegisterUserRequest model)
    {
        var afi = new AfiDemo()
        {
            FirstName = model.FirstName,
            Surname = model.Surname,
            ReferenceNumber = model.ReferenceNumber,
            DateOfBirth = model.DateOfBirth,
            EmailAddress = model.Email
        };

        _dbContext.AfiDemos.Add(afi);

        await _dbContext.SaveChangesAsync();

        return afi.Id;
    }
}

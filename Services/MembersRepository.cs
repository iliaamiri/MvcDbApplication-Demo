using Microsoft.EntityFrameworkCore;
using MvcDbApplication.Data.Baraga;

namespace MvcDbApplication.Services;

public class MembersRepository : IMembersRepositry
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MembersRepository> _logger;

    public MembersRepository(IServiceProvider serviceProvider, ILogger<MembersRepository> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<Member> AddMember(Member member)
    {
        try
        {
            using (
                var db = new BaragaContext(
                    _serviceProvider.GetRequiredService<DbContextOptions<BaragaContext>>()
                )
            )
            {
                await db.Members.AddAsync(member);
                await db.SaveChangesAsync();
                return member;
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            throw new Exception("Something Wrong happened");
        }
    }

    public async Task DeleteMemberById(int memberId)
    {
        try
        {
            using (
                var db = new BaragaContext(
                    _serviceProvider.GetRequiredService<DbContextOptions<BaragaContext>>()
                )
            )
            {
                db.Members.Remove(new Member { MemberId = memberId });
                await db.SaveChangesAsync();
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            throw new Exception("Something Wrong happened");
        }
    }

    public async Task<Member?> GetMemberById(int memberId)
    {
        Member? foundMember;
        try
        {
            using (
                var db = new BaragaContext(
                    _serviceProvider.GetRequiredService<DbContextOptions<BaragaContext>>()
                )
            )
            {
                foundMember = await db.Members.FindAsync(new Member() { MemberId = memberId });
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            throw new Exception("Something Wrong happened");
        }
        
        return foundMember;
    }

    public async Task<Member[]> GetMembers()
    {
        return await Task.Run(() =>
        {
            try
            {
                using (
                    var db = new BaragaContext(
                        _serviceProvider.GetRequiredService<DbContextOptions<BaragaContext>>()
                    )
                )
                {
                    return db.Members.ToArray();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw new Exception("Something Wrong happened");
            }
        });
    }

    public async Task UpdateMember(Member member)
    {
        try
        {
            using (
                var db = new BaragaContext(
                    _serviceProvider.GetRequiredService<DbContextOptions<BaragaContext>>()
                )
            )
            {
                db.Members.Update(member);
                await db.SaveChangesAsync();
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            throw new Exception("Something Wrong happened");
        }
    }
}

using MvcDbApplication.Data.Baraga;

namespace MvcDbApplication.Services;

public interface IMembersRepositry {
    public Task<Member[]> GetMembers();

    public Task<Member?> GetMemberById(int memberId);

    public Task UpdateMember(Member member);

    public Task<Member> AddMember(Member member);

    public Task DeleteMemberById(int memberId);
}
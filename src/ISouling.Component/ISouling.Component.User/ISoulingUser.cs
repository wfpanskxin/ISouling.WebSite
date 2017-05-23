using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ISouling.Component.User
{
    // Add profile data for application users by adding properties to the MemberUser class
    public class MemberUser : IdentityUser<int>
    {
    }

    // Add profile data for application users by adding properties to the CompanyUser class
    public class CompanyUser : IdentityUser<int>
    {
    }
}

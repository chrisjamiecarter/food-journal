using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Models;

internal sealed class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}

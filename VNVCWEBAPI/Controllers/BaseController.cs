using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VNVCWEBAPI.Common.Constraint;

namespace VNVCWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected bool isAccess(int id, string Role)
        {
            var StringUserId = User.Claims
                .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            var isTrue = User.Claims
                .Where(x => x.Type.Equals(ClaimTypes.Role))
                .Any(r => r.Value.Equals(DefaultRoles.SuperAdmin) ||
                            r.Value.Equals(DefaultRoles.Admin) ||
                            r.Value.Equals(Role.Split(',')[0]));

            int.TryParse(StringUserId, out int UserId);
            return (UserId == id || isTrue);
        }

        protected bool isAccess(string Role)
        {
           
            var isTrue = User.Claims
                .Where(x => x.Type.Equals(ClaimTypes.Role))
                .Any(r => r.Value.Equals(DefaultRoles.SuperAdmin) ||
                r.Value.Equals(DefaultRoles.Admin) ||
                r.Value.Equals(Role.Split(',')[0]));

            return (isTrue);
        }
    }
}

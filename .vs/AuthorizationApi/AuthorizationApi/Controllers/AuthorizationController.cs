using AuthorizationApi.Entities;
using Microsoft.AspNetCore.Mvc;
namespace AuthorizationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController: ControllerBase
    {
        private AuthorizationContext _context;
        public AuthorizationController(AuthorizationContext context)
        {
            _context = context;
        }
 
    [HttpGet]
    public IActionResult Login(string username, string password)
    {
            var userFromDb =  _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            
            if(userFromDb == null)
            {
                return NotFound();
            }

            return Ok(userFromDb);

        }
    }
}

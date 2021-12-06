using GeekShopping.ProductAPI.Data.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthorizeController(UserManager<IdentityUser> userManager, 
                                   SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult<string> GetUser()
        {
            return "AutorizaController :: Acessado em: "
                + DateTime.Now.ToString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(UserVO model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            { 
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);
            return Ok(model);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserVO userInfo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
                                                                  userInfo.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Login inválido...");
                return BadRequest();
            }
        }
    }
}
 
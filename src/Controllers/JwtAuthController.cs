using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAppAuthentication.Configuration;
using WebAppAuthentication.Models;

namespace WebAppAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JwtAuthenticationConfig jwtConfig;

        public JwtAuthController(
                        UserManager<AppUser> userManager,
                        RoleManager<IdentityRole> roleManager,
                        IOptions<JwtAuthenticationConfig> jwtConfig)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.jwtConfig = jwtConfig?.Value ?? throw new ArgumentNullException(nameof(jwtConfig));
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));

                var token = new JwtSecurityToken(
                    issuer: jwtConfig.ValidIssuer,
                    audience: jwtConfig.ValidAudience,
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

                return Ok(new AuthenticatedUserModel()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidUntil = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return BadRequest("User already exists!");
            }

            AppUser user = new AppUser()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result?.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }

            if (!await roleManager.RoleExistsAsync(Constants.JwtAuthSection.UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.JwtAuthSection.UserRoles.User));
            }

            await userManager.AddToRoleAsync(user, Constants.JwtAuthSection.UserRoles.User);

            return Ok("Successfully created user.");
        }

        [HttpPost("createadmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateUserModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return BadRequest("User already exists!");
            }

            AppUser user = new AppUser()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result?.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }

            if (!await roleManager.RoleExistsAsync(Constants.JwtAuthSection.UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.JwtAuthSection.UserRoles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(Constants.JwtAuthSection.UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.JwtAuthSection.UserRoles.User));
            }

            await userManager.AddToRoleAsync(user, Constants.JwtAuthSection.UserRoles.User);
            await userManager.AddToRoleAsync(user, Constants.JwtAuthSection.UserRoles.Admin);

            return Ok("Successfully created admin.");
        }
    }
}

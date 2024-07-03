using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RedditApi.helpers;
using System.IdentityModel.Tokens.Jwt;
using RedditApi.Models;

namespace RedditApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly RedditDbContext _context;
         private readonly IConfiguration _configuration;
         public Account(RedditDbContext context,IConfiguration configuration)
         {
             _context = context;
             _configuration = configuration;
         }


         [HttpPost("login")]
         public async Task<ApiResponse<List<TokenModel>>> login([FromBody] UserDTO user)
         {

             if(user == null)
             {
                 return new ApiResponse<List<TokenModel>>
                 {
                     status="fail",
                     error="data is null"
                 };
             }
             var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
             if (foundUser != null && BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password))
             {
                 var token = createToken(foundUser);
                 return token.Length > 0 ? new ApiResponse<List<TokenModel>>
                 {
                     status = "success",
                     data = [new TokenModel() { UserToken = token}]
                 } : new ApiResponse<List<TokenModel>>
                 {
                     status = "fail",
                     error = "no token"
                 };
             }
             else
             {
                 return new ApiResponse<List<TokenModel>>
                 {
                     status = "fail",
                     error = "login failed"
                 };
             }
         }

         [HttpPost("register")]
         public async Task<ApiResponse<UserDTO>> register([FromBody] UserDTO user)
         {
             if (user == null)
             {
                 return new ApiResponse<UserDTO>
                 {
                     status = "fail",
                     error = "data is null"
                 };
             }
             else
             {
                 var existing = await _context.Users.FirstOrDefaultAsync(e => e.Username == user.Username);
                 if (existing == null)
                 {
                     user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 12);
                     await _context.Users.AddAsync(new User() { Username=user.Username,
                     Password=user.Password,CreatedAt=user.CreatedAt});
                     await _context.SaveChangesAsync();
                     return new ApiResponse<UserDTO>
                     {
                         status = "success",
                     };
                 }
                 else
                 {
                     return new ApiResponse<UserDTO>
                     {
                         status = "error",
                         error = "user exists already"
                     };
                 }
             }
         }


         private string createToken(User data) 
         {
             List<Claim> claim = new List<Claim>
             {
                 new Claim("Id",data.Id.ToString()),
                  new Claim("Username",data.Username!)
             };

             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value!));
             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
             var token = new JwtSecurityToken(
                 _configuration["Jwt:Issuer"],
                 _configuration["Jwt:Audience"],
                 claims:claim,
                 signingCredentials: creds,
                 expires:DateTime.Now.AddDays(1)
                 );
             var jwt = new JwtSecurityTokenHandler().WriteToken(token);
             return jwt;
         }

     }
}

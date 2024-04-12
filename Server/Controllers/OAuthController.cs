using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string response_type, //Autho Flow Type
            string client_id, //cleint 
            string redirect_uri,
            string scope,// What info I want - email,grandma
            string state //random string 
            )
        
        {
            
            var query = new QueryBuilder();
            query.Add("redirect_uri", redirect_uri);
            query.Add("state", state);



            
            
            return View(model:query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(string userName,
            string redirect_uri,
            string state)
        {
            const string code = "BABAABAAA";
            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            return Redirect($"{redirect_uri}{query.ToString()}");
        }

        public async Task<IActionResult> token(string grant_type,//f;pw of Access request.
            string code, //confirmation of authentication process
            string redirect_uri,
            string client_id
            )
        {
            //some mechanism to validate the code


            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub,"ID"),
                new Claim("granny","cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constant.Secret);

            var key = new SymmetricSecurityKey(secretBytes);

            var algorithm = SecurityAlgorithms.HmacSha256;

            SigningCredentials signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constant.Issuer,
                Constant.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1), signingCredentials
                );

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new { access_token ,
                                       token_type="Bearer"
                
            };

            var resonseJson = JsonConvert.SerializeObject(responseObject);

            var responseByte = Encoding.UTF8.GetBytes(resonseJson);

            await Response.Body.WriteAsync(responseByte,0,responseByte.Length);

            return Redirect(redirect_uri);
        }
    }
}

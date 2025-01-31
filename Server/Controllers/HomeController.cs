﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        } 

        public IActionResult Authenicate()
        {

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

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            
            return Ok(new {access_token = tokenJson });
        }

        public IActionResult Decode(string part)
        {

            return Ok();
        }

    }
}

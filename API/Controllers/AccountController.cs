using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UsuarioDto>> Register(RegisterDto registerDto)
    {
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            Email = registerDto.email.ToLower(),
            DisplayName = registerDto.displayName,
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

          return new UsuarioDto
       {
           Id = user.Id,
           Email = user.Email,
           DisplayName = user.DisplayName,
           Token = tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
    {
        var user =  await context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.email);

        if (user == null) return Unauthorized("Invalid email");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

       return new UsuarioDto
       {
           Id = user.Id,
           Email = user.Email,
           DisplayName = user.DisplayName,
           Token = tokenService.CreateToken(user)
        };
    }
}

using Microsoft.AspNetCore.Mvc;
using CalendarApp.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using CalendarApp.Repositories.Interfaces;
using CalendarApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UsuarioModel usuarioModel = new UsuarioModel();
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly CalendarDbContext _context;

        public AuthController(IConfiguration configuration, CalendarDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Cadastro")]
        public ActionResult<UsuarioModel> Register(UsuarioDto request)
        {
            if(_context.Usuarios.Any(x => x.Email == request.Email))
            {
                return BadRequest("Usuário já existe.");
            }
            if (request.Senha != request.ConfirmarSenha)
            {
                return BadRequest("As senhas não coincidem.");
            }
                
            string senhaHash
                = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            usuarioModel.Nome = request.Nome;
            usuarioModel.Email = request.Email;
            usuarioModel.SenhaHash = senhaHash;

            return Ok(usuarioModel);
                
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioModel>> Login(LoginDto request)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == request.UsuarioOuEmail || u.Nome == request.UsuarioOuEmail);

            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuarioModel.SenhaHash))
            {
                return BadRequest("Senha incorreta.");
            }

            string token = CreateToken(usuarioModel);

            return Ok(token);
        }

        
        private string CreateToken(UsuarioModel usuarioModel)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioModel.Id.ToString()),
                new Claim(ClaimTypes.Name, usuarioModel.Nome)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
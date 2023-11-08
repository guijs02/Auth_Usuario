using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UsuariosApi.Api.Domain.Data.Dtos;
using UsuariosApi.Api.Domain.Models;
using UsuariosApi.Api.Infraestructure.Context;

namespace UsuariosApi.Api.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        public IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(
            IMapper mapper,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            TokenService tokenService,
            RoleManager<IdentityRole> roleManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        public async Task Cadastro(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
                throw new ApplicationException("Falha ao cadastrar o usúario");
        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(
                dto.Username,
                dto.Password,
                false,
                false
            );


            var usuario = _signInManager.UserManager.Users.FirstOrDefault(
                user => user.NormalizedUserName == dto.Username.ToUpper()
            );

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuario não autenticado");
            }

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}

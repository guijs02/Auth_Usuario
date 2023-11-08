using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Api.Application.Authorization;
using UsuariosApi.Api.Application.Services;
using UsuariosApi.Api.Domain.Data.Dtos;


namespace UsuariosApi.Api.UI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        public IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto dto)
        {
            try
            {
                await _usuarioService.Cadastro(dto);
                return Ok("Cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message.ToString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            try
            {
                string token = await _usuarioService.Login(dto);
                return Ok($"Olá {dto.Username} \n token: {token}");
            }
            catch (Exception e)
            {
                return BadRequest("Usuario não encontrado");
            }
        }
        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _usuarioService.Logout();
                await HttpContext.SignOutAsync();
                return Ok("Deslogado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao deslogar");
            }
        }

        [HttpGet("acesso")]
        [Authorize]
        public async Task<IActionResult> Acesso()
        {

            return Ok("Acessado");
        }

        [HttpGet]
        [Route("supervisor")]
        [Authorize(Roles = "Supervisor")]
        public IActionResult SupervisionarFuncionarios()
        {
            return Ok("acesso permitido");
        }
    }
}

using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
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
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            try
            {
                string token = await _usuarioService.Login(dto);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest("Usuario não encontrado");
            }
        }

        [HttpGet("acesso")]
        [Authorize]
        public async Task<IActionResult> Acesso()
        {
            return Ok("acessado");
        }

        [HttpGet("acess-supervisor")]
        [Authorize(Policy = "ApenasSupervisor")]
        public IActionResult SupervisionarFuncionarios()
        {
            return Ok("acessado");
        }

    }
}

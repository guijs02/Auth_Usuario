using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcessoController : Controller
    {
        private readonly ILogger<AcessoController> _logger;

        public AcessoController(ILogger<AcessoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "IdadeMinima")]
        public IActionResult Get(){
            
            return Ok("Acesso permitido");
        }

    }
}
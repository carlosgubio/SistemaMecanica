using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Models;
using SistemaMecanica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace SistemaMecanica.Controllers
{
        [Route("[controller]/[action]")]
        [ApiController]
        public class LoginController : Controller
        {
            public static readonly List<Logins> logins = new List<Logins>();
            private readonly LoginsRepository _LoginsRepository;


            public LoginController()
            {
            _LoginsRepository = new LoginsRepository();
            }
            [HttpGet]
        public IActionResult ConfirmarCliente(int id)
        {
            var resultado = _LoginsRepository.ConfirmarCliente(id);
            return Ok(resultado);
        }
        public IActionResult ConfirmaProfissional(int id)
        {
            var resultado = _LoginsRepository.ConfirmarProfissional(id);
            return Ok(resultado);
        }
    }
}

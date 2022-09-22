using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Models;
using SistemaMecanica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaMecanica.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginsController : Controller
    {
        public static readonly List<Logins> logins = new List<Logins>();
        private readonly LoginsRepository _LoginsRepository;


        public LoginsController()
        {
            _LoginsRepository = new LoginsRepository();
        }

        [HttpGet]
        public IActionResult ConfirmarCliente(int id)
        {
            var resultado = _LoginsRepository.ConfirmarCliente(id);
            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult ConfirmaProfissional(int id)
        {
            var resultado = _LoginsRepository.ConfirmarProfissional(id);
            return Ok(resultado);
        }
    }
}

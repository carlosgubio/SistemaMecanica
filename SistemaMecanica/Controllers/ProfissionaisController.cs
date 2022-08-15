using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Repositories;
using SistemaMecanica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProfissionaisController : Controller
    {
        private readonly ProfissionaisRepository _profissionaisRepository;

        public ProfissionaisController()
        {
            _profissionaisRepository = new ProfissionaisRepository();
        }

        [HttpPost]
        public IActionResult SalvarProfissional(CadastrarProfissionalViewModel salvarProfissionalViewModel)
        {
            if (salvarProfissionalViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarProfissionalViewModel.NomeProfissional == null)
                return Ok("Dados do profissional não informados.");

            if (salvarProfissionalViewModel.CargoProfissional == null)
                throw new ArgumentNullException($"campo {nameof(salvarProfissionalViewModel.CargoProfissional)} vazio ou nulo.");

            var resultado = _profissionaisRepository.SalvarProfissional(salvarProfissionalViewModel);

            if (resultado) return Ok("Profissional cadastrado com sucesso.");

            return Ok("Houve um problema ao salvar. Profissional não cadastrado.");
        }
        [HttpGet]
        public IActionResult BuscarPorNomeProfissional(string nome)
        {
        var resultado = _profissionaisRepository.BuscarProfissionais(nome);
            return Ok(resultado);
        }
    }
}

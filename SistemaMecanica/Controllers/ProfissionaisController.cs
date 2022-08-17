using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Models;
using SistemaMecanica.Repositories;
using SistemaMecanica.ViewModels;
using SistemaMecanica.ViewModelsAtualizar;
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
        public static readonly List<Profissionais> profissionais = new List<Profissionais>();
        private readonly ProfissionaisRepository _profissionaisRepository;

        public ProfissionaisController()
        {
            _profissionaisRepository = new ProfissionaisRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarProfissionalViewModel salvarProfissionalViewModel)
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
        public IActionResult ConsultaPorNome(string nome)
        {
        var resultado = _profissionaisRepository.BuscarProfissionais(nome);
            return Ok(resultado);
        }
        public IActionResult Atualizar(AtualizarProfisionalViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == null)
                return NoContent();

            var cEncontrada = profissionais.FirstOrDefault(x => x.NomeProfissional == model.Encontrar.NomeProfissional);
            if (cEncontrada == null)
                return NotFound("Não há nenhum registro com esse nome.");

            cEncontrada.NomeProfissional = model.Atualizar.NomeProfissional;
            cEncontrada.CargoProfissional = model.Atualizar.CargoProfissional;

            return Ok(cEncontrada);
        }
        [HttpDelete]
        public IActionResult Remover(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return NoContent();

            var cliente = profissionais.FirstOrDefault(x => x.NomeProfissional.Contains(nome));

            if (cliente == null)
                return NotFound();

            profissionais.Remove(cliente);
            return Ok("Removido com sucesso!");
        }
    }
}

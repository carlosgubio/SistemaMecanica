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
    public class OrdensServicoController : Controller
    {
        public static readonly List<OrdensServico> ordensServico = new List<OrdensServico>();
        private readonly OrdensServicoRepository _ordensServicoRepository;

        public OrdensServicoController()
        {
            _ordensServicoRepository = new OrdensServicoRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarOrdemServicoViewModel salvarOrdemServicoViewModel)
        {
            if (salvarOrdemServicoViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarOrdemServicoViewModel.IdOrdemServico == 0)
                return Ok("Dados da Ordem de Servico não informados.");

            if (salvarOrdemServicoViewModel.IdProfissional == 0)
                throw new ArgumentNullException($"campo {nameof(salvarOrdemServicoViewModel.IdProfissional)} vazio ou nulo.");

            if (salvarOrdemServicoViewModel.IdCliente == 0)
                throw new ArgumentNullException($"campo {nameof(salvarOrdemServicoViewModel.IdCliente)} vazio ou nulo.");

            if (salvarOrdemServicoViewModel.IdServico == 0)
                throw new ArgumentNullException($"campo {nameof(salvarOrdemServicoViewModel.IdServico)} vazio ou nulo.");

            if (salvarOrdemServicoViewModel.IdPeca == 0)
                throw new ArgumentNullException($"campo {nameof(salvarOrdemServicoViewModel.IdPeca)} vazio ou nulo.");

            if (salvarOrdemServicoViewModel.TotalGeral == 0)
                throw new ArgumentNullException($"campo {nameof(salvarOrdemServicoViewModel.TotalGeral)} vazio ou nulo.");


            var resultado = _ordensServicoRepository.SalvarOrdemServico(salvarOrdemServicoViewModel);

            if (resultado) return Ok("Ordem de Serviço cadastrada com sucesso.");

            return Ok("Houve um problema ao salvar. Ordem de Serviço não cadastrada.");
        }
        [HttpGet]
        public IActionResult Consultar(string nomeVeiculoCliente)
        {
            var resultado = _ordensServicoRepository.BuscarOrdemServico(nomeVeiculoCliente);
            return Ok(resultado);
        }
        [HttpPut]
        public IActionResult Atualizar(AtualizarOrdensServicoViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == 0)
                return NoContent();

            _ordensServicoRepository.Atualizar(model.Atualizar, model.Encontrar);
            
            return Ok();
        }
        [HttpDelete]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NoContent();

            var ordemServico = ordensServico.FirstOrDefault(x => x.IdOrdemServico == id);

            if (ordemServico == null)
                return NotFound();

            ordensServico.Remove(ordemServico);
            return Ok("Removido com sucesso!");
        }
    }
}

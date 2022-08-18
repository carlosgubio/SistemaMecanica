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
        public static readonly List<OrdemServico> ordensServico = new List<OrdemServico>();
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
        public IActionResult Atualizar(AtualizarOrdemServicoViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == null)
                return NoContent();

            var osEncontrada = ordensServico.FirstOrDefault(x => x.IdOrdemServico == model.Encontrar.IdOrdemServico);
            if (osEncontrada == null)
                return NotFound("Não há nenhum registro com esse nome.");

            osEncontrada.IdOrdemServico = model.Atualizar.IdOrdemServico;
            osEncontrada.IdProfissional = model.Atualizar.IdProfissional;
            osEncontrada.IdCliente = model.Atualizar.IdCliente;
            osEncontrada.IdServico = model.Atualizar.IdServico;
            osEncontrada.IdPeca = model.Atualizar.IdPeca;
            osEncontrada.TotalGeral = model.Atualizar.TotalGeral;

            return Ok(osEncontrada);
        }
        [HttpDelete]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return NoContent();

            var cliente = ordensServico.FirstOrDefault(x => x.IdOrdemServico == id);

            if (cliente == null)
                return NotFound();

            ordensServico.Remove(cliente);
            return Ok("Removido com sucesso!");
        }
    }
}

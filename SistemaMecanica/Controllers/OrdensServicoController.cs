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
        public IActionResult Cadastrar(CadastrarOrdemServicoViewModel cadastrarOrdemServicoViewModel)
        {
            if (cadastrarOrdemServicoViewModel == null)
                return Ok("Não foram informados dados");

            if (cadastrarOrdemServicoViewModel.IdCliente == 0)
                throw new ArgumentNullException($"campo {nameof(cadastrarOrdemServicoViewModel.IdCliente)} vazio ou nulo.");

            if (cadastrarOrdemServicoViewModel.IdVeiculo == 0)
                throw new ArgumentNullException($"campo {nameof(cadastrarOrdemServicoViewModel.IdVeiculo)} vazio ou nulo.");

            var resultado = _ordensServicoRepository.Salvar(cadastrarOrdemServicoViewModel);

            if (resultado) return Ok("Ordem de Serviço cadastrada com sucesso!");

            return Ok("Houve um problema ao salvar. Ordem de Serviço não cadastrada!");
        }
        [HttpPut]
        public IActionResult Atualizar(AtualizarOrdensServicoViewModel atualizarOrdensServicoViewModel)
        {
            if(atualizarOrdensServicoViewModel?.Atualizar?.IdOrdemServico<=0)
                throw new ArgumentNullException($"campo {nameof(atualizarOrdensServicoViewModel.Atualizar.IdOrdemServico)} com valor inválido.");

            var ordensServico = atualizarOrdensServicoViewModel.Atualizar;

            var ordemAtual = _ordensServicoRepository.BuscarOrdemServicoPorId(ordensServico.IdOrdemServico);

            //compara os dados entre ambas para saber o que precisa remover ou atualizar.

            //primeiro: identificar os registros que precisamos remover

            if (ordemAtual != null)
            {
                var itensRemover = ordemAtual.Itens.Where(x => !ordensServico.IdItens.Contains(x.IdProduto)).Select(y=>  y.IdProduto );

                var servicosExecutadosRemover = ordemAtual.ServicosExecutados.Where(x => !ordensServico.IdServicosExecutados.Contains(x.IdServico)).Select(y =>y.IdServico);

                var profissionaisRemover = ordemAtual.Execucoes.Where(x => !ordensServico.IdProfissionais.Contains(x.IdProfissional)).Select(y => y.IdProfissional);

                if(itensRemover!= null && itensRemover.Any())
                {
                    _ordensServicoRepository.RemoverItensOS(itensRemover, ordensServico.IdOrdemServico);
                }
                if(servicosExecutadosRemover != null && servicosExecutadosRemover.Any())
                {
                    _ordensServicoRepository.RemoverServicosExecutadosOs(servicosExecutadosRemover, ordensServico.IdOrdemServico);
                }
                if (profissionaisRemover != null && profissionaisRemover.Any())
                {
                    _ordensServicoRepository.RemoverProfissionaisOS(profissionaisRemover, ordensServico.IdOrdemServico);
                }
            }

            return Ok("Atualizado com sucesso!");
        }

        
        [HttpGet]
        public IActionResult Consultar(int id)
        {
            var resultado = _ordensServicoRepository.BuscarOrdemServicoPorId(id);
            return Ok(resultado);            
        }
        [HttpGet]
        public IActionResult ConsultarPorVeiculo(string veiculo)
        {
            var resultado = _ordensServicoRepository.BuscarOrdemServicoPorVeiculo(veiculo);
            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult BuscarTodas()
        {
            var resultado = _ordensServicoRepository.BuscarTodos();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult Faturamento()
        {
            var resultado = _ordensServicoRepository.FaturamentoBruto();

            if (resultado == 0)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult Confirmar(int id)
        {
            var resultado = _ordensServicoRepository.Confirmar(id);
            return Ok(resultado);
        }
        
        [HttpPut]
        public IActionResult AdicionarProfissional(int id, List<int> profissionais)
        {
            _ordensServicoRepository.InserirProfissionalOS(profissionais, id);
            return Ok("Profissional adicionado com sucesso!");
        }
        [HttpPut]
        public IActionResult AdicionarProduto(int id, List<int> produtos)
        {
            _ordensServicoRepository.InserirProdutoOS(produtos, id);
            return Ok("Peça adicionada com sucesso!");
        }
        [HttpPut]
        public IActionResult AdicionarServico(int id, List<int> servicos)
        {
            _ordensServicoRepository.InserirServicoOS(servicos, id);
            return Ok("Serviço adicionado com sucesso!");
        }
    }
}

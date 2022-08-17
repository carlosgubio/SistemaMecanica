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
    public class ServicosController : Controller
    {
        public static readonly List<Servicos> servicos = new List<Servicos>();
        private readonly ServicosRepository _servicosRepository;

        public ServicosController()
        {
            _servicosRepository = new ServicosRepository();
        }
        [HttpPost]
        public IActionResult Cadastrar(CadastrarServicoViewModel salvarServicoViewModel)
        {
            if (salvarServicoViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarServicoViewModel.DescricaoServico == null)
                return Ok("Dados do Serviço não informados.");

            if (salvarServicoViewModel.ValorServico == 0)
                throw new ArgumentNullException($"campo {nameof(salvarServicoViewModel.ValorServico)} vazio ou nulo.");

            var resultado = _servicosRepository.SalvarServico(salvarServicoViewModel);

            if (resultado) return Ok("Serviço cadastrado com sucesso.");

            return Ok("Houve um problema ao salvar. Serviço não cadastrado.");
        }
        [HttpGet]
        public IActionResult ConsultaPorNome(string descricaoServico)
        {
            var resultado = _servicosRepository.BuscarServicos(descricaoServico);
            return Ok(resultado);
        }
        public IActionResult Atualizar(AtualizarServicoViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == null)
                return NoContent();

            var cEncontrada = servicos.FirstOrDefault(x => x.DescricaoServico == model.Encontrar.DescricaoServico);
            if (cEncontrada == null)
                return NotFound("Não há nenhum registro com esse nome.");

            cEncontrada.DescricaoServico = model.Atualizar.DescricaoServico;
            cEncontrada.ValorServico = model.Atualizar.ValorServico;

            return Ok(cEncontrada);
        }
        [HttpDelete]
        public IActionResult Remover(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return NoContent();

            var cliente = servicos.FirstOrDefault(x => x.DescricaoServico.Contains(nome));

            if (cliente == null)
                return NotFound();

            servicos.Remove(cliente);
            return Ok("Removido com sucesso!");
        }
    }
}

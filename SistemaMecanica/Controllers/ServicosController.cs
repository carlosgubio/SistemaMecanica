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
        public IActionResult Cadastrar([FromBody]CadastrarServicoViewModel cadastrarServicoViewModel)
        {
            if (cadastrarServicoViewModel == null)
                return Ok("Não foram informados dados");

            if (cadastrarServicoViewModel.DescricaoServico == null)
                return Ok("Dados do Serviço não informados.");

            if (cadastrarServicoViewModel.ValorServico == 0)
                throw new ArgumentNullException($"campo {nameof(cadastrarServicoViewModel.ValorServico)} vazio ou nulo.");

            var resultado = _servicosRepository.Salvar(cadastrarServicoViewModel);

            if (resultado) return Ok("Serviço cadastrado com sucesso!");

            return Ok("Houve um problema ao salvar. Serviço não cadastrado!");
        }
        
        [HttpGet]
        public IActionResult ConsultaNome(string nome)
        {
            var resultado = _servicosRepository.BuscarPorNome(nome);
            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var resultado = _servicosRepository.BuscarTodos();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult Confirmar(int id)
        {
            var resultado = _servicosRepository.Confirmar(id);
            return Ok(resultado);
        }
        
        [HttpPut]
        public IActionResult Atualizar(AtualizarServicoViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == 0)
                return NoContent();

           _servicosRepository.Atualizar(model.Atualizar, model.Encontrar);

            return Ok("Serviço Atualizado com sucesso!");
        }
        
        [HttpDelete]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return Ok("Ocorreu um erro!");

            _servicosRepository.Deletar(id);
            return Ok("Serviço Removido com sucesso!");
        }
    }
}

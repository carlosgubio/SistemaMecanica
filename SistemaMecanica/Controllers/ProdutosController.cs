using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using SistemaMecanica.Repositories;
using SistemaMecanica.ViewModels;
using SistemaMecanica.ViewModelsAtualizar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        public static readonly List<Produtos> produtos = new List<Produtos>();
        private readonly ProdutosRepository _produtosRepository;

        public ProdutosController()
        {
            _produtosRepository = new ProdutosRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]CadastrarProdutoViewModel cadastrarProdutoViewModel)
        {
            if (cadastrarProdutoViewModel == null)
                return Ok("Não foram informados dados");

            if (cadastrarProdutoViewModel.DescricaoPeca == null)
                return Ok("Dados do produto não informados.");

            if (cadastrarProdutoViewModel.ValorPeca == 0)
                throw new ArgumentNullException($"campo {nameof(cadastrarProdutoViewModel.ValorPeca)} vazio ou nulo.");

            var resultado = _produtosRepository.Salvar(cadastrarProdutoViewModel);

            if (resultado) return Ok("Produto cadastrado com sucesso!");

            return Ok("Houve um problema ao salvar. Produto não cadastrado!");
        }
        
        [HttpGet]
        public IActionResult ConsultaNome(string nome)
        {
            var resultado = _produtosRepository.BuscarPorNome(nome);
            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var resultado = _produtosRepository.BuscarTodos();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult Confirmar(int id)
        {
            var resultado = _produtosRepository.Confirmar(id);
            return Ok(resultado);
        }
        
        [HttpPut]
        public IActionResult Atualizar(AtualizarProdutoViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == 0)
                return NoContent();
            _produtosRepository.Atualizar(model.Atualizar, model.Encontrar);

            return Ok("Peça atualizada com sucesso!");
        }
        
        [HttpDelete]
        public IActionResult Remover(int id)
        {
            if (id == 0)
            return Ok("Ocorreu um erro!");

            _produtosRepository.Deletar(id);
            return Ok("Produto deletado com sucesso!");
        }
    }
}

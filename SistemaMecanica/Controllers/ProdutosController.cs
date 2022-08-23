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
        public IActionResult Cadastrar(CadastrarProdutoViewModel salvarProdutoViewModel)
        {
            if (salvarProdutoViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarProdutoViewModel.DescricaoPeca == null)
                return Ok("Dados do produto não informados.");

            if (salvarProdutoViewModel.ValorPeca == 0)
                throw new ArgumentNullException($"campo {nameof(salvarProdutoViewModel.ValorPeca)} vazio ou nulo.");

            var resultado = _produtosRepository.SalvarProduto(salvarProdutoViewModel);

            if (resultado) return Ok("Produto cadastrado com sucesso.");

            return Ok("Houve um problema ao salvar. Produto não cadastrado.");
        }
        [HttpGet]
        public IActionResult ConsultaPorNome(string descricaoProdutos)
        {
            var resultado = _produtosRepository.BuscarPorNomeProduto(descricaoProdutos);
            return Ok(resultado);
        }
        [HttpPut]
        public IActionResult AtualizarProduto(AtualizarProdutoViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == 0)
                return NoContent();
            _produtosRepository.Atualizar(model.Atualizar, model.Encontrar);

            return Ok();
        }
        [HttpDelete]
        public IActionResult Remover(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return NoContent();

            var produto = produtos.FirstOrDefault(x => x.DescricaoPeca.Contains(nome));

            if (produto == null)
                return NotFound();

            produtos.Remove(produto);
            return Ok("Removido com sucesso!");
        }
        [HttpGet]
        public IActionResult ConfirmarOProduto(int id)
        {
            var resultado = _produtosRepository.ConfirmarProduto(id);
            return Ok(resultado);
        }
    }
}

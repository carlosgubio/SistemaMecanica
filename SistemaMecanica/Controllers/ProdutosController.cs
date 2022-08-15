using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Dtos;
using SistemaMecanica.Repositories;
using SistemaMecanica.ViewModels;
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
        private readonly ProdutosRepository _produtosRepository;

        public ProdutosController()
        {
            _produtosRepository = new ProdutosRepository();
        }

        [HttpPost]
        public IActionResult SalvarProduto(CadastrarProdutoViewModel salvarProdutoViewModel)
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
        public IActionResult BuscarPorNomeProduto(string descricaoProdutos)
        {
            var resultado = _produtosRepository.BuscarPorNomeProduto(descricaoProdutos);
            return Ok(resultado);
        }
    }
}

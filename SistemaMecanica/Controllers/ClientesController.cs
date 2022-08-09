using Microsoft.AspNetCore.Http;
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
    public class ClientesController : ControllerBase
    {
        private readonly ClientesRepository _ClientesRepository;

        public ClientesController()
        {
            _ClientesRepository = new ClientesRepository();
        }

        [HttpPost]
        public IActionResult Salvar(SalvarClienteViewModel salvarPessoaViewModel)
        {
            if (salvarPessoaViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarPessoaViewModel.Clientes == null)
                return Ok("Dados da pessoa não informados.");

            if (salvarPessoaViewModel.Profissionais == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.Profissionais)} vazio ou nulo.");

            if (salvarPessoaViewModel.Servicos == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.Servicos)} vazio ou nulo.");

            if (salvarPessoaViewModel.Produtos == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.Produtos)} vazio ou nulo.");

            var resultado = _ClientesRepository.SalvarCliente(salvarPessoaViewModel.Clientes, salvarPessoaViewModel.Profissionais, salvarPessoaViewModel.Servicos, salvarPessoaViewModel.Produtos);

            if (resultado) return Ok("Pessoa cadastrada com sucesso.");

            return Ok("Houve um problema ao salvar. Pessoa não cadastrada.");
        }

        [HttpPost]
        public IActionResult BuscarPorNome(string nome)
        {
            var resultado = _ClientesRepository.BuscarPorNomeClientes(nome);
            return Ok(resultado);
        }
    }
}


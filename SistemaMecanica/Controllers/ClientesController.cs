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

            if (salvarPessoaViewModel.NomeCliente == null)
                return Ok("Dados da pessoa não informados.");

            if (salvarPessoaViewModel.CpfCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.CpfCliente)} vazio ou nulo.");

            if (salvarPessoaViewModel.TelefoneCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.TelefoneCliente)} vazio ou nulo.");

            if (salvarPessoaViewModel.EnderecoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.EnderecoCliente)} vazio ou nulo.");

            if (salvarPessoaViewModel.VeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.VeiculoCliente)} vazio ou nulo.");

            if (salvarPessoaViewModel.PlacaVeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.PlacaVeiculoCliente)} vazio ou nulo.");

            if (salvarPessoaViewModel.CorVeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.CorVeiculoCliente)} vazio ou nulo.");

            var resultado = _ClientesRepository.SalvarCliente(salvarPessoaViewModel);

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


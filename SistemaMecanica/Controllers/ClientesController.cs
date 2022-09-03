using Microsoft.AspNetCore.Http;
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
    public class ClientesController : ControllerBase
    {
        public static readonly List<Clientes> clientes = new List<Clientes>();
        private readonly ClientesRepository _clientesRepository;
        

        public ClientesController()
        {
            _clientesRepository = new ClientesRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarClienteViewModel cadastrarClienteViewModel)
        {
            if (cadastrarClienteViewModel == null)
                return Ok("Não foram informados dados");

            if (cadastrarClienteViewModel.NomeCliente == null)
                return Ok("Dados do Cliente não informados.");

            if (cadastrarClienteViewModel.CpfCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.CpfCliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.TelefoneCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.TelefoneCliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.EnderecoCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.EnderecoCliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.VeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.VeiculoCliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.PlacaVeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.PlacaVeiculoCliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.CorVeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.CorVeiculoCliente)} vazio ou nulo.");

            var resultado = _clientesRepository.Salvar(cadastrarClienteViewModel);

            if (resultado) return Ok("Cliente cadastrado com sucesso!");

            return Ok("Houve um problema ao salvar. Cliente não cadastrado!");
        }
        
        [HttpGet]
        public IActionResult ConsultaNome(string nome )
        {
            var resultado = _clientesRepository.BuscarPorNome(nome);
            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var resultado = _clientesRepository.BuscarTodos();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult Confirmar(int id)
        {
            var resultado = _clientesRepository.Confirmar(id);
            return Ok(resultado);
        }
        
        [HttpPut]
        public IActionResult Atualizar(AtualizarClienteViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == 0)
                return NoContent();
            _clientesRepository.Atualizar(model.Atualizar, model.Encontrar);

            return Ok("Cliente atualizado com sucesso!");
        }
        
        [HttpDelete]
        public IActionResult Remover(int id) 
        {
            if (id == 0)
            return Ok ("Ocorreu um erro!");

            _clientesRepository.Deletar(id);
            return Ok ("Cliente deletado com sucesso!");
        }
    }
}


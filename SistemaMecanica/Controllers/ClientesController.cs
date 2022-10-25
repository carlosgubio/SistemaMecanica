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
        private readonly VeiculosRepository _veiculosRepository;
        

        public ClientesController()
        {
            _clientesRepository = new ClientesRepository();
            _veiculosRepository = new VeiculosRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarClienteViewModel cadastrarClienteViewModel)
        {
            if (cadastrarClienteViewModel == null)
                return Ok("Não foram informados dados");

            if(cadastrarClienteViewModel.Cliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.Cliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.Cliente.NomeCliente == null)
                return Ok("Dados do Cliente não informados.");

            if (cadastrarClienteViewModel.Cliente.CpfCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.Cliente.CpfCliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.Cliente.TelefoneCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.Cliente.TelefoneCliente)} vazio ou nulo.");

            if (cadastrarClienteViewModel.Cliente.EnderecoCliente == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarClienteViewModel.Cliente.EnderecoCliente)} vazio ou nulo.");

            var resultado = _clientesRepository.Salvar(cadastrarClienteViewModel);

            if (resultado > 0) return Ok(resultado);

            return Ok("Houve um problema ao salvar. Cliente não cadastrado!");
        }
        
        [HttpGet]
        public IActionResult ConsultarNome(string nome )
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
            resultado.VeiculosDto = _veiculosRepository.BuscarDoCliente(id);
            return Ok(resultado);
        }
        
        [HttpPut]
        public IActionResult Atualizar(AtualizarClienteViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            _clientesRepository.Atualizar(model.Atualizar);

            return Ok("Cliente Atualizado com Sucesso!");
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


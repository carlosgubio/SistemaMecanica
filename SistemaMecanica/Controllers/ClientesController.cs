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
        public IActionResult Cadastrar(CadastrarClienteViewModel salvarClienteViewModel)
        {
            if (salvarClienteViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarClienteViewModel.NomeCliente == null)
                return Ok("Dados do Cliente não informados.");

            if (salvarClienteViewModel.CpfCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarClienteViewModel.CpfCliente)} vazio ou nulo.");

            if (salvarClienteViewModel.TelefoneCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarClienteViewModel.TelefoneCliente)} vazio ou nulo.");

            if (salvarClienteViewModel.EnderecoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarClienteViewModel.EnderecoCliente)} vazio ou nulo.");

            if (salvarClienteViewModel.VeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarClienteViewModel.VeiculoCliente)} vazio ou nulo.");

            if (salvarClienteViewModel.PlacaVeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarClienteViewModel.PlacaVeiculoCliente)} vazio ou nulo.");

            if (salvarClienteViewModel.CorVeiculoCliente == null)
                throw new ArgumentNullException($"campo {nameof(salvarClienteViewModel.CorVeiculoCliente)} vazio ou nulo.");

            var resultado = _clientesRepository.SalvarCliente(salvarClienteViewModel);

            if (resultado) return Ok("Pessoa cadastrada com sucesso.");

            return Ok("Houve um problema ao salvar. Pessoa não cadastrada.");
        }

        [HttpGet]
        public IActionResult ConsultaPorNome(string nomeCliente)
        {
            var resultado = _clientesRepository.BuscarPorNomeCliente(nomeCliente);
            return Ok(resultado);
        }
        [HttpPut]
        public IActionResult Atualizar(AtualizarClienteViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == null)
                return NoContent();

            var cEncontrada = clientes.FirstOrDefault(x=> x.NomeCliente == model.Encontrar.NomeCliente);
            if (cEncontrada == null)
                return NotFound("Não há nenhum registro com esse nome.");

            cEncontrada.NomeCliente = model.Atualizar.NomeCliente;
            cEncontrada.CpfCliente = model.Atualizar.CpfCliente;
            cEncontrada.TelefoneCliente = model.Atualizar.TelefoneCliente;
            cEncontrada.EnderecoCliente = model.Atualizar.EnderecoCliente;
            cEncontrada.VeiculoCliente = model.Atualizar.VeiculoCliente;
            cEncontrada.PlacaVeiculoCliente = model.Atualizar.PlacaVeiculoCliente;
            cEncontrada.CorVeiculoCliente = model.Atualizar.CorVeiculoCliente;
            
            return Ok(cEncontrada);
        }
        [HttpDelete]
        public IActionResult Remover(string nome) 
        {
            if (string.IsNullOrEmpty(nome))
                return NoContent();

            var cliente = clientes.FirstOrDefault(x=> x.NomeCliente.Contains(nome));

            if (cliente == null)
                return NotFound();

            clientes.Remove(cliente);
            return Ok("Removido com sucesso!");
        }
    }
}


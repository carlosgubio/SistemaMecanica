using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Models;
using SistemaMecanica.Repositories;
using SistemaMecanica.ViewModelsAtualizar;
using SistemaMecanica.ViewModelsCadastrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VeiculosController: Controller
    {
        public static readonly List<Veiculos> veiculos = new List<Veiculos>();
        private readonly VeiculosRepository _veiculosRepository;


        public VeiculosController()
        {
        _veiculosRepository = new VeiculosRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarVeiculoViewModel cadastrarVeiculoViewModel)
        {
            if (cadastrarVeiculoViewModel == null)
                return Ok("Não foram informados dados");

            if(cadastrarVeiculoViewModel.IdCliente <= 0)
                throw new ArgumentNullException($"campo {nameof(cadastrarVeiculoViewModel.IdCliente)} vazio ou nulo.");
            if(cadastrarVeiculoViewModel.Veiculos == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarVeiculoViewModel.Veiculos)} vazio ou nulo.");

            var resultado = _veiculosRepository.Salvar(cadastrarVeiculoViewModel.Veiculos, cadastrarVeiculoViewModel.IdCliente);

            if (resultado) return Ok("Veículo cadastrado com sucesso!");

            return Ok("Houve um problema ao salvar. Veículo não cadastrado!");
        }

        [HttpGet]
        public IActionResult ConsultaNome(string nome)
        {
            var resultado = _veiculosRepository.BuscarPorNome(nome);
            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var resultado = _veiculosRepository.BuscarTodos();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult Confirmar(int id)
        {
            var resultado = _veiculosRepository.Confirmar(id);
            return Ok(resultado);
        }

        [HttpPut]
        public IActionResult Atualizar(AtualizarVeiculoViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();                            
            _veiculosRepository.Atualizar(model.Atualizar);

            return Ok("Veículo atualizado com sucesso!");
        }

        [HttpDelete]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return Ok("Ocorreu um erro!");

        _veiculosRepository.Deletar(id);
            return Ok("Veículo deletado com sucesso!");
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
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
    public class ProfissionaisController : Controller
    {
        public static readonly List<Profissionais> profissionais = new List<Profissionais>();
        private readonly ProfissionaisRepository _profissionaisRepository;

        public ProfissionaisController()
        {
            _profissionaisRepository = new ProfissionaisRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]CadastrarProfissionalViewModel cadastrarProfissionalViewModel)
        {
            if (cadastrarProfissionalViewModel == null)
                return Ok("Não foram informados dados");

            if (cadastrarProfissionalViewModel.NomeProfissional == null)
                return Ok("Dados do Profissional não informados.");

            if (cadastrarProfissionalViewModel.CargoProfissional == null)
                throw new ArgumentNullException($"campo {nameof(cadastrarProfissionalViewModel.CargoProfissional)} vazio ou nulo.");

            var resultado = _profissionaisRepository.Salvar(cadastrarProfissionalViewModel);

            if (resultado) return Ok("Profissional cadastrado com sucesso!");

            return Ok("Houve um problema ao salvar. Profissional não cadastrado!");
        }
        
        [HttpGet]
        public IActionResult ConsultaNome(string nome)
        {
        var resultado = _profissionaisRepository.BuscarPorNome(nome);
            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var resultado = _profissionaisRepository.BuscarTodos();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult Confirmar(int id)
        {
            var resultado = _profissionaisRepository.Confirmar(id);
            return Ok(resultado);
        }
        
        [HttpPut]
        public IActionResult Atualizar(AtualizarProfisionalViewModel model)
        {
            if (model == null)
                return NoContent();
            _profissionaisRepository.Atualizar(model.Atualizar);

            return Ok("Profissional Atualizado com sucesso!");
        }
        
        [HttpDelete]
        public IActionResult Remover(int id)
        {
            if (id == 0)
                return Ok("Ocorreu um erro!");

            _profissionaisRepository.Deletar(id);
            return Ok("Profissional deletado com sucesso!");
        }
       
    }
}

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
    public class ServicosController : Controller
    {
        private readonly ServicosRepository _servicosRepository;

        public ServicosController()
        {
            _servicosRepository = new ServicosRepository();
        }
        [HttpPost]
        public IActionResult SalvarServico(CadastrarServicoViewModel salvarServicoViewModel)
        {
            if (salvarServicoViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarServicoViewModel.DescricaoServico == null)
                return Ok("Dados do Serviço não informados.");

            if (salvarServicoViewModel.ValorServico == 0)
                throw new ArgumentNullException($"campo {nameof(salvarServicoViewModel.ValorServico)} vazio ou nulo.");

            var resultado = _servicosRepository.SalvarServico(salvarServicoViewModel);

            if (resultado) return Ok("Serviço cadastrado com sucesso.");

            return Ok("Houve um problema ao salvar. Serviço não cadastrado.");
        }
        [HttpGet]
        public IActionResult BuscarServicos(string descricaoServico)
        {
            var resultado = _servicosRepository.BuscarServicos(descricaoServico);
            return Ok(resultado);
        }
    }
}

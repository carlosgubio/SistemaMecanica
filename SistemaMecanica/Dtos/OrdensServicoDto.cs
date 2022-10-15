using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Dtos
{
    public class OrdensServicoDto
    {
        public int IdOrdemServico { get; set; }
        public int IdProfissional { get; set; }
        public int IdCliente { get; set; }
        public int IdServico { get; set; }
        public int IdProduto { get; set; }
        public int IdVeiculo { get; set; }
        public float TotalGeral { get; set; }
        public List<ProdutosDto> Itens { get;set; }
        public List<ProfissionaisDto> Execucoes { get; set; }
        public List<ServicosDto> ServicosExecutados { get; set; }

    }
    public class OrdensServicoListagemDto
    {
        public int IdOrdemServico { get; set; }
        public string NomeCliente { get; set; }
        public string VeiculoCliente { get; set; }
        public string NomeProfissional { get; set; }
        public string DescricaoServico { get; set; }
        public string DescricaoPeca { get; set; }
        public string TotalGeral { get; set; }
       
    }
}

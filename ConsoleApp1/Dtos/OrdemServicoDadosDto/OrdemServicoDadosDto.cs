using SistemaMecanica.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Dtos.OrdemServicoDadosDto
{
    public class OrdemServicoDadosDto
    {
        public int IdOrdemServico { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public string VeiculoCliente { get; set; }
        public string PlacaVeiculoCliente { get; set; }
        public string CorVeiculoCliente { get; set; }
        public string NomeProfissional { get; set; }
        public string DescricaoServico { get; set; }
        public string ValorServico { get; set; }
        public string descricaoPeca { get; set; }
        public string ValorPeca { get; set; }
        public string TotalGeral { get; set; }
        public List<ProdutosDto> Itens { get; set; }
        public List<ProfissionaisDto> Execucoes { get; set; }
        public List<ServicosDto> ServicosExecutados { get; set; }
    }
}

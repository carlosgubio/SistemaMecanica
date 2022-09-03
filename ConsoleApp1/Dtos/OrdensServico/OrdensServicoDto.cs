using Client.Dtos.Produtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Dtos.OrdensServico
{
    public class OrdensServicoDto
    {
        public int IdOrdemServico { get; set; }
        public int IdProfissional { get; set; }
        public int IdCliente { get; set; }
        public int IdServico { get; set; }
        public int IdProduto { get; set; }
        public float TotalGeral { get; set; }
        public List<ProdutosDto> Itens { get; set; }
    }
}

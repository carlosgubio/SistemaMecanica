using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Models.OrdensServico
{
    public class OrdensServico
    {
        public int IdProfissional { get; set; }
        public int IdCliente { get; set; }
        public int IdServico { get; set; }
        public int IdProduto { get; set; }
        public int IdVeiculo { get; set; }
        public float TotalGeral { get; set; }
        public List<int> IdItens { get; set; }
        
    }
}

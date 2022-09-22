using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Dtos.Veiculos
{
    class Veiculos
    {
        public int IdVeiculo { get; set; }
        public string VeiculoCliente { get; set; }
        public string PlacaVeiculoCliente { get; set; }
        public string CorVeiculoCliente { get; set; }
        public int IdCliente { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.ViewModelsDeletar
{
    public class DeletarVeiculoViewModel
    {
        public int IdVeiculo { get; set; }
        public string VeiculoCliente { get; set; }
        public string PlacaVeiculoCliente { get; set; }
        public string CorVeiculoCliente { get; set; }
        public int IdCliente { get; set; }
    }
}

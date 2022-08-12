﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Dtos
{
    public class ClientesDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public string VeiculoCliente { get; set; }
        public string PlacaVeiculoCliente { get; set; }
        public string CorVeiculoCliente { get; set; }
        public ProfissionaisDto Profissionais { get; set; }
        public ProdutosDto Produtos { get; set; }
        public ServicosDto Servicos { get; set; }
        public OrdemServicoDto OrdemServico { get; set; }

    }
}
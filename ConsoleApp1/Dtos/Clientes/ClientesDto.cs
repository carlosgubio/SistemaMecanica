using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Dtos.Clientes
{
    public class ClientesDto
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EnderecoCliente { get; set; }

    }
}

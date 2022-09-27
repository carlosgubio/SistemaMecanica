using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Dtos
{
    public class ProdutosDto
    {
        public int IdProduto { get; set; }
        public string DescricaoPeca { get; set; }
        public float ValorPeca { get; set; }
        public List<ProdutosDto> Itens { get; set; }
    }
}

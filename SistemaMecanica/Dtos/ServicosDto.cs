using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Dtos
{
    public class ServicosDto
    {
        public int IdServico { get; set; }
        public string DescricaoServico { get; set; }
        public float ValorServico { get; set; }
        public List<ServicosDto> ServicosExecutados { get; set; }
    }
}

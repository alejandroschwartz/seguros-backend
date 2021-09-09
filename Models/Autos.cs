using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SegurosApi.Models
{
    public class Autos
    {
        public long Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Precio { get; set; }
    }
}
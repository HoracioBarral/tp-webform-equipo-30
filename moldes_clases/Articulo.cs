using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [DisplayName("Marca")]
        public Marca NombreMarca { get; set; }

        [DisplayName("Categoria")]
        public Categoria TipoCategoria { set; get; }

        public List<Imagen> UrlImagen { set; get; }

        public decimal Precio { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> listadoMarcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            datos.SetConsulta("select id,Descripcion from marcas");

            try
            {
                datos.Consulta_A_DB();
                while (datos.Lector.Read())
                {
                   Marca marca = new Marca();
                   marca.Id = (int)datos.Lector["id"];
                   marca.Descripcion = (string)datos.Lector["Descripcion"];
                   listadoMarcas.Add(marca);
                }
                return listadoMarcas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}

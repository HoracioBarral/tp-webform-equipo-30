using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPWeb_Carrrito
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> listadoArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articulos = new ArticuloNegocio();
            listadoArticulos = articulos.listar();
            if (!IsPostBack)
            {
                repeaterProductos.DataSource = articulos.listar();
                repeaterProductos.DataBind();
            }

        }

        protected void VerDetalles_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            Articulo seleccionado = listadoArticulos.Find(x => x.Id == id);
            Response.Redirect("Detalles.aspx",false);
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            Articulo agregado = listadoArticulos.Find(x => x.Id == id);
            Session.Add("agregado", agregado);
        }
    }
}
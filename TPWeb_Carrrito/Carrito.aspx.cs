using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPWeb_Carrrito
{
    public partial class Carrito : System.Web.UI.Page
    {
        public Articulo agregado = new Articulo();
        public List<Articulo> artAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) artAgregados = new List<Articulo>();
            cargarCarrito();
            repeaterCarrito.DataSource = artAgregados;
            repeaterCarrito.DataBind();
        }

        private void cargarCarrito()
        {
            agregado = (Articulo)Session["agregado"];
            artAgregados.Add(agregado);
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using dominio;
using negocio;

namespace TPWeb_Carrrito
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> listadoArticulos;
        public List<Articulo> artAgregados;
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articulos = new ArticuloNegocio();
            listadoArticulos = articulos.listar();
            if (!IsPostBack)
            {
                artAgregados = new List<Articulo>();
                Session.Add("artAgregados", artAgregados);
                repeaterProductos.DataSource = listadoArticulos;
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
            Articulo agregado = new Articulo();
            agregado = listadoArticulos.Find(x => x.Id == id);
            artAgregados = (List<Articulo>)Page.Session["artAgregados"];
            Page.Session.Remove("artAgregados");
            artAgregados.Add(agregado);
            Page.Session.Add("artAgregados", artAgregados);
        }
    }
}
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
        public List<Articulo> artAgregados { get; set; }
        public List<Articulo> listadoArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            artAgregados = (List<Articulo>)Session["artAgregados"];
            if (!IsPostBack)
            {
                repeaterCarrito.DataSource = artAgregados;
                repeaterCarrito.DataBind();
            }
            /*if (!IsPostBack)
            {
                artAgregados = new List<Articulo>();
                artAgregados = (List<Articulo>)Session["artAgregados"];
                Session.Add("listadoArticulos", artAgregados);
                repeaterCarrito.DataSource = artAgregados;
                repeaterCarrito.DataBind();
            }*/
        }


        protected void Eliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            listadoArticulos = (List<Articulo>)Session["artAgregados"];
            Articulo artEliminado = listadoArticulos.Find(x => x.Id == id);
            //listadoArticulos.Remove(artEliminado);
            artAgregados.Remove(artEliminado);
            repeaterCarrito.DataSource = artAgregados;
            repeaterCarrito.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }
    }
}
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
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Articulo> carrito;
            if (Session["artAgregados"] == null)
            {
                carrito = new List<Articulo>();
                Session.Add("carrito", carrito);
                return;
            }
            carrito = (List<Articulo>)Session["artAgregados"];
            Session.Add("carrito", carrito);
            if (!IsPostBack)
            {
                repeaterCarrito.DataSource = Session["artAgregados"];
                repeaterCarrito.DataBind();
            }
        }


        protected void Eliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            List<Articulo> listadoArticulos = (List<Articulo>)Session["artAgregados"];
            Articulo artEliminado = listadoArticulos.Find(x => x.Id == id);
            listadoArticulos.Remove(artEliminado);
            repeaterCarrito.DataSource = Session["artAgregados"];
            repeaterCarrito.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }
    }
}
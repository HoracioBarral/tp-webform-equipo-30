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
            calcularImporte();
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
            calcularImporte();
            repeaterCarrito.DataSource = Session["artAgregados"];
            repeaterCarrito.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }

        private void calcularImporte()
        {
            decimal importeTotal=0;
            int cantidadProductos=0;

            if (Session["artAgregados"] != null)
            {
                List<Articulo> importes = (List<Articulo>)Session["artAgregados"];
                foreach (Articulo art in importes)
                {
                    importeTotal += art.Precio;
                    cantidadProductos++;
                }
            }
            lblImporte.Text = importeTotal.ToString();
            lblCantidadProductos.Text = "Cantidad de Productos: " + cantidadProductos.ToString();
        }
    }
}
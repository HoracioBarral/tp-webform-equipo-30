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
        List<Articulo> listadoArticulos;
        protected void Page_Load(object sender, EventArgs e)
        {
            cantidadProductos();
            if (!IsPostBack)
             {
                ArticuloNegocio articulos = new ArticuloNegocio();
                Session.Add("listadoArticulos", articulos.listar());
                repeaterProductos.DataSource = articulos.listar();
                repeaterProductos.DataBind();
             }
        }

        protected void VerDetalles_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            listadoArticulos = (List<Articulo>)Session["listadoArticulos"];
            Articulo seleccionado = listadoArticulos.Find(x => x.Id == id);
            Session.Add("Seleccionado", seleccionado);
            Response.Redirect("Detalles.aspx",false);
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            List<Articulo> artAgregados;
            if (Session["artAgregados"] == null)
            {
                artAgregados= new List<Articulo>();
                Session.Add("artAgregados", artAgregados);
            }
            int id = int.Parse(((Button)sender).CommandArgument);
            Articulo agregado = new Articulo();
            listadoArticulos = (List<Articulo>)Session["listadoArticulos"];
            agregado = listadoArticulos.Find(x => x.Id == id);
            ((List<Articulo>)Session["artAgregados"]).Add(agregado);
            cantidadProductos();
        }

        protected void btncarrito_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Carrito.aspx",false);
        }

        private void cantidadProductos()
        {
            int cant=0;
            if (Session["artAgregados"] != null)
            {
                cant = ((List<Articulo>)Session["artAgregados"]).Count;
            }
            TxtBoxCantidad.Text = cant.ToString();
        }

        protected void BtnFiltro_Click(object sender, EventArgs e)
        {
            //string filtro = TxtFiltro.Text.Trim().ToUpper();
            //List<Articulo> listadoActual = (List<Articulo>)Session["listadoArticulos"];

            //List<Articulo> productosFiltrados = new List<Articulo>();

            //foreach (var articulo in listadoActual)
            //{
            //    if (articulo.Descripcion.ToUpper().Contains(filtro))
            //    {
            //        productosFiltrados.Add(articulo);
            //    }
            //}
            //repeaterProductos.DataSource = productosFiltrados;
            //repeaterProductos.DataBind();
        }
    }
}
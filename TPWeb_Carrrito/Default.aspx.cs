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
        public List<Articulo> listadoArticulos { get; set; }
        public List<Articulo> artAgregados { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
             {
                 ArticuloNegocio articulos = new ArticuloNegocio();
                 Session.Add("listadoArticulos", articulos.listar());
                 repeaterProductos.DataSource = articulos.listar();
                 repeaterProductos.DataBind();
             }
            if (Session["artAgregados"] == null)
            {
                artAgregados = new List<Articulo>();
                Session.Add("artAgregados", artAgregados);
            }
        }

        protected void VerDetalles_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            listadoArticulos = (List<Articulo>)Session["listadoArticulos"];
            Articulo seleccionado = listadoArticulos.Find(x => x.Id == id);
            if (Session["Seleccionado"] != null)
            {
                Session.Remove("Seleccionado");
                Session.Add("Seleccionado", seleccionado);
            }
            else
            {
                Session.Add("Seleccionado", seleccionado);
            }
            Response.Redirect("Detalles.aspx",false);
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            Articulo agregado = new Articulo();
            listadoArticulos = (List<Articulo>)Session["listadoArticulos"];
            agregado = listadoArticulos.Find(x => x.Id == id);
            if(artAgregados == null)
            {
                //artAgregados = new List<Articulo>();
            }
            /*if (Session["artAgregados"] != null) {
                artAgregados = (List<Articulo>)Session["artAgregados"];
                Session.Remove("artAgregados");
            }*/
            List<Articulo> lista = (List<Articulo>)Session["artAgregados"];
            lista.Add(agregado);
            //artAgregados.Add(agregado);
            //Session.Add("artAgregados", artAgregados);
        }

        protected void btncarrito_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Carrito.aspx",false);
        }
    }
}
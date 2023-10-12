using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPWeb_Carrrito
{
    public partial class Detalles : System.Web.UI.Page
    {
        public Articulo artDetalles { get;set;}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Seleccionado"] == null) return;
            Articulo artDetalles = new Articulo();
            artDetalles = (Articulo)Session["Seleccionado"];
            repeaterImagenes.DataSource = artDetalles.UrlImagen;
            repeaterImagenes.DataBind();
        }
    }
}
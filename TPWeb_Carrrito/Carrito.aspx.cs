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
        public Articulo agregado;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Articulo agregado = new Articulo();
            agregado=(Articulo)Session["agregado"];

        }
    }
}
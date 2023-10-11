<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPWeb_Carrrito.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repeaterCarrito" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                       <img src="<%#Eval("UrlImagen[0].Url") %>" onerror="this.src='Imagen/error.png';" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <asp:Button Text="Eliminar" runat="server" ID="Eliminar" CommandArgument='<%#Eval("Id") %>' CommandName="Eliminar" OnClick="Eliminar_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

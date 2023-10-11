<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPWeb_Carrrito.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repeaterProductos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                       <img src="<%#Eval("UrlImagen[0].Url") %>" onerror="this.src='Imagen/error.png';" class="card-img-top" alt="...">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <asp:Button Text="Detalles" runat="server" ID="VerDetalles" CommandArgument='<%#Eval("Id") %>' CommandName="IdArticulo" OnClick="VerDetalles_Click" />
                            <asp:Button Text="Agregar" runat="server" ID="Agregar" CommandArgument='<%#Eval("Id") %>' CommandName="Agregar" OnClick="Agregar_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

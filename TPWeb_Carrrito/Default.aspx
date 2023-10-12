<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPWeb_Carrrito.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="car bg-dark text-white">
        <div class="row">
            <div class="col" style="text-align: center; font: bold 20px verdana;">
                <dl>
                    <dt class="col-sm-6">Carrito de Compras</dt>
                </dl>
            </div>
          

            <div class="col" style="text-align: right; position: center">
                <asp:Label ID="LblVacio" runat="server" Text=""></asp:Label>
                <asp:ImageButton ID="btncarrito" Text="Carrito" runat="server" Onclick="btncarrito_Click" CssClass="btn btn-danger" BorderColor="Black" ImageUrl="https://cdn-icons-png.flaticon.com/512/107/107831.png?w=740&t=st=1665782017~exp=1665782617~hmac=95808e2329e630a6ba9074a08d0e67d284da4975037a7d5e51dd48611f5c47fa" Width="45" Height="35" />
                <asp:TextBox ID="TxtBoxCantidad" runat="server" Text="" ReadOnly="true" Width="40" Height="30"></asp:TextBox>
            </div>
        </div>
    </div>



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

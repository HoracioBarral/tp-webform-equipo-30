<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Detalles.aspx.cs" Inherits="TPWeb_Carrrito.Detalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="car bg-dark text-white">
        <div class="row">
            <div class="col" style="text-align: center; font: bold 20px verdana;">
                <dl>
                    <dt class="col-sm-6">Detalle de Producto</dt>
                </dl>
                <div class="col" style="text-align: right; position: center">
                    <asp:Button ID="Button1" Text="Volver" runat="server" OnClick="btnVolver_Click" CssClass="btn btn-danger" BorderColor="Black" Width="160" Height="40" />
                </div>
            </div>
        </div>
    </div>
    <div id="divDescripcion">
        <asp:TextBox runat="server" ID="txtDescripcion" />
    </div>
    <div id="divMarca">
        <asp:TextBox runat="server" ID="txtMarca" />
    </div>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repeaterImagenes" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%#Eval("Url") %>" onerror="this.src='Imagen/error.png';" class="card-img-top" alt="...">
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

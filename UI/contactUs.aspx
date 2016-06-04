<%@ Page Title="Contáctenos | DEAS" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="contactUs.aspx.cs" Inherits="UI.contactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Cont&aacute;ctenos</p>
            <p class="subtitle">Dejenos su mensaje</p>
            <section class="form row">
                <section class="col-md-7 form-horizontal">
                    <section class="form-group">
                        <asp:Label ID="lblName" CssClass="control-label col-sm-4" Text="Nombre completo:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtName" CssClass="form-control col-sm-8" runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Label ID="lblEmail" CssClass="control-label col-sm-4" Text="Correo Electrónico:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtEmail" CssClass="form-control col-sm-8" runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Label ID="lblPhone" CssClass="control-label col-sm-4" Text="Teléfono:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtPhone" CssClass="form-control col-sm-8" runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Label ID="lblMessage" CssClass="control-label col-sm-4" Text="Mensaje:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtMessage" CssClass="form-control col-sm-8" TextMode="MultiLine" Rows="5" Columns="10"  runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Button runat="server" Text="Enviar" CssClass="pull-right form-button" ToolTip="Enviar el mensaje" />
                    </section>
                </section>
            </section>
            <p class="subtitle">Números de contacto</p>
        </section>
    </section>
</asp:Content>

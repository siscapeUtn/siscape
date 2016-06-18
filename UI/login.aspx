<%@ Page Title="Ingresar al Sistema | DEAS" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="UI.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <section class="login_container">
            <section class="login_header">
                <p>Ingresar al sistema</p>
            </section>
            <section class="login_content">
                <section class="row">
                    <section class="col-md-12">
                        <section class="form-group">
                            <asp:TextBox ID="txtUser" placeholder="correo electrónico" runat="server"></asp:TextBox>
                            <span class="help-block"></span>
                        </section>
                        <section class="form-group">
                            <asp:TextBox ID="txtPassword" placeholder="contraseña" runat="server" TextMode="Password" ></asp:TextBox>
                            <span class="help-block"></span>
                        </section>
                        <section>
                            <asp:DropDownList ID="cboPeriod" runat="server">
                                <asp:ListItem Value="0">Seleccione el periodo</asp:ListItem>
                            </asp:DropDownList>
                            <span class="help-block"></span>
                        </section>
                    </section>
                    <section class="col-md-8">
                        <a id="forgotPassword" ><asp:Label Text="¿Has olvidado tu contraseña?" runat="server" ></asp:Label></a>
                    </section>
                    <section class="col-md-4">
                        <section class="form-group">
                            <asp:Button CssClass="pull-right" Text="Ingresar" runat="server" OnClick="login_Click" />
                        </section>
                    </section>
                </section>
            </section>
        </section>
    </section>
</asp:Content>

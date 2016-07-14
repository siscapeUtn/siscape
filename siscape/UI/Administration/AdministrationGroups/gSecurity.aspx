<%@ Page Title="Seguridad - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="gSecurity.aspx.cs" Inherits="UI.Administration.gSecurity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p class="title">Seguridad</p>
    <section class="row">
        <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
            <section class="accesses">
                <a href="../Security/Role.aspx" ><img alt="Programa" src="../../images/security/user_rol.svg" />
                <p>Roles</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
            <section class="accesses">
                <a href="../Security/User.aspx" ><img alt="Programa" src="../../images/security/users.svg" />
                <p>Usuarios</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
            <section class="accesses">
                <a href="administration.aspx" ><img alt="Regresar" src="../../images/page-icons/return.svg" />
                <p>Regresar</p></a>
            </section>
        </section>
    </section>
</asp:Content>

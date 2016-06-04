<%@ Page Title="Seguridad - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="gSecurity.aspx.cs" Inherits="UI.Administration.gSecurity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Security/Role.aspx" ><img alt="Programa" src="../../images/administration/program_2.png" />
            <p>Roles</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Security/User.aspx" ><img alt="Programa" src="../../images/administration/program_2.png" />
            <p>Usuarios</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="administration.aspx" ><img alt="Regresar" src="../../images/page-icons/return.png" />
            <p>Regresar</p></a>
        </section>
    </section>
</asp:Content>

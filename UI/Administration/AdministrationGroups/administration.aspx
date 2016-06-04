<%@ Page Title="Administración - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="administration.aspx.cs" Inherits="UI.Administration.administration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="gProgram.aspx" ><img alt="Programa" src="../../images/administration/program.png" />
            <p>Programa</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="gPeriod.aspx" ><img alt="Periodo" src="../../images/administration/period.png" />
            <p>Periodo</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="gSecurity.aspx" ><img alt="Seguridad" src="../../images/administration/security.png" />
            <p>Seguridad</p></a>
        </section>
    </section>
</asp:Content>

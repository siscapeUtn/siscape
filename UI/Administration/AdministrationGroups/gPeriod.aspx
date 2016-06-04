<%@ Page Title="Periodo - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="gPeriod.aspx.cs" Inherits="UI.Administration.gPeriod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Period/periodType.aspx" ><img alt="Tipo de Periodo" src="../../images/administration/period_type.png" />
            <p>Tipo de periodo</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Period/period.aspx" ><img alt="Periodo" src="../../images/administration/period_2.png" />
            <p>Periodo</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="administration.aspx" ><img alt="Regresar" src="../../images/page-icons/return.png" />
            <p>Regresar</p></a>
        </section>
    </section>
</asp:Content>

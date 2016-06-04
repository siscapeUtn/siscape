<%@ Page Title="Infraestructura - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="gBuilding.aspx.cs" Inherits="UI.Academic.gBuilding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Building/headquarters.aspx"><img alt="Administración de Sedes" src="../../images/academic/headquarters.png" />
            <p>Sede</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Building/location.aspx"><img alt="Administración de Localizaciones" src="../../images/academic/localizacion.png" />
            <p>Localización</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Building/classRoomType.aspx"><img alt="Administración de Tipos de Aulas" src="../../images/academic/typeClassRoom.png" />
            <p>Tipo de Aula</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../Building/classRoom.aspx"><img alt="Administración de Aulas" src="../../images/academic/classRoom.png" />
            <p>Aula</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../AcademicGroups/academic.aspx"><img alt="Regresar" src="../../images/page-icons/return.png" />
            <p>Regresar</p></a>
        </section>
    </section>
</asp:Content>

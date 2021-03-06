﻿<%@ Page Title="Infraestructura - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="gBuilding.aspx.cs" Inherits="UI.Academic.gBuilding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
        <p class="title">Infraestructura</p>    
        <section class="row">
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Building/headquarters.aspx"><img alt="Administración de Sedes" src="../../images/academic/headquarters.svg" />
                    <p>Sede</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Building/location.aspx"><img alt="Administración de Localizaciones" src="../../images/academic/localizacion.svg" />
                    <p>Localización</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Building/classRoomType.aspx"><img alt="Administración de Tipos de Aulas" src="../../images/academic/typeClassRoom.svg" />
                    <p>Tipo de Aula</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Building/classRoom.aspx"><img alt="Administración de Aulas" src="../../images/academic/classRoom.svg" />
                    <p>Aula</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Building/ActivesStatus.aspx"><img alt="Administración de Status" src="../../images/academic/statusActive.svg" />
                    <p>Estado de Activos </p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Building/Actives.aspx"><img alt="Administración de Activos" src="../../images/academic/Actives.svg" />
                    <p>Activos</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../AcademicGroups/academic.aspx"><img alt="Regresar" src="../../images/page-icons/return.svg" />
                    <p>Regresar</p></a>
                </section>
            </section>
           <%--<section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../../index.aspx"><img alt="Inicio" src="../../images/page-icons/home.svg" />
                    <p>Inicio</p></a>
                </section>
            </section>--%>
        </section>
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#academic').addClass('isSelected');
    </script>
</asp:Content>

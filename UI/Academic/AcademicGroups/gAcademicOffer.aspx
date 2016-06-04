﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="gAcademicOffer.aspx.cs" Inherits="UI.Academic.AcademicGroups.gAcademicOffer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../AcademicOffer/schedule.aspx"><img alt="Nombramineto Interno" src="../../images/academic/schedule.png" />
            <p>Horarios</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../AcademicOffer/Course.aspx"><img alt="Administración de Profesores" src="../../images/academic/course.png" />
            <p>Cursos</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../AcademicOffer/AcademicOffer.aspx"><img alt="Administración de Funcionario" src="../../images/academic/academicOffer.png" />
            <p>Oferta académica</p></a>
        </section>
        <section class="accesses col-md-4 col-sm-4 col-xs-4">
            <a href="../AcademicGroups/academic.aspx"><img alt="Regresar" src="../../images/page-icons/return.png" />
            <p>Regresar</p></a>
        </section>
    </section>
</asp:Content>

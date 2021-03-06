﻿<%@ Page Title="Acad&eacute;mico - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="academic.aspx.cs" Inherits="UI.Academic.academic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
         <% showOAcademic(); %>
        <p class="title">Académico</p>
        <section class="row">
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="gBuilding.aspx"><img alt="Infraestructura" src="../../images/academic/building.svg" />
                    <p>Infraestructura</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="gFunctionary.aspx"><img alt="Funcionarios" src="../../images/academic/functionary.svg" />
                    <p>Funcionarios</p></a>
                </section>
            </section>
             <% showOfferAcademic(); %>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="gReport.aspx"><img alt="Reportes" src="../../images/academic/report-academic.svg" />
                    <p>Reportes</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../../index.aspx"><img alt="Inicio" src="../../images/page-icons/home.svg" />
                    <p>Inicio</p></a>
                </section>
            </section>
        </section>
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#academic').addClass('isSelected');
    </script>
</asp:Content>

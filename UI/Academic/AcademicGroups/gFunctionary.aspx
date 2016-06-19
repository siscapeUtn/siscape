<%@ Page Title="Funcionarios - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="gFunctionary.aspx.cs" Inherits="UI.Academic.gFunctionary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="../Functionary/InternalDesignation.aspx"><img alt="Nombramineto Interno" src="../../images/academic/internalDesignation.png" />
                <p>Nombramiento interno</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="../Functionary/teacher.aspx"><img alt="Administración de Profesores" src="../../images/academic/teacher.png" />
                <p>Profesores</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="../Functionary/functionary.aspx"><img alt="Administración de Funcionario" src="../../images/academic/functionary2.png" />
                <p>Funcionario</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="../Functionary/externalDesignation.aspx"><img alt="Nombramineto Externo" src="../../images/academic/externalDesignation.png" />
                <p>Nombramiento externo</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="../AcademicGroups/academic.aspx"><img alt="Regresar" src="../../images/page-icons/return.svg" />
                <p>Regresar</p></a>
            </section>
        </section>
    </section>
</asp:Content>

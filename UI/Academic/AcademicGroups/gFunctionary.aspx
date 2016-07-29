<%@ Page Title="Funcionarios - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="gFunctionary.aspx.cs" Inherits="UI.Academic.gFunctionary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
        <p class="title">Funcionarios</p>
        <section class="row">
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Functionary/InternalDesignation.aspx"><img alt="Cat. Salariales" src="../../images/academic/internalDesignation.svg" />
                    <p>Cat. Salariales</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Functionary/teacher.aspx"><img alt="Administración de Profesores" src="../../images/academic/teacher.svg" />
                    <p>Profesores</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Functionary/functionary.aspx"><img alt="Administración de Funcionario" src="../../images/academic/functionary2.svg" />
                    <p>Funcionario</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Functionary/externalDesignation.aspx"><img alt="Nomb. Externo" src="../../images/academic/externalDesignation.svg" />
                    <p>Nombr. externo</p></a>
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

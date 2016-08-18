<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="gAcademicOffer.aspx.cs" Inherits="UI.Academic.AcademicGroups.gAcademicOffer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
        <% showOfferAcademic(); %>
        <p class="title">Oferta Académica</p>
        <section class="row">
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../AcademicOffer/schedule.aspx"><img alt="Nombramineto Interno" src="../../images/academic/schedule.svg" />
                    <p>Horarios</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../AcademicOffer/Course.aspx"><img alt="Administración de Profesores" src="../../images/academic/course.svg" />
                    <p>Cursos</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../AcademicOffer/AcademicOffer.aspx"><img alt="Administración de Funcionario" src="../../images/academic/academicOffer.svg" />
                    <p>Oferta académica</p></a>
                </section>
            </section>
            <%--<section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../AcademicOffer/OpeningJustification.aspx"><img alt="Justificacion de Apertura" src="../../images/academic/opening.svg" />
                    <p>Just. Apertura</p></a>
                </section>
            </section>--%>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../AcademicGroups/academic.aspx"><img alt="Regresar" src="../../images/page-icons/return.svg" />
                    <p>Regresar</p></a>
                </section>
            </section>
        </section>
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#academic').addClass('isSelected');
    </script>
</asp:Content>

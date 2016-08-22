<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="gReport.aspx.cs" Inherits="UI.Academic.AcademicGroups.gReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
        <p class="title">Reportes</p>
        <section class="row">
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Report/teacherReport.aspx"><img alt="Reportes" src="../../images/report/teacher.svg" />
                    <p>Profesores</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Report/classroomReport.aspx"><img alt="Reportes" src="../../images/report/classroom.svg" />
                    <p>Aulas</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Report/waitingReport.aspx"><img alt="Reportes" src="../../images/report/waiting.svg" />
                    <p>Lista Espera</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Report/externalDesignationReport.aspx"><img alt="Reportes" src="../../images/report/external.svg" />
                    <p>Nomb. Externo</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Report/academicOfferReport.aspx"><img alt="Reportes" src="../../images/report/offer.svg" />
                    <p>Oferta académica</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../AcademicGroups/academic.aspx"><img alt="Regresar" src="../../images/page-icons/return.svg" />
                    <p>Regresar</p></a>
                </section>
            </section>
        </section>
</asp:Content>

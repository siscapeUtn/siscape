<%@ Page Title="" Language="C#" MasterPageFile="~/Services/WaitingList.Master" AutoEventWireup="true" CodeBehind="ServicesGroups.aspx.cs" Inherits="UI.Services.ServicesGroups.ServicesGroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% showOService(); %>
        <section class="group-container">
        <section class="row">
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Services/Services.aspx" ><img alt="Servicios" src="../../images/administration/offer.svg" />
                    <p>Oferta de Cursos</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Services/Report_waiting_list.aspx" ><img alt="Programa" src="../../images/academic/report-academic.svg" />
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
        $('#service').addClass('isSelected');
    </script>
</asp:Content>

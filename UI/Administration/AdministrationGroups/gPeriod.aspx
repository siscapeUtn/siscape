<%@ Page Title="Periodo - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="gPeriod.aspx.cs" Inherits="UI.Administration.gPeriod" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
        <section class="row">
            <p class="title">Período</p>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Period/periodType.aspx" ><img alt="Tipo de Periodo" src="../../images/administration/period_type.svg" />
                    <p>Tipo de periodo</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Period/period.aspx" ><img alt="Periodo" src="../../images/administration/period_2.svg" />
                    <p>Periodo</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="administration.aspx" ><img alt="Regresar" src="../../images/page-icons/return.svg" />
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
        $('#administration').addClass('isSelected');
    </script>
</asp:Content>

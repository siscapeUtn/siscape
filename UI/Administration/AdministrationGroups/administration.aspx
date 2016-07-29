<%@ Page Title="Administración - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="administration.aspx.cs" Inherits="UI.Administration.administration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
        <section class="row">
            <p class="title">Administración</p>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="gProgram.aspx" ><img alt="Programa" src="../../images/administration/program.svg" />
                    <p>Programa</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="gPeriod.aspx" ><img alt="Periodo" src="../../images/administration/period.svg" />
                    <p>Periodo</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Slider/slider.aspx" ><img alt="Slider" src="../../images/administration/uploadImage.svg" />
                    <p>Slideshow</p></a>
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
        $('#administration').addClass('isSelected');
    </script>
</asp:Content>

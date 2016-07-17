<%@ Page Title="Administración - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="administration.aspx.cs" Inherits="UI.Administration.administration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#administration').addClass('isSelected');
    </script>
</asp:Content>

<%@ Page Title="Acad&eacute;mico - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="academic.aspx.cs" Inherits="UI.Academic.academic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="gBuilding.aspx"><img alt="Infraestructura" src="../../images/academic/building.png" />
                <p>Infraestructura</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="gFunctionary.aspx"><img alt="Funcionarios" src="../../images/academic/functionary.png" />
                <p>Funcionarios</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="gAcademicOffer.aspx"><img alt="Oferta acad&eacute;mica" src="../../images/academic/offerAcedemic.png" />
                <p>Oferta acad&eacute;mica</p></a>
            </section>
        </section>
    </section>
</asp:Content>

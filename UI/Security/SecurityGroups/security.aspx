<%@ Page Title="" Language="C#" MasterPageFile="~/Security/Security.Master" AutoEventWireup="true" CodeBehind="security.aspx.cs" Inherits="UI.Security.SecurityGroups.security" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="row">
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="../Security/Role.aspx" ><img alt="Programa" src="../../images/security/user_rol.svg" />
                <p>Roles</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="../Security/User.aspx" ><img alt="Programa" src="../../images/security/users.svg" />
                <p>Usuarios</p></a>
            </section>
        </section>
        <section class="col-md-4 col-sm-4 col-xs-12">
            <section class="accesses">
                <a href="security.aspx" ><img alt="Regresar" src="../../images/page-icons/return.svg" />
                <p>Regresar</p></a>
            </section>
        </section>
    </section>
</asp:Content>

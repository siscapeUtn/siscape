<%@ Page Title="" Language="C#" MasterPageFile="~/Security/Security.Master" AutoEventWireup="true" CodeBehind="security.aspx.cs" Inherits="UI.Security.SecurityGroups.security" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="group-container">
        <section class="row">
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Security/Role.aspx" ><img alt="Programa" src="../../images/security/user_rol.svg" />
                    <p>Roles</p></a>
                </section>
            </section>
            <section class="col-md-4 col-sm-4 col-xs-12 accesses-container">
                <section class="accesses">
                    <a href="../Security/User.aspx" ><img alt="Programa" src="../../images/security/users.svg" />
                    <p>Usuarios</p></a>
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
        $('#security').addClass('isSelected');
    </script>
</asp:Content>

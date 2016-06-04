<%@ Page Title="Nombramiento externo - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="externalDesignation.aspx.cs" Inherits="UI.Academic.Functionary.externalDesignation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Nombramientos Externos</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="row">
                        <section class="col-md-12">
                            <asp:Button CssClass="button pull-right" ID="btnAddExternal" Text="Nuevo" OnClick="btnAddExternal_Click" runat="server" />
                        </section>
                        <section class="col-md-12">
                            <section class="dataExists">
                            </section>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section> <!-- End .maintanance -->
    </section> <!-- End content_2 -->

    <!------------------------ Create Modal  ---------------------------------------------->
    <section class="fade modal" id="createDesignation" role="dialog" data-backdrop="static" data-keyboard="false"  aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="createModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="form row create modal-container">
                        <p class="special-label">Agregar nombramiento externo</p>
                        <section class="col-md-12">
                            <section class="form-group">
                                <section class="form-group">
                                    <asp:Label runat="server" Visible="false" ID="lblCode" Text="Código"></asp:Label>
                                    <asp:TextBox runat="server" Visible="false" ID="txtCode"></asp:TextBox>
                                </section>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblFunctionary" Text="Funcionario:" runat="server"></asp:Label>
                                <asp:DropDownList ID="cboFunctionary" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:Label ID="lblMessageFunctionary" CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblPosition" Text="Cargo:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtPosition" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblWorkPlace" Text="Lugar de trabajo:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtWorkPlace" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error help-block" runat="server"></asp:Label>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblStartDesignation" Text="Inicio del nombramiento:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtStartDesignation" CssClass="form-control" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="calendar" ID="txtStartDesignation_CalendarExtender" runat="server" TargetControlID="txtStartDesignation" />
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblEndDesignation" Text="Fin del nombramiento:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEndDesignation" CssClass="form-control" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="calendar" ID="txtEndDesignation_CalendarExtender" runat="server" TargetControlID="txtEndDesignation" />
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblHoursDisignation" Text="Horas del nombramiento:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtHoursDisignation" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                        </section>
                        <p class="special-label help-block">Detalles del nombramiento</p>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblDay" Text="Día:" runat="server"></asp:Label>
                                <asp:DropDownList ID="cboDay" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:Label ID="lblMessage" CssClass="message-has-error help-block" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblStart" Text="Hora de Inicio:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblEnd" Text="Hora final:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                        </section>
                        <section class="col-md-12">
                            <asp:Button CssClass="closePopUp" runat="server" ID="btnClose" Text="Cerrar" OnClick="btnClose_Click" />
                            <asp:Button CssClass="button pull-right" runat="server" ID="btnCreate" Text="Guardar" OnClick="btnCreate_Click" />
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
    <!----------------------------- End Create modal --------------------->
</asp:Content>

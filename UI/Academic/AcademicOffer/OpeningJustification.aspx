<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="OpeningJustification.aspx.cs" Inherits="UI.Academic.AcademicOffer.OpeningJustification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Justificaci&oacute;n de Apertura</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <fieldset>
                            <section class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <asp:Label runat="server" Visible="false" ID="lblCode" Text="Código"></asp:Label>
                                    <asp:TextBox runat="server" Visible="false" ID="txtCode"></asp:TextBox>
                                </section>
                            </section>
                            <section class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblTeacher" Text="Profesor:"></asp:Label>
                                    <asp:DropDownList ID="cboTeacher" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageTeacher"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblCourse" Text="Curso:"></asp:Label>
                                    <asp:DropDownList ID="cboCourse" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageCourse"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblDesignationHours" Text="Nombramiento:"></asp:Label>
                                    <asp:TextBox ID="txtDesignationHours" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageDesignationHours"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblSalary" Text="Salario:"></asp:Label>
                                    <asp:TextBox ID="txtSalary" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageSalary"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblAnuality" Text="Anualidad:"></asp:Label>
                                    <asp:TextBox ID="txtAnnuality" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageAnnuality"></asp:Label></span>
                                </section>
                            </section>
                            <section class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblValue" Text="Anualidad:"></asp:Label>
                                    <asp:TextBox ID="txtValue" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageValue"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblStudents" Text="Estudiantes:"></asp:Label>
                                    <asp:TextBox ID="txtStudents" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageStudents"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblIncome" Text="Ingresos:"></asp:Label>
                                    <asp:TextBox ID="txtIncome" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageIncome"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblDifference" Text="Diferencia:"></asp:Label>
                                    <asp:TextBox ID="txtDifference" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageDifference"></asp:Label></span>
                                </section>
                            </section>
                            <section class="col-md-12 form-buttons">
                                <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" />
                                <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" />
                                <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" />
                            </section> <!-- End .form-buttons -->
                            <section class="col-md-12 message">
                                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                            </section>
                        </fieldset>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>

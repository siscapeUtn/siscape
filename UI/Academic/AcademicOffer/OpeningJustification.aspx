<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="OpeningJustification.aspx.cs" Inherits="UI.Academic.AcademicOffer.OpeningJustification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <% showOfferAcademic(); %>
        <section class="maintanance">
            <p class="title">Justificaci&oacute;n de Apertura</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <fieldset>
                            <section class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblTeacher" Text="Profesor: "></asp:Label>
                                    <asp:Label runat="server" ID="lblTeacherDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblCourse" Text="Curso: "></asp:Label>
                                    <asp:Label runat="server" ID="lblCourseDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblDesignationHours" Text="Categoria: "></asp:Label>
                                    <asp:Label runat="server" ID="lblPositionDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblHours" Text="Nombramiento: "></asp:Label>
                                    <asp:Label runat="server" ID="lblHoursDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblAnuality" Text="Anualidades:"></asp:Label>
                                    <asp:TextBox ID="txtAnnuality" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageAnnuality"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblSalary" Text="Salario: "></asp:Label>
                                    <asp:Label runat="server" ID="lblSalaryDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblTotalAnuality" Text="Anualidades: "></asp:Label>
                                    <asp:Label runat="server" ID="lblTotalAnualityDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblCCSS" Text="Cargos Sociales: "></asp:Label>
                                    <asp:Label runat="server" ID="lblCCSSDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblPublicity" Text="Publicidad: "></asp:Label>
                                    <asp:Label runat="server" ID="lblPublicityDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblOther" Text="Otros:"></asp:Label>
                                    <asp:TextBox ID="txtOther" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageOther"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblTotaTotalMouth" Text="Total mensual: "></asp:Label>
                                    <asp:Label runat="server" ID="lblTotaTotalMouthDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblTotalbimensual" Text="Total bimensual: "></asp:Label>
                                    <asp:Label runat="server" ID="lblTotalbimensualDescription" Text=""></asp:Label>
                                </section>
                            </section>
                            <section class="col-lg-6 col-md-6 col-sm-12 col-xs-12">

                                <section class="form-group subtitle-income">
                                    <asp:Label runat="server" ID="lblSubIncomes" Text="Ingresos"></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblValue" Text="Valor del Curso:"></asp:Label>
                                    <asp:Label runat="server" ID="lblValueDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblStudents" Text="Estudiantes:"></asp:Label>
                                    <asp:TextBox ID="txtStudents" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageStudents"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblIncome" Text="Ingresos: "></asp:Label>
                                    <asp:Label runat="server" ID="lblIncomeDescription" Text=""></asp:Label>
                                </section>
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblDifference" Text="Diferencia: "></asp:Label>
                                    <asp:Label runat="server" ID="lblDifferenceDescription" Text=""></asp:Label>
                                </section>
                            </section>
                            <section class="col-md-12 form-buttons">
                                 <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Calcular" runat="server" OnClick="btnNew_Click" />
                                <asp:ImageButton ID="btnReport" CssClass="image_align" ImageUrl="~/images/maintenance/report.png" ToolTip="Reporte" runat="server" OnClick="btnReport_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                            </section> <!-- End .form-buttons -->
                            <section class="col-md-12 message">
                                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                            </section>
                        </fieldset>
                    </section>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger  ControlID="btnReport"/>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>

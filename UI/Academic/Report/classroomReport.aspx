<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="classroomReport.aspx.cs" Inherits="UI.Academic.Report.classroomReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Reporte de Aulas</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                         <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblPeriod" Text="Seleccione el período"></asp:Label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboPeriod"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label ID="lblMessage" runat="server"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                               <asp:ImageButton runat="server" ID="btnReport" CssClass="reportBtn" ToolTip="Generar reporte" ImageUrl="~/images/maintenance/report.png" OnClick="btnReport_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="reportBtn" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                            </section>
                        </section>
                    </section>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnReport" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>

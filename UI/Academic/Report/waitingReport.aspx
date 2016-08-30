<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="waitingReport.aspx.cs" Inherits="UI.Academic.Report.waitingReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Reporte de Lista de Espera</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblSelect" Text="Seleccione la opción"></asp:Label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="cboOptions">
                                    <asp:ListItem Value="0">No contactado</asp:ListItem>
                                    <asp:ListItem Value="1">Contactado</asp:ListItem>
                                </asp:DropDownList>
                            </section>
                        </section>
                         <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblPeriod"  Text="Seleccione el período:"></asp:Label>
                                <asp:DropDownList runat="server" ID="cboPeriod" CssClass="form-control"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label ID="lblMessage" runat="server"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-12">
                            <section class="form-group form-buttons">
                               <asp:ImageButton runat="server" ID="btnReport" CssClass="image_align" ToolTip="Generar reporte" ImageUrl="~/images/maintenance/report.png" OnClick="btnReport_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
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
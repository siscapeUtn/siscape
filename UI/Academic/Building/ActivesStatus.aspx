<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="ActivesStatus.aspx.cs" Inherits="UI.Academic.Building.ActivesStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Status de los Activos</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <section class="col-md-12">
                            <section class="form-group">
                                <asp:Label runat="server" Visible="false" ID="lblCode" Text="Código"></asp:Label>
                                <asp:TextBox runat="server" Visible="false" ID="txtCode"></asp:TextBox>
                            </section>
                        </section>

                            <section class="col-md-3">
                            </section>
                            <section class="form-horizontal col-md-6">
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblDescription" Text="Descripción:" CssClass="control-label col-sm-2"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control col-sm-10"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageDescription"></asp:Label></span>
                                </section>
                            </section>
                            <section class="col-md-3"></section>


                        <section class="col-md-12 form-buttons">
                            <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                            <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                            <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                            <asp:ImageButton ID="btnReport" CssClass="image_align" ImageUrl="~/images/maintenance/report.png" ToolTip="Reporte" runat="server" OnClick="btnReport_Click" />
                            <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                        </section> <!-- End .form-buttons -->
                        <section class="col-md-12 message">
                            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                        </section>
                    </section>
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvActiveStatus" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvActiveStatus_PageIndexChanging" PageSize="12"  OnRowEditing="gvActiveStatus_RowEditing">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" DataField="activesSatus_ID" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Descripción" DataField="description" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Editar" ImageUrl="~/images/maintenance/edit.png" Text="Editar" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                </Columns>
                            </asp:GridView>
                        </section>
                    </section>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger  ControlID="btnReport"/>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>

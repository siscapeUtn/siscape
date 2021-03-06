﻿<%@ Page Title="Tipo de periodo - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="periodType.aspx.cs" Inherits="UI.Administration.PeriodType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de tipo de periodos</p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <fieldset>
                            <section class="col-md-12">
                                <section class="form-group">
                                    <asp:Label Visible="false" ID="lblCode" Text="Código:" runat="server"></asp:Label>
                                    <asp:TextBox Visible="false" ID="txtCode" runat="server"></asp:TextBox>
                                </section><!-- End .form-group -->
                            </section>
                            <section class="col-md-6">
                                <section class="form-group">
                                    <asp:Label ID="lblName" Text="Descripción:" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label ID="lblNameMessage" Text="" runat="server"></asp:Label></span>
                                </section><!-- End .form-group -->
                            </section>
                            <section class="col-md-6">
                                <section class="form-group">
                                    <asp:Label ID="lblState" Text="Estado:" runat="server"></asp:Label>
                                    <asp:DropDownList ID="cboState" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                    </asp:DropDownList>
                                </section><!-- End .form-group -->
                            </section>
                            <section class="form-buttons col-md-12">
                                <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                                <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                                <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                                <asp:ImageButton ID="btnReport" CssClass="image_align" ImageUrl="~/images/maintenance/report.png" ToolTip="Reporte" runat="server" OnClick="btnReport_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                            </section> <!-- End .form-buttons -->
                            <section class="col-md-12 message">
                                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                            </section> <!-- End .message -->
                        </fieldset> <!-- End fieldset -->
                    </section> <!-- End .form -->
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvPeriodType" runat="server" AutoGenerateColumns="False" OnRowEditing="gvPeriodType_RowEditing" AllowPaging="True" OnPageIndexChanging="gvPeriodType_PageIndexChanging" PageSize="12" OnRowDeleting="gvPeriodType_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="code" HeaderText="Código" ReadOnly="True" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField DataField="description" HeaderText="Descripción" ReadOnly="True" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" DataTextField="code" HeaderText="Editar" ImageUrl="~/images/maintenance/edit.png" Text="Editar" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" CommandName="Delete" HeaderText="Delete" ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                </Columns>
                            </asp:GridView>
                        </section>
                    </section>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger  ControlID="btnReport"/>
                </Triggers>
            </asp:UpdatePanel> <!-- End UpdatePanel -->
        </section><!-- End .maintance -->
    </section> <!-- End .content_2 -->
    <section class="modal fade" id="confirmMessage" role="dialog" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <p>¿Esta seguro de eliminar el Tipo de Periodo <strong><asp:Label ID="lblPeriodTypeDescription" Text="" runat="server"></asp:Label></strong>?</p>
                        </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm" OnClick="btnDelete_Click" ID="btnDelete" Text="Eliminar" runat="server"></asp:Button>
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#administration').addClass('isSelected');
    </script>
</asp:Content>
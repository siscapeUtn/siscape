<%@ Page Title="Tipo de Aula - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="classRoomType.aspx.cs" Inherits="UI.Academic.classRoomType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Tipo de Aula</p>
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
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblDescription" Text="Descripción:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageDescription"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblState" Text="Estado:"></asp:Label>
                                <asp:DropDownList ID="cboState" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    <asp:ListItem Value="1">Activo</asp:ListItem>
                                </asp:DropDownList>
                            </section>
                        </section>
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
                            <asp:GridView ID="gvClassRoomType" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvClassRoomType_PageIndexChanging" PageSize="12" OnRowEditing="gvClassRoomType_RowEditing" OnRowDeleting="gvClassRoomType_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" DataField="code" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Descripción" DataField="description" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Editar" ImageUrl="~/images/maintenance/edit.png" Text="Editar" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Eliminar" ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" CommandName="Delete" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
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
    <!-- Delete modal -->
    <section class="modal fade" id="confirmMessage" role="dialog" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <p>¿Esta seguro de eliminar el Tipo de Aula <strong><asp:Label ID="lblClassRoomTypeDescription" Text="" runat="server"></asp:Label></strong>?</p>
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
        $('#academic').addClass('isSelected');
    </script>
</asp:Content>

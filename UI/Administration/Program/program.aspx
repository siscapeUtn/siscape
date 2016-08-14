<%@ Page Title="Programa - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="program.aspx.cs" Inherits="UI.Administration.Program" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="up"></div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content_2">
        <section class="maintanance">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <p id="title" class="title">Administraci&oacute;n de Programas</p>
                    <section class="form row">
                        <fieldset>
                            <section class="col-md-12">
                                <section class="form-group">
                                    <asp:Label Visible="false" ID="lblCode" Text="Código:" runat="server"></asp:Label>
                                    <asp:TextBox Visible="false" ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-md-6">
                                <section class="form-group">
                                    <asp:Label ID="lblName" Text="Descripción:" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label ID="lblNameMessage" Text="" runat="server"></asp:Label></span>
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-md-6">
                                <section class="form-group">
                                    <asp:Label ID="lblUnit" Text="Unidad ejecutora:" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtUnit" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label ID="lblUnitMessage" Text="" runat="server"></asp:Label></span>
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-md-3"></section>
                            <section class="form-horizontal col-md-6">
                                <section class="form-group">
                                    <asp:Label ID="lblState" Text="Estado:" CssClass="control-label col-sm-2" runat="server"></asp:Label>
                                    <section class="col-sm-10">
                                        <asp:DropDownList ID="cboState" CssClass="form-control col-sm-10" runat="server">
                                            <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                            <asp:ListItem Value="1">Activo</asp:ListItem>
                                        </asp:DropDownList>
                                    </section>
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-md-3"></section>
                            <section class="col-md-12 form-buttons">
                                <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                                <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                                <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                            </section> <!-- End .form-buttons -->
                            <section class="col-md-12 message">
                                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                            </section> <!-- End .message -->
                        </fieldset> <!-- End fieldset -->
                    </section> <!--End .form -->
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvProgram" CssClass="table" runat="server" AutoGenerateColumns="False" OnRowEditing="gvProgram_RowEditing" AllowPaging="True" OnPageIndexChanging="gvProgram_PageIndexChanging" PageSize="12" OnRowDeleting="gvProgram_RowDeleting" >
                                <Columns>
                                    <asp:BoundField HeaderText="Código" ReadOnly="true" DataField="code" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Descripción" ReadOnly="true" DataField="name" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Unidad ejecutora" ReadOnly="true" DataFormatString="{0:N}" DataField="unit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Editar" ImageUrl="~/images/maintenance/edit.png" Text="Editar" DataTextField="code" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField HeaderText="Eliminar" ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" DataTextField="code" CommandName="Delete" ButtonType="Image" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                </Columns>
                            </asp:GridView>
                            <!-- End GridView -->
                        </section>
                    </section><!-- End .dataExists -->
                </ContentTemplate> 
            </asp:UpdatePanel> <!-- End UpdatePanel -->
        </section> <!--End .maintance -->
    </section>
    <section class="modal fade" id="confirmMessage" role="dialog" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <p>¿Esta seguro de eliminar el programa <strong><asp:Label ID="lblProgramName" Text="" runat="server"></asp:Label></strong>?</p>
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

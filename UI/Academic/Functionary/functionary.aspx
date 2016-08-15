<%@ Page Title="Funcionario - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="functionary.aspx.cs" Inherits="UI.Academic.functionary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Funcionarios</p>
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
                                <asp:Label ID="lblId"  Text="Identificación:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtId" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageId"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblName"  Text="Nombre:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageName"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblLastName"  Text="Apellidos:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageLastName"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblHomePhone"  Text="Teléfono Residencial:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtHomePhone" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageHomePhone"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblCellPhone"  Text="Teléfono Celular:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtCellPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageCellPhone"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblEmail"  Text="Correo Electrónico:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageEmail"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-3"></section>
                        <section class="form-horizontal col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblState" CssClass="control-label col-sm-2" Text="Estado:" runat="server"></asp:Label>
                                <section class="col-sm-10">
                                    <asp:DropDownList ID="cboState" CssClass="form-control col-sm-4" runat="server">
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">Activo</asp:ListItem>
                                    </asp:DropDownList>
                                </section>
                            </section>
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
                        <section class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Button runat="server" ID="btnReport" Text="Reporte de funcionarios" CssClass="pull-right reportButton" OnClick="btnReport_Click" />
                        </section>
                    </section>
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvFunctionary" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvFunctionary_PageIndexChanging" PageSize="12" OnRowEditing="gvFunctionary_RowEditing" OnRowDeleting="gvFunctionary_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" ReadOnly="true" DataField="code" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Identificación" ReadOnly="true" DataField="id" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Nombre" ReadOnly="true" DataField="name" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Apellidos" ReadOnly="true" DataField="lastName" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Teléfono Celular" ReadOnly="true" DataField="cellPhone" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
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
                            <p>¿Esta seguro de eliminar el Funcionario <strong><asp:Label ID="lblFunctionaryDescription" Text="" runat="server"></asp:Label></strong>?</p>
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

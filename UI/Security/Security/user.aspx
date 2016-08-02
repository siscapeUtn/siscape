<%@ Page Title="" Language="C#" MasterPageFile="~/Security/Security.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="UI.Administration.Security.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Usuarios</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <fieldset>
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
                            <section class="form-group">
                                <asp:Label ID="lblHomePhone"  Text="Teléfono Residencial:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtHomePhone" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageHomePhone"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-6">
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
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblProgram" Text="Programa:"></asp:Label>
                                <asp:DropDownList ID="cboProgram" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageProgram"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblRole" Text="Rol del usuario:"></asp:Label>
                                <asp:DropDownList ID="cboRole" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageRole"></asp:Label></span>
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
                       </fieldset> <!--end fieldset-->
                    </section> <!--end section form row-->
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvUserSystem" CssClass="table" runat="server" AutoGenerateColumns="False"  AllowPaging="True" PageSize="12" OnRowEditing="gvUserSystem_RowEditing" OnRowDeleting="gvUserSystem_RowDeleting" OnRowUpdating="gvUserSystem_RowUpdating">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" ReadOnly="true" DataField="code" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" >
                                    <ItemStyle CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Identificación" ReadOnly="true" DataField="id" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" >
                                    <ItemStyle CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <%# Eval("name").ToString() + ' ' +  Eval("lastName").ToString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:boundfield headertext="celular" readonly="true" datafield="cellphone" itemstyle-cssclass="visible-lg visible-md visible-sm visible-xs" ></asp:boundfield>
                                    <asp:boundfield headertext="Programa" readonly="true" datafield="oProgram.name" itemstyle-cssclass="visible-lg visible-md visible-sm visible-xs" ></asp:boundfield>                                    
                                    <asp:boundfield headertext="Rol" readonly="true" datafield="oRole.Description" itemstyle-cssclass="visible-lg visible-md visible-sm visible-xs" ></asp:boundfield>                                    
                                    <asp:ButtonField ButtonType="Image" HeaderText="Editar" ImageUrl="~/images/maintenance/edit.png" Text="Editar" DataTextField="code" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ></asp:ButtonField>                                    
                                    <asp:ButtonField ButtonType="Image" HeaderText="Restablecer" ImageUrl="~/images/maintenance/edit.png" Text="Editar" DataTextField="code" CommandName="Update" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ></asp:ButtonField>
                                    <asp:ButtonField HeaderText="Eliminar" ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" DataTextField="code" CommandName="Delete" ButtonType="Image" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ></asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                            <!-- End GridView -->
                        </section>
                    </section><!-- End .dataExists -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </section> <!-- End .maintance -->
    </section><!-- End .content_2 -->

         <section class="modal fade" id="confirmMessage" role="dialog" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <p>¿Esta seguro de eliminar el Usuario <strong><asp:Label ID="lblUserName" Text="" runat="server"></asp:Label></strong>?</p>
                        </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm" OnClick="btnDelete_Click" ID="btnDelete" Text="Eliminar" runat="server"></asp:Button>
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section><!--end modal dialog-->
    </section><!--end section modal delete-->

         <section class="modal fade" id="ResetPassword" role="dialog" aria-labelledby="ResetPasswordLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="Label1" Text="Restablecer la Contraseña" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                                <section class="col-md-6">
                                    <section class="form-group">
                                        <asp:TextBox ID="txtUser" placeholder="correo electrónico" runat="server"></asp:TextBox>
                                        <span class="help-block"></span>
                                    </section>
                                </section>
                                <section class="col-md-6">
                                    <section class="form-group">
                                        <asp:TextBox ID="txtPassword" placeholder="contraseña" runat="server" TextMode="Password" ></asp:TextBox>
                                        <span class="help-block"></span>
                                    </section>
                                </section>
                         </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm" OnClick="btnReset_Click" ID="Button1" Text="Restablecer" runat="server"></asp:Button>
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section><!--end modal reset password-->
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#security').addClass('isSelected');
    </script>
</asp:Content>

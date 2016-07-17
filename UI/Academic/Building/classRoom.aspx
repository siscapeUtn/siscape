<%@ Page Title="Aula - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="classRoom.aspx.cs" Inherits="UI.Academic.classRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Aulas</p>
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
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblClassRoomType" Text="Tipo de Aula:"></asp:Label>
                                <asp:DropDownList ID="cboClassRoomType" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageClassRoomType"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblLocation" Text="Localización:"></asp:Label>
                                <asp:DropDownList ID="cboLocation" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMesageLocation"></asp:Label></span>
                            </section> 
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblSize" Text="Capacidad:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtSize" CssClass="form-control"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageSize"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblProgram" Text="Programa:"></asp:Label>
                                <asp:DropDownList ID="cboProgram" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageProgram"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblState" Text="Estado:"></asp:Label>
                                <asp:DropDownList ID="cboState" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    <asp:ListItem Value="1">Activo</asp:ListItem>
                                </asp:DropDownList>
                            </section> <!-- End .form-group -->
                        </section>
                        <section class="col-md-12 form-buttons">
                            <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                            <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                            <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                            <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                        </section> <!-- End .form-buttons -->
                        <section class="col-md-12 message">
                            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                        </section>
                    </section>
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvClassRoom" runat="server" AutoGenerateColumns="False" OnRowEditing="gvClassRoom_RowEditing" OnRowDeleting="gvClassRoom_RowDeleting" >
                                <Columns>
                                    <asp:BoundField HeaderText="C&#243;digo" DataField="code" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ></asp:BoundField>
                                    <asp:BoundField HeaderText="N&#176; Aula" DataField="num_room" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:BoundField HeaderText="Capacidad" DataField="size" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:BoundField HeaderText="Tipo de Aula" DataField="oClassRoomsType.description" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:BoundField HeaderText="Programa" DataField="oProgram.name" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:ButtonField ImageUrl="~/images/maintenance/edit.png" Text="Editar" ButtonType="Image" HeaderText="Editar" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:ButtonField>
                                    <asp:ButtonField ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" ButtonType="Image" HeaderText="Eliminar" CommandName="Delete" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </section>
                </ContentTemplate>
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
                            <p>¿Esta seguro de eliminar el Aula <strong><asp:Label ID="lblClassRoomDescription" Text="" runat="server"></asp:Label></strong>?</p>
                        </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm" OnClick="btnDelete_Click"  ID="btnDelete" Text="Eliminar" runat="server"></asp:Button>
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

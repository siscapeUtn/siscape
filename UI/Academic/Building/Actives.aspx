<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="Actives.aspx.cs" Inherits="UI.Academic.Building.Actives" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Activos</p>
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
                                <asp:label runat="server" id="lblprogram" text="Progr&#225ma:"></asp:label>
                                <asp:dropdownlist id="cboprogram" cssclass="form-control"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboprogram_SelectedIndexChanged"></asp:dropdownlist>
                                <span class="message-has-error"><asp:label runat="server" id="lblmessageprogram" ></asp:label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblcodealphanumeric" Text="Código Alfanumérico:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtcodeAlphaNumeric" CssClass="form-control"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessagecodeAlphaNumeric"></asp:Label></span>
                            </section>

                        </section>

                        <section class="col-md-6">                            
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblClassroom" Text="Aula:"></asp:Label>
                                <asp:DropDownList ID="cboClassroom" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMesageClassroom"></asp:Label></span>
                            </section>
                             <section class="form-group">
                                <asp:Label runat="server" ID="lblDescription" Text="Descripción del Activo:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageDescription"></asp:Label></span>
                            </section>
                        </section>

                        <section class="col-md-3"></section>
                          <section class="col-md-6 form-horizontal">
                            <section class="form-group">
                                <asp:Label ID="lblState" Text="Estado:" CssClass="control-label col-sm-2" runat="server"></asp:Label>
                                <section class="col-sm-10">
                                    <asp:DropDownList ID="cboStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <span class="message-has-error"><asp:Label runat="server" ID="lblMessageStatus"></asp:Label></span>
                                </section>
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
                            <%--<asp:GridView ID="gvClassRoom" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvClassRoom_PageIndexChanging" PageSize="12" OnRowEditing="gvClassRoom_RowEditing" OnRowDeleting="gvClassRoom_RowDeleting" >--%>
                             <asp:GridView ID="gvActives" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="12" OnRowEditing="gvActives_RowEditing" OnRowDeleting="gvActives_RowDeleting" OnPageIndexChanging="gvActives_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField HeaderText="Id" DataField="code" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" ></asp:BoundField>
                                    <asp:BoundField HeaderText="C&#243;digo" DataField="codeAlphaNumeric" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:BoundField HeaderText="Detalle" DataField="description" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:BoundField HeaderText="Aula" DataField="oClassRoom.num_room" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:BoundField HeaderText="Prográma" DataField="oProgram.name" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:BoundField HeaderText="Estado" DataField="status.description" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:BoundField>
                                    <asp:ButtonField ImageUrl="~/images/maintenance/edit.png" Text="Editar" ButtonType="Image" HeaderText="Editar" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:ButtonField>
                                    <asp:ButtonField ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" ButtonType="Image" HeaderText="Eliminar" CommandName="Delete" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"></asp:ButtonField>
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
    <!-- delete modal -->
       <section class="modal fade" id="confirmmessage" role="dialog" aria-labelledby="confirmmessagelabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:updatepanel id="confirmmodal" runat="server" childrenastriggers="false" updatemode="conditional">
                <contenttemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:label id="modalheader" text="mensaje de confirmación" runat="server"></asp:label>
                        </section>
                        <section class="modal-body">
                            <p>¿esta seguro de eliminar el Activo <strong><asp:label id="lblActivesdescription" text="" runat="server"></asp:label></strong>?</p>
                        </section>
                        <section class="modal-footer">
                            <asp:button cssclass="btn btn-confirm" onclick="btndelete_click"  id="btndelete" text="eliminar" runat="server"></asp:button>
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">cancelar</button>
                        </section>
                    </section>
                </contenttemplate>
            </asp:updatepanel>
        </section>
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#academic').addClass('isSelected');
    </script>


</asp:Content>

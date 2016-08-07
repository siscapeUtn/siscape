<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="slider.aspx.cs" Inherits="UI.Administration.Slider.slider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content_2">
        <section class="maintanance">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <p class="title">Administraci&oacute;n de Images</p>
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
                                <section class="form-group">
                                    <asp:Label ID="lblState" Text="Estado:" CssClass="control-label" runat="server"></asp:Label>
                                    <asp:DropDownList ID="cboState" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                    </asp:DropDownList>
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-md-6">
                                <section class="form-group">
                                    <asp:Label ID="lblImage" Text="Seleccione la imagen:" runat="server"></asp:Label>
                                    <section class="uploadImage">
                                        <asp:TextBox runat="server" ID="uploadFile" CssClass="form-control uploadText" placeholder="Seleccione la imagen" disabled="disabled" ></asp:TextBox>
                                        <div class="fileUpload btn btn-primary">
                                            <span><img alt="Subir" src="../../images/page-icons/upload.svg" /></span>   
                                            <asp:FileUpload ID="flLoadImage" runat="server" CssClass="upload"  />
                                        </div>
                                    </section>
                                    <span class="message-has-error"><asp:Label ID="lblImageError" Text="" runat="server"></asp:Label></span>
                                </section>
                                <section class="form-group">
                                    <asp:Image ID="imgUpload" runat="server"  />
                                </section>
                            </section>
                            <section class="form-horizontal">
                                
                            </section>                           
                            <section class="col-md-12 form-buttons">
                                <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click"  />
                                <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                                <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                            </section> <!-- End .form-buttons -->
                            <section class="col-md-12 message">
                                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                            </section> <!-- End .message -->
                        </fieldset>
                    </section>
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvSlider" CssClass="table" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="12" OnRowEditing="gvSlider_RowEditing" OnRowDeleting="gvSlider_RowDeleting">

                                <Columns>
                                    <asp:BoundField DataField="code" HeaderText="C&#243;digo" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="description" HeaderText="Descripi&#243;n" ReadOnly="True"></asp:BoundField>
                                    <asp:ButtonField CommandName="Edit" ImageUrl="~/images/maintenance/edit.png" Text="Editar" ButtonType="Image" HeaderText="Editar"></asp:ButtonField>
                                    <asp:ButtonField CommandName="Delete" ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" ButtonType="Image" HeaderText="Eliminar"></asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </section>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
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
                            <p>¿Esta seguro de eliminar la imagen de slider <strong><asp:Label ID="lblSliderName" Text="" runat="server"></asp:Label></strong>?</p>
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
        uploadImage();
    </script>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#administration').addClass('isSelected');
    </script>
</asp:Content>

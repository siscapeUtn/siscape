<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" CodeBehind="slider.aspx.cs" Inherits="UI.Administration.Slider.slider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content_2">
        <section class="maintanance">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            </section>
                            <section class="col-md-6">
                                <section class="form-group">
                                    <asp:Label ID="lblImage" Text="Seleccione la imagen:" runat="server"></asp:Label>
                                    <section class="uploadImage">
                                        <asp:TextBox runat="server" ID="uploadFile" CssClass="form-control uploadText" placeholder="Seleccione la imagen" disabled="disabled" ></asp:TextBox>
                                        <div class="fileUpload btn btn-primary">
                                            <span><img alt="Subir" src="../../images/page-icons/upload.svg" /></span>
                                            <asp:FileUpload ID="flLoadImage" runat="server" enctype="multipart/form-data" CssClass="upload"  />
                                        </div>
                                    </section>
                                    <span class="message-has-error"><asp:Label ID="lblImageError" Text="" runat="server"></asp:Label></span>
                                </section>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
    <script type="text/javascript">
        uploadImage();
    </script>
</asp:Content>

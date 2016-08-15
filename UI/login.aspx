<%@ Page Title="Ingresar al Sistema | DEAS" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="UI.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content">
        <section class="login_container">
            <section class="login_header">
                <p>Ingresar al sistema</p>
            </section>
            <section class="login_content">
                <section class="row">
                    <section class="col-md-12">
                        <section class="form-group">
                            <asp:TextBox ID="txtUser" placeholder="ejemplo@dominio.com" runat="server"></asp:TextBox>
                            <span class="help-block"></span>
                        </section>
                        <section class="form-group">
                            <asp:TextBox ID="txtPassword" placeholder="contraseña" runat="server" TextMode="Password"></asp:TextBox>
                            <span class="help-block"></span>
                        </section>
                    </section>
                    <section class="col-md-12 message">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </section>
                    <section class="col-md-4">
                        <a id="forgotPassword" onclick="showResetPassword();">
                            <asp:Label Text="¿Has olvidado tu contraseña?" runat="server"></asp:Label></a>
                    </section>
                    <section class="col-md-4">
                        <a id="changePassword" onclick="showChangePassword();">
                            <asp:Label Text="¿Cambiar tú contraseña?" runat="server"></asp:Label></a>
                    </section>
                    <section class="col-md-4">
                        <section class="form-group">
                            <asp:Button CssClass="pull-right" Text="Ingresar" runat="server" OnClick="login_Click" />
                        </section>
                    </section>
                </section>
            </section>
        </section>
    </section>

    <section class="modal fade" id="confirmMessage" role="dialog" data-keyboard="false" data-backdrop="static" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="modalHeader" Text="Seleccione el período" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <section class="col-md-12">
                                <section class="form-group">
                                    <asp:Label runat="server" ID="lblSelectPeriod" Text="Seleccione el período:"></asp:Label>
                                    <asp:DropDownList ID="cboPeriod" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <span class="help-block"></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label ID="lblMsj" runat="server"></asp:Label>
                                    <span class="help-block"></span>
                                </section>
                            </section>
                        </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm pull-right" OnClick="btnPeriod_Click" ID="btnDelete" Text="Aceptar" runat="server"></asp:Button>
                            <asp:Button CssClass="btn btn-confirm pull-right" OnClick="btnCancel_Click" ID="btnCancel" Text="Cancelar" runat="server"></asp:Button>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>

    <section class="modal fade" id="changePasswordModal" role="dialog" aria-labelledby="changePasswordLabel" aria-hidden="true">
        <section class="modal-dialog changePassword">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="lblChangePassword" Text="Cambiar la Contraseña" runat="server"></asp:Label>
                        </section>
                        <section  class="modal-body">
                                <section class="col-md-12">
                                    <section class="form-group">
                                        <asp:Label runat="server" ID="lblNewPassword" Text="Nueva Contraseña:"></asp:Label>
                                        <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server"></asp:TextBox>
                                        <span class="help-block"></span>
                                    </section>
                                </section>
                                <section class="col-md-12">
                                    <section class="form-group">
                                        <asp:Label runat="server" ID="lblConfirmPassword" Text="Confirmar contraseña:"></asp:Label>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" ></asp:TextBox>
                                        <span class="help-block"></span>
                                    </section>
                                </section>
                                <section class="col-md-12">
                                    <section class="form-group">
                                        <asp:Label runat="server" ID="lblLastPassword" Text="Antiguo contraseña:"></asp:Label>
                                        <asp:TextBox ID="txtLastPassword" runat="server" TextMode="Password" ></asp:TextBox>
                                        <span class="help-block"></span>
                                    </section>
                                </section>
                         </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm"  ID="btnChange" Text="Cambiar" OnClick="btnChange_Click1" runat="server"></asp:Button>
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section><!--end modal reset password-->

    <section class="modal fade" id="resetPasswordModal" role="dialog" aria-labelledby="resetPasswordLabel" aria-hidden="true">
        <section class="modal-dialog changePassword">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="Label1" Text="Cambiar la Contraseña" runat="server"></asp:Label>
                        </section>
                        <section  class="modal-body">
                                <span>Para restablecer su contraseña contactese con <strong>siscape.utn@gmail.com</strong></span>
                         </section>
                        <section class="modal-footer">
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>

    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#login').addClass('isSelected');
    </script>
    <script type="text/javascript">
        function showResetPassword() {
            $('#resetPasswordModal').modal();
        }
    </script>
    <script type="text/javascript">
        function showChangePassword() {
            $('#changePasswordModal').modal();
        }
    </script>
</asp:Content>

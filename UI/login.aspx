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
                    <section class="col-md-8">
                        <a id="forgotPassword">
                            <asp:Label Text="¿Has olvidado tu contraseña?" runat="server"></asp:Label></a>
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
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <section class="col-md-12">
                                <section class="form-group">
                                    <asp:DropDownList ID="cboPeriod" runat="server"></asp:DropDownList>
                                    <span class="help-block"></span>
                                </section>
                                <section class="form-group">
                                    <asp:Label ID="lblMsj" Text="hola" runat="server"></asp:Label>
                                    <span class="help-block"></span>
                                </section>
                            </section>
                        </section>
                        <section class="modal-footer">
                            <section class="col-md-2">
                                <asp:Button CssClass="btn btn-confirm pull-right" OnClick="btnPeriod_Click" ID="btnDelete" Text="Aceptar" runat="server"></asp:Button>
                            </section>
                            <section class="col-md-2">
                                <asp:Button CssClass="btn btn-confirm pull-right" OnClick="btnCancel_Click" ID="btnCancel" Text="Cancelar" runat="server"></asp:Button>
                            </section>
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
</asp:Content>

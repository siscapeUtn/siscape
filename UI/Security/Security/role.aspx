<%@ Page Title="" Language="C#" MasterPageFile="~/Security/Security.Master" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="UI.Administration.Security.Role" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section class="content_2">
        <section class="maintanance">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <p class="title">Administraci&oacute;n de Roles del Sistema</p>
                    <section class="form row">
                        <fieldset>
                            <section class="col-lg-12 col-md-12col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <asp:Label Visible="false" ID="lblCode" Text="Código:" runat="server"></asp:Label>
                                    <asp:TextBox Visible="false" ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <asp:Label ID="lblName" Text="Descripción:" runat="server"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                    <span class="message-has-error"><asp:Label ID="lblNameMessage" Text="" runat="server"></asp:Label></span>
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                               <section class="form-group">
                                    <asp:Label ID="lblState" Text="Estado:" CssClass="control-label" runat="server"></asp:Label>                               
                                        <asp:DropDownList ID="cboState" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">Activo</asp:ListItem>
                                        </asp:DropDownList>                                                                                             
                                </section> <!-- End .form-group -->
                            </section>
                            <%--<section class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <div id="rolesAccordion" class="rolesAccordion" >
                                        <h3>Reglas de acceso</h3>
                                        <div class="listRules">
                                            <div class="rule">
                                                <div class="name">Administracción</div>
                                                <div class="description">En la sección de Administración, los usuarios tendran acceso a la manipulación de programas y periodos.</div>
                                            </div>
                                            <div class="rule">
                                                <div class="name">Seguridad</div>
                                                <div class="description">En este apartado se podran crear reglas de acceso al sistema y a la creación de nuevos usuarios y sus dependencias con el sistema.</div>
                                            </div>
                                            <div class="rule">
                                                <div class="name">Servicios</div>
                                                <div class="description">Los usuarios podran acceder a las listas de esperas y revisar o contactar a los clientes interesados en matricular algun curso.</div>
                                            </div>
                                            <div class="rule">
                                                <div class="name">Académico</div>
                                                <div class="description">En esta sección se gestionan la mayoria de las actividades administrativas y o gerenciales de los diferentes programas con los que cuenta el departamento DEAS.</div>
                                            </div>
                                            <div class="rule">
                                                <div class="name">Oferta Academica</div>
                                                <div class="description">En esta seccion es donde se diseñan los horarios y los cursos de cada programa y ademas se crea la oferta académica.</div>
                                            </div>
                                        </div>
                                    </div>
                                </section>
                            </section>--%>
                            <section class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <section class="form-group">
                                    <asp:CheckBoxList ID="chkModules" runat="server" CssClass="listCheckbox" RepeatDirection="Horizontal" >
                                        <asp:ListItem Value="1"> Administración</asp:ListItem>
                                        <asp:ListItem Value="2"> Seguridad</asp:ListItem>
                                        <asp:ListItem Value="3"> Servicios</asp:ListItem>
                                        <asp:ListItem Value="4"> Academico</asp:ListItem>
                                        <asp:ListItem Value="5"> Oferta Academica</asp:ListItem>
                                    </asp:CheckBoxList>
                                    <span class="message-has-error"><asp:Label ID="lblchkModulesMessage" Text="" runat="server"></asp:Label></span>  
                                </section> <!-- End .form-group -->
                            </section>
                            <section class="col-md-12 col-xs-12 col-sm-12 form-buttons">
                                <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                                <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                                <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                                <asp:ImageButton ID="btnReport" CssClass="image_align" ImageUrl="~/images/maintenance/report.png" ToolTip="Reporte" runat="server" OnClick="btnReport_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                            </section> <!-- End .form-buttons -->
                            <section class="col-md-12 message">
                                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                            </section> <!-- End .message -->
                        </fieldset> <!-- End fieldset -->
                    </section> <!--End .form -->
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvRole" CssClass="table" runat="server" AutoGenerateColumns="False"  AllowPaging="True" OnPageIndexChanging="gvRole_PageIndexChanging" PageSize="12" OnRowEditing="gvRole_RowEditing" OnRowDeleting="gvRole_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" ReadOnly="true" DataField="Role_Id" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Descripción" ReadOnly="true" DataField="Description" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Acceso" ReadOnly="true"  DataField="modulos" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Editar" ImageUrl="~/images/maintenance/edit.png" Text="Editar" DataTextField="Role_Id" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField HeaderText="Eliminar" ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" DataTextField="Role_Id" CommandName="Delete" ButtonType="Image" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                </Columns>
                            </asp:GridView>
                            <!-- End GridView -->
                        </section>
                    </section><!-- End .dataExists -->
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger  ControlID="btnReport"/>
                </Triggers> 
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
                            <p>¿Esta seguro de eliminar el Rol <strong><asp:Label ID="lblRoleName" Text="" runat="server"></asp:Label></strong>?</p>
                        </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm" OnClick="btnDelete_Click" ID="btnDelete" Text="Eliminar" runat="server"></asp:Button>
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </section>
                    </section>
                    <script type="text/javascript">
                        securityAccordion();
                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#security').addClass('isSelected');
    </script>
</asp:Content>

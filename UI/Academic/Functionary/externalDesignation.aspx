<%@ Page Title="Nombramiento externo - DEAS" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="externalDesignation.aspx.cs" Inherits="UI.Academic.Functionary.externalDesignation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Nombramientos Externos</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row create">
                        <p class="special-label col-md-12"><strong>Agregar nombramiento externo</strong></p>
                        <section class="col-md-12">
                            <section class="form-group">
                                <section class="form-group">
                                    <asp:Label runat="server" Visible="false" ID="lblCode" Text="Código"></asp:Label>
                                    <asp:TextBox runat="server" Visible="false" ID="txtCode"></asp:TextBox>
                                </section>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblFunctionary" Text="Funcionario:" runat="server"></asp:Label>
                                <asp:DropDownList ID="cboFunctionary" CssClass="form-control" runat="server" OnSelectedIndexChanged="cboFunctionary_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <asp:Label ID="lblMessageFunctionary" CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblPosition" Text="Cargo:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtPosition" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblWorkPlace" Text="Lugar de trabajo:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtWorkPlace" CssClass="form-control" runat="server" ></asp:TextBox>
                                <asp:Label CssClass="message-has-error help-block" runat="server"></asp:Label>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblStartDesignation" Text="Inicio del nombramiento:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtStartDesignation" CssClass="form-control" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="calendar" ID="txtStartDesignation_CalendarExtender" runat="server" TargetControlID="txtStartDesignation" />
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblEndDesignation" Text="Fin del nombramiento:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEndDesignation" CssClass="form-control" runat="server" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="calendar" ID="txtEndDesignation_CalendarExtender" runat="server" TargetControlID="txtEndDesignation" />
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblHoursDisignation" Text="Horas del nombramiento:" runat="server"></asp:Label>
                                <asp:DropDownList ID="cboHoursDisignation" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                        </section>
                        <p class="special-label col-md-12"><strong>Detalle del nombramiento</strong></p>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblDay" Text="Día:" runat="server"></asp:Label>
                                <asp:DropDownList ID="cboDay" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:Label ID="lblMessage" CssClass="message-has-error help-block" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblStart" Text="Hora de Inicio:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblEnd" Text="Hora final:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label CssClass="message-has-error" runat="server"></asp:Label>
                            </section>
                            <section class="form-group" style="text-align:left;">
                                
                                <%--<asp:ImageButton CssClass="image_align_2" ImageUrl="~/images/maintenance/add.png" runat="server" ID="btnAdd" ToolTip="Agregar" OnClick="btnAdd_Click1" /></td>--%>
                                <asp:Button CssClass="" runat="server" ID="btnAdd" ToolTip="Agregar" OnClick="btnAdd_Click" Text="Agregar" />
                            </section>
                        </section>
                       <section class="col-md-6 dataExists">
                           <asp:Label ID="lblMsjHours" Text="" runat="server" CssClass="message-has-error" /><br />
                           <section class="table-responsive">
                                <asp:GridView ID="grvDias" runat="server" AutoGenerateColumns="False" OnRowDeleting="grvDias_RowDeleting">
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="code" ></asp:BoundField>
                                        <asp:BoundField HeaderText="Dia" DataField="day"></asp:BoundField>
                                        <asp:BoundField HeaderText="Hora inicial" DataField="startHour" DataFormatString="{0:t}"></asp:BoundField>
                                        <asp:BoundField HeaderText="Hora final" DataField="FinishHour" DataFormatString="{0:t}"></asp:BoundField>
                                        <asp:ButtonField CommandName="Delete" ImageUrl="~/images/maintenances/delete.png" Text="Eliminar" ButtonType="Image" HeaderText="Eliminar" ControlStyle-ForeColor="Red"></asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                          </section>
                       </section><br /><br /><br />
                        <section class="col-md-12 form-buttons">
                                <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                                <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                                <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                        </section> <!-- End .form-buttons -->
                        <section class="col-md-12 message">
                            <asp:Label ID="Label1" runat="server" ></asp:Label>
                        </section> <!-- End .message -->
                        <section class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Button runat="server" ID="btnReport" Text="Reporte de designación externa" CssClass="pull-right reportButton" OnClick="btnReport_Click" />
                        </section>
                    </section>
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvExternalDesignation" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvExternalDesignation_PageIndexChanging" PageSize="12" OnRowDeleting="gvExternalDesignation_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" ReadOnly="true" DataField="code" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Profesor" ReadOnly="true" DataField="oTeacher.name" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Cédula" ReadOnly="true" DataField="oTeacher.id" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Cargo" ReadOnly="true" DataField="position" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Lugar de Trabajo" ReadOnly="true" DataField="location" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Fecha inicio" ReadOnly="true" DataFormatString="{0:d}" DataField="initial_day" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Fecha fin" ReadOnly="true" DataFormatString="{0:d}" DataField="final_day" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Horas" ReadOnly="true" DataField="hours" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
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
        </section> <!-- End .maintanance -->
    </section> <!-- End content_2 -->
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
                            <p>¿Esta seguro de eliminar el Nombramiento externo <strong><asp:Label ID="lblExternalDesignationDescription" Text="" runat="server"></asp:Label></strong>?</p>
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
    <!--out-->
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#academic').addClass('isSelected');
    </script>
</asp:Content>

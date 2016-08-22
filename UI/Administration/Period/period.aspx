<%@ Page Title="Periodo - DEAS" Language="C#" MasterPageFile="~/Administration/Administration.Master" AutoEventWireup="true" Culture="Auto" UICulture="Auto" CodeBehind="period.aspx.cs" Inherits="UI.Administration.period" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Combobox --->
    <link rel="stylesheet" type="text/css" media="screen" href="../libraries/bootstrap-combobox-master/css/bootstrap-combobox.css" />
    <script src="../libraries/bootstrap-combobox-master/js/bootstrap-combobox.js" type="text/javascript" ></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="true"></asp:ScriptManager>
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Periodos</p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <section class="col-md-12">
                            <section class="form-group">
                                <asp:Label Visible="false" ID="lblCode" Text="Código:" runat="server"></asp:Label>
                                <asp:TextBox Visible="false" ID="txtCode" runat="server"></asp:TextBox>
                            </section><!-- End .form-group -->
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblDescription"  Text="Descripción:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" Text="" ID="lblMessageDescription" ></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblModality" Text="Modalidad:" runat="server"></asp:Label>
                                <asp:DropDownList ID="cboModality" CssClass="combobox form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" Text="" ID="lblMessageModality" ></asp:Label></span>
                            </section><!-- End .form-group -->
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblStartDate" Text="Fecha Inicial:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="calendar" ID="txtStartDate_CalendarExtender" runat="server" TargetControlID="txtStartDate" />
                                <span class="message-has-error"><asp:Label runat="server" Text="" ID="lblMessageStartDate" ></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblFinishDate" Text="Fecha final:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtFinishDate" CssClass="form-control" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="calendar" ID="txtFinishDate_CalendarExtender" runat="server" TargetControlID="txtFinishDate" />
                                <span class="message-has-error"><asp:Label runat="server" Text="" ID="lblMessageFinalDate" ></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-3"></section>
                        <section class="col-md-6 form-horizontal">
                            <section class="form-group">
                                <asp:Label ID="lblState" Text="Estado:" CssClass="control-label col-sm-2" runat="server"></asp:Label>
                                <section class="col-sm-10">
                                    <asp:DropDownList ID="cboState" CssClass="form-control col-sm-10" runat="server">
                                    <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">Activo</asp:ListItem>
                                </asp:DropDownList>
                                </section>
                            </section>
                        </section>
                        <section class="col-md-3"></section>
                        <section class="col-md-12 form-buttons">
                                <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                                <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click"  />
                                <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                                <asp:ImageButton ID="btnReport" CssClass="image_align" ImageUrl="~/images/maintenance/report.png" ToolTip="Reporte" runat="server" OnClick="btnReport_Click" />
                                <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click"  />
                        </section> <!-- End .form-buttons -->
                        <section class="col-md-12 message">
                            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                        </section> <!-- End .message -->
                    </section> <!-- End .form -->
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvPeriod" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvPeriod_PageIndexChanging" PageSize="12" OnRowEditing="gvPeriod_RowEditing" OnRowDeleting="gvPeriod_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" DataField="code" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Descripción" DataField="name" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Modalidad" DataField="oPeriodType.description" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Fecha Inicial" DataField="startDate" ReadOnly="true" DataFormatString="{0:d}" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField HeaderText="Fecha Final " DataField="finalDate" ReadOnly="true" DataFormatString="{0:d}" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Edit" ImageUrl="~/images/maintenance/edit.png" Text="Edit" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
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
    <section class="modal fade" id="confirmMessage" role="dialog" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <p>¿Esta seguro de eliminar el Periodo <strong><asp:Label ID="lblPeriodDescription" Text="" runat="server"></asp:Label></strong>?</p>
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
        function comboBox() {
            $(document).ready(function () {
                $('.combobox').combobox()
            });
        }
    </script>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#administration').addClass('isSelected');
    </script>
</asp:Content>

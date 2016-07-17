<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="UI.Academic.AcademicOffer.schedule" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <section class="content_2">
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Horarios</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <section class="form row">
                        <section class="col-md-12">
                            <section class="form-group">
                                <asp:Label runat="server" Visible="false" ID="lblCode" Text="Código"></asp:Label>
                                <asp:TextBox runat="server" Visible="false" ID="txtCode"></asp:TextBox>
                            </section> <!-- End .form-group -->
                        </section> <!-- End .col-md-12 -->          
                        <section class="col-md-12">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblbDays" Text="Días: "></asp:Label>
                               <asp:CheckBoxList ID="chkld" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="621px" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                                    <asp:ListItem Value="1"> Lunes</asp:ListItem>
                                    <asp:ListItem Value="2"> Martes</asp:ListItem>
                                    <asp:ListItem Value="3"> Miercoles</asp:ListItem>
                                    <asp:ListItem Value="4"> Jueves</asp:ListItem>
                                    <asp:ListItem Value="5"> Viernes</asp:ListItem>
                                    <asp:ListItem Value="6"> Sabado</asp:ListItem>
                                    <asp:ListItem Value="7"> Domingo</asp:ListItem>
                               </asp:CheckBoxList>
                            </section> <!-- End .form-group -->
                        </section> <!-- End .col-md-12 -->
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblDescription" Text="Descripción:"></asp:Label>
                                <asp:TextBox ID="txtDescription" Enabled="false" CssClass="form-control" runat="server" ></asp:TextBox>
                                <span class="message-has-error" ><asp:Label runat="server" ID="lblMessageDescription"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblTypeSchedule" Text="Tipo de Horario:"></asp:Label>
                                <asp:DropDownList ID="cboTypeSchedule" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Mañana">Mañana</asp:ListItem>
                                    <asp:ListItem Value="Tarde">Tarde</asp:ListItem>
                                    <asp:ListItem Value="Noche">Noche</asp:ListItem>
                                </asp:DropDownList>
                                <span class="message-has-error" ><asp:Label runat="server" ID="lblMessageTypeSchedule"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblStartHour" Text="Hora Inicial:"></asp:Label>
                                <asp:TextBox ID="txtStart" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="txtStart_MaskedEditExtender" AcceptAMPM="True" Mask="99:99" MaskType="Time" runat="server" TargetControlID="txtStart" />                              
                                <span class="message-has-error" ><asp:Label runat="server" ID="lblMessageStartHour"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblEndHour" Text="Hora Final:"></asp:Label>
                                <asp:TextBox ID="txtEndHour" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="txtEndHour_MaskedEditExtender" AcceptAMPM="True" Mask="99:99" MaskType="Time" runat="server" TargetControlID="txtEndHour" />
                                <span class="message-has-error" ><asp:Label runat="server" ID="lblMessageEndHour"></asp:Label></span>
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
                    </section> <!-- End .form -->
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvSchedule" runat="server" AutoGenerateColumns="False" OnRowEditing="gvSchedule_RowEditing" OnRowDeleting="gvSchedule_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="code" HeaderText="Código" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"  />
                                    <asp:BoundField DataField="name" HeaderText="Descripción" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"  />
                                    <asp:BoundField DataField="typeSchedule" HeaderText="Horario" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"  />
                                    <asp:BoundField DataField="startTime" HeaderText="Hora Inicio"  DataFormatString="{0:t}" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                    <asp:BoundField DataField="endTime" HeaderText="Hora Final"  DataFormatString="{0:t}" ReadOnly="true" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"  />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Editar" ImageUrl="~/images/maintenance/edit.png" Text="Editar" CommandName="Edit" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"  />
                                    <asp:ButtonField ButtonType="Image" HeaderText="Eliminar" ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" CommandName="Delete" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs"  />
                                </Columns>
                            </asp:GridView>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
    <!-- Delete PopUp -->
    <section class="modal fade" id="confirmMessage" role="dialog" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <p>¿Esta seguro de eliminar el horario <strong><asp:Label ID="lblScheduleDescription" Text="" runat="server"></asp:Label></strong>?</p>
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

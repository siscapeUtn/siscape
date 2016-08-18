<%@ Page Title="Lista de espera - SISCAPE" Language="C#" MasterPageFile="~/Services/WaitingList.Master" AutoEventWireup="true" CodeBehind="Waiting_list.aspx.cs" Inherits="UI.Services.Waiting_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% showOService(); %>
    <section class="content_2">
        <section class="maintanance row">
            <p class="title">Administraci&oacute;n de listas de espera</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                        <section class="col-md-12">
                            <section class="form-group">
                                <asp:Label runat="server" Visible="false" ID="lblCode" Text="Código"></asp:Label>
                                <asp:TextBox runat="server" Visible="false" ID="txtCode"></asp:TextBox>
                            </section>
                        </section>
                        <section class="col-md-12">
                            <section class="form-group">
                                <asp:Label runat="server" Visible="true" ID="lblCourse" ></asp:Label>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblId" Text="Identificación:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtId" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error">
                                    <asp:Label runat="server" ID="lblMessageId" ToolTip="Debe digitar una cédula correcta menor o igual a 15 digitos"></asp:Label ></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblName" Text="Nombre:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error">
                                    <asp:Label runat="server" ID="lblMessageName" ToolTip="Debe digitar el nombre correctamente sin números o caracteres especiales"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblLastName" Text="Apellidos:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error">
                                    <asp:Label runat="server" ID="lblMessageLastName" ToolTip="Debe digitar el apellido correctamente sin números o caracteres especiales"></asp:Label></span>
                            </section>
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label ID="lblHomePhone" Text="Teléfono Residencial:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtHomePhone" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error">
                                    <asp:Label runat="server" ID="lblMessageHomePhone" ToolTip="Debe digitar un número de teléfono sin letras o caracteres especiales(Permitidos: -, 506+)"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblCellPhone" Text="Teléfono Celular:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtCellPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error">
                                    <asp:Label runat="server" ID="lblMessageCellPhone" ToolTip="Debe digitar un número de teléfono celular correctamente sin letras o caracteres especiales(Permitidos: -, 506+)"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label ID="lblEmail" Text="Correo Electrónico:" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="message-has-error">
                                    <asp:Label runat="server" ID="lblMessageEmail" ToolTip="Debe digitar una dirección de correo valida"></asp:Label></span>
                            </section>
                        </section>
                        
                           
                                <%-- dropdownlist program--%>

                                <%--<section class="form-group">
                                    <asp:Label ID="lblProgram" CssClass="control-label col-sm-2" Text="Programa:" runat="server"></asp:Label>
                                    <section class="col-sm-10">
                                        <asp:DropDownList ID="cboProgram" CssClass="form-control col-sm-4" runat="server" OnSelectedIndexChanged="cboProgram_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </section>
                                </section>
                                <span class="message-has-error"></span>--%>
                                <%-- dropdownlist course--%>
                                <%--<section class="form-group">
                                    <asp:Label ID="lblCourse" CssClass="control-label col-sm-2" Text="Curso:" runat="server"></asp:Label>
                                    <section class="col-sm-10">
                                        <asp:DropDownList ID="cboCourse" CssClass="form-control col-sm-4" runat="server">
                                        </asp:DropDownList>
                                    </section>
                                </section>
                                <span class="message-has-error"></span>--%>
                                <%-- dropdownlist schedule--%>
                                <%--<section class="form-group">
                                    <asp:Label ID="lblSchedule" CssClass="control-label col-sm-2" Text="Horario:" runat="server"></asp:Label>
                                    <section class="col-sm-10">
                                        <asp:DropDownList ID="cboSchedule" CssClass="form-control col-sm-4" runat="server">
                                        </asp:DropDownList>
                                    </section>
                                </section>
                                <span class="message-has-error"></span>
                                <section>
                                    <asp:TextBox ID="txtDays" runat="server" Visible="false"></asp:TextBox>
                                </section>
                            </section>--%>
                            <%--<section>
                                <section class="form-group">
                                    <asp:CheckBoxList CssClass="list-group-item " ID="chkDays" runat="server" RepeatDirection="Horizontal" Width="370px" RepeatLayout="Flow" CellPadding="5" CellSpacing="5" AutoPostBack="true" OnSelectedIndexChanged="chkDays_SelectedIndexChanged">
                                        <asp:ListItem  Value="Lunes">Lunes&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Martes">Martes&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Miercoles">Miercoles&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Jueves">Jueves&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Viernes">Viernes&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Sabado">Sabado&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="Domingo">Domingo</asp:ListItem>
                                    </asp:CheckBoxList>
                                </section>
                            </section>--%>
                       <%-- </section>
                        <br />
                        <section>
                            <asp:ImageButton CssClass="image_align" ImageUrl="~/images/maintenance/add.png" runat="server" ID="btnAdd" ToolTip="Agregar" OnClick="btnAdd_Click1" /></td>
                        </section>--%>

                      <%--  <section class="form row">
                            <section class="dataExists">
                                <section class="table-responsive">
                                    <asp:GridView ID="grvCourse" runat="server" AutoGenerateColumns="False" OnRowDeleting="grvCourse_RowDeleting">
                                        <Columns>
                                            <asp:BoundField HeaderText="Código de control" DataField="code" ></asp:BoundField>
                                            <asp:BoundField HeaderText="Curso" DataField="des"></asp:BoundField>
                                            <asp:BoundField HeaderText="Programa" DataField="prog"></asp:BoundField>
                                            <asp:BoundField HeaderText="Horario" DataField="sch"></asp:BoundField>
                                            <asp:BoundField HeaderText="Dias tentativos" DataField="day"></asp:BoundField>
                                            <asp:ButtonField CommandName="Delete" ImageUrl="~/images/maintenances/delete.png" Text="Eliminar" ButtonType="Image" HeaderText="Eliminar" ></asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                </section>
                            </section>
                        </section>--%>
                        <section class="col-md-3"></section>
                        <section class="col-md-9 form-buttons buttonsWaiting">
                                <asp:Button Text="Enviar" ToolTip="Agregar a lista de espera" ID="Save" runat="server" OnClick="btnSave" CssClass="btnWaitingList"  />
                                <asp:Button  ToolTip="Cancelar" ID="Cancel" runat="server" Text="Cancelar" OnClick="btn_Cancel" CssClass="btnWaitingList" />    
                        </section>
                       
                        <!-- End .form-buttons -->
                        <section class="col-md-12 message">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </section>
                        <!-- End .message -->
                    

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
                            <p>
                                ¿Esta seguro de eliminar el Funcionario <strong>
                                    <asp:Label ID="lblFunctionaryDescription" Text="" runat="server"></asp:Label></strong>?
                            </p>
                        </section>
                        <section class="modal-footer">
                            <asp:Button CssClass="btn btn-confirm"  ID="btnDelete" Text="Eliminar" runat="server"></asp:Button>
                            <button class="btn btn-confirm" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>

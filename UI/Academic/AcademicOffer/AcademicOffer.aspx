<%@ Page Title="" Language="C#" MasterPageFile="~/Academic/Academic.Master" AutoEventWireup="true" CodeBehind="AcademicOffer.aspx.cs" Inherits="UI.Academic.AcademicOffer.AcademicOffer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content_2">
        <% showOfferAcademic(); %>
        <section class="maintanance">
            <p class="title">Administraci&oacute;n de Oferta Academica</p>
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
                                <asp:Label runat="server" ID="lblPeriod" Text="Periodo:"></asp:Label>
                                <asp:DropDownList ID="cboPeriod" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessagePeriod"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblCourse" Text="Curso:"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkEspecial" Text=" &nbsp;&nbsp;Especial" runat="server" AutoPostBack="true" OnCheckedChanged="chkEspecial_CheckedChanged" />
                                <asp:DropDownList ID="cboCourse" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageCourse"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblSchedule" Text="Horario:"></asp:Label>
                                <asp:DropDownList ID="cboSchedule" CssClass="form-control"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboSchedule_SelectedIndexChanged"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMesageSchedule"></asp:Label></span>
                            </section> 
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblTeacher" Text="Profesor:"></asp:Label>
                                <asp:DropDownList ID="cboTeacher" CssClass="form-control"  runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageTeacher"></asp:Label></span>
                            </section> 
                        </section>
                        <section class="col-md-6">
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblProgram" Text="Programa:"></asp:Label>
                                <asp:DropDownList ID="cboProgram" CssClass="form-control" runat="server" OnSelectedIndexChanged="cboProgram_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageProgram"></asp:Label></span>
                            </section>
                            <section class="form-group">
                                <asp:Label runat="server" ID="lblPrice" Text="Precio:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control"></asp:TextBox>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessagePrice"></asp:Label></span>  
                            </section>
                            <section class="form-group">
                            <asp:Label runat="server" ID="lblRoom" Text="Aula:"></asp:Label>
                                <asp:DropDownList ID="cboRoom" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageRoom"></asp:Label></span>
                            </section>
                            <section class="form-group">
                            <asp:Label runat="server" ID="lblHours" Text="Cantidad de Horas:"></asp:Label>
                                <asp:DropDownList ID="cboHours" CssClass="form-control" runat="server"></asp:DropDownList>
                                <span class="message-has-error"><asp:Label runat="server" ID="lblMessageHours"></asp:Label></span>
                            </section>
                         </section>
                 
                       <section class="col-md-12 form-buttons">
                            <asp:ImageButton ID="btnNew" CssClass="image_align" ImageUrl="~/images/maintenance/add.png" ToolTip="Nuevo" runat="server" OnClick="btnNew_Click" />
                            <asp:ImageButton ID="btnSave" CssClass="image_align" ImageUrl="~/images/maintenance/save.png" ToolTip="Guardar" runat="server" OnClick="btnSave_Click" />
                            <asp:ImageButton ID="btnCancel" CssClass="image_align" ImageUrl="~/images/maintenance/cancel.png" ToolTip="Cancelar" runat="server" OnClick="btnCancel_Click" />
                            <asp:ImageButton ID="btnReturn" CssClass="image_align" ImageUrl="~/images/maintenance/return.png" ToolTip="Regresar" runat="server" OnClick="btnReturn_Click" />
                        </section> <!-- End .form-buttons -->
                        <section class="col-md-12 message">
                            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                        </section>
                    </section>
                    <section class="dataExists">
                        <section class="table-responsive">
                            <asp:GridView ID="gvAcademicOffer" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvAcademicOffer_RowDeleting" AllowPaging="True" >
                                <Columns>
                                    <asp:BoundField HeaderText="C&#243;digo" DataField="code" ReadOnly="true" ></asp:BoundField>
                                    <asp:BoundField HeaderText="Curso" DataField="oCourse.description" ReadOnly="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Profesor">
                                        <ItemTemplate>
                                            <%# Eval("oteacher.name").ToString() + ' ' +  Eval("oteacher.lastName").ToString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Horario">
                                        <ItemTemplate>
                                            <%# Eval("oSchedule.name").ToString() + ' ' +  Eval("oSchedule.typeSchedule").ToString() %>
                                        </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField HeaderText="Aula" DataField="oClassRoom.num_room" ReadOnly="true"></asp:BoundField>
                                   <asp:BoundField HeaderText="Precio" DataField="price"  DataFormatString="{0:C}" ReadOnly="true"></asp:BoundField>
                                   <asp:ButtonField ImageUrl="~/images/maintenance/delete.png" Text="Eliminar" ButtonType="Image" HeaderText="Eliminar" CommandName="Delete"></asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </section>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
    <!-- Delete modal -->
    <section class="modal fade" id="confirmMessage" role="dialog" data-keyboard="false" data-backdrop="static" aria-labelledby="confirmMessageLabel" aria-hidden="true">
        <section class="modal-dialog">
            <asp:UpdatePanel ID="confirmModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <section class="modal-content">
                        <section class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <asp:Label ID="modalHeader" Text="Mensaje de confirmación" runat="server"></asp:Label>
                        </section>
                        <section class="modal-body">
                            <p>¿Esta seguro de eliminar la oferta Academica <strong><asp:Label ID="lblAcademicOfferDescription" Text="" runat="server"></asp:Label></strong>?</p>
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

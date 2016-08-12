<%@ Page Title="" Language="C#" MasterPageFile="~/Services/WaitingList.Master" AutoEventWireup="true" CodeBehind="Report_waiting_list.aspx.cs" Inherits="UI.Services.Report_waiting_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% showOService(); %>
    <section class="content_3">
        <section class="maintanance">
            <p class="title">Clientes en lista de espera</p>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <span class="span-report-waiting-list">
                        <section class="col-md-12">
                            <section class="form-group">
                                <asp:Label ID="lblFilter" CssClass="control-label col-sm-1" Text="Filtrar estudientes:" runat="server"></asp:Label>
                                <section class="col-sm-2">
                                    <asp:DropDownList ID="cboFilter" CssClass="form-control col-sm-4" runat="server" OnSelectedIndexChanged="cboFilter_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </section>
                            </section>
                        </section>
                    </span>
                    <span class="col-lg-offset-12">
                        <section class="dataExists">
                            <section class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" ReadOnly="true" DataField="code" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Identificación" ReadOnly="true" DataField="id" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Nombre" ReadOnly="true" DataField="name" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Teléfono Celular" ReadOnly="true" DataField="cellPhone" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Teléfono Recidencial" ReadOnly="true" DataField="phone" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Correo electrónico" ReadOnly="true" DataField="mail" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Curso" ReadOnly="true" DataField="course" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Días tentativos" ReadOnly="true" DataField="description" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:BoundField HeaderText="Horario" ReadOnly="true" DataField="schedule" ItemStyle-CssClass="visible-lg visible-md visible-sm visible-xs" />
                                        <asp:TemplateField HeaderText="Contactado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="contacted" runat="server" Checked='<%# bool.Parse(Eval("contacted").ToString()) %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </section>
                            
                        </section>
                        <section class="btn-group-lg">
                                <asp:Button ID="Button1" Text="Actualizar" runat="server" OnClick="Btn_Update"/>
                        </section>
                    </span>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>

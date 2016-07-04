<%@ Page Title="Contáctenos | DEAS" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="contactUs.aspx.cs"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content_2">
        <section class="maintanance">
            <p class="title">Cont&aacute;ctenos</p>
            <p class="subtitle">Dejenos su mensaje</p>
            <section class="form row">
                <section class="col-md-7 form-horizontal">
                    <section class="form-group">
                        <asp:Label ID="lblName" CssClass="control-label col-sm-4" Text="Nombre completo:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtName" CssClass="form-control col-sm-8" runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Label ID="lblEmail" CssClass="control-label col-sm-4" Text="Correo Electrónico:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtEmail" CssClass="form-control col-sm-8" runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Label ID="lblPhone" CssClass="control-label col-sm-4" Text="Teléfono:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtPhone" CssClass="form-control col-sm-8" runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Label ID="lblMessage" CssClass="control-label col-sm-4" Text="Mensaje:" runat="server"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtMessage" CssClass="form-control col-sm-8" TextMode="MultiLine" Rows="5" Columns="10"  runat="server"></asp:TextBox>
                        </div>
                    </section>
                    <section class="form-group">
                        <asp:Button runat="server" Text="Enviar" CssClass="pull-right form-button" ToolTip="Enviar el mensaje" />
                    </section>
                </section>
            </section>
            <p class="subtitle">Números de contacto</p>
            <section id="contactUs-accordion" class="accordion">
                <h3>Programa de Idiomas</h3>
                <div class="contact-container">
                    <div class="contact_info">
                        <p class="subtitle">Programa de Idiomas</p>
                        <p>Télefono: 2435-5000 ext:1193 </p>
                        <p>Correo electrónico: programaidiomas@utn.ac.cr</p>
                    </div>
                    <div class="contact_info">
                        <p class="subtitle">Jefe</p>
                        <p>Nombre: Álvaro Salas Parra</p>
                        <p>Télefono: 2435-5000 ext:1191</p>
                        <p>Correo electrónico: asalas@utn.ac.cr</p>
                    </div>
                    <div class="contact_info">
                        <p class="subtitle">Asistente</p>
                        <p>Nombre: Jhonatan Rojas Campos</p>
                        <p>Télefono: 2435-5000 ext:1192</p>
                        <p>Correo electrónico: jrojas@utn.ac.cr</p>
                    </div>
                    <div class="contact_info">
                        <p class="subtitle">Secretaria</p>
                        <p>Nombre: Erika Aguero Ledezma</p>
                        <p>Télefono: 2435-5000 ext:1193</p>
                        <p>Correo electrónico: eaguero@utn.ac.cr</p>
                    </div>
                </div>
                <h3>Programa de Tecnología Informática y Comunicación (TIC'S)</h3>
                <div class="contact-container">
                    <div class="contact_info">
                        <p class="subtitle">Programa de Tecnología Informática y Comunicación (TIC'S)</p>
                        <p>Télefono: 2430-4050 exts:215 o 216</p>
                        <p>Correo electrónico: cetics@utn.ac.cr</p>
                    </div>
                </div>
                <h3>Programas Técnicos y de Acción Social</h3>
                <div class="contact-container">
                    <div class="contact_info">
                        <p class="subtitle">Director</p>
                        <p>Nombre: Marco Tulio López Duran</p>
                        <p>Télefono: 2435-5000 ext:1076</p>
                        <p>Correo electrónico: mlopez@utn.ac.cr</p>
                    </div>
                    <div class="contact_info">
                        <p class="subtitle">Encargada de Cursos Libres</p>
                        <p>Nombre: Seidy Serrano Vargas</p>
                        <p>Télefono: 2435-5000 ext:1077</p>
                        <p>Correo electrónico: serrano@utn.ac.cr</p>
                    </div>
                    <div class="contact_info">
                        <p class="subtitle">Encargada de Programas Técnicos</p>
                        <p>Nombre: Iriabel Madrigal Soto</p>
                        <p>Télefono: 2435-5000 ext:1078</p>
                        <p>Correo electrónico: imadrigal@utn.ac.cr</p>
                    </div>
                    <div class="contact_info">
                        <p class="subtitle">Encargado de Asistencia Técnica</p>
                        <p>Nombre: Marvin Torres Hernández</p>
                        <p>Télefono: 2435-5000 ext:1079</p>
                        <p>Correo electrónico: mtorres@utn.ac.cr</p>
                    </div>
                    <div class="contact_info">
                        <p class="subtitle">Encargada de Acción Social</p>
                        <p>Nombre: Ileana Cartín Guerrero</p>
                        <p>Télefono: 2435-5000 ext:1080</p>
                        <p>Correo electrónico: icartin@utn.ac.cr</p>
                    </div>
                </div>
            </section>
        </section>
    </section>
    <script type="text/javascript">
        accordion();
    </script>
</asp:Content>

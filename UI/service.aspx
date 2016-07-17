<%@ Page Title="Servicios | DEAS" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="service.aspx.cs"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section class="service">

        <section class="listCourse">
            <section class="course">
                <div class="name">Inglés Regular 1-2</div>
                <div class="schedule">Lunes - Martes de 6:00 p.m. a 9:30 p.m.</div>
                <div class="price">Precio: &#162; 61.360</div>
                <asp:Button runat="server" ID="btnInteresado" CssClass="btnService" Text="Estoy interesado" />
            </section>
            <section class="course">
                <div class="name">Portugués 1-2</div>
                <div class="schedule">Lunes - Martes de 6:00 p.m. a 9:30 p.m.</div>
                <div class="price">Precio: &#162; 51.480</div>
                <asp:Button runat="server" ID="btnInteresado2" CssClass="btnService" Text="Estoy interesado" />
            </section>
            <section class="course">
                <div class="name">Inglés Regular 3-4</div>
                <div class="schedule">Lunes - Martes de 6:00 p.m. a 9:30 p.m.</div>
                <div class="price">Precio: &#162; 61.360</div>
                <asp:Button runat="server" ID="Button1" CssClass="btnService" Text="Estoy interesado" />
            </section>
            <section class="course">
                <div class="name">Portugués 3-4</div>
                <div class="schedule">Lunes - Martes de 6:00 p.m. a 9:30 p.m.</div>
                <div class="price">Precio: &#162; 51.480</div>
                <asp:Button runat="server" ID="Button2" CssClass="btnService" Text="Estoy interesado" />
            </section>
        </section>
    </section>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#service').addClass('isSelected');
    </script>
</asp:Content>

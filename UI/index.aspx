<%@ Page Title="Inicio - DEAS" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="UI.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/jquery.cycle.all.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="slider">
        <img alt="" src="images/slideshow/1.jpg"/>
        <img alt="" src="images/slideshow/2.jpg""/>
        <img alt="" src="images/slideshow/3.jpg""/>
        <img alt="" src="images/slideshow/4.jpg""/>
    </section> <!-- end .slider -->
    <!-- Slider function -->
    <script type="text/javascript">
        slideshow();
    </script>
</asp:Content>

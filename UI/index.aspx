﻿<%@ Page Title="Inicio - DEAS" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="UI.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/jquery.cycle2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="slider">
        <% printSlider();  %>
    </section> <!-- end .slider -->
    <!-- Slider function -->
    <script type="text/javascript">
        slideshow();
    </script>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#index').addClass('isSelected');
    </script>
</asp:Content>

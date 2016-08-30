<%@ Page Title="Servicios | DEAS" Language="C#" MasterPageFile="~/Services/WaitingList.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script runat="server">
    //this method doesn´t work in codeBehind
        protected void programs()
        {
            List<Entities.Program> listProgram = new List<Entities.Program>();

            List<Entities.AcademicOffer> listAcademicOffer = new List<Entities.AcademicOffer>();

            //This line brings the list of programs to show them.
            listProgram = BLL.ProgramBLL.getInstance().getAll();

            if (listProgram.Count == 0) {
                Response.Write("<section class=''><h2>No hay ofertas registradas</h2></section>");
            }
            else
            {
                foreach (Entities.Program program in listProgram)
                {
                    Response.Write("<h3 class='subtitle'>" + program.name + "</h3><div class='listCourse'>");

                    //This line brings the list of courses by program to show them
                    listAcademicOffer = BLL.AcademicOfferBLL.getInstance().getAcademicOfferByProgram(program.code);
                    string format = "0.00";

                    foreach (Entities.AcademicOffer offer in listAcademicOffer)
                    {
                        string price = offer.price.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
                        Response.Write(
                        "<section class='course'>" +
                        "</form><form  method='POST' action='Waiting_list.aspx'>" +
                        "<div class='name'>" + offer.oCourse.description + "</div><br/>" +
                        "<div class='days'>Días: " + offer.oSchedule.name.TrimEnd(',') + "</div>" +
                        "<div class='price'>Precio: " + price + "</div>" +
                        "<input type='hidden' name='idOffer' value='" + offer.code + "' runat='server' />" +
                        "<input type='hidden' name='nameCourse' value='" + offer.oCourse.description + "' runat='server' />" +
                        "<input type='submit' class='btnService' value='Estoy interesado' runat='server'/>" +
                        "</form>" +
                        "</section>");
                    }
                    Response.Write("</div>");
                }
            }
        }
                
    </script>
    <div class="listCourse">
        <div class="programAccordion">
           <% programs(); %>
        </div>
    </div>

    

    <script type="text/javascript">
        offerAccordion();
    </script>
    <script type="text/javascript">
        $('li').removeClass('isSelected');
        $('#service').addClass('isSelected');
    </script>
    <script type="text/javascript">
        function SendCourse() {
            var course = document.getElementsByName('')
            sessionStorage["course", ] = document.getElementById('');
        }
    </script>
</asp:Content>

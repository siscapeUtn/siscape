<%@ Page Title="Servicios | DEAS" Language="C#" MasterPageFile="~/Services/WaitingList.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script runat="server">
        protected void programs()
        {
            List<Entities.Program> listProgram = new List<Entities.Program>();

            List<Entities.Course> listCourse = new List<Entities.Course>();
            
            //This line brings the list of programs to show them.
            listProgram = BLL.ProgramBLL.getInstance().getAll();

            foreach (Entities.Program program in listProgram)
            {
                Response.Write("<h3 class='subtitle'>" + program.name + "</h3><div class='listCourse'>");

                //This line brings the list of courses by program to show them.
                listCourse = BLL.CourseBLL.getInstance().getAllActivedCourseProgram(program.code);

                foreach (Entities.Course course in listCourse)
                {
                    Response.Write(
                    "<section class='course'>"+
                    "</form><form  method='POST' action='Waiting_list.aspx'>" +
                    "<div class='name'>" + course.description + "</div>" +
                    "<input type='hidden' name='idCourse' value='" + course.id + "' runat='server' />" +
                    "<input type='hidden' name='nameCourse' value='" + course.description + "' runat='server' />" +
                    "<input type='submit' class='btnService' value='Estoy interesado' runat='server'/>" +
                    "</form>" +
                    "</section>");
                }
                Response.Write("</div>");
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

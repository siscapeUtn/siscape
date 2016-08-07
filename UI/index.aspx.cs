using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void printSlider()
        {
            List<Slider> listSlider = SliderBLL.getInstance().getAll();

            if (listSlider.Count > 0)
            {
                String strList = "";

                foreach( Slider oSlider in listSlider ){
                    strList += "<img alt='" + oSlider.description  + "' src='" + oSlider.image  + "'/>"; 
                }
                Response.Write(strList);
            }
            else
            {
                displayDefaultOptions();
            }

        }

        private void displayDefaultOptions()
        {
            Response.Write("<img alt='' src='images/slideshow/1.jpg'/>" +
                            "<img alt='' src='images/slideshow/2.jpg'/>" +
                            "<img alt='' src='images/slideshow/3.jpg'/>" + 
                            "<img alt='' src='images/slideshow/4.jpg'/>");
        }
    }
}
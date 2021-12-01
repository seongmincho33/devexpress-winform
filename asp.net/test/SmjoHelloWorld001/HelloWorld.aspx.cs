using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmjoHelloWorld001
{
    public partial class HelloWorld : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.TextArea1.InnerText = "Hello~!";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.TextArea1.InnerText = "안녕?";
        }
    }
}
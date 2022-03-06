using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmjoHelloWorld001
{
    public partial class FrmResponseRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGoogle_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://www.google.com/");
        }
        protected void btnNaver_Click(object sender, EventArgs e)
        {
            string url = "http://www.naver.com/";
            Response.Redirect(url);
        }
        protected void btnYouTube_Click(object sender, EventArgs e)
        {
            string url = String.Format(
                "http://{0}/{1}"
                , "yt3.ggpht.com"
                , "yti/APfAmoFnlBcMY-O75AkJXEr6J26SXxUKVus6tVm0PQ=s88-c-k-c0x00ffffff-no-rj-mo");
            Response.Redirect(url);

        }
    }
}
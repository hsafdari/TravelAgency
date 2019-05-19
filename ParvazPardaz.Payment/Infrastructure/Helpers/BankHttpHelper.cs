using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace ParvazPardaz.Payment.Infrastructure.Helpers
{
    /// <summary>
    /// ساخت فرم و ارسال به صفحه ی بانک مورد نظر
    /// </summary>
    public static class BankHttpHelper
    {
        #region PreparePOSTForm
        /// <summary>
        /// ایجاد فرم اچ.تی.ام.ال که اطلاعات هیدن-فیلد رو با جاوااسکریپت سابمیت می کنه
        /// </summary>
        /// <param name="destinationUrl">آدرس صفحه ی بانک</param>
        /// <param name="formData">مجموعه ای از اطلاعات داخل فرم مورد نظر که ارسال خواهد شد</param>
        /// <returns>فرم حاوی دیتا و اسکریپت ارسال</returns>
        public static String PreparePOSTForm(string destinationUrl, NameValueCollection formData)
        {
            //شناسه ی فرم اچ.تی.ام.ال
            string formID = "PostForm";

            //ساخت فرم با اطلاعاتی که قراره پست بشه
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + destinationUrl + "\" method=\"POST\">");
            foreach (string key in formData)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + formData[key] + "\">");
            }
            strForm.Append("</form>");

            //ساخت اسکریپت جاوااسکریپت که قراره فرم رو سابمیت کنه
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //بازگرداندن فرم اچ.تی.ام.الی که حاوی دیتاهای هیدن فیلده؛
            //و در انتهای آن اسکریپت سابمیتش قرار داره 
            return strForm.ToString() + strScript.ToString();
        }
        #endregion

        #region POSTAndRedirect
        /// <summary>
        /// پست دیتا و هدایت به آدرس بانک ، با استفاده از کلاس پیج
        /// </summary>
        /// <param name="page">شی ای از کلاس پیج که صفحات ای.اس.پی رو ارایه می ده</param>
        /// <param name="destinationUrl">آدرس صفحه ی بانک</param>
        /// <param name="formData">مجموعه ای از اطلاعات داخل فرم مورد نظر که ارسال خواهد شد</param>
        public static void POSTAndRedirect(Page page, string destinationUrl, NameValueCollection formData)
        {
            //آماده سازی فرم اچ.تی.ام.ال
            string strForm = PreparePOSTForm(destinationUrl, formData);

            //استفاده از لیترال-کنترل برای ارایه صفحه ی ای.اس.پی.دات.نت ، که سمت سرور نیاز به پردازش نداره
            page.Controls.Add(new LiteralControl(strForm));
        }
        #endregion
    }
}

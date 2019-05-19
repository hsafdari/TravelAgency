using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public class BaseComponent : IHtmlString
    {
        #region Ctor
        public BaseComponent()
        {
        }
        #endregion

        #region Properties
        protected string name { get; set; }
        protected object htmlAttribute { get; set; }
        protected HtmlHelper htmlHelper { get; set; }
        #endregion

        #region Methods
        public string ToHtmlString()
        {
            return ToString();
        }
        #endregion
    }
}

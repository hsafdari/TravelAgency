using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;

namespace ParvazPardaz.Common.Extension
{
    public static class ControllerExtension
    {
        #region RenderViewToString
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>Result</returns>
        public static string RenderPartialViewToString(this ControllerBase controller)
        {
            return RenderPartialViewToString(controller, null, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public static string RenderPartialViewToString(this ControllerBase controller, string viewName)
        {
            return RenderPartialViewToString(controller, viewName, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderPartialViewToString(this ControllerBase controller, object model)
        {
            return RenderPartialViewToString(controller, null, model);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderPartialViewToString(this ControllerBase controller, string viewName, object model)
        {
            if (viewName.IsEmpty())
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                ThrowIfViewNotFound(viewResult, viewName);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller)
        {
            return RenderViewToString(controller, null, null, null);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller, object model)
        {
            return RenderViewToString(controller, null, null, model);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller, string viewName)
        {
            return RenderViewToString(controller, viewName, null, null);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller, string viewName, string masterName)
        {
            return RenderViewToString(controller, viewName, masterName, null);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <param name="masterName">Master name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller, string viewName, string masterName, object model)
        {
            if (viewName.IsEmpty())
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(controller.ControllerContext, viewName, masterName);

                ThrowIfViewNotFound(viewResult, viewName);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        private static void ThrowIfViewNotFound(ViewEngineResult viewResult, string viewName)
        {
            // if view not found, throw an exception with searched locations
            if (viewResult.View != null) return;
            var locations = new StringBuilder();
            locations.AppendLine();

            foreach (var location in viewResult.SearchedLocations)
            {
                locations.AppendLine(location);
            }

            throw new InvalidOperationException(
                $"The view '{viewName}' or its master was not found, searched locations: {locations}");
        }

        #endregion
    }
}

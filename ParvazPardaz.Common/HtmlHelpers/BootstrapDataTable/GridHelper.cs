using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public static class GridHelper
    {
        public static Grid<T> BootstrapDataTable<T>(this HtmlHelper<T> htmlHelper, string name) where T : class
        {
            return new Grid<T>(htmlHelper, name);
        }
    }
    public class Grid<T> : BaseComponent
      where T : class
    {
        #region Property

        private List<Column> bindColumns { get; set; }
        public GridColumn<T> GridColumn { get; set; }
        private string gridStyle { get; set; }
        private bool dataStriped { get; set; }
        private string onRowStyle { get; set; }
        private string Name { get; set; }
        private bool showColumns { get; set; }
        private bool showSearch { get; set; }
        private bool showRefresh { get; set; }
        private bool showToggle { get; set; }
        private int? height { get; set; }
        private bool cardView { get; set; }
        private bool clickToSelect { get; set; }
        private bool clickToMultiSelect { get; set; }
        private string radioName { get; set; }
        private bool singleSelectWithCheckBox { get; set; }
        private string customToolbar { get; set; }
        private List<string> htmlInputs { get; set; }
        private bool hasPaging { get; set; }
        private List<int> pageSize { get; set; }
        private string dataBindFuncName { get; set; }
        private string action { get; set; }
        private string controller { get; set; }
        private string route { get; set; }

        #endregion

        #region Constractor

        public Grid(System.Web.Mvc.HtmlHelper<T> htmlString, string name)
        {
            this.htmlHelper = htmlString;
            this.Name = name;
            this.height = null;
        }

        #endregion

        /// <summary>
        /// You can add columns
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> Columns(Action<GridColumn<T>> expression)
        {
            var GridColumn = new GridColumn<T>();
            this.GridColumn = GridColumn;
            expression(GridColumn);
            bindColumns = GridColumn.Columns;
            return (Grid<T>)this;
        }
        public Grid<T> DataStriped(bool striped = false)
        {
            dataStriped = striped;
            return (Grid<T>)this;
        }
        /// <summary>
        /// Grid Style
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> GridStyle(string @class)
        {
            gridStyle = @class;
            return (Grid<T>)this;
        }
        /// <summary>
        /// Get javascript function and set row style
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> OnRowStyle(string jsFunction)
        {
            onRowStyle = @jsFunction;
            return (Grid<T>)this;
        }
        /// <summary>
        /// show columns
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> ShowColumns(bool show = false)
        {
            showColumns = show;
            return (Grid<T>)this;
        }
        /// <summary>
        /// show search textBox
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> ShowSearch(bool show = false)
        {
            showSearch = show;
            return (Grid<T>)this;
        }
        /// <summary>
        /// Show refresh button
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> ShowRefresh(bool show = false)
        {
            showRefresh = show;
            return (Grid<T>)this;
        }
        public Grid<T> ShowToggle(bool show = false)
        {
            showToggle = show;
            return (Grid<T>)this;
        }
        /// <summary>
        /// Set grid height
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> Height(int? height = null)
        {
            this.height = height;
            return (Grid<T>)this;
        }
        /// <summary>
        /// Show grid for card shape
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> CardView(bool show = false)
        {
            cardView = show;
            return (Grid<T>)this;
        }
        /// <summary>
        /// Click on row and select the row
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> ClickToSelect(bool flag = false)
        {
            this.clickToSelect = flag;
            return (Grid<T>)this;
        }
        /// <summary>
        /// this item access to you have multi select or single select
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> SingleSelectWithCheckBox(bool flag = false)
        {
            this.singleSelectWithCheckBox = flag;
            return (Grid<T>)this;
        }
        /// <summary>
        /// radio button on columns name
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> RadioName(string radioName = "RBChecked")
        {
            this.radioName = this.clickToSelect ? radioName : null;
            return (Grid<T>)this;
        }
        /// <summary>
        /// you can using this item create toolbar items
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> CustomToolBar(string toolBarName, List<string> htmlInputs)
        {
            this.customToolbar = toolBarName;
            this.htmlInputs = htmlInputs;
            return (Grid<T>)this;
        }
        /// <summary>
        /// maximum row in your grid
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> HasPaging(bool show, List<int> pageSize = null)
        {
            this.hasPaging = show;
            this.pageSize = pageSize == null ? new List<int> { 5, 10, 20, 50, 100 } : new List<int> { };
            return (Grid<T>)this;
        }
        public Grid<T> DataBindFunction(string name)
        {
            dataBindFuncName = name;
            return (Grid<T>)this;
        }
        /// <summary>
        /// this item access to you for get data using json
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Grid<T> Url(string action, string controller, string route = null)
        {
            this.action = action;
            this.controller = controller;
            this.route = route;
            return (Grid<T>)this;
        }
        public override string ToString()
        {
            string columns = null;
            bindColumns.ForEach(f => columns += "<th " +
                (f.Sortable ? " data-sortable=\"" + f.Sortable.ToString().ToLower() + "\"" : null) +
                (f.Align != null ? " data-align=\"" + f.Align.ToString().ToLower() + "\"" : null) +
                (f.HAlign != null ? " data-halign=\"" + f.HAlign.ToString().ToLower() + "\"" : null) +
                (f.OnColumnStyle != null ? " data-cell-style=\"" + f.OnColumnStyle + "\"" : null) +
                (f.OnFormatter != null ? " data-formatter=\"" + f.OnFormatter + "\"" : null) +
                (f.ShowHeader ? " data-show-header=\"" + f.ShowHeader + "\"" : null) +
                (f.NotSwitchable ? " data-switchable=\"" + (!f.NotSwitchable).ToString().ToLower() + "\"" : null) +
                (f.Visible ? " data-visible=\"true\"" : " data-visible=\"false\"") +
                (f.SingleSelect ? " data-radio=\"" + f.SingleSelect.ToString().ToLower() + "\"" : null) +
                (f.MultiSelect ? " data-checkbox=\"" + f.MultiSelect.ToString().ToLower() + "\"" : null) +
                (f.ActionFormater != null ? " data-formatter=\"" + f.ActionFormater.ToString() + "\"" : null) +
                (f.ActionEvents != null ? " data-events=\"" + f.ActionEvents.ToString() + "\"" : null) +
                " data-field=\"" + f.Key + "\">" + (f.Display ?? f.Key) + "</th>");
            columns = String.Format("<thead><tr>{0}</tr></thead>", columns);

            string customeToolbars = null;
            htmlInputs = htmlInputs ?? new List<string>();
            htmlInputs.ForEach(f => customeToolbars += f);
            customeToolbars = customeToolbars != null ? String.Format("<div id=\"" + customToolbar + "\" class=\"btn-group\">{0}</div>", customeToolbars) : null;


            string grid = customeToolbars + "<table id=\"" + Name + "\" " +
                (dataBindFuncName != null ? " data-query-Params=\"" + dataBindFuncName + "\"" : null) +
                (onRowStyle != null ? " data-row-style=\"" + onRowStyle + "\"" : null) +
                (dataStriped ? " data-striped=\"" + dataStriped + "\"" : null) +
                (gridStyle != null ? " data-classes=\"" + gridStyle + "\"" : null) +
                (showColumns ? " data-show-columns=\"" + showColumns + "\"" : null) +
                (showSearch ? " data-search=\"" + showSearch + "\"" : null) +
                (showRefresh ? " data-show-refresh=\"" + showRefresh + "\"" : null) +
                (showToggle ? " data-show-toggle=\"" + showToggle + "\"" : null) +
                (cardView ? " data-card-view=\"" + cardView + "\"" : null) +
                (height != null ? " data-height=\"" + height + "\"" : null) +
                (clickToSelect ? " data-click-to-select=\"" + clickToSelect.ToString().ToLower() + "\"" : null) +
                (hasPaging ? " data-pagination=\"true\" data-side-pagination=\"server\" data-page-list=\"[" + String.Join(",", pageSize.Select(s => Convert.ToString(s)).ToArray()) + "]\"" : null) +
                (singleSelectWithCheckBox ? " data-single-select=\"" + singleSelectWithCheckBox.ToString().ToLower() + "\"" : null) +
                (customToolbar != null ? " data-toolbar=\"#" + customToolbar + "\"" : null) +
                "  data-toggle=\"table\" data-url=\"/" + controller + "/" + action + "" + (route != null ? "/" + route : null) + "\">" +
                columns +
            "</table>";
            return grid;
        }
    }
}

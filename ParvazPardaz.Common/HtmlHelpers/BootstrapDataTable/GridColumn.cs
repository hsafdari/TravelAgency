using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public class GridColumn<T>
        where T : class
    {
        #region Property

        internal List<Column> Columns = new List<Column>();
        private string Key { get; set; }
        private string Name { get; set; }

        #endregion

        public GridColumn<T> BindTo<TProperty>(Expression<Func<T, TProperty>> expression, string name = null)
        {
            var Metadata = ModelMetadata.FromLambdaExpression<T, TProperty>(expression, new ViewDataDictionary<T>());
            Key = expression.MemberWithoutInstance();
            Name = name != null ? name : Key;
            Columns.Add
                (
                    new Column
                    {
                        Key = expression.MemberWithoutInstance(),
                        Name = name != null ? name : expression.MemberWithoutInstance(),
                        Display = Metadata.DisplayName,
                        Visible = Metadata.ShowForDisplay,
                        Type = expression.ToMemberExpression().Type(),
                        Format = Metadata.DisplayFormatString,
                    }
                );
            return this;
        }
        public GridColumn<T> Bind(string key, string name = null, string displayName = null, bool visible = true)
        {
            Name = name != null ? name : Key;
            Columns.Add
                (
                    new Column
                    {
                        Key = key,
                        Name = name != null ? name : key,
                        Display = displayName,
                        Visible = visible
                    }
                );
            return this;
        }
        public GridColumn<T> Sortable(bool sortable)
        {
            Columns.First(c => c.Name == this.Name).Sortable = sortable;
            return this;
        }
        public GridColumn<T> Align(Column.AlignType? align = null)
        {
            Columns.First(c => c.Name == this.Name).Align = align;
            return this;
        }
        public GridColumn<T> HAlign(Column.AlignType? align = null)
        {
            Columns.First(c => c.Name == this.Name).HAlign = align;
            return this;
        }
        public GridColumn<T> OnColumnStyle(string jsFunction)
        {
            Columns.First(c => c.Name == this.Name).OnColumnStyle = jsFunction;
            return this;
        }
        public GridColumn<T> OnFormatter(string jsFunction)
        {
            Columns.First(c => c.Name == this.Name).OnFormatter = jsFunction;
            return this;
        }
        public GridColumn<T> ShowHeader(bool show = false)
        {
            Columns.First(c => c.Name == this.Name).ShowHeader = show;
            return this;
        }
        public GridColumn<T> NotSwitchable(bool swich = false)
        {
            Columns.First(c => c.Name == this.Name).NotSwitchable = swich;
            return this;
        }
        public GridColumn<T> Visible(bool show = false)
        {
            Columns.First(c => c.Name == this.Name).Visible = show;
            return this;
        }
        public GridColumn<T> SingleSelect(bool flag = false)
        {
            Columns.First(c => c.Name == this.Name).SingleSelect = flag;
            return this;
        }
        public GridColumn<T> MultiSelect(bool flag = false)
        {
            Columns.First(c => c.Name == this.Name).MultiSelect = flag;
            return this;
        }
        public GridColumn<T> ActionFormatter(string name)
        {
            Columns.First(c => c.Name == this.Name).ActionFormater = name;
            return this;
        }
        public GridColumn<T> ActionEvents(string name)
        {
            Columns.First(c => c.Name == this.Name).ActionEvents = name;
            return this;
        }
    }
}

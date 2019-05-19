using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace ParvazPardaz.Common.Extension
{
    public static class CommonHelper
    {
        public static IDictionary<string, object> AddProperty(this object obj, string name, object value)
        {
            var dictionary = obj.ToDictionary();
            dictionary.Add(name, value);
            return dictionary;
        }
        public static Dictionary<string, object> ToDictionary(this object myObj)
        {
            return myObj.GetType()
                .GetProperties()
                .Select(pi => new { Name = pi.Name, Value = pi.GetValue(myObj, null) })
                .Union(
                    myObj.GetType()
                    .GetFields()
                    .Select(fi => new { Name = fi.Name, Value = fi.GetValue(myObj) })
                 )
                .ToDictionary(ks => ks.Name, vs => vs.Value);
        }
        public static string ToStringAttr(this Dictionary<string, object> myObj)
        {
            return string.Join(" ", myObj.Where(w => w.Value != null).Select(s => s.Key + "=\"" + s.Value.ToString() + "\"")).StringResolve();
        }
        public static string MemberWithoutInstance(this LambdaExpression expression)
        {
            return ExpressionHelper.GetExpressionText(expression);
        }
        public static MemberExpression ToMemberExpression(this LambdaExpression expression)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                UnaryExpression expression3 = expression.Body as UnaryExpression;
                if (expression3 != null)
                {
                    body = expression3.Operand as MemberExpression;
                }
            }
            return body;
        }
        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static System.Type Type(this MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                return null;
            }
            MemberInfo member = memberExpression.Member;
            if (member.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)member).PropertyType;
            }
            if (member.MemberType != MemberTypes.Field)
            {
                throw new NotSupportedException();
            }
            return ((FieldInfo)member).FieldType;
        }
        public static string Action(this UrlHelper helper, string action, string controller, RouteValueDictionary routeValues, string protocol)
        {
            string hostName = helper.RequestContext.HttpContext.Request.Url.Host;
            return helper.Action(action, controller, routeValues, protocol, hostName);
        }
        public static object ToObject(this Dictionary<string, object> obj)
        {
            var dict = obj;
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (var kvp in dict)
            {
                eoColl.Add(kvp);
            }

            dynamic eoDynamic = eo;

            string value = eoDynamic.Property;
            return eo;
        }
        public static string SetStringHtmlAttribute(this object htmlAttribute, string param = null)
        {
            var htmlatt = new Dictionary<string, object>();
            if (htmlAttribute != null)
            {
                htmlatt = htmlAttribute.ToDictionary();
            }
            if (htmlatt.Any(w => w.Key.ToLower() == "class"))
            {
                var found = htmlatt.FirstOrDefault(w => w.Key.ToLower() == "class");
                htmlatt.Remove(found.Key);
                htmlatt.Add("Class", found.Value + param);
            }
            else
            {
                htmlatt.Add("Class", param);
            }
            return htmlatt.ToStringAttr();
        }
        public static Dictionary<string, object> SetObjectHtmlAttribute(this object htmlAttribute, object param = null)
        {
            var htmlatt = new Dictionary<string, object>();
            var newhtmlatt = new Dictionary<string, object>();
            if (htmlAttribute != null)
                htmlatt = htmlAttribute.ToDictionary();
            if (param != null)
                newhtmlatt = param.ToDictionary();
            if (htmlatt.Any(w => w.Key.ToLower() == "class"))
            {
                var found = htmlatt.FirstOrDefault(w => w.Key.ToLower() == "class");
                var newfound = newhtmlatt.FirstOrDefault(w => w.Key.ToLower() == "class");
                htmlatt.Remove(found.Key);
                htmlatt.Add("Class", found.Value.ToString() + " " + newfound.Value);
                htmlatt.Union(newhtmlatt).Where(w => w.Key.ToLower() != "class").ToList().ForEach(f => htmlatt.Add(f.Key, f.Value));
            }
            else
            {
                return newhtmlatt;
            }
            return htmlatt;
        }
        public static string ResolveUIEnums(this string param)
        {
            return String.Join("-", param.Split('_')).ToLower();
        }
        public static string StringResolve(this string param)
        {
            return param == "" ? null : param;
        }
        public static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
        public static string ToDescriptionString(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static string CreateStringParams(this Dictionary<string, object> parameters)
        {
            return String.Join(",", parameters.ToList().Select(s => s.Key + ":" + s.Value).ToList());
        }

        public static string SetFileType(this string extension)
        {
            switch (extension)
            {
                case ".jpg":
                case ".jpe":
                case ".jpeg":
                    {
                        return "image/jpg";
                    }
                case ".png":
                    {
                        return "image/x-png";
                    }
                case ".gif":
                    {
                        return "image/gif";
                    }
                case ".pdf":
                    {
                        return "application/pdf";
                    }
                default:
                    {
                        return "image/jpg";
                    }
            }
        }
        //public static Int32 GetAge(this DateTime dateOfBirth)
        //{
        //    var today = DateTime.Today;
        //    var a = (today.Year * 100 + today.Month) * 100 + today.Day;
        //    var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
        //    return (a - b) / 10000;
        //}
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public static class StringExtensions
{
    public static string Left(this string s, int count)
    {
        return s.Substring(0, count);
    }

    public static string Right(this string s, int count)
    {
        return s.Substring(s.Length - count, count);
    }

    public static string Mid(this string s, int index, int count)
    {
        return s.Substring(index, count);
    }

    public static int ToInteger(this string s)
    {
        int integerValue = 0;
        int.TryParse(s, out integerValue);
        return integerValue;
    }

    public static double ToDouble(this string s)
    {
        double value = 0;
        double.TryParse(s, out value);
        return value;
    }

    public static decimal ToDecimal(this string value)
    {
        decimal number;
        Decimal.TryParse(value, out number);
        return number;
    }

    public static bool IsInteger(this string s)
    {
        Regex regularExpression = new Regex("^-[0-9]+$|^[0-9]+$");
        return regularExpression.Match(s).Success;
    }

    public static string MaskName(this string src)
    {
        char[] chars = src.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (i + 1 < chars.Length)
                chars[++i] = 'O';
        }

        return new string(chars);
    }

    public static string StripHtmlTag(this string src)
    {
        return Regex.Replace(src, "<.*?>", "");
    }

    /*
    C# Generic String Parser Extension Method
    Code Snippet By: Pinal Bhatt [www.PBDesk.com]
    http://blog.pbdesk.com/2012/02/c-generic-string-parser-extension.html
    Working Example at http://ideone.com/ZP5xo
    Usage:
    string s = "32";
    int i = s.As<int>();
    */

    public static T As<T>(this string strValue, T defaultValue)
    {
        T output = defaultValue;
        if (output == null)
        {
            output = default(T);
        }
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
        if (converter != null)
        {
            try
            {
                output = (T)converter.ConvertFromString(strValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        return output;
    }

    public static bool IsNullOrEmpty(this string value)
    {
        return (value == null || value.Length == 0);
    }

    public static bool IsNotNullOrEmpty(this string value)
    {
        return !(value == null || value.Length == 0);
    }

    /// <summary>
    /// 地籍所有權人去識別
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public static string MaskOwner(this string owner)
    {
        if (string.IsNullOrEmpty(owner)) return "";

        if (owner.Trim().Length > 1)
        {
            var trimed = owner.Trim();
            owner = trimed.Substring(0, 1) + string.Concat(Enumerable.Repeat("*", trimed.Length - 1));
        }

        return owner;
    }

    /// <summary>
    /// 地籍所有權人地址去識別
    /// </summary>
    /// <param name="addr"></param>
    /// <returns></returns>
    public static string MaskOwnerAddr(this string addr)
    {
        if (string.IsNullOrEmpty(addr)) return "";

        addr = addr.Trim();
        var pattern = @"(^.{2,}[縣市](.{1,}[鄉鎮市區])?)(.*)$";
        var matched = Regex.Match(addr, pattern);

        if (matched.Success)
        {
            addr = matched.Groups[1].Value + string.Concat(Enumerable.Repeat("*", matched.Groups[matched.Groups.Count - 1].Length));
        }
        else if (addr.Length > 6)
        {
            addr = addr.Substring(0, 6) + string.Concat(Enumerable.Repeat("*", addr.Length - 6));
        }

        return addr;
    }
}

public static class ControlExtensions
{
    /// <summary>
    /// 設定屬性值
    /// </summary>
    /// <param name="ctl"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetAttribute(this Control ctl, string key, string value)
    {
        if (ctl is WebControl)
        {
            ((WebControl)ctl).Attributes[key] = value;
        }

        if (ctl is HtmlControl)
        {
            ((HtmlControl)ctl).Attributes[key] = value;
        }
    }

    public static IEnumerable<T> FindControlsOfType<T>(this Control parent) where T : Control
    {
        foreach (Control child in parent.Controls)
        {
            if (child is T)
            {
                yield return (T)child;
            }
            else if (child.Controls.Count > 0)
            {
                foreach (T grandChild in child.FindControlsOfType<T>())
                {
                    yield return grandChild;
                }
            }
        }
    }

    public static Control FindControlRecursive(this Control rootControl, string controlID)
    {
        if (rootControl.ID == controlID) return rootControl;

        foreach (Control controlToSearch in rootControl.Controls)
        {
            Control controlToReturn = FindControlRecursive(controlToSearch, controlID);
            if (controlToReturn != null) return controlToReturn;
        }
        return null;
    }

    public static void SetDataSourceAndBind(this ListControl ctl, object datasource, string dataTextField = null, string dataValueField = null, bool appendDataBoundItems = false)
    {
        if (dataTextField.IsNotNullOrEmpty()) ctl.DataTextField = dataTextField;
        if (dataValueField.IsNotNullOrEmpty()) ctl.DataValueField = dataValueField;
        ctl.AppendDataBoundItems = appendDataBoundItems;

        ctl.DataSource = datasource;
        ctl.DataBind();
    }

    public static void SetDataSourceAndBind<TProp>(this ListControl ctl, object datasource, Expression<Func<TProp, object>> textExpr = null, Expression<Func<TProp, object>> valueExpr = null, bool appendDataBoundItems = false)
    {
        if (textExpr != null) ctl.DataTextField = GetMemberExpression(textExpr).Member.Name;
        if (valueExpr != null) ctl.DataValueField = GetMemberExpression(valueExpr).Member.Name;
        ctl.AppendDataBoundItems = appendDataBoundItems;

        ctl.DataSource = datasource;
        ctl.DataBind();
    }

    private static MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> exp)
    {
        var member = exp.Body as MemberExpression;
        var unary = exp.Body as UnaryExpression;

        return member ?? (unary != null ? unary.Operand as MemberExpression : null);
    }
}

public static class Extensions
{
    public static bool IsNull<T>(this T source)
    {
        return source == null;
    }

    public static double ToDouble(this int? source)
    {
        return Convert.ToDouble(source);
    }

    public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, int, TSource> func)
    {
        int index = 0;
        using (IEnumerator<TSource> enumerator = source.GetEnumerator())
        {
            enumerator.MoveNext();
            index++;
            TSource current = enumerator.Current;
            while (enumerator.MoveNext())
                current = func(current, enumerator.Current, index++);
            return current;
        }
    }
}

public static class EnumerableExtensions
{
    /// <summary>
    /// Creates a <see cref="T:System.Collections.Generic.Dictionary`2"/> from an
    /// <see cref="T:System.Collections.Generic.IEnumerable`1"/> according to a specified
    /// key selector function, and an element selector function.
    /// </summary>
    public static IDictionary<TKey, TElement> ToDistinctDictionary<TSource, TKey, TElement>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TElement> elementSelector)
    {
        if (source == null) throw new NullReferenceException("The 'source' cannot be null.");
        if (keySelector == null) throw new ArgumentNullException("keySelector");
        if (elementSelector == null) throw new ArgumentNullException("elementSelector");

        var dictionary = new Dictionary<TKey, TElement>();
        foreach (TSource current in source)
        {
            dictionary[keySelector(current)] = elementSelector(current);
        }
        return dictionary;
    }
}

public static class TreeViewExtensions
{
    public static List<TreeNode> GetAllNodes(this TreeView _self)
    {
        List<TreeNode> result = new List<TreeNode>();
        foreach (TreeNode child in _self.Nodes)
        {
            result.AddRange(child.GetAllNodes());
        }
        return result;
    }

    public static List<TreeNode> GetAllNodes(this TreeNode _self)
    {
        List<TreeNode> result = new List<TreeNode>();
        result.Add(_self);
        foreach (TreeNode child in _self.ChildNodes)
        {
            result.AddRange(child.GetAllNodes());
        }
        return result;
    }
}

public static class ObjectExtensions
{
    public static T As<T>(this object obj)
    {
        if (obj == null)
        {
            throw new InvalidCastException();
        }

        if (obj is T)
        {
            return (T)obj;
        }

        //TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

        //if (converter != null)
        //{
        //    return (T) converter.ConvertFrom(obj);
        //}

        return default(T);
    }

    public static bool Is<T>(this object obj)
    {
        return obj is T;
    }
}

public static class GridViewExtensions
{
    /// <summary>
    /// 反轉排序方向
    /// </summary>
    public static SortDirection Toggle(this SortDirection sort)
    {
        return sort == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
    }

    public static string ToSqlString(this SortDirection sort)
    {
        return sort == SortDirection.Ascending ? "ASC" : "DESC";
    }

    public static int GetBoundFieldColumnIndex(this GridView gv, Expression<Func<BoundField, bool>> colSelector)
    {
        var col = gv.Columns.OfType<BoundField>().ToList().SingleOrDefault(colSelector.Compile());

        if (col != null)
        {
            return gv.Columns.Contains(col) ? gv.Columns.IndexOf(col) : -1;
        }

        throw new NullReferenceException("Field not found!");
    }

}
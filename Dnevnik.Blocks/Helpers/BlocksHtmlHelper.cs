using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Dnevnik.Blocks.Helpers
{
    public static class BlocksHtmlHelper
    {
        public static MvcHtmlString Block(this HtmlHelper htmlHelper, string partialViewName, object model = null)
        {
            var blockData = GetBlockData(partialViewName);

            var builder = new StringBuilder();
            builder.Append(htmlHelper.Partial(blockData.FullView, model, new ViewDataDictionary { { "mod", blockData.Modifier } }));

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString BlockWithJs(this HtmlHelper htmlHelper, string partialViewName, object model = null)
        {
            var builder = new StringBuilder();
            builder.Append(Block(htmlHelper, partialViewName, model));
            builder.Append(GetJavaScript(partialViewName));

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString EditorBlockForWithJs<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
        {
            var builder = new StringBuilder();
            builder.Append(EditorBlockFor(html, expression, templateName));
            builder.Append(GetJavaScript(templateName));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString EditorBlockFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
        {
            var blockData = GetBlockData(templateName);

            var builder = new StringBuilder();
            builder.Append(html.EditorFor(expression, blockData.View, new ViewDataDictionary { { "mod", blockData.Modifier } }));
            return new MvcHtmlString(builder.ToString());
        }

        private static string GetJavaScript(string name)
        {
            return string.Format("<script>require(['{0}']);</script>", name);
        }

        private static BlockData GetBlockData(string partialViewName)
        {
            return BlockData.Parse(partialViewName);
        }

        private class BlockData
        {
            public string FullView { get; private set; }

            public string Modifier { get; private set; }

            public string View { get; private set; }

            public static BlockData Parse(string partialViewName)
            {
                var regex = new Regex(@"^(?<path>[^\-]*)(\-?(?<mod>\S*)?)$", RegexOptions.IgnoreCase);
                int pathPart = regex.GroupNumberFromName("path");
                int modPart = regex.GroupNumberFromName("mod");

                var match = regex.Match(partialViewName);
                string path = match.Groups[pathPart].Value;
                string mod = match.Groups[modPart].Value;

                return new BlockData
                       {
                           Modifier = mod,
                           FullView = string.Format("~/views/{0}.cshtml", path),
                           View = path
                       };
            }
        }
    }
}
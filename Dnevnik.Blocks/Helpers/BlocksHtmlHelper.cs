using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System;
using System.Linq.Expressions;

namespace Dnevnik.Blocks.Helpers
{
    public static class BlocksHtmlHelper
    {
        public static MvcHtmlString Block(this HtmlHelper htmlHelper, string partialViewName)
        {
            return PartialWithJs(htmlHelper, partialViewName, null);
        }

        public static MvcHtmlString BlockWithJs(this HtmlHelper htmlHelper, string partialViewName)
        {
            return PartialWithJs(htmlHelper, partialViewName, null);
        }

        public static MvcHtmlString PartialWithJs(this HtmlHelper htmlHelper, string partialViewName, object model)
        {
            var parts = partialViewName.Split('/');
            var newPath = new StringBuilder();
            for(int i = 0; i < parts.Length - 1; i++)
            {
                newPath.AppendFormat("/{0}", parts[i]);
            }
            newPath.AppendFormat("/{0}", parts[parts.Length - 2]);
            
            var builder = new StringBuilder();
            var fullViewName = string.Format("~/views{0}.cshtml", newPath);

            builder.Append(htmlHelper.Partial(fullViewName, model, parts[parts.Length - 2] != parts[parts.Length - 1] ? new ViewDataDictionary
                                                                                                                        {
                                                                                                                            { "mod", parts[parts.Length - 1] }
                                                                                                                        }: null));
            builder.Append(GetJavaScript(partialViewName));

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString EditorForWithJs<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
        {
            var builder = new StringBuilder();
            builder.Append(html.EditorFor(expression, templateName));
            builder.Append(GetJavaScript(templateName));
            return new MvcHtmlString(builder.ToString());
        }

        private static string GetJavaScript(string name)
        {
            return "<script>require(['" + name + "']);</script>";
        }

        private static BlockData GetBlockData(string partialViewName)
        {
            return BlockData.Parse(partialViewName);
        }

        private class BlockData
        {
            public string FullView { get; set; }

            public bool HasModifier { get; set; }

            public string Modifier { get; set; }

            public static BlockData Parse(string partialViewName)
            {
                var parts = partialViewName.Split('/');
                return new BlockData();
            }
        }
    }
}
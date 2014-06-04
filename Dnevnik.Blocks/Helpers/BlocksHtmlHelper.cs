using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System;
using System.Linq.Expressions;
using Dnevnik.Blocks.Core;

namespace Dnevnik.Blocks.Helpers
{
    using System.Linq;

    public static class BlocksHtmlHelper
    {
        public static MvcHtmlString Block(this HtmlHelper htmlHelper, string block, object model = null)
        {
            return Block(htmlHelper, new Block { Name = block }, model);
        }

        public static MvcHtmlString BlockWithJs(this HtmlHelper htmlHelper, string block, object model = null)
        {
            return BlockWithJs(htmlHelper, new Block { Name = block }, model);
        }

        public static MvcHtmlString Block(this HtmlHelper htmlHelper, Block block, object model = null)
        {
            var blockData = GetBlockData(block.Name);

            var builder = new StringBuilder();
            builder.Append(htmlHelper.Partial(blockData.FullView, block.Model ?? model, new ViewDataDictionary { { "block", block } }));

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString BlockWithJs(this HtmlHelper htmlHelper, Block block, object model = null)
        {
            var builder = new StringBuilder();
            builder.Append(Block(htmlHelper, block, model));
            builder.Append(GetJavaScript(block));

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString EditorBlockForWithJs<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Block block)
        {
            var builder = new StringBuilder();
            builder.Append(EditorBlockFor(html, expression, block));
            builder.Append(GetJavaScript(block));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString EditorBlockFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Block block)
        {
            var blockData = GetBlockData(block.Name);

            var builder = new StringBuilder();
            builder.Append(html.EditorFor(expression, blockData.View, new ViewDataDictionary { { "block", block} }));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString EditorBlockFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string block)
        {
            return EditorBlockFor(html, expression, new Block { Name = block }); 
        }

        public static MvcHtmlString EditorBlockForWithJs<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string block)
        {
            return EditorBlockForWithJs(html, expression, new Block { Name = block });
        }

        public static MvcHtmlString BlockCss(this HtmlHelper htmlHelper)
        {
            var block = htmlHelper.ViewData["block"] as Block;
            if (block == null) {  return null; }

            string itemName = block.Name.Split('/').Last();
            if(block.Mods == null || block.Mods.Length == 0) { return new MvcHtmlString(itemName); }

            var css = new StringBuilder();
            foreach(string mod in block.Mods)
            {
                css.AppendFormat("{0}-{1} ", itemName, mod);
            }
            return new MvcHtmlString(css.ToString());
        }

        public static MvcHtmlString Inners(this HtmlHelper htmlHelper)
        {
            var block = htmlHelper.ViewData["block"] as Block;
            if (block == null) { return null; }
            if(block.Inners == null || block.Inners.Length == 0) { return null; }

            var page = new StringBuilder();

            foreach(var inner in block.Inners)
            {
                page.Append(inner.WithJs ? BlockWithJs(htmlHelper, inner) : Block(htmlHelper, inner));
            }

            return new MvcHtmlString(page.ToString());
        }

        private static string GetJavaScript(Block block)
        {
            if (block.Mods == null || block.Mods.Length == 0)
            {
                return string.Format("<script>require(['{0}']);</script>", block.Name);
            }

            var js = new StringBuilder();
            foreach (string mod in block.Mods)
            {
                js.AppendFormat("<script>require(['{0}-{1}']);</script>", block.Name, mod);
            }
            return js.ToString();
        }

        private static BlockData GetBlockData(string partialViewName)
        {
            return BlockData.Parse(partialViewName);
        }

        private class BlockData
        {
            public string FullView { get; private set; }

            public string View { get; private set; }

            public static BlockData Parse(string partialViewName)
            {
                string[] pathItems = partialViewName.Split('/');
                return new BlockData
                       {
                           FullView = string.Format("~/views/{0}/{1}.cshtml", partialViewName, pathItems[pathItems.Length - 1]),
                           View = partialViewName
                       };
            }
        }
    }
}
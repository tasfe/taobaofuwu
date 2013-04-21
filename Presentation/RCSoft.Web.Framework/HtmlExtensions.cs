using System.Text;
using System.Web.Mvc;

namespace RCSoft.Web.Framework
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString ResolveUrl(this HtmlHelper htmlHelper, string url)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return MvcHtmlString.Create(urlHelper.Content(url));
        }

        public static MvcHtmlString Hint(this HtmlHelper helper, string value)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", ResolveUrl(helper, "").ToHtmlString());
            builder.MergeAttribute("alt", value);
            builder.MergeAttribute("title", value);

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}

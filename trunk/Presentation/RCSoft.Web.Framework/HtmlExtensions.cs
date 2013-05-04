using System;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Expressions;
using RCSoft.Core.Infrastructure;
using RCSoft.Core;
using RCSoft.Services.Localization;
using System.Collections.Generic;
using System.Web.Routing;

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

        public static MvcHtmlString RCSoftLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, bool displayHint = true)
        {
            var result = new StringBuilder();
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var hintResource = string.Empty;
            object value = null;
            if (metadata.AdditionalValues.TryGetValue("RCSoftResourceDispalyName", out value))
            {
                var resourceDisplayName = value as RCSoftResourceDisplayName;
                if (resourceDisplayName != null && displayHint)
                {
                    hintResource = EngineContext.Current.Resolve<ILocalizationService>()
                        .GetResource(resourceDisplayName.ResourceKey + ".Hint");
                    result.Append(helper.Hint(hintResource).ToHtmlString());
                }
            }
            result.Append(helper.LabelFor(expression, new { title = hintResource }));
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DataTypeName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (string.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}

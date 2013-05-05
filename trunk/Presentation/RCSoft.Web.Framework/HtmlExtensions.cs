using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq;
using System.Linq.Expressions;
using RCSoft.Core.Infrastructure;
using RCSoft.Core;
using RCSoft.Services.Localization;
using System.Collections.Generic;
using System.Web.Routing;
using RCSoft.Web.Framework.Mvc;
using Telerik.Web.Mvc.UI;
using System.IO;

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
            builder.MergeAttribute("src", ResolveUrl(helper, "~/Content/images/ico-help.gif").ToHtmlString());
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
            if (metadata.AdditionalValues.TryGetValue("RCSoftResourceDisplayName", out value))
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
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (string.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static string FiledIdFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return id.Replace('[', '_').Replace(']', '_');
        }

        public static MvcHtmlString DeleteConfirmation<T>(this HtmlHelper<T> helper, string buttonsSelector = null) where T : BaseEntityModel
        {
            return DeleteConfirmation<T>(helper, "", buttonsSelector);
        }

        public static MvcHtmlString DeleteConfirmation<T>(this HtmlHelper<T> helper, string actionName, string buttonsSelector) where T : BaseEntityModel
        {
            if (String.IsNullOrEmpty(actionName))
                actionName = "Delete";
            var modalId = MvcHtmlString.Create(helper.ViewData.ModelMetadata.ModelType.Name.ToLower() + "-delete-confirmation").ToHtmlString();

            helper.Telerik();

            if (!string.IsNullOrEmpty(buttonsSelector))
            {
                var textWriter = new StringWriter();
                IClientSideObjectWriter objectWriter = new ClientSideObjectWriterFactory().Create(buttonsSelector, "click", textWriter);
                objectWriter.Start();
                textWriter.Write("function(e){e.preventDefault();openModalWindow(\"" + modalId + "\");}");
                objectWriter.Complete();
                var value = textWriter.ToString();
                ScriptRegistrar.Current.OnDocumentReadyStatements.Add(value);
            }

            var deleteConfirmationModel = new DeleteConfirmationModel
            {
                Id = helper.ViewData.Model.Id,
                ControllerName = helper.ViewContext.RouteData.GetRequiredString("controller"),
                ActionName = actionName
            };
            var window = helper.Telerik().Window().Name(modalId)
                .Title(EngineContext.Current.Resolve<ILocalizationService>().GetResource("Common.Confirm"))
                .Modal(true)
                .Effects(x => x.Toggle())
                .Resizable(x => x.Enabled(false))
                .Buttons(x => x.Close())
                .Visible(false)
                .Content(helper.Partial("Delete", deleteConfirmationModel).ToHtmlString()).ToHtmlString();

            return MvcHtmlString.Create(window);
        }
    }
}

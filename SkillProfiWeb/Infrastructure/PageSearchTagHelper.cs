using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SkillProfiWeb.Infrastructure
{
    /// <summary>
    /// Класс, для создания перехода страниц по номеру страницы.
    /// </summary>
    [HtmlTargetElement("div", Attributes = "page-model-search")]
    public class PageSearchTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageSearchTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public string PageModelSearch { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder block = new TagBuilder("div");
            TagBuilder form = new TagBuilder("form");
            form.Attributes["method"] = "get";
            form.Attributes["asp-action"] = "Index";
            form.AddCssClass("search__page");
            TagBuilder label = new TagBuilder("label");
            label.InnerHtml.Append("Поиск");
            TagBuilder inputStatus = new TagBuilder("input");
            inputStatus.Attributes["id"] = "status";
            inputStatus.Attributes["name"] = "status";
            inputStatus.Attributes["hidden"] = "hidden";
            inputStatus.Attributes["value"] = PageModelSearch;
            TagBuilder input = new TagBuilder("input");
            input.Attributes["placeholder"] = "Номер страницы";
            input.Attributes["id"] = "requestPage";
            input.Attributes["name"] = "requestPage";
            TagBuilder btn = new TagBuilder("button");
            btn.Attributes["type"] = "submit";
            btn.AddCssClass("btn-blue");
            btn.AddCssClass("btn__reset");
            btn.InnerHtml.Append("Найти");
            form.InnerHtml.AppendHtml(label);
            form.InnerHtml.AppendHtml(inputStatus);
            form.InnerHtml.AppendHtml(input);
            form.InnerHtml.AppendHtml(btn);
            block.InnerHtml.AppendHtml(form);
            output.Content.AppendHtml(block.InnerHtml);
        }
    }
}

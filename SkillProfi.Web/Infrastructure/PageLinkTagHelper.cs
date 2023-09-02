using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SkillProfi.Web.ViewModels;

namespace SkillProfi.Web.Infrastructure
{
    /// <summary>
    /// Класс, для создания пагинации страниц.
    /// </summary>
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }   
            = new Dictionary<string, object>();
        public bool PageClassedEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder pagination = new TagBuilder("div");
            pagination.AddCssClass("paging-info btn-group pull-right");

            if (PageModel.TotalPages <= 10)
            {
                CreatePaginationLight(urlHelper, pagination);
                output.Content.AppendHtml(pagination.InnerHtml);
            }
            else
            {
                CreatePaginationFull(urlHelper, pagination);
                output.Content.AppendHtml(pagination.InnerHtml);
            }


        }

        private void CreatePaginationLight(IUrlHelper urlHelper, TagBuilder result)
        {
            var max = PageModel.TotalPages;
            for (int i = 1; i <= max; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                PageUrlValues["requestPage"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                if (PageClassedEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass("btn-blue");
                    tag.AddCssClass(i == PageModel.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
        }

        private void CreatePaginationFull(IUrlHelper urlHelper, TagBuilder result)
        {
            var max = PageModel.TotalPages;
            var freeDots = true;
            for (int i = 1; i <= max; i++)
            {
                if (i == 1 || i == PageModel.CurrentPage || i == max
                    || i == PageModel.CurrentPage - 1 || i == PageModel.CurrentPage + 1)
                {

                    TagBuilder tag = new TagBuilder("a");
                    PageUrlValues["requestPage"] = i;
                    tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    if (PageClassedEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass("btn-blue");
                        tag.AddCssClass(i == PageModel.CurrentPage
                            ? PageClassSelected : PageClassNormal);
                    }
                    tag.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(tag);

                    if (PageModel.CurrentPage > 1 && PageModel.CurrentPage < max
                        && freeDots == false && i != PageModel.CurrentPage)
                    {
                        freeDots = true;
                    }
                }
                else
                {
                    if (freeDots && i > 1 && i < max)
                    {
                        TagBuilder tag = new TagBuilder("p");
                        tag.AddCssClass("pagination__threedots");
                        tag.InnerHtml.Append("...");
                        result.InnerHtml.AppendHtml(tag);
                        freeDots = false;
                    }
                }
            }
        }
    }
}

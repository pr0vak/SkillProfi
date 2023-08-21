using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SkillProfiWeb.ViewModels;

namespace SkillProfiWeb.Infrastructure
{
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
            TagBuilder block = new TagBuilder("div");
            block.AddCssClass("paging__search__block");
            TagBuilder blockSearchPage = new TagBuilder("div");
            TagBuilder pagination = new TagBuilder("div");
            pagination.AddCssClass("paging-info1");
            pagination.AddCssClass("btn-group");
            pagination.AddCssClass("pull-right");

            if (PageModel.TotalPages <= 10)
            {
                CreatePaginationLight(PageModel, urlHelper, pagination, PageAction, 
                    PageUrlValues, PageClassedEnabled, PageClass, PageClassNormal, PageClassSelected);
                block.InnerHtml.AppendHtml(pagination);
            }
            else
            {
                CreatePaginationFull(PageModel, urlHelper, pagination, PageAction,
                    PageUrlValues, PageClassedEnabled, PageClass, PageClassNormal, PageClassSelected);
                CreateSearchPage(blockSearchPage);
                block.InnerHtml.AppendHtml(pagination);
                block.InnerHtml.AppendHtml(blockSearchPage);
            }


            output.Content.AppendHtml(block.InnerHtml);
        }

        private void CreatePaginationLight(PagingInfo pageModel, IUrlHelper urlHelper, 
            TagBuilder result, string pageAction, Dictionary<string, object> pageUrlValues, 
            bool pageClassedEnabled, string pageClass, string pageClassNormal, string pageClassSelected)
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

        private void CreatePaginationFull(PagingInfo pageModel, IUrlHelper urlHelper, 
            TagBuilder result, string pageAction, Dictionary<string, object> pageUrlValues, 
            bool pageClassedEnabled, string pageClass, string pageClassNormal, string pageClassSelected)
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


        private void CreateSearchPage(TagBuilder blockSearchPage)
        {
            TagBuilder form = new TagBuilder("form");
            form.Attributes["method"] = "get";
            form.Attributes["asp-action"] = "Index";
            form.AddCssClass("search__page");
            TagBuilder label = new TagBuilder("label");
            label.InnerHtml.Append("Поиск");
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
            form.InnerHtml.AppendHtml(input);
            form.InnerHtml.AppendHtml(btn);
            blockSearchPage.InnerHtml.AppendHtml(form); 
        }
    }
}

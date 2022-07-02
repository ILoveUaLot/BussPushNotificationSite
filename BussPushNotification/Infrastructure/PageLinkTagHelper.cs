using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BussPushNotification.Infrastructure
{
    [HtmlTargetElement("nav", Attributes ="page-model")]
    public class PageLinkTagHelper: TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; } 
        public string? PageAction { get; set; }
        public string? Url = "AboutUs";
        public string TagText = "About";
        public override void Process(TagHelperContext context, 
            TagHelperOutput output)
        {
            if (ViewContext != null)
            {
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new TagBuilder("nav");
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, Url);
                tag.InnerHtml.AppendHtml(TagText);
                
                result.InnerHtml.AppendHtml(tag);
                output.Content.AppendHtml(result.InnerHtml);
            }
            else
            {
                throw new InvalidOperationException("ViewContext cannot be null.");
            }  
        }
    }
}

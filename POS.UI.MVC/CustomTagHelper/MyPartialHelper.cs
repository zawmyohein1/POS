using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System;

namespace POS.UI.MVC.CustomTagHelper
{
    [HtmlTargetElement("mypartial", Attributes = "name", TagStructure = TagStructure.WithoutEndTag)]
    public sealed class MyPartialTagHelper
     : Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper
    {

        private static string[] SkipAttributeArray = new[]
        {
        "for", "model", "fallback-name", "optional", "name"
    };

        public MyPartialTagHelper(ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope)
            : base(viewEngine, viewBufferScope)
        { }

        private ViewDataDictionary getViewData([DisallowNull] TagHelperContext tagHelperContext)
        {
            if (tagHelperContext == null) throw new ArgumentNullException(nameof(tagHelperContext));
            var viewData = new ViewDataDictionary(this.ViewContext.ViewData);
            foreach (var attribute in tagHelperContext?.AllAttributes.Where(a => SkipAttributeArray.Contains(a.Name, StringComparer.InvariantCultureIgnoreCase) == false))
                viewData.Add(attribute.Name, attribute.Value?.ToString());
            return viewData;
        }

        public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            this.ViewData = getViewData(tagHelperContext);
            base.Process(tagHelperContext, tagHelperOutput);
        }

        public override Task ProcessAsync(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
        {
            this.ViewData = getViewData(tagHelperContext);
            return base.ProcessAsync(tagHelperContext, tagHelperOutput);
        }
    }
}

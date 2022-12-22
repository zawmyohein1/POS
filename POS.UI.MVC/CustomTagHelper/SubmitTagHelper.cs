using Microsoft.AspNetCore.Razor.TagHelpers;

namespace POS.UI.MVC.CustomTagHelper
{

    public class SubmitTagHelper : TagHelper
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "submit";
            output.Attributes.SetAttribute("class", "btn btn-defaul");
            output.Attributes.SetAttribute("data-val", Value);
            output.Content.SetContent(Text);

        }
    }
}



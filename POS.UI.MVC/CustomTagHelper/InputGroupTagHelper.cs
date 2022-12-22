using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace POS.UI.MVC.CustomTagHelper
{
    public class InputGroupTagHelper<T> : TagHelper where T : class
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public T Object { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var id = Name;
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", Name);
            label.InnerHtml.AppendHtml(Name);

            output.PreContent.SetHtmlContent(label);

            var input = new TagBuilder("input");
            input.Attributes.Add("type", "text");
            input.Attributes.Add("class", "form-control");
            input.Attributes.Add("name", Name);
            input.Attributes.Add("value", Value);
            input.Attributes.Add("placeholder", Name);
            input.Attributes.Add("id", id);

            output.Content.SetHtmlContent(input);

        }
    }
}

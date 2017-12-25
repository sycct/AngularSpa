using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace AngularSpa.TagHelpers
{
    [HtmlTargetElement("tag-dd")]
    public class TagDDTagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set;  }

        [HtmlAttributeName("pipe")]
        public string Pipe { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var labelTag = new TagBuilder("label");
            labelTag.InnerHtml.Append(For.Metadata.Description);
            labelTag.AddCssClass("control-label");

            var pTag = new TagBuilder("p");
            pTag.AddCssClass("form-control-static form-tag-dd");

            if(Pipe == null || Pipe.Length <= 0)
                pTag.InnerHtml.Append("{{" + For.AngularName() + "}}");
            else
                pTag.InnerHtml.Append("{{" + For.AngularName() + "|" + Pipe + "}}");

            output.TagName = "div";
            output.Attributes.Add("class", "form-group");

            output.Content.AppendHtml(labelTag);
            output.Content.AppendHtml(pTag);
        }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.TagHelpers
{
    [HtmlTargetElement("tag-di")]
    public class TagDITagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            DefaultModelMetadata model = (DefaultModelMetadata)For.Metadata;

            string shortName   = model.DisplayName ?? For.Name;
            string labelText   = model.Placeholder ?? shortName;
            string description = model.Description ?? labelText;
            string propName    = For.AngularPropertyName();

            var labelTag = new TagBuilder("label");
            labelTag.InnerHtml.Append(description);
            labelTag.MergeAttribute("for", propName);
            labelTag.AddCssClass("control-label");

            var inputTag = new TagBuilder("input");
            inputTag.AddCssClass("form-control");
            inputTag.TagRenderMode = TagRenderMode.StartTag;
            inputTag.MergeAttribute("id", propName);
            inputTag.MergeAttribute("name", propName);
            inputTag.MergeAttribute("placeholder", model.Placeholder);
            inputTag.MergeAttribute("[(ngModel)]", For.AngularName());

            string typeName = "text";
            switch(model.DataTypeName)
            {
                default:
                    break;

                case "Password":
                    typeName = "password";
                    break;

                case "Currency":
                    typeName = "number";
                    break;
            }

            inputTag.MergeAttribute("type", typeName);

            TagBuilder validationBox = new TagBuilder("div");
            validationBox.MergeAttribute("class", "alert alert-danger");
            validationBox.MergeAttribute("*ngIf", propName + ".errors && (" + propName + ".dirty || " + propName + ".touched)");

            string regex = model.RegexExpression();
            if(regex != string.Empty)
            {
                var regexValidation = new TagBuilder("div");
                regexValidation.MergeAttribute("[hidden]", string.Format("!{0}.errors.pattern", propName));
                regexValidation.InnerHtml.Append(string.Format("{0} введено неверно.", labelText));
                validationBox.InnerHtml.AppendHtml(regexValidation);
                inputTag.Attributes.Add("pattern", ((DefaultModelMetadata)For.Metadata).RegexExpression());
            }

            int? value = model.GetMinLength();
            if(value != null)
            {
                var minLengthValidation = new TagBuilder("div");
                minLengthValidation.MergeAttribute("[hidden]", string.Format("!{0}.errors.minlength", propName));
                minLengthValidation.InnerHtml.Append(string.Format("{0} должно быть максимум {1} символ(а) длиной.", labelText, value));
                validationBox.InnerHtml.AppendHtml(minLengthValidation);
                inputTag.Attributes.Add("minlength", value.ToString());
            }

            value = model.GetMaxLength();
            if (value != null)
            {
                var maxLengthValidation = new TagBuilder("div");
                maxLengthValidation.MergeAttribute("[hidden]", string.Format("!{0}.errors.maxlength", propName));
                maxLengthValidation.InnerHtml.Append(string.Format("{0} должно быть минимум {1} символ(а) длиной.", labelText, value));
                validationBox.InnerHtml.AppendHtml(maxLengthValidation);
                inputTag.Attributes.Add("maxlength", value.ToString());
            }

            // add validation box
            inputTag.MergeAttribute("#" + propName, "ngModel");

            if (model.IsRequired)
            {
                inputTag.Attributes.Add("required", "required");

                TagBuilder validationMsgBox = new TagBuilder("div");
                validationMsgBox.MergeAttribute("[hidden]", "!" + propName  + ".errors.required");
                validationMsgBox.InnerHtml.Append(labelText + " поле обьязательно к заполнению!");

                validationBox.InnerHtml.AppendHtml(validationMsgBox);
            }


            output.TagName = "div";
            output.Attributes.Add("class", "form-group");

            output.Content.AppendHtml(labelTag);

            switch (model.DataTypeName)
            {
                default:
                    output.Content.AppendHtml(inputTag);
                    break;


                case "Currency":
                    {
                        var divInputGroup = new TagBuilder("div");
                        divInputGroup.MergeAttribute("class", "input-group");

                        var divCurrencySign = new TagBuilder("div");
                        divCurrencySign.MergeAttribute("class", "input-group-addon");
                        divCurrencySign.InnerHtml.Append("$");

                        divInputGroup.InnerHtml.AppendHtml(divCurrencySign);
                        divInputGroup.InnerHtml.AppendHtml(inputTag);

                        output.Content.AppendHtml(divInputGroup);
                        break;
                    }
            }

            if (validationBox != null)
                output.Content.AppendHtml(validationBox);
        }
    }
}

using CommonMark;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkdownTagHelper.TagHelpers
{
  [HtmlTargetElement("markdown")]
  [HtmlTargetElement(Attributes = "markdown")]
  public class MarkdownTagHelper : TagHelper
  {
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = null;

      var childContent = await output.GetChildContentAsync();
      var lines = childContent.GetContent()
          .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
          .Select(line => line.Trim());
      var content = string.Join(" ", lines);
      var transformedContent = CommonMarkConverter.Convert(content);

      output.Content.SetHtmlContent(transformedContent);

      output.Attributes.RemoveAll("markdown");
    }
  }
}

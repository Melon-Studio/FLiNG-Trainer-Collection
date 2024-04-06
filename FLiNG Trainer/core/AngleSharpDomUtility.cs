using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace FLiNG_Trainer.core;

public class AngleSharpDomUtility
{
    public async Task<string> GetContentByClass(string url, string className)
    {
        using (var httpClient = new HttpClient())
        {
            var html = await httpClient.GetStringAsync(url);

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(html));

            var element = document.QuerySelector("." + className);

            return element != null ? element.InnerHtml : "";
        }
    }

    public async Task<(string href, string text)[]> GetAnchorTagsContent(string url, string className)
    {
        using (var httpClient = new HttpClient())
        {
            var html = await httpClient.GetStringAsync(url);

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(html));

            var elements = document.QuerySelectorAll("." + className);
            var anchorTagsContent = new List<(string href, string text)>();

            foreach (var element in elements)
            {
                var anchorTags = element.QuerySelectorAll("a");
                foreach (var anchorTag in anchorTags)
                {
                    var href = anchorTag.GetAttribute("href");
                    var text = anchorTag.TextContent.Trim();
                    anchorTagsContent.Add((href, text));
                }
            }

            return anchorTagsContent.ToArray();
        }
    }
}

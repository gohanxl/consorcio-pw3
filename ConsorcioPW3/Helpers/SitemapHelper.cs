using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsorcioPW3.Helpers
{
    public static class SitemapHelper
    {
        public static void SetConsorcioBreadcrumbTitle(string name)
        {
            var node = SiteMaps.Current.CurrentNode;
            FindParentNode(node, "Consorcio", $"Consorcio \"{name}\"");
        }

        public static void FindParentNode(ISiteMapNode node, string oldTitle, string newTitle)
        {
            if (node.Title == oldTitle)
            {
                node.Title = newTitle;
            }
            else
            {
                if (node.ParentNode != null)
                {
                    FindParentNode(node.ParentNode, oldTitle, newTitle);
                }
            }
        }
    }
}
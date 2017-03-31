using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Presentation;
using ModernBusiness.Helpers;
using Sitecore.Data.Items;

namespace ModernBusiness.Models
{
    public class ContentSectionModel : Sitecore.Mvc.Presentation.IRenderingModel
    {
        public void Initialize(Rendering rendering)
        {
            var resolver = new ComponentDatasourceResolver(rendering, ItemNames.Templates.ContentSection);
            PlaceholderMessage = resolver.PlaceholderMessage;
            InnerItem = resolver.DatasourceItem;
        }

        public string PlaceholderMessage { get; private set; }

        public Item InnerItem { get; private set; }

        public string ButtonLink
        {
            get
            {
                try
                {
                    // determine link from button link if defined, else item URL
                    return SitecoreViewHelper.ResolveLinkField(InnerItem.Fields[FieldNames.__Button.Link]);
                }
                catch (Exception e)
                {
                    return "#";
                }
            }
        }
    }
}
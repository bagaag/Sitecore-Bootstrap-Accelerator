using System;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using ModernBusiness.Helpers;

namespace ModernBusiness.Models
{
    public class CalloutModel : Sitecore.Data.Items.CustomItem
    {
        private Item _folder;

        public CalloutModel(Item folder, Item item) : base(item)
        {
            Assert.IsNotNull(item, "item");
            _folder = folder;
        }

        // Style
        public string Style
        {
            get
            {
                // use folder style unless specified at the individual callout level
                string innerItemCalloutStyle = InnerItem[FieldNames.Callout.CalloutStyle];
                string folderCalloutStyle = _folder[FieldNames.CalloutContainer.CalloutStyle];
                if (string.IsNullOrEmpty(innerItemCalloutStyle) || innerItemCalloutStyle == ItemNames.Options.CalloutStyles.Inherit)
                {
                    return folderCalloutStyle;
                }
                else
                {
                    return innerItemCalloutStyle;
                }
            }
        }

        // ImageSrc
        public string ImageSrc
        {
            get
            {
                return SitecoreViewHelper.ResolveImageSource(InnerItem, FieldNames.__TeaserImamge.Image);
            }
        } 

        // ImageAlt
        public string ImageAlt
        {
            get
            {
                return SitecoreViewHelper.ResolveImageAlt(InnerItem, FieldNames.__TeaserImamge.Image);
            }
        }

        // Link
        public string Link
        {
            get
            {
                // determine link from button link if defined, else item URL
                var link = Sitecore.Links.LinkManager.GetItemUrl(InnerItem);
                if (InnerItem.TemplateName != ItemNames.Templates.Callout)
                {
                    link = SitecoreViewHelper.ResolveLinkField(InnerItem.Fields[FieldNames.__Button.Link]);
                }
                return link;

            }
        }

        // Icon
        public string IconClass
        {
            get
            {
                return InnerItem[FieldNames.Callout.Icon];
            }
        }

        public string ColumnWidth
        {
            get
            {
                return InnerItem[FieldNames.Callout.ColumnWidth];
            }
        }

    }
}
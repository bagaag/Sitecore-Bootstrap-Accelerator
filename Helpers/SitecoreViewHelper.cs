using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace ModernBusiness
{
    public class SitecoreViewHelper
    {
        public static string ResolveLinkField(LinkField lf)
        {
            if (lf == null) return "null";
            switch (lf.LinkType.ToLower())
            {
                case "internal":
                    // Use LinkMananger for internal links, if link is not empty
                    return lf.TargetItem != null ? Sitecore.Links.LinkManager.GetItemUrl(lf.TargetItem) : string.Empty;
                case "media":
                    // Use MediaManager for media links, if link is not empty
                    return lf.TargetItem != null ? Sitecore.Resources.Media.MediaManager.GetMediaUrl(lf.TargetItem) : string.Empty;
                case "external":
                    // Just return external links
                    return lf.Url;
                case "anchor":
                    // Prefix anchor link with # if link if not empty
                    return !string.IsNullOrEmpty(lf.Anchor) ? "#" + lf.Anchor : string.Empty;
                case "mailto":
                    // Just return mailto link
                    return lf.Url;
                case "javascript":
                    // Just return javascript
                    return lf.Url;
                default:
                    // Just please the compiler, this
                    // condition will never be met
                    return lf.Url;
            }
        }

        public static string ResolveImageSource(Item item, string fieldName)
        {
            ImageField imgField = (ImageField)item.Fields[fieldName];
            return MediaManager.GetMediaUrl(imgField.MediaItem);
        }

        public static string ResolveImageAlt(Item item, string fieldName)
        {
            ImageField imgField = (ImageField)item.Fields[fieldName];
            return imgField.MediaItem["Alt"];
        }
    }
}
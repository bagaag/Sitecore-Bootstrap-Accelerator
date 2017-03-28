using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using ModernBusiness.Helpers;

namespace ModernBusiness.Models
{
    public class SlideModel : Sitecore.Data.Items.CustomItem
    {
        public SlideModel(Item item) : base(item)
        {
            Assert.IsNotNull(item, "item");
        }

        /// <summary>
        /// Returns the Caption field of the slide item
        /// </summary>
        public string Caption { get { return InnerItem[FieldNames.SliderSlide.Caption]; } }

        /// <summary>
        /// Returns the src URL for the slide's Image field
        /// </summary>
        public string ImageSrc
        {
            get
            {
                return SitecoreViewHelper.ResolveImageSource(InnerItem, FieldNames.SliderSlide.Image);
            }
        }

        /// <summary>
        /// Returns true if this item is the first among its siblings in the content tree
        /// </summary>
        public bool IsFirst
        {
            get
            {
                return InnerItem.Parent.Children[0].ID.Equals(InnerItem.ID);
            }
        }

        /// <summary>
        /// Returns the index of this item among its siblings
        /// </summary>
        public int Index
        {
            get
            {
                return InnerItem.Parent.Children.IndexOf(InnerItem);
            }
        }
    }
}
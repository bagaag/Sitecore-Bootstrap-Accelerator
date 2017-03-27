namespace ModernBusiness.Helpers
{
    /// <summary>
    /// Hard-coded Sitecore item names go here and only here.
    /// </summary>
    public static class ItemNames
    {

        public const string ContentFolder = "Content";

        public static class Templates
        {
            public const string CalloutContainer = "Callout Container";
            public const string SliderContainer = "Slider Container";
        }

        public static class Options
        {
            public static class CalloutStyles
            {
                public const string Inherit = "Inherit from Container";
                public const string IconBox = "Icon Heading with Call to Action";
                public const string Image = "Image Only";
                public const string ImageHeading = "Image with Heading";
                public const string ImageHeadingIntro = "Image with Heading and Intro";
            }
        }

    }
}
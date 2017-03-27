using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Presentation;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;
using ModernBusiness.Helpers;

namespace ModernBusiness.Models
{
    public class SliderModel : Sitecore.Mvc.Presentation.IRenderingModel
    {
        private List<SlideModel> _slides = new List<SlideModel>();

        /// <summary>
        /// Populates the slider from [Context Item]/Content/Slider Container or Slider Container datasource
        /// </summary>
        /// <param name="rendering"></param>
        public void Initialize(Rendering rendering)
        {
            var resolver = new ComponentDatasourceResolver(rendering, ItemNames.Templates.SliderContainer);
            PlaceholderMessage = resolver.PlaceholderMessage;
            Item slider = resolver.DatasourceItem;

            if (slider != null)
            {
                ChildList slides = slider.Children;
                if (slides.Count == 0)
                {
                    PlaceholderMessage = "Slider folder is empty. " + slider.ID;
                }
                else
                {
                    foreach (Item child in slides.InnerChildren)
                    {
                        _slides.Add(new SlideModel(child));
                    }
                }
            }
        }

        public IReadOnlyList<SlideModel> Slides { get { return _slides; } }

        public string PlaceholderMessage { get; private set; }

    }
}
using System;
using System.Linq;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Collections;
using ModernBusiness.Helpers;
using Sitecore.Data.Fields;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ModernBusiness.Models
{
    public class CalloutsModel : Sitecore.Mvc.Presentation.IRenderingModel
    {

        /// <summary>
        /// Locates a folder of callouts, either at PageItem/Content/Callouts or as specified as the component data source.
        /// </summary>
        /// <param name="rendering"></param>
        public void Initialize(Rendering rendering)
        {
            var resolver = new ComponentDatasourceResolver(rendering, ItemNames.Templates.CalloutContainer);
            PlaceholderMessage = resolver.PlaceholderMessage;
            CalloutFolder = resolver.DatasourceItem;
            if (CalloutFolder != null)
            {
                Format = CalloutFolder[FieldNames.CalloutContainer.CalloutStyle];

                Callouts = new List<CalloutModel>();

                // add children to callouts collection
                foreach (Item callout in CalloutFolder.Children)
                {
                    Callouts.Add(new CalloutModel(CalloutFolder, callout));
                }

                // if there are no children, try the Callouts field
                if (Callouts.Count == 0)
                {
                    Sitecore.Data.Fields.MultilistField mlf = CalloutFolder.Fields[FieldNames.CalloutContainer.Callouts];
                    foreach (Item callout in mlf.GetItems())
                    {
                        Callouts.Add(new CalloutModel(CalloutFolder, callout));
                    }
                }

                if (Callouts.Count == 0)
                {
                    PlaceholderMessage = "Callout folder is empty. " + CalloutFolder.ID;
                }
            }
        }

        public Item CalloutFolder { get; private set; }

        public List<CalloutModel> Callouts { get; private set; }

        public string PlaceholderMessage { get; private set; }

        public string Format { get; private set; }

        public ReadOnlyDictionary<string, string> CalloutStyles { get; private set; }

    }
}
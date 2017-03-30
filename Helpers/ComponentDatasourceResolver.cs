using System;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System.Linq;

namespace ModernBusiness.Helpers
{
    /// <summary>
    /// Provides datasource lookup from value set in datasource OR looks for the item in
    /// the Content folder under the current page if datasource is not set.
    /// This allows a single view rendering to work whether statically bound to a page layout 
    /// or inserted as a placeholder component.
    /// </summary>
    public class ComponentDatasourceResolver
    {
        /// <summary>
        /// Provides datasource lookup from value set in datasource OR looks for the item in
        /// the Content folder under the current page if datasource is not set.
        /// </summary>
        /// <param name="rendering">The view rendering. Get this from the view model's Initialize method.</param>
        /// <param name="datasourceTemplate">The expected name of the datasource template.</param>
        /// <param name="datasourceName">Optional name of the item to look for in the Content folder.</param>
        public ComponentDatasourceResolver(Rendering rendering, string datasourceTemplate, string datasourceName=null)
        {
            PlaceholderMessage = "Could not resolve Data Source.";
            Item item = null;

            // allow the data source name to be specified in a rendering property
            string paramDataSourceName = rendering.Properties["DataSourceName"];
            if (datasourceName == null && paramDataSourceName != null) 
            {
                datasourceName = paramDataSourceName;
            }

            // set item to datasource if its set
            if (!string.IsNullOrEmpty(rendering.DataSource))
            {
                item = PageContext.Current.Database.GetItem(rendering.DataSource);
                if (item.TemplateName != datasourceTemplate)
                {
                    PlaceholderMessage = $"Datasource item is not a {datasourceTemplate}. " + item.ID;
                    return;
                }
            }
            // otherwise set the item to page context item
            else
            {
                item = PageContext.Current.Item;
            }
            // get out with default message if unable to resolve
            if (item == null)
            {
                return;
            }

            // item has the datasource template
            if (item.TemplateName == datasourceTemplate)
            {
                DatasourceItem = item;
            }
            // item is the page containing this component
            // look for datasource at [PageItem]/Content/[datasourceName]
            else
            {
                Item contentFolder = item.Children[ItemNames.ContentFolder];
                if (contentFolder != null)
                {
                    // get item by name if specified
                    if (datasourceName != null)
                    {
                        DatasourceItem = contentFolder.Children
                            .Where(child => child.TemplateName == datasourceTemplate 
                                && child.Name == datasourceName).First();
                    }
                    // otherwise get first item with specified template
                    else
                    {
                        DatasourceItem = contentFolder.Children
                            .Where(child => child.TemplateName == datasourceTemplate).First();
                    }
                }
                else
                {
                    PlaceholderMessage = $"Page does not have a {ItemNames.ContentFolder} folder and datasource is not set. " + item.ID;
                }
            }
        }

        public string PlaceholderMessage { get; private set; }

        public Item DatasourceItem { get; private set; }
    }
}
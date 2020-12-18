using Examine;
using Examine.Providers;
using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;
using static Umbraco.Core.Constants;

namespace Searching.Site.Composing
{
    public class IndexerComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<IndexerComponent>();
        }

        public class IndexerComponent : IComponent
        {
            private readonly IExamineManager _examineManager;
            private readonly IUmbracoContextFactory _umbracoContextFactory;

            public IndexerComponent(IExamineManager examineManager, 
                IUmbracoContextFactory umbracoContextFactory)
            {
                _examineManager = examineManager;
                _umbracoContextFactory = umbracoContextFactory 
                    ?? throw new ArgumentNullException(nameof(umbracoContextFactory));
            }

            public void Initialize()
            {
                if (!_examineManager.TryGetIndex(UmbracoIndexes.ExternalIndexName, out IIndex index))
                    return;

                index.FieldDefinitionCollection.AddOrUpdate(
                    new FieldDefinition("searchableCategories", FieldDefinitionTypes.FullText));

                ((BaseIndexProvider)index).TransformingIndexValues += IndexerComponent_TransformingIndexValues;
            }

            private void IndexerComponent_TransformingIndexValues(object sender, IndexingItemEventArgs e)
            {
                if(int.TryParse(e.ValueSet.Id, out var nodeId))
                {
                    switch (e.ValueSet.ItemType)
                    {
                        case "blogpost":
                        case "contentPage":
                            using(var umbracoContext = _umbracoContextFactory.EnsureUmbracoContext())
                            {
                                var contentNode = umbracoContext.UmbracoContext.Content.GetById(nodeId);
                                if(contentNode != null)
                                {
                                    var categories = contentNode.Value<IEnumerable<string>>("category");
                                    if(categories != null)
                                    {
                                        e.ValueSet.Set("searchableCategories", string.Join(",", categories));
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            public void Terminate()
            {
                // nothing to terminate
            }
        }
    }
}
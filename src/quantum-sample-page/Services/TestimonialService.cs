using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Dto;
using Progress.Sitefinity.AspNetCore.SitefinityApi.Filters;
using Progress.Sitefinity.Renderer.Entities.Content;

namespace Renderer.Services
{
    public static class TestimonialService
    {

        public static async Task<CollectionResponse<T>> GetItems<T>(this IRestClient restClient, MixedContentContext contentContext, IDictionary<string, GetAllArgs> getAllArgsPerType)
            where T : class, ISdkItem
        {
            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));



            if (contentContext == null)
                return new CollectionResponse<T>();



            var collectionContextForAll = new CollectionResponse<T>();
            foreach (var context in contentContext.Content)
            {
                if (context.Variations != null)
                {
                    foreach (var variation in context.Variations)
                    {
                        GetAllArgs argsLocal = null;
                        if (getAllArgsPerType != null)
                            getAllArgsPerType.TryGetValue(context.Type, out argsLocal);



                        if (argsLocal == null)
                        {
                            argsLocal = new GetAllArgs()
                            {
                                Type = context.Type,
                            };
                        }



                        argsLocal.Type = context.Type;
                        argsLocal.Provider = variation.Source;
                        argsLocal.Filter = FilterConverter.From(variation.Filter);



                        var itemsForContext = await restClient.GetItems<T>(argsLocal).ConfigureAwait(true);
                        foreach (var dataItem in itemsForContext.Items)
                        {
                            collectionContextForAll.Items.Add(dataItem);
                        }



                        collectionContextForAll.TotalCount += itemsForContext.TotalCount;
                    }
                }
            }



            var orderedCollection = new List<T>();
            if (contentContext.ItemIdsOrdered != null && contentContext.ItemIdsOrdered.Length > 0 && contentContext.ItemIdsOrdered.Length == collectionContextForAll.Items.Count)
            {
                foreach (var id in contentContext.ItemIdsOrdered)
                {
                    var orderedItem = collectionContextForAll.Items.FirstOrDefault(x => x.Id == id);
                    if (orderedItem != null)
                    {
                        orderedCollection.Add(orderedItem);
                    }
                }



                collectionContextForAll.Items = orderedCollection;
            }



            return collectionContextForAll;
        }



        public static Task<CollectionResponse<T>> GetItems<T>(this IRestClient restClient, MixedContentContext contentContext, GetAllArgs getAllArgsSingle)
            where T : class, ISdkItem
        {
            if (contentContext == null)
                return Task.FromResult(new CollectionResponse<T>());



            var getAllArgs = contentContext.Content.Select(x => x.Type).ToDictionary(x => x, y => getAllArgsSingle);
            return restClient.GetItems<T>(contentContext, getAllArgs);
        }
    }
}

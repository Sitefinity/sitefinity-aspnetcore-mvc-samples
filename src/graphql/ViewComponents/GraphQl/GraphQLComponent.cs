using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using GraphQL.Client.Http;
using Progress.Sitefinity.AspNetCore.Configuration;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Linq;
using Progress.Sitefinity.AspNetCore.Web;

namespace graphql.ViewComponents.GraphQL
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget(Title="GraphQL")]
    public class GraphQLViewComponent : ViewComponent
    {
        private ISitefinityConfig sitefinityConfig;
        private IRenderContext renderContext;
        
        public GraphQLViewComponent(ISitefinityConfig sitefinityConfig, IRenderContext renderContext)
        {
            this.sitefinityConfig = sitefinityConfig;
            this.renderContext = renderContext;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<GraphQLEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!string.IsNullOrEmpty(context.Entity.ServiceUrl) && !string.IsNullOrEmpty(context.Entity.Query))
            {
                string fullUrl = context.Entity.ServiceUrl;
                if (!context.Entity.ServiceUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    fullUrl = $"{this.sitefinityConfig.Uri.ToString().TrimEnd('/')}/{context.Entity.ServiceUrl.TrimStart('/')}";
                }

                var graphqlClient = new GraphQLHttpClient(fullUrl, new NewtonsoftJsonSerializer());
                var graphQLRequest = new GraphQLHttpRequest(context.Entity.Query);

                var response = await graphqlClient.SendQueryAsync<JObject>(graphQLRequest);

                var viewModel = new GraphQLViewModel()
                {
                    Data = response.Data.ToString(),
                };

                if (response.Errors != null)
                {
                    viewModel.Errors = string.Join(", ", response.Errors.Select(x => x.Message));
                }

                return this.View(viewModel);
            }

            if (this.renderContext.IsEdit)
                context.SetWarning("No service URL or query set");

            return this.View(new GraphQLViewModel());
        }
    }
}

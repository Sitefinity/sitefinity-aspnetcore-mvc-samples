using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;

namespace graphql.ViewComponents.GraphQL
{
    /// <summary>
    /// The test model for the load tests widget.
    /// </summary>
    public class GraphQLEntity
    {
        /// <summary>
        /// Gets or sets the service URL.
        /// </summary>
        [Description("Relative url for sitefinity graphql endpoints. E.g. /api/myservice/graphql. Full URL for others.")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        public string Query { get; set; }
    }
}

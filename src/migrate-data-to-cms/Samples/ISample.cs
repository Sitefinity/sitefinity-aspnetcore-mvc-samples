using Progress.Sitefinity.RestSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples
{
    internal interface ISample
    {
        Task Run(IRestClient restClient);
    }
}

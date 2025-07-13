using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My.Core.Repository.Base;
using MyApp.Core.Models;

namespace MyApp.Core.Repository.Lookup
{
    public interface ILookupRepository : IRepository<GenLookup>
    {
        Task<List<GenLookup>> GetLookupsByTypeAsync(int lookupType);

        Task<List<GenLookup>> GetStoresGategory();

    }
}
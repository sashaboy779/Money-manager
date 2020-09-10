using System.Collections.Generic;

namespace MoneyManagerUi.Data
{
    public class PagedResponse<TData> where TData : class
    {
        public IEnumerable<TData> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
    }
}

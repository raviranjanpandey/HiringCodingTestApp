using System;
using System.Collections.Generic;
using System.Text;

namespace HiringCodingTestApis.Core.Filters
{
    public class PaginationFilter
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

        public int GetSkip()
        {
            int _skip = 0;
            if (PageNumber != null && PageNumber > 0 && PageSize != null && PageSize > 0)
            {
                _skip = ((int)PageNumber - 1) * (int)PageSize;
            }
            return _skip;
        }

        public int GetTake()
        {
            return PageSize ?? 0;
        }
    }
    public class SearchFilter
    {
        //column name as per database
        public string ColumnName { get; set; }
        //string or number or date
        public string ColumnType { get; set; }
        //contains, isEqualTo etc.
        public string ColumnOperator { get; set; }
        //and /or
        public string QueryOperator { get; set; }
        //first search string
        public string SearchValue { get; set; }
        //second search string
        public string SecondSearchValue { get; set; }
    }
}

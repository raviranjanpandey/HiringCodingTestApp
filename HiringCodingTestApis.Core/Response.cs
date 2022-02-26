using System.Collections.Generic;
using System.Linq;

namespace HiringCodingTestApis.Core
{
    public class Response
    {
        public class GetAllResponse<T> where T : class
        {

            public IEnumerable<T> Data { get; private set; }
            public int Records { get; private set; }
            public int TotalRecords { get; private set; }
            public GetAllResponse(IEnumerable<T> data)
            {
                Data = data;
                Records = data != null ? data.ToList().Count : 0;

            }
            public GetAllResponse(IEnumerable<T> data, int totalCount)
            {
                Data = data;
                Records = data != null ? data.ToList().Count : 0;
                TotalRecords = totalCount;
            }


        }

        public class GetResponse<T> where T : class
        {
            public T Data { get; private set; }
            public GetResponse(T data)
            {
                Data = data;
            }
        }

    }


}

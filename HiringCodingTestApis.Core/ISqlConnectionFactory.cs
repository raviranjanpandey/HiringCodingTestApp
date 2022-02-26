using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HiringCodingTestApis.Core
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
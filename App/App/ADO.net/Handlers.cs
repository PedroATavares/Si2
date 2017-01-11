using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Handler
    {
        public readonly string CONNECTION_STRING;
        public Handler(String cs)
        {
            CONNECTION_STRING = cs;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Model.ResultModel
{
    public enum ErrorCode
    {
        ValidationError,
        InternalServerError,
        NotFound,
        Conflict
    }
}

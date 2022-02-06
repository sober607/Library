using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Model.ResultModel
{
    public class ResultModel<T>
    {
        public T Data { get; set; }

        public ErrorData Error { get; set; }

        public static ResultModel<T> GetSuccess(T transferredData)
        {
            if(object.Equals(transferredData, default(T)) && typeof (T) != typeof(bool))
            {
                throw new ArgumentNullException();
            }
            else
            {
                var dataToReturn = new ResultModel<T>()
                {
                    Data = transferredData
                };

                return dataToReturn;
            }
        }

        public static ResultModel<T> GetError(ErrorCode errorCode, string errorMessage)
        {
            var resultModelWithErrorToReturn = new ResultModel<T>()
            {
                Error = new ErrorData()
                {
                    Code = errorCode,
                    Message = errorMessage
                }
            };

            return resultModelWithErrorToReturn;
        }
    }
}

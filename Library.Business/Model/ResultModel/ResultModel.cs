using Library.Business.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

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

            return new ResultModel<T>() { Data = transferredData };
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

        public static ActionResult ToActionResult(ResultModel<T> resultModel)
        {
            ResultModel<T> result;

            if (resultModel.Data == null && resultModel.Error == default )
            {
                result = new ResultModel<T>
                {
                    Error = new ErrorData()
                    {
                        Code = ErrorCode.InternalServerError,
                        Message = "No data received while processing data model"
                    }
                };

                return new JsonResult(ToErrorDto(result.Error)) { StatusCode = 500 };
            }
            else if (resultModel.Error != null)
            {
                switch (resultModel.Error.Code)
                {
                    //case ErrorCode.Unauthorized:
                    //    return new JsonResult(ToErrorDto(result.Error)) { StatusCode = 401 };//401
                    case ErrorCode.ValidationError:
                        return new JsonResult(ToErrorDto(resultModel.Error)) { StatusCode = 400 };//400
                    case ErrorCode.InternalServerError:
                        return new JsonResult(ToErrorDto(resultModel.Error)) { StatusCode = 500 };//500
                    case ErrorCode.NotFound:
                        return new JsonResult(ToErrorDto(resultModel.Error)) { StatusCode = 404 };//404
                    case ErrorCode.Conflict:
                        return new JsonResult(ToErrorDto(resultModel.Error)) { StatusCode = 409 };//409
                    default:
                        return new JsonResult(ToErrorDto(resultModel.Error)) { StatusCode = 500 };
                }
            }
            else if (!object.Equals(resultModel.Data, default(T)) || resultModel.Data.GetType() == typeof(bool))
            {
                return new OkObjectResult(resultModel.Data);
            }
            else
            {
                return new OkResult();
            }

            ErrorDto ToErrorDto(ErrorData errorData)
            {
                var errorDtoData = new ErrorDto
                {
                    Error = errorData
                };

                return errorDtoData;
            }
        }
    }
}

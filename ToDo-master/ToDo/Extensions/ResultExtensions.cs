using System;
using ToDo.Common;
using ToDo.Core.Common;

namespace ToDo.Extensions
{
    public static class ResultExtensions
    {
        public static ApiError ToApiError<T>(this Result<T> result)
        {
            if (result.Succeeded)
            {
                throw new InvalidOperationException("Result has succeeded");
            }

            return new ApiError(result.Field, result.Message);
        }

        public static T ToApiResponse<T>(this Result<T> result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Result has failed");
            }

            return result.Data;
        }
    }
}
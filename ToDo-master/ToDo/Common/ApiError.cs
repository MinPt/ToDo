using System.Collections.Generic;

namespace ToDo.Common
{
    public class ApiError
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; }

        public ApiError(string field, string error)
        {
            Errors = new Dictionary<string, IEnumerable<string>>
            {
                {field, new[] {error}}
            };
        }
    }
}
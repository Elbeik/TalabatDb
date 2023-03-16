using System.Collections;
using System.Collections.Generic;

namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorRespone : ApiResponse
    {
        public IEnumerable<string> ErrorsResponse { get; set; }
        public ApiValidationErrorRespone() : base(400)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonanClaimsWebApi.Models
{
    public class ExecutionObjectResult<T>
    {
        public string SuccessMessage { get; set; }
        public string FailureMessage { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFailure { get; set; }
        public T ResultObject { get; set; }
    }

    public class ExecutionResult
    {
        public string SuccessMessage { get; set; }
        public string FailureMessage { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFailure { get; set; }
        public string ResultObject { get; set; }
    }
}

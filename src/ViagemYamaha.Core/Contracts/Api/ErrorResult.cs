using System.Collections.Generic;
using System.Text.Json;

namespace ViagemYamaha.Core.Contracts.Api
{
    public class ErrorResult
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

}

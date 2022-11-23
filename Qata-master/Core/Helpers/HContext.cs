using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public static class HContext
    {
        public static HttpContext _current() => new HttpContextAccessor().HttpContext;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PdfSecureAPI.Entity
{
    public class HttpInput
    {
        public string Pdf { get; set; }
        public string Password { get; set; }
    }
}

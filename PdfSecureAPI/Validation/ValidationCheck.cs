﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfSecureAPI.Validation
{
    public static  class ValidationCheck
    {
        public static bool IsBase64String(string base64)
        {
            base64 = base64.Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}

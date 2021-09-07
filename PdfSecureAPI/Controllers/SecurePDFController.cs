﻿using Microsoft.AspNetCore.Mvc;
using PdfSecureAPI.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using pdf = PdfSharpCore;
using pdfIO = PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace PdfSecureAPI.Controllers
{
    [Route("api/SecurePDF")]
    [ApiController]
    public class SecurePDFController : Controller
    {
        [HttpPost]
        [Route("PDF")]
        public IActionResult CreatePDFAsync(HttpInput req)
        {
            try
            {
                var myStr = req.Pdf.Trim('"');
                var imageBytes = Convert.FromBase64String(myStr);
                MemoryStream mem = new MemoryStream(imageBytes);


                pdf.Pdf.PdfDocument document = pdf.Pdf.IO.PdfReader.Open(mem, pdfIO.PdfDocumentOpenMode.Modify);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1252 = Encoding.GetEncoding(1252);

                PdfSecuritySettings securitySettings = document.SecuritySettings;


                securitySettings.UserPassword = req.Password;
                securitySettings.OwnerPassword = req.Password;



                // Restrict some rights.
                securitySettings.PermitAccessibilityExtractContent = false;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = false;
                securitySettings.PermitFormsFill = true;
                securitySettings.PermitFullQualityPrint = false;
                securitySettings.PermitModifyDocument = true;
                securitySettings.PermitPrint = false;

                MemoryStream ms = new MemoryStream();
                document.Save(ms);
                ms.Position = 0;
                return new FileContentResult(ms.ToArray(), "application/pdf") { FileDownloadName = "test.pdf" };



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared
{
    public static class HelperMethods
    {
        /// <summary>
        /// Retrieves the MimeType bound to the given filename or extension by looking into the Windows Registry entries.
        /// NOTE: This method supports only the MimeTypes registered in the server OS / Windows installation.
        /// </summary>
        /// <param name="fileNameOrExtension">a valid filename (file.txt) or extension (.txt or txt)</param>
        /// <returns>A valid Mime Type (es. text/plain)</returns>
        public static String? GetMimeTypeByWindowsRegistry(String? fileNameOrExtension)
        {
            if (string.IsNullOrEmpty(fileNameOrExtension))
            {
                return null;
            }
            string mimeType = "application/unknown";
            string ext = (fileNameOrExtension.Contains(".")) ? System.IO.Path.GetExtension(fileNameOrExtension).ToLower() : "." + fileNameOrExtension;
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Adapter.Blob.Repository
{
    internal static class FileHelper
    {
        private static Dictionary<string, string> mimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".bmp", "image/bmp" },
            { ".tiff", "image/tiff" },
            { ".ico", "image/x-icon" },
            { ".svg", "image/svg+xml" },
            { ".pdf", "application/pdf" },
            { ".txt", "text/plain" },
            { ".html", "text/html" },
            { ".css", "text/css" },
            { ".js", "application/javascript" },
            { ".json", "application/json" },
            { ".xml", "application/xml" },
        };

        public static string GetContentTypeByExtension(this string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
            {
                throw new ArgumentException("File extension cannot be null or empty.", nameof(fileExtension));
            }

            if (fileExtension[0] != '.')
            {
                fileExtension = "." + fileExtension;
            }

            if (mimeTypes.TryGetValue(fileExtension, out string contentType))
            {
                return contentType;
            }
            else
            {
                return "application/octet-stream";
            }
        }

        public static string GetContentTypeByFileName(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            }

            string fileExtension = Path.GetExtension(fileName);

            return fileExtension.GetContentTypeByExtension();
        }
    }
}

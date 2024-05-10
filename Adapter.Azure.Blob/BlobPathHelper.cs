using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Azure.Blob
{
    public static class BlobPathHelper
    {
        private static string BaseUrl { get; set; }
        private static readonly object _lock = new();

        internal static void Initialize(string baseUrl)
        {
            lock (_lock)
            {
                BaseUrl = baseUrl;
            }
        }

        public static string GetUrl(string container, string filename)
        {
            if (filename is null) return null;

            return $"{BaseUrl.TrimEnd('/')}/{container.TrimStart('/')}/{filename.TrimStart('/')}";
        }
    }
}

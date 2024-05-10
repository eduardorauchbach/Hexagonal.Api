using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Common.Extensions
{
    public static class ByteExtension
    {
        public static Stream ToStream(this byte[] byteArray)
        {
            // Create a new MemoryStream and write the byte array into it
            MemoryStream stream = new MemoryStream();
            stream.Write(byteArray, 0, byteArray.Length);

            // Reset the position of the MemoryStream to the beginning
            stream.Seek(0, SeekOrigin.Begin);

            // Return the stream with its position set to the beginning
            return stream;
        }
    }
}

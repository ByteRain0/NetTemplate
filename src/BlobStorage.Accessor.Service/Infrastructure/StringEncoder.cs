using System;
using System.Text;

namespace BlobStorage.Accessor.Contracts.Utilities;

public static class StringEncoder
{
    public static bool IsBase64String(this string base64)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
    }
        
    public static string EncodeToBase64(this string toEncode)
    {
        byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);

        string returnValue = Convert.ToBase64String(toEncodeAsBytes);

        return returnValue;
    }
}
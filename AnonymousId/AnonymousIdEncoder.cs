using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace ReturnTrue.AspNetCore.Identity.Anonymous
{
    internal static class AnonymousIdEncoder
    {
        internal static string Encode(AnonymousIdData data)
        {
            if (data == null)
            {
                return null;
            }

            byte[] bufferId = Encoding.UTF8.GetBytes(data.AnonymousId);
            byte[] bufferIdLenght = BitConverter.GetBytes(bufferId.Length);
            byte[] bufferDate = BitConverter.GetBytes(data.ExpireDate.ToFileTimeUtc());
            byte[] buffer = new byte[12 + bufferId.Length];

            Buffer.BlockCopy(bufferDate, 0, buffer, 0, 8);
            Buffer.BlockCopy(bufferIdLenght, 0, buffer, 8, 4);
            Buffer.BlockCopy(bufferId, 0, buffer, 12, bufferId.Length);

            return Base64UrlEncoder.Encode(buffer);
        }

        internal static AnonymousIdData Decode(string data)
        {
            if (data == null || data.Length < 1)
            {
                return null;
            }

            try
            {
                byte[] blob = Base64UrlEncoder.DecodeBytes(data);

                if (blob == null || blob.Length < 13)
                {
                    return null;
                }

                DateTime expireDate = DateTime.FromFileTimeUtc(BitConverter.ToInt64(blob, 0));

                if (expireDate < DateTime.UtcNow)
                {
                    return null;
                }

                int len = BitConverter.ToInt32(blob, 8);

                if (len < 0 || len > blob.Length - 12)
                {
                    return null;
                }

                string id = Encoding.UTF8.GetString(blob, 12, len);

                return new AnonymousIdData(id, expireDate);
            }
            catch { }

            return null;
        }
    }
}
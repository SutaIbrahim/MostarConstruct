using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public static class Slika
    {
        public static string GetSlika(byte[] slika, string contentType)
        {
            string str64 = Convert.ToBase64String(slika);
            string slikaSrc = string.Format($"data:{contentType};base64,{str64}");
            return slikaSrc;
        }
    }
}

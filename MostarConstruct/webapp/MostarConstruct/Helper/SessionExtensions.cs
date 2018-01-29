using Microsoft.AspNetCore.Http;
using MostarConstruct.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value) => session.SetString(key, JsonConvert.SerializeObject(value));
        public static T GetJson<T>(this ISession session, string key) => session.GetString(key) == null ? default(T) : JsonConvert.DeserializeObject<T>(session.GetString(key));
    }
}

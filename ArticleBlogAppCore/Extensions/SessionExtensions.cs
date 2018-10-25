using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Extensions
{
    public static class SessionExtensions
    {
        public static string TryGetSessionValue(this ISession session,string key)
        {
            byte[] datas;
            if (session.TryGetValue(key, out datas))
            {
                return Encoding.UTF8.GetString(datas);
            }
            else
                return null;
        }
    }
}

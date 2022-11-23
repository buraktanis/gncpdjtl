using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Core.Extensions;

namespace Core.Helpers
{
    public static class CurrentSession
    {
        public static string id
        {
            get
            {

                return HContext._current().User.Identity.NameIdentifier();

            }
        }
       
        public static string Name
        {
            get
            {

                return HContext._current().User.Identity.Name();

            }
        } 
        public static string Token
        {
            get
            {

                return HContext._current().User.Identity.Token();

            }
        }  
        public static string PhotoUrl
        {
            get
            {

                return HContext._current().User.Identity.PhotoUrl();

            }
        }
        public static string GetCookies(string val)
        {
            return HContext._current().Request.Cookies[val];
        }
        public static string Username
        {
            get
            {

                return HContext._current().User.Identity.Name();

            }
        }

        public static bool IsAuthenticated
        {
            get
            {

                return HContext._current().User.Identity.IsAuthenticated;

            }
        }

        public static List<String> Roles
        {
            get
            {

                return HContext._current().User.Identity.Role();

            }
        }

        public static IIdentity identity
        {
            get
            {

                return HContext._current().User.Identity;

            }
        }


        //public static bool Role(string role)
        //{

        //    List<string> roles = new List<string>();
        //    roles = HContext._current().User.Identity.Role().ToList();
        //    var model = roles.FindIndex(x => x == role);
        //    if (model != -1)
        //    {
        //        return true;
        //    }
        //    return false;

        //}
        //public static void Set<T>(string key, T obj)
        //{
        //    HContext._current().Session.SetObject(key, obj);

        //}

        //public static T Get<T>(string key)
        //{
        //    if (HContext._current().Session.GetObject<T>(key) != null)
        //    {
        //        return (T)HContext._current().Session.GetObject<T>(key);
        //    }

        //    return default(T);
        //}

        //public static void Remove(string key)
        //{
        //    if (HContext._current().Session.GetString(key) != null)
        //    {
        //        HContext._current().Session.Remove(key);
        //    }
        //}

        //public static void Clear()
        //{
        //    HContext._current().Session.Clear();
        //}

    }
}

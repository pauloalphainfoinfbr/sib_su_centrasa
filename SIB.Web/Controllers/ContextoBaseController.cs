using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using SIB.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SIB.Web.Controllers
{        
    public sealed class Multiton
    {
        static Dictionary<string, Multiton> _multitons = new Dictionary<string, Multiton>();
        static object _lock = new object();

        private Multiton()
        {
            check = null;
        }

        public static Multiton GetInstance(string Key)
        {
            lock (_lock)
            {
                if (!_multitons.ContainsKey(Key)) _multitons.Add(Key, new Multiton());
            }
            return _multitons[Key];
        }

        public Data.checklist check { get; set; }
    }

    public sealed class GlobalBD<T> where T : class, new()
    {
        private static T _instance;

        public static T GetInstace()
        {
            lock (typeof(T))
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }
    }        
}

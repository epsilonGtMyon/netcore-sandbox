using System;
using System.Collections.Generic;
using System.Text;

namespace DispatchProxySandbox.Sandbox02
{
    public class Sandbox02Hoge : ISandbox02Hoge
    {
        private readonly string _x;
        public Sandbox02Hoge(string x)
        {
            _x = x;
        }

        public string DoHoge01(string value01, string value02)
        {
            return $"{_x}:{value01}: {value02}";
        }
    }

    public interface ISandbox02Hoge
    {

        string DoHoge01(string value01, string value02);
    }
}

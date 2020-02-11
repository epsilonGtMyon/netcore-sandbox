using System;

namespace DispatchProxySandbox.Sandbox02
{
    /// <summary>
    /// 前後割込み
    /// </summary>
    public class Sandbox02Main
    {
        public static void Main_(string[] args)
        {

            ISandbox02Hoge hoge =  Sandbox02Proxy.CreateProxy<ISandbox02Hoge>(new Sandbox02Hoge("original"));

            string ret =  hoge.DoHoge01("args01", "args02");
            Console.WriteLine(ret);
        }
    }
}

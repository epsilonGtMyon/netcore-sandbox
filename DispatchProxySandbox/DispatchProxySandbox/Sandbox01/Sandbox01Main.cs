using System;

namespace DispatchProxySandbox.Sandbox01
{
    /// <summary>
    /// とりあえず何もしないプロキシをつくってみる。
    /// </summary>
    public class Sandbox01Main
    {
        public static void Main_(string[] args)
        {
            //プロキシつくる
            ISandbox01Hoge hoge = Sandbox01Proxy.CreateProxy<ISandbox01Hoge>();

            //実行と戻り値の確認をそれぞれ

            string hoge01 = hoge.DoHoge01("hoge0101");
            Console.WriteLine(hoge01);

            string hoge02 = hoge.DoHoge02("hoge0201", "hoge0202");
            Console.WriteLine(hoge02);

            //-----------------------
            //インターフェースじゃないとダメみたい
            //DispatchProxyGenerator.GenerateProxyTypeあたりを参照


            //Sandbox01Foo foo = Sandbox01Proxy.CreateProxy<Sandbox01Foo>();

            //string foo01 = foo.DoFoo01("foo0101");
            //Console.WriteLine(foo01);

            //string foo02 = foo.DoFoo02("foo0201", "foo0202");
            //Console.WriteLine(foo02);
        }
    }

}

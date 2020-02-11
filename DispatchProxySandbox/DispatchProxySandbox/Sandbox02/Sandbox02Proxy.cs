using System;
using System.Linq;
using System.Reflection;

namespace DispatchProxySandbox.Sandbox02
{
    public class Sandbox02Proxy : DispatchProxy
    {
        //本体のインスタンス
        private object _instance;


        public static T CreateProxy<T>(object instance)
        {
            object t = Create<T, Sandbox02Proxy>();

            //tは Sandbox02Proxyを継承しTも実装してるからキャスト可能なようで..
            ((Sandbox02Proxy)t)._instance = instance;

            return (T)t;
        }


        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            //前処理
            Console.WriteLine($"before: {targetMethod.Name}({string.Join(", ", args.Select(x => x.ToString()))})");

            //本処理
            object ret = targetMethod.Invoke(_instance, args);

            //後処理
            Console.WriteLine($"after: {targetMethod.Name}: {ret}");

            return ret;
        }
    }
}

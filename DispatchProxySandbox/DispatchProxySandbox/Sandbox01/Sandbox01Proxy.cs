using System;
using System.Linq;
using System.Reflection;

namespace DispatchProxySandbox.Sandbox01
{
    public class Sandbox01Proxy : DispatchProxy
    {

        public static T CreateProxy<T>()
        {
            return Create<T, Sandbox01Proxy>();
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"メソッド名：{targetMethod.Name}");
            Console.WriteLine($"引数:{string.Join(", ", args.Select(x => x.ToString()))}");

            return $"なんかかえす。[{string.Join(", ", args.Select(x => x.ToString()))}]";
        }
    }
}

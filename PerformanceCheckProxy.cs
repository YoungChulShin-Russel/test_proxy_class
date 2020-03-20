using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace ProxyClass
{
    public class PerformanceCheckProxy<T> : RealProxy
    {
        private readonly T _instance;

        private PerformanceCheckProxy(T instance) : base(typeof(T))
        {
            _instance = instance;
        }

        public static T Create(T instance)
        {
            return (T)new PerformanceCheckProxy<T>(instance).GetTransparentProxy();
        }
        
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var method = (MethodInfo)methodCall.MethodBase;
            
            try
            {
                DateTime startTime = DateTime.Now;
                var returnVal = method.Invoke(_instance, methodCall.InArgs);
                Console.WriteLine($"ElapsedTime : {DateTime.Now.Subtract(startTime).TotalMilliseconds} (ms)");

                return new ReturnMessage(returnVal, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException && ex.InnerException != null)
                {
                    return new ReturnMessage(ex.InnerException, msg as IMethodCallMessage);
                }

                return new ReturnMessage(ex, msg as IMethodCallMessage);
            }
        }
    }
}

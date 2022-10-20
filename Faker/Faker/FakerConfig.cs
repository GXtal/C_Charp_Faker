using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public struct ConfigInfo
    {
        public Type configType;
        public MemberInfo member;
        public Type generatorType;
        public ConfigInfo(Type configType, MemberInfo member, Type generatorType)
        {
            this.configType = configType;
            this.member = member;
            this.generatorType = generatorType;
        }
    }
    public class FakerConfig
    {
        public FakerConfig()
        {
            Config = new List<ConfigInfo>();
        }

        public List<ConfigInfo> Config { get; set; }
        public void Add<TObj, TParam, TGenerator>(Expression<Func<TObj,TParam>> expression)
            where TObj : class
            where TGenerator : IValueGenerator
        {
            Type objectType = typeof(TObj);
            Type generatorType = typeof(TGenerator);

            MemberInfo field = (expression.Body as MemberExpression).Member;
            Config.Add(new ConfigInfo(objectType, field , generatorType));
        }



    }
    
}
    


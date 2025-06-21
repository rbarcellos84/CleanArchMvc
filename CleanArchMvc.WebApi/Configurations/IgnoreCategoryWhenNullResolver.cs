using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace CleanArchMvc.WebApi.Configurations
{
    public class IgnoreCategoryWhenNullResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            var property = base.CreateProperty(member, serialization);

            if (property.PropertyName == "Category")
            {
                property.NullValueHandling = NullValueHandling.Ignore;
            }

            return property;
        }
    }
}
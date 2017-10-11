using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms.Core.Infrastructure.FunqContainer
{
    internal sealed class ServiceKey
    {
        private int hash;
        public Type FactoryType;
        public string Name;

        public ServiceKey(Type factoryType, string serviceName)
        {
            this.FactoryType = factoryType;
            this.Name = serviceName;
            this.hash = factoryType.GetHashCode();
            if (serviceName == null)
                return;
            this.hash ^= serviceName.GetHashCode();
        }

        public bool Equals(ServiceKey other)
        {
            return ServiceKey.Equals(this, other);
        }

        public override bool Equals(object obj)
        {
            return ServiceKey.Equals(this, obj as ServiceKey);
        }

        public static bool Equals(ServiceKey obj1, ServiceKey obj2)
        {
            if (object.Equals(null, obj1) || object.Equals(null, obj2) || obj1.FactoryType != obj2.FactoryType)
                return false;
            return obj1.Name == obj2.Name;
        }

        public override int GetHashCode()
        {
            return this.hash;
        }
    }
}

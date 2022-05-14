namespace Xamarin.Core.Infrastructure.Container;

internal sealed class ServiceKey
{
    private readonly int _hash;
    private readonly Type _factoryType;
    private readonly string _name;

    public ServiceKey(Type factoryType, string serviceName)
    {
        _factoryType = factoryType;
        _name = serviceName;
        _hash = factoryType.GetHashCode();
        if (serviceName == null)
            return;
        _hash ^= serviceName.GetHashCode();
    }

    public bool Equals(ServiceKey other)
    {
        return Equals(this, other);
    }

    public override bool Equals(object obj)
    {
        return Equals(this, obj as ServiceKey);
    }

    public static bool Equals(ServiceKey obj1, ServiceKey obj2)
    {
        if (object.Equals(null, obj1) || object.Equals(null, obj2) || obj1._factoryType != obj2._factoryType)
            return false;
            
        return obj1._name == obj2._name;
    }

    public override int GetHashCode()
    {
        return _hash;
    }
}
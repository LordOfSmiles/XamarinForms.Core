using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xamarin.Core.Services
{
    public sealed class CloneService: ICloneService
    {
        public T Clone<T>(T original)
        {
            T result = default(T);

            try
            {
                var clone = JsonSerializer.Serialize(original);
                result= JsonSerializer.Deserialize<T>(clone);
            }
            catch
            {
                //
            }

            return result;
        }
    }

    public interface ICloneService
    {
        T Clone<T>(T original);
    }
}
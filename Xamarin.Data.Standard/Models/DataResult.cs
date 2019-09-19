using System;

namespace Xamarin.Data.Standard.Models
{
    public class DataResult
    {
        public DataResultEnum Type { get; set; } = DataResultEnum.Success;
        
        public bool IsSuccess => Type == DataResultEnum.Success;
        
        public Exception Error { get; set; }
    }
    
    public sealed class DataResult<T>:DataResult
    {
        public T Result { get; set; } = default(T);
    }

    public enum DataResultEnum
    {
        Success,
        Failed
    }
}
using System;

namespace Xamarin.Data.Models
{
    public class DbResult
    {
        public DataResultEnum Status { get; set; } = DataResultEnum.Success;
        
        public bool IsSuccess => Status == DataResultEnum.Success;
        
        public Exception Error { get; set; }
    }
    
    public sealed class DbResult<T>:DbResult
    {
        public T Result { get; set; } = default(T);
    }

    public enum DataResultEnum
    {
        Success,
        Failed
    }
}
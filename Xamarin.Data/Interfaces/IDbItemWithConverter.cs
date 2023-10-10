using Xamarin.Data.Dto;

namespace Xamarin.Data.Interfaces;

public interface IDbItemWithConverter<out TDto>
    where TDto : DtoBase
{
    public TDto ConvertToDto();
}
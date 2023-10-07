using Xamarin.Data.Dto;

namespace Xamarin.Data.Interfaces;

public interface IConvertableToDto<TDto>
    where TDto : DtoBase
{
    public TDto ConvertToDto();
}
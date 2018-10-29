using System.Threading.Tasks;

namespace Xamarin.Core.Standard.Infrastructure.Interfaces
{
    public interface IAdvRewarded
    {
        Task<bool> ShowAsync(string adUnitId);
    }
}
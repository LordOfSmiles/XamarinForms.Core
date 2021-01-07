using System.Threading.Tasks;

namespace Xamarin.Core.Infrastructure.Interfaces
{
    public interface IAdvRewarded
    {
        Task<bool> ShowAsync(string adUnitId);
    }
}
using System.Threading.Tasks;

namespace Xamarin.Core.Interfaces
{
    public interface IAdvRewarded
    {
        Task<bool> ShowAsync(string adUnitId);
    }
}
using System.Threading.Tasks;

namespace XamarinForms.Core.Interfaces
{
	public interface ISupportDelayInitialization
	{
		bool IsInitialized { get; set; }

		Task InitializeforOpenAsync ();
	}
}


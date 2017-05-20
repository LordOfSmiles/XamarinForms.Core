using System.Threading.Tasks;

namespace XamarinForms.Core.Interfaces
{
	public interface ISupportEditable
	{
		bool IsModified { get; }

		bool IsEditMode { get; set; }

		Task LoadProperties ();

		void ResetProperties ();
	}
}


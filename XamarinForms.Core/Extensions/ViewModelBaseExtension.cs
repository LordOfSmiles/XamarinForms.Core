using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Extensions;

public static class ViewModelBaseExtension
{
    public static async Task NavigateTo(this ViewModelBase viewModel, string pageName)
    {
        if (viewModel.InputTransparent)
            return;

        viewModel.InputTransparent = true;

        await Shell.Current.GoToAsync(pageName);

        viewModel.InputTransparent = false;
    }
}
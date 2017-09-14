using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public class AutoCompleteView : View
    {
        public AutoCompleteView()
        {

        }

        #region BindableProperties

        #region SearchKeyword

        public static readonly BindableProperty SearchKeywordProperty = BindableProperty.Create(nameof(SearchKeyword), typeof(string), typeof(AutoCompleteView), string.Empty, BindingMode.TwoWay);

        public string SearchKeyword
        {
            get => (string)GetValue(SearchKeywordProperty);
            set => SetValue(SearchKeywordProperty, value);
        }

        #endregion

        //#region Suggestion

        //public static readonly BindableProperty SuggestionsProperty = BindableProperty.Create(nameof(Suggestions), typeof(ObservableCollection<string>), typeof(AutoCompleteView), new ObservableCollection<string>(), BindingMode.OneWay);

        //public ObservableCollection<string> Suggestions
        //{
        //    get { return (ObservableCollection<string>)GetValue(SuggestionsProperty); }
        //    set { SetValue(SuggestionsProperty, value); }
        //}

        //#endregion

        #region SelectedItem

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(AutoCompleteView), null, BindingMode.OneWay);

        public string SelectedItem
        {
            get => (string)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        #endregion

        #endregion

        #region Properties

        public Func<string, Task<IEnumerable<string>>> SearchDelegate { get; set; }


        #endregion

    }
}

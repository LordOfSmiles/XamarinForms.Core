using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Core.Standard.ViewModels.ErrorValidation;

namespace Xamarin.Core.Standard.ViewModels
{
    public abstract class ValidationViewModelBase : ViewModelBase
    {
        #region Fields

        private readonly List<PropertyValidation> _validations = new List<PropertyValidation>();

        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();

        #endregion

        protected ValidationViewModelBase()
        {
        }

        #region INotifyDataErrorInfo implementation

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = (s, e) => { };

        public IEnumerable<string> GetErrors(string propertyName)
        {
            if (_errorMessages.ContainsKey(propertyName))
                return _errorMessages[propertyName];

            return new string[0];
        }

        public string GetFirstError(string propertyName)
        {
            if (_errorMessages.ContainsKey(propertyName) && _errorMessages[propertyName].Any())
                return _errorMessages[propertyName].First();

            return string.Empty;
        }



        public bool HasErrors => _errorMessages.Any();

        public Dictionary<string, List<string>> ErrorMessages => _errorMessages;

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected PropertyValidation AddValidationFor(string propertyName)
        {
            var validation = new PropertyValidation(propertyName);
            _validations.Add(validation);

            return validation;
        }

        public void ValidateAll()
        {
            var propertyNamesWithValidationErrors = _errorMessages.Keys;
            _errorMessages = new Dictionary<string, List<string>>();

            foreach (var propertyValidation in _validations)
            {
                PerformValidation(propertyValidation);
            }

            var propertyNamesThatMightHaveChangedValidation = _errorMessages.Keys.Union(propertyNamesWithValidationErrors).ToList();
            foreach (var propertyName in propertyNamesThatMightHaveChangedValidation)
            {
                OnErrorsChanged(propertyName);
            }

            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(ErrorMessages));
        }

        public void ValidateProperty([CallerMemberName] string propertyName = null)
        {
            DoValidateProperty(propertyName);
        }

        private void DoValidateProperty(string propertyName)
        {
            _errorMessages.Remove(propertyName);
            var propertyValidations = _validations.Where(v => v.PropertyName == propertyName).ToList();
            foreach (var item in propertyValidations)
            {
                PerformValidation(item);
            }

            OnErrorsChanged(propertyName);

            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(ErrorMessages));
        }

        private void PerformValidation(PropertyValidation validation)
        {
            if (validation.IsInvalid())
            {
                AddErrorMessageForProperty(validation.PropertyName, validation.GetErrorMessage());
            }
        }


        private void AddErrorMessageForProperty(string propertyName, string errorMessage)
        {
            if (_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages[propertyName].Add(errorMessage);
            }
            else
            {
                var orderedPropertyNames = _validations.Select(x => x.PropertyName).Distinct().ToArray();

                var tempErrors = _errorMessages.ToDictionary(x => x.Key, y => y.Value);
                tempErrors.Add(propertyName, new List<string> { errorMessage });

                _errorMessages.Clear();

                foreach (var orderedPropertyName in orderedPropertyNames)
                {
                    if (tempErrors.ContainsKey(orderedPropertyName) && !_errorMessages.ContainsKey(orderedPropertyName))
                    {
                        _errorMessages.Add(orderedPropertyName, tempErrors[orderedPropertyName]);
                    }
                }
            }
        }

        #endregion
    }
}

﻿namespace XamarinForms.Core.ViewModels.ErrorValidation;

public sealed class PropertyValidation
{
    #region Fields

    private Func<bool> _validationCriteria;
    private string _errorMessage;

    #endregion

    #region Constructor

    public PropertyValidation(string propertyName)
    {
        PropertyName = propertyName;
    }

    #endregion

    #region Properties

    public string PropertyName { get; }

    #endregion

    #region Public Methods

    public PropertyValidation When(Func<bool> validationCriteria)
    {
        if (_validationCriteria != null)
            throw new InvalidOperationException("You can only set the validation criteria once.");

        _validationCriteria = validationCriteria;
        return this;
    }

    public PropertyValidation Show(string errorMessage)
    {
        if (_errorMessage != null)
            throw new InvalidOperationException("You can only set the message once.");

        _errorMessage = errorMessage;
        return this;
    }

    public bool IsInvalid()
    {
        if (_validationCriteria == null)
            throw new InvalidOperationException("No criteria have been provided for this validation. (Use the 'When(..)' method.)");

        return _validationCriteria();
    }

    public string GetErrorMessage()
    {
        if (_errorMessage == null)
            throw new InvalidOperationException("No error message has been set for this validation. (Use the 'Show(..)' method.)");

        return _errorMessage;
    }

    #endregion
}
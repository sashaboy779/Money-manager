﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace MoneyManagerApi.Infrastructure.ValidationAttributes
{
    public class GenericAttributeAdapter<TAttribute> : AttributeAdapterBase<TAttribute> 
        where TAttribute: ValidationAttribute
    {
        public GenericAttributeAdapter(TAttribute attribute, IStringLocalizer stringLocalizer) 
            : base(attribute, stringLocalizer) {}

        public override void AddValidation(ClientModelValidationContext context) {}

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}
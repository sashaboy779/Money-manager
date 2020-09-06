using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace MoneyManagerApi.Infrastructure.ValidationAttributes
{
    public class CustomValidationAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is NotZeroAttribute amountAttribute)
                return new GenericAttributeAdapter<NotZeroAttribute>(amountAttribute, stringLocalizer);
            if (attribute is NotUnknownAttribute unknownAttribute)
                return new GenericAttributeAdapter<NotUnknownAttribute>(unknownAttribute, stringLocalizer);
            
            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
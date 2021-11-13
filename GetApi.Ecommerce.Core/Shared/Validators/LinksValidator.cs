using FluentValidation;
using FluentValidation.Validators;
using System;

namespace GetApi.Ecommerce.Core.Shared.Validators
{
    public class LinkValidatorProperty<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override bool IsValid(ValidationContext<T> context, TProperty property)
        {
            return LinkMustBeAUri(property as string);
        }

        private static bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return false;

            Uri outUri;

            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }

        public override string Name => nameof(LinkValidatorProperty<T, TProperty>);

        protected override string GetDefaultMessageTemplate(string errorCode) => "The field {PropertyName} is required.";
    }
}

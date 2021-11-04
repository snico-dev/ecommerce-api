using GetApi.Ecommerce.Core.Catalog.Requests;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace GetApi.Ecommerce.Api.Helpers
{
    public static class ValidatorExtension
    {
        public static (bool, IEnumerable<ValidationResult>) Validate<T>(this T entity) where T : IValidatable
        {
            var context = new ValidationContext(entity, null, null);
            var validationResults = new List<ValidationResult>();

            bool valid = TryValidateObjectRecursive(entity, context, validationResults, true);

            return (valid, validationResults);
        }

        public static bool TryValidateObjectRecursive(object obj, ValidationContext context, List<ValidationResult> results, bool validateAllProperties)
        {
            bool result = Validator.TryValidateObject(obj, context, results, validateAllProperties);

            var properties = obj.GetType().GetProperties().Where(prop => prop.CanRead && prop.GetIndexParameters().Length == 0).ToList();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string) || property.PropertyType.IsValueType) continue;

                object value = property.GetValue(obj);

                if (value == null) continue;

                var asEnumerable = value as IEnumerable;

                if (asEnumerable != null)
                {
                    foreach (var enumObj in asEnumerable)
                    {
                        var nestedResult = Validate(results, validateAllProperties, result, property, value);
                        if (nestedResult is false) result = nestedResult;
                    }
                }
                else
                {
                    var nestedResult = Validate(results, validateAllProperties, result, property, value);
                    if (nestedResult is false) result = nestedResult;
                }
            }

            return result;
        }

        private static bool Validate(List<ValidationResult> results, bool validateAllProperties, bool result, PropertyInfo property, object value)
        {
            var nestedResults = new List<ValidationResult>();
            var nestedContext = new ValidationContext(value, null, null);
            if (!TryValidateObjectRecursive(value, nestedContext, nestedResults, validateAllProperties))
            {
                result = false;
                foreach (var validationResult in nestedResults)
                {
                    PropertyInfo property1 = property;
                    results.Add(new ValidationResult(validationResult.ErrorMessage, validationResult.MemberNames.Select(x => property1.Name + '.' + x)));
                }
            }

            return result;
        }
    }
}

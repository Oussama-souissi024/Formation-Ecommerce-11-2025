using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Test.Common;

/// <summary>
/// Helper de tests pour valider un objet via les DataAnnotations (ex: <see cref="RequiredAttribute" />).
/// </summary>
/// <remarks>
/// Dans une application ASP.NET Core, la validation des DataAnnotations est souvent faite automatiquement
/// via ModelBinding / ModelState. En tests unitaires, on peut reproduire cette validation manuellement.
/// </remarks>
public static class ValidationHelper
{
    public static IList<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);

        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }
}

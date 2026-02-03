using Formation_Ecommerce_11_2025.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Formation_Ecommerce_11_2025.Test.Fakes;

/// <summary>
/// Fake de <see cref="IFileHelper" />.
/// </summary>
/// <remarks>
/// Objectif pédagogique :
/// - Ne pas écrire de fichiers sur disque pendant les tests.
/// - Compter le nombre d'appels (UploadCallCount/DeleteCallCount) pour vérifier la logique du service.
/// </remarks>
public class FakeFileHelper : IFileHelper
{
    public int UploadCallCount { get; private set; }
    public int DeleteCallCount { get; private set; }

    public string? UploadReturnValue { get; set; } = "file.png";
    public bool DeleteReturnValue { get; set; } = true;

    public string? UploadFile(IFormFile file, string folder)
    {
        UploadCallCount++;
        return UploadReturnValue;
    }

    public bool DeleteFile(string Path, string folder)
    {
        DeleteCallCount++;
        return DeleteReturnValue;
    }
}

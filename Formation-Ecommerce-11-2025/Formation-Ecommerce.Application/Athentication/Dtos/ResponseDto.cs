using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Athentication.Dtos
{
    /// <summary>
    /// DTO de réponse générique : indique le succès/échec d'une opération et porte un message d'erreur éventuel.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce type de wrapper est pratique pour uniformiser les retours de services (ex: Login).
    /// - Il évite de propager des exceptions "métier" vers la couche UI quand un simple état suffit.
    /// - Dans un projet plus avancé, on peut enrichir ce modèle (code d'erreur, données, liste de validations).
    /// </remarks>
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; } = string.Empty;

        public ResponseDto() { }

        public ResponseDto(bool isSuccess, string error = "")
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}

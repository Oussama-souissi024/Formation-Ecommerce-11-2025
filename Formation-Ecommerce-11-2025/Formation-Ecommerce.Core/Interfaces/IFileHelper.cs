using Microsoft.AspNetCore.Http;

namespace Formation_Ecommerce_11_2025.Core.Interfaces
{
    // Interface pour la gestion des fichiers(upload/suppression d'images)
    /// <summary>
    /// Contrat de gestion de fichiers (upload/suppression) : abstraction pour manipuler des fichiers sans exposer la technique aux couches supérieures.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La couche Core définit le "quoi" (contrat) ; Infrastructure fournit le "comment" (accès disque, wwwroot, etc.).
    /// - Cette abstraction facilite les tests : on peut remplacer l'implémentation par un fake/mock.
    /// - Les services applicatifs (ex: produits) utilisent ce contrat pour gérer les images sans dépendre du système de fichiers.
    /// </remarks>
    public interface IFileHelper
    {
        // Télécharge un fichier vers le serveur et retourne son chemin (ou null si échec)
        public string? UploadFile(IFormFile file, string folder);

        // Supprime un fichier du serveur (retourne true si succès)
        public bool DeleteFile(string Path, string folder);
    }
}

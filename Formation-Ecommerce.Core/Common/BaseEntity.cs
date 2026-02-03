using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Core.Common
{
    // Classe de base dont héritent toutes les entités (Product, Category, Order, etc.)
    // Fournit des propriétés communes: identifiant unique et dates de suivi
    /// <summary>
    /// Classe de base des entités du domaine : fournit des propriétés communes (Id, dates de suivi).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Dans une application en couches, les entités partagent souvent des champs techniques (clé primaire, audit).
    /// - Le choix d'un <see cref="Guid"/> comme identifiant simplifie la génération côté serveur et les scénarios distribués.
    /// - Les propriétés de dates (création / modification) facilitent le suivi (audit) et peuvent être alimentées par EF Core.
    /// </remarks>
    public abstract class BaseEntity
    {
        // Identifiant unique de l'entité (clé primaire, GUID)
        public Guid Id { get; set; }

        // Date de création de l'entité dans la base de données
        public DateTime CreatedDate { get; set; }

        // Date de dernière modification (null si jamais modifiée)
        public DateTime? LastModifiedDate { get; set; }
    }
}

using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    /// <summary>
    /// Interface pour le service de gestion des commandes temporaires par utilisateur.
    /// Permet d'ajouter, supprimer et modifier les ingrédients commandés
    /// par chaque utilisateur, ainsi que de gérer leurs quantités.
    /// </summary>
    public interface IOrderCartService
    {
        /// <summary>
        /// Ajoute un ingrédient à la commande temporaire d'un utilisateur.
        /// </summary>
        void AddIngredient(int userId, Ingredient ingredient, int quantity);

        /// <summary>
        /// Supprime un ingrédient de la commande temporaire d'un utilisateur.
        /// </summary>
        void RemoveIngredient(int userId, Ingredient ingredient);

        /// <summary>
        /// Met à jour la quantité d'un ingrédient pour un utilisateur.
        /// Si l'ingrédient n'existe pas, il est ajouté.
        /// </summary>
        void SetIngredientQuantity(int userId, Ingredient ingredient, int quantity);

        Dictionary<Ingredient, int> GetOrderQuantitiesForEmployee(int userId);

        /// <summary>
        /// Récupère tous les ingrédients de la commande temporaire d'un utilisateur.
        /// </summary>
        Dictionary<Ingredient, int> GetOrderIngredients(int userId);

        /// <summary>
        /// Vide la commande temporaire d'un utilisateur.
        /// </summary>
        void ClearOrder(int userId);

        /// <summary>
        /// Vide toutes les commandes temporaires de tous les utilisateur.
        /// </summary>
        void ClearAllOrders();
    }
}

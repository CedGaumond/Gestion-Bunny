using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    /// <summary>
    /// Interface pour le service de gestion des commandes temporaires par employé.
    /// Permet d'ajouter, supprimer et modifier les ingrédients commandés
    /// par chaque employé, ainsi que de gérer leurs quantités.
    /// </summary>
    public interface IOrderCartService
    {
        /// <summary>
        /// Ajoute un ingrédient à la commande temporaire d'un employé.
        /// </summary>
        void AddIngredient(int employeeId, Ingredient ingredient, int quantity);

        /// <summary>
        /// Supprime un ingrédient de la commande temporaire d'un employé.
        /// </summary>
        void RemoveIngredient(int employeeId, Ingredient ingredient);

        /// <summary>
        /// Met à jour la quantité d'un ingrédient pour un employé.
        /// Si l'ingrédient n'existe pas, il est ajouté.
        /// </summary>
        void SetIngredientQuantity(int employeeId, Ingredient ingredient, int quantity);

        Dictionary<Ingredient, int> GetOrderQuantitiesForEmployee(int employeeId);

        /// <summary>
        /// Récupère tous les ingrédients de la commande temporaire d'un employé.
        /// </summary>
        Dictionary<Ingredient, int> GetOrderIngredients(int employeeId);

        /// <summary>
        /// Vide la commande temporaire d'un employé.
        /// </summary>
        void ClearOrder(int employeeId);

        /// <summary>
        /// Vide toutes les commandes temporaires de tous les employés.
        /// </summary>
        void ClearAllOrders();
    }
}

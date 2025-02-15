using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    /// <summary>
    /// Service de gestion des commandes temporaires par employé.
    /// Permet d'ajouter, supprimer et modifier les ingrédients commandés
    /// par chaque employé, ainsi que de gérer leurs quantités.
    /// </summary>
    public class OrderCartService : IOrderCartService
    {
        // Dictionnaire: clé = employeeId, valeur = dictionnaire (clé = Ingredient, valeur = quantité)
        private readonly Dictionary<int, Dictionary<Ingredient, int>> _pendingOrders = new();

        /// <summary>
        /// Ajoute un ingrédient à la commande temporaire d'un employé.
        /// </summary>
        public void AddIngredient(int employeeId, Ingredient ingredient, int quantity)
        {
            if (!_pendingOrders.ContainsKey(employeeId))
            {
                _pendingOrders[employeeId] = new Dictionary<Ingredient, int>();
            }

            // Ajoute ou met à jour la quantité de l'ingrédient
            if (_pendingOrders[employeeId].ContainsKey(ingredient))
            {
                _pendingOrders[employeeId][ingredient] += quantity;
            }
            else
            {
                _pendingOrders[employeeId].Add(ingredient, quantity);
            }
        }

        /// <summary>
        /// Supprime un ingrédient de la commande temporaire d'un employé.
        /// </summary>
        public void RemoveIngredient(int employeeId, Ingredient ingredient)
        {
            if (_pendingOrders.ContainsKey(employeeId))
            {
                _pendingOrders[employeeId].Remove(ingredient);

                // Supprime l'entrée de l'employé si la commande est vide
                if (_pendingOrders[employeeId].Count == 0)
                {
                    _pendingOrders.Remove(employeeId);
                }
            }
        }

        /// <summary>
        /// Met à jour la quantité d'un ingrédient pour un employé.
        /// Si l'ingrédient n'existe pas, il est ajouté.
        /// </summary>
        public void SetIngredientQuantity(int employeeId, Ingredient ingredient, int quantity)
        {
            if (quantity <= 0)
            {
                RemoveIngredient(employeeId, ingredient);
                return;
            }

            if (!_pendingOrders.ContainsKey(employeeId))
            {
                _pendingOrders[employeeId] = new Dictionary<Ingredient, int>();
            }

            if (_pendingOrders[employeeId].ContainsKey(ingredient))
            {
                _pendingOrders[employeeId][ingredient] = quantity;
            }
            else
            {
                _pendingOrders[employeeId].Add(ingredient, quantity);
            }
        }

        public Dictionary<Ingredient, int> GetOrderQuantitiesForEmployee(int employeeId)
        { 
            return _pendingOrders[employeeId];
        }

        /// <summary>
        /// Récupère tous les ingrédients de la commande temporaire d'un employé.
        /// </summary>
        public Dictionary<Ingredient, int> GetOrderIngredients(int employeeId)
        {
            return _pendingOrders.ContainsKey(employeeId) ? _pendingOrders[employeeId] : new Dictionary<Ingredient, int>();
        }

        /// <summary>
        /// Vide la commande temporaire d'un employé.
        /// </summary>
        public void ClearOrder(int employeeId)
        {
            _pendingOrders.Remove(employeeId);
        }


        /// <summary>
        /// Vide toutes les commandes temporaires de tous les employés.
        /// </summary>
        public void ClearAllOrders()
        {
            _pendingOrders.Clear();
        }
    }
}

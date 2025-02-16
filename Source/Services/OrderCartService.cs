using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    /// <summary>
    /// Service de gestion des commandes temporaires par utilisateur.
    /// Permet d'ajouter, supprimer et modifier les ingrédients commandés
    /// par chaque utilisateur, ainsi que de gérer leurs quantités.
    /// </summary>
    public class OrderCartService : IOrderCartService
    {
        // Dictionnaire: clé = userId, valeur = dictionnaire (clé = Ingredient, valeur = quantité)
        private readonly Dictionary<int, Dictionary<Ingredient, int>> _pendingOrders = new();

        /// <summary>
        /// Ajoute un ingrédient à la commande temporaire d'un utilisateur.
        /// </summary>
        public void AddIngredient(int userId, Ingredient ingredient, int quantity)
        {
            if (!_pendingOrders.ContainsKey(userId))
            {
                _pendingOrders[userId] = new Dictionary<Ingredient, int>();
            }

            // Ajoute ou met à jour la quantité de l'ingrédient
            if (_pendingOrders[userId].ContainsKey(ingredient))
            {
                _pendingOrders[userId][ingredient] += quantity;
            }
            else
            {
                _pendingOrders[userId].Add(ingredient, quantity);
            }
        }

        /// <summary>
        /// Supprime un ingrédient de la commande temporaire d'un utilisateur.
        /// </summary>
        public void RemoveIngredient(int userId, Ingredient ingredient)
        {
            if (_pendingOrders.ContainsKey(userId))
            {
                _pendingOrders[userId].Remove(ingredient);

                // Supprime l'entrée de l'employé si la commande est vide
                if (_pendingOrders[userId].Count == 0)
                {
                    _pendingOrders.Remove(userId);
                }
            }
        }

        /// <summary>
        /// Met à jour la quantité d'un ingrédient pour un utilisateur.
        /// Si l'ingrédient n'existe pas, il est ajouté.
        /// </summary>
        public void SetIngredientQuantity(int userId, Ingredient ingredient, int quantity)
        {
            if (quantity <= 0)
            {
                RemoveIngredient(userId, ingredient);
                return;
            }

            if (!_pendingOrders.ContainsKey(userId))
            {
                _pendingOrders[userId] = new Dictionary<Ingredient, int>();
            }

            if (_pendingOrders[userId].ContainsKey(ingredient))
            {
                _pendingOrders[userId][ingredient] = quantity;
            }
            else
            {
                _pendingOrders[userId].Add(ingredient, quantity);
            }
        }

        /// <summary>
        /// Récupère les quantités d'ingrédients associées à un employé dans les commandes en attente.
        /// Si aucun résultat n'est trouvé pour l'utilisateur, un dictionnaire vide est retourné.
        /// </summary>
        /// <param name="userId">L'ID de l'utilisateur dont on souhaite récupérer les quantités de commande.</param>
        /// <returns>
        /// Un dictionnaire associant chaque ingrédient à sa quantité dans la commande de l'utilisateur.
        /// Si l'utilisateur n'a pas de commande en attente, un dictionnaire vide est retourné.
        /// </returns>
        public Dictionary<Ingredient, int> GetOrderQuantitiesForEmployee(int userId)
        {
            if (_pendingOrders.ContainsKey(userId))
            {
                return _pendingOrders[userId];
            }

            return new Dictionary<Ingredient, int>();
        }

        /// <summary>
        /// Récupère tous les ingrédients de la commande temporaire d'un utilisateur.
        /// </summary>
        public Dictionary<Ingredient, int> GetOrderIngredients(int userId)
        {
            return _pendingOrders.ContainsKey(userId) ? _pendingOrders[userId] : new Dictionary<Ingredient, int>();
        }

        /// <summary>
        /// Vide la commande temporaire d'un utilisateur.
        /// </summary>
        public void ClearOrder(int userId)
        {
            _pendingOrders.Remove(userId);
        }


        /// <summary>
        /// Vide toutes les commandes temporaires de tous les utilisateurs.
        /// </summary>
        public void ClearAllOrders()
        {
            _pendingOrders.Clear();
        }
    }
}

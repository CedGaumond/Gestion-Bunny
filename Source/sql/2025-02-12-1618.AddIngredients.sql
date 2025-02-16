INSERT INTO public.ingredients (name, quantity_remaining, quantity_per_delivery_unit, minimum_threshold_notification, deleted_date, is_deleted, price)
VALUES
    ('Farine', 100, 25, 10, NULL, false, 1.20),
    ('Sucre', 50, 10, 5, NULL, false, 0.75),
    ('Sel', 30, 5, 3, NULL, false, 0.50),
    ('Poivre', 20, 2, 1, NULL, false, 2.00),
    ('Huile d''olive', 40, 5, 5, NULL, false, 3.50),
    ('Beurre', 60, 10, 8, NULL, false, 2.30),
    ('Lait', 80, 20, 10, NULL, false, 1.50),
    ('Œufs', 90, 12, 6, NULL, false, 2.00),
    ('Levure', 15, 1, 2, NULL, false, 1.00),
    ('Bicarbonate de soude', 10, 2, 1, NULL, false, 0.80),
    ('Chocolat', 25, 5, 3, NULL, false, 2.50),
    ('Miel', 35, 7, 4, NULL, false, 3.00),
    ('Cannelle', 12, 3, 2, NULL, false, 1.80),
    ('Vanille', 18, 2, 1, NULL, false, 3.00),
    ('Riz', 100, 25, 15, NULL, false, 1.50),
    ('Pâtes', 110, 30, 20, NULL, false, 1.20),
    ('Tomates', 50, 10, 5, NULL, false, 2.00),
    ('Carottes', 40, 8, 4, NULL, false, 1.00),
    ('Pommes de terre', 60, 15, 10, NULL, false, 1.20),
    ('Oignons', 55, 12, 7, NULL, false, 1.50),
    ('Ail', 25, 5, 3, NULL, false, 0.90),
    ('Gingembre', 30, 5, 3, NULL, false, 2.50),
    ('Basilic', 10, 2, 1, NULL, false, 2.00),
    ('Thym', 12, 3, 2, NULL, false, 2.00),
    ('Romarin', 14, 3, 2, NULL, false, 2.50),
    ('Lentilles', 50, 12, 6, NULL, false, 1.80),
    ('Pois chiches', 45, 10, 5, NULL, false, 1.50),
    ('Maïs', 35, 8, 4, NULL, false, 1.20),
    ('Haricots rouges', 38, 10, 5, NULL, false, 2.00),
    ('Haricots blancs', 42, 11, 6, NULL, false, 2.00),
    ('Noix', 20, 5, 3, NULL, false, 3.50),
    ('Amandes', 22, 6, 3, NULL, false, 4.00),
    ('Noisettes', 18, 5, 2, NULL, false, 3.00),
    ('Cacahuètes', 30, 7, 4, NULL, false, 2.50),
    ('Graines de tournesol', 25, 6, 3, NULL, false, 1.80),
    ('Graines de lin', 28, 7, 4, NULL, false, 2.00),
    ('Graines de chia', 15, 4, 2, NULL, false, 3.00),
    ('Graines de sésame', 20, 5, 3, NULL, false, 1.80),
    ('Vinaigre', 30, 8, 4, NULL, false, 1.20),
    ('Moutarde', 35, 10, 5, NULL, false, 1.50),
    ('Ketchup', 50, 12, 6, NULL, false, 2.00),
    ('Mayonnaise', 40, 10, 5, NULL, false, 1.80),
    ('Soja', 30, 8, 4, NULL, false, 3.00),
    ('Tofu', 25, 6, 3, NULL, false, 3.50),
    ('Yaourt', 45, 10, 5, NULL, false, 1.50),
    ('Fromage', 55, 12, 7, NULL, false, 2.80),
    ('Jambon', 50, 10, 5, NULL, false, 2.50),
    ('Poulet', 65, 15, 8, NULL, false, 4.50),
    ('Bœuf', 70, 20, 10, NULL, false, 5.00);

INSERT INTO public.recipes (name, price, pic, deleted_date, is_deleted, recipe_category_id) 
VALUES 
('Lapin à la moutarde', 18.99, NULL, NULL, FALSE, 2),
('Burger de lièvre', 15.50, NULL, NULL, FALSE, 2),
('Salade de chèvre chaud', 12.00, NULL, NULL, FALSE, 1),
('Frites maison', 5.00, NULL, NULL, FALSE, 1),
('Tarte au sucre', 7.50, NULL, NULL, FALSE, 4),
('Civet de lièvre', 20.99, NULL, NULL, FALSE, 2),
('Fondant au chocolat', 8.00, NULL, NULL, FALSE, 4),
('Jus de pomme maison', 4.00, NULL, NULL, FALSE, 3),
('Café expresso', 3.00, NULL, NULL, FALSE, 3);

INSERT INTO public.recipe_ingredients (recipe_id, ingredient_id, quantity) 
SELECT r.id, i.id, 1.0 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Lapin à la moutarde' AND i.name = 'Bœuf'
UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Lapin à la moutarde' AND i.name = 'Sel'
UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Lapin à la moutarde' AND i.name = 'Poivre'

UNION ALL
SELECT r.id, i.id, 1.0 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Burger de lièvre' AND i.name = 'Bœuf'
UNION ALL
SELECT r.id, i.id, 0.5 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Burger de lièvre' AND i.name = 'Farine'

UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Salade de chèvre chaud' AND i.name = 'Sel'
UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Salade de chèvre chaud' AND i.name = 'Poivre'

UNION ALL
SELECT r.id, i.id, 2.0 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Frites maison' AND i.name = 'Pommes de terre'
UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Frites maison' AND i.name = 'Sel'

UNION ALL
SELECT r.id, i.id, 1.0 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Tarte au sucre' AND i.name = 'Sucre'
UNION ALL
SELECT r.id, i.id, 1.5 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Tarte au sucre' AND i.name = 'Farine'

UNION ALL
SELECT r.id, i.id, 1.0 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Civet de lièvre' AND i.name = 'Bœuf'
UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Civet de lièvre' AND i.name = 'Poivre'
UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Civet de lièvre' AND i.name = 'Sel'

UNION ALL
SELECT r.id, i.id, 1.0 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Fondant au chocolat' AND i.name = 'Chocolat'
UNION ALL
SELECT r.id, i.id, 0.5 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Fondant au chocolat' AND i.name = 'Farine'

UNION ALL
SELECT r.id, i.id, 0.2 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Jus de pomme maison' AND i.name = 'Sucre'

UNION ALL
SELECT r.id, i.id, 0.1 FROM public.recipes r JOIN public.ingredients i ON r.name = 'Café expresso' AND i.name = 'Sucre';

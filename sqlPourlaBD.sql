-- Créer la base de données
CREATE DATABASE bunny_db WITH ENCODING 'UTF8';

-- Se connecter à la base de données bunny_db
\c bunny_db;

-- Créer la table restaurant_profile
CREATE TABLE IF NOT EXISTS public.restaurant_profile (
    id SERIAL PRIMARY KEY,
    restaurant_name VARCHAR(255),
    restaurant_address VARCHAR(255),
    opening_hours VARCHAR(100)
);

-- Créer la table employee_role
CREATE TABLE IF NOT EXISTS public.employee_role (
    id SERIAL PRIMARY KEY,
    role_name VARCHAR(255) NOT NULL
);

-- Créer la table employees
CREATE TABLE IF NOT EXISTS public.employees (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    e_mail VARCHAR(100),
    birth_date DATE,
    social_insurance_number VARCHAR(9),
    employee_role_id INT REFERENCES employee_role(id) ON DELETE SET NULL,
    pic BYTEA,
    password_hash VARCHAR(255),
    password_salt VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    deleted_date TIMESTAMP,
    is_deleted BOOLEAN,
    temp_password BOOLEAN
);

-- Créer la table schedule
CREATE TABLE IF NOT EXISTS public.schedule (
    id SERIAL PRIMARY KEY,
    shift_start TIMESTAMP,
    shift_end TIMESTAMP,
    employee_id INT REFERENCES employees(id)
);

-- Créer la table bill_customer
CREATE TABLE IF NOT EXISTS public.bill_customer (
    id SERIAL PRIMARY KEY,
    order_date DATE,
    bill_file BYTEA,
    employee_id INT REFERENCES employees(id)
);

-- Créer la table bill_provider
CREATE TABLE IF NOT EXISTS public.bill_provider (
    id SERIAL PRIMARY KEY,
    order_date DATE,
    bill_file BYTEA,
    is_delivered BOOLEAN
);

-- Créer la table item_category
CREATE TABLE IF NOT EXISTS public.item_category (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

-- Créer la table items
CREATE TABLE IF NOT EXISTS public.items (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    price NUMERIC,
    pic BYTEA,
    deleted_date TIMESTAMP,
    is_deleted BOOLEAN,
    item_category_id INT REFERENCES item_category(id)
);

-- Créer la table ingredients
CREATE TABLE IF NOT EXISTS public.ingredients (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    quantity_remaining NUMERIC,
    quantity_per_delivery_unit NUMERIC,
    minimum_threshold_notification NUMERIC,
    deleted_date TIMESTAMP,
    is_deleted BOOLEAN
);

-- Créer la table bill_items
CREATE TABLE IF NOT EXISTS public.bill_items (
    bill_customer_id INT,
    item_id INT,
    quantity NUMERIC,
    quantity_deleted INT,
    PRIMARY KEY (bill_customer_id, item_id),
    FOREIGN KEY (bill_customer_id) REFERENCES bill_customer(id),
    FOREIGN KEY (item_id) REFERENCES items(id)
);

-- Créer la table bill_ingredients
CREATE TABLE IF NOT EXISTS public.bill_ingredients (
    bill_customer_id INT,
    ingredient_id INT,
    quantity NUMERIC,
    PRIMARY KEY (bill_customer_id, ingredient_id),
    FOREIGN KEY (bill_customer_id) REFERENCES bill_customer(id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(id)
);

-- Créer la table item_recipe
CREATE TABLE IF NOT EXISTS public.item_recipe (
    item_id INT,
    ingredient_id INT,
    quantity NUMERIC,
    PRIMARY KEY (item_id, ingredient_id),
    FOREIGN KEY (item_id) REFERENCES items(id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(id)
);

-- Insérer des rôles d'employés
INSERT INTO employee_role (role_name) 
VALUES 
    ('Admin'), 
    ('Gérant'), 
    ('Service');

-- Insérer des catégories d'articles
INSERT INTO item_category (name) 
VALUES 
    ('Entrées'), 
    ('Plats principaux'),
    ('Boissons'), 
    ('Desserts');

-- Insérer des employés
INSERT INTO public.employees (
    first_name, last_name, e_mail, birth_date, social_insurance_number,
    employee_role_id, pic, password_hash, password_salt, is_deleted, temp_password
)
VALUES
('Carolande', 'Dupont', 'carolande.dupont@example.com', '1988-06-25', '123456788', 1, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword1', 'salthash1', FALSE, FALSE),
('Cedrick', 'Lemoine', 'cedrick.lemoine@example.com', '1992-11-30', '987654322', 2, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword2', 'salthash2', FALSE, FALSE),
('Jacob', 'Bertier', 'jacob.bertier@example.com', '1985-01-15', '111223343', 3, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword3', 'salthash3', FALSE, TRUE),
('Marie', 'Vidal', 'marie.vidal@example.com', '1990-02-20', '556677889', 1, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword4', 'salthash4', FALSE, FALSE),
('Paul', 'Morin', 'paul.morin@example.com', '1983-12-01', '998877665', 2, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword5', 'salthash5', FALSE, FALSE),
('Sophie', 'Leclerc', 'sophie.leclerc@example.com', '1994-05-14', '223344556', 3, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword6', 'salthash6', FALSE, FALSE),
('David', 'Lemoine', 'david.lemoine@example.com', '1992-08-12', '667788998', 1, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword7', 'salthash7', FALSE, TRUE),
('Lucie', 'Martin', 'lucie.martin@example.com', '1987-09-30', '889977556', 2, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword8', 'salthash8', FALSE, FALSE),
('Antoine', 'Robert', 'antoine.robert@example.com', '1991-11-22', '554433221', 3, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword9', 'salthash9', FALSE, FALSE),
('Elsa', 'Garcia', 'elsa.garcia@example.com', '1986-03-14', '223344667', 1, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword10', 'salthash10', FALSE, FALSE),
('Julien', 'Lefevre', 'julien.lefevre@example.com', '1993-07-09', '776655443', 2, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword11', 'salthash11', FALSE, FALSE),
('Luc', 'Fontaine', 'luc.fontaine@example.com', '1984-05-20', '998877443', 3, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword12', 'salthash12', FALSE, TRUE),
('Chloe', 'Vermette', 'chloe.vermette@example.com', '1995-02-11', '335577889', 1, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword13', 'salthash13', FALSE, FALSE),
('Thierry', 'Renard', 'thierry.renard@example.com', '1990-01-30', '223366778', 2, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword14', 'salthash14', FALSE, FALSE),
('Valerie', 'Chartrand', 'valerie.chartrand@example.com', '1987-12-05', '554466889', 3, 
'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 'hashedpassword15', 'salthash15', FALSE, TRUE);

-- Insérer des ingrédients dans la table ingredients
INSERT INTO public.ingredients (name, quantity_remaining, quantity_per_delivery_unit, minimum_threshold_notification, deleted_date, is_deleted)
VALUES 
('Sucre', 100.0, 10.0, 5.0, NULL, FALSE),
('Sel', 50.0, 5.0, 2.0, NULL, FALSE),
('frite', 30.0, 1.0, 3.0, NULL, FALSE),
('Farine', 200.0, 20.0, 10.0, NULL, FALSE),
('Poivre', 25.0, 2.0, 1.0, NULL, FALSE),
('lièvre', 40, 2.0, 1.0, NULL, FALSE);

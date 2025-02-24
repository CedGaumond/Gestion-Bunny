CREATE TABLE IF NOT EXISTS public.restaurant_profile (
    id SERIAL PRIMARY KEY,
    restaurant_name VARCHAR(255),
    restaurant_address VARCHAR(255),
    opening_hours VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS public.user_role (
    id SERIAL PRIMARY KEY,
    role_name VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.employees (
    id SERIAL PRIMARY KEY,
    birth_date DATE,
    social_insurance_number VARCHAR(9),
    address VARCHAR(255),
    pic BYTEA,
    number_hours_desired NUMERIC,
    hourly_salary NUMERIC
);

CREATE TABLE IF NOT EXISTS public.users (
    id SERIAL PRIMARY KEY,
    e_mail VARCHAR(100),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    user_role_id INT REFERENCES user_role(id),
    employee_id INT REFERENCES employees(id),
    password_hash VARCHAR(255),
    password_salt VARCHAR(255),
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    deleted_date TIMESTAMP DEFAULT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT false,
    temp_password BOOLEAN
);


CREATE TABLE IF NOT EXISTS public.schedule (
    id SERIAL PRIMARY KEY,
    shift_start TIMESTAMP,
    shift_end TIMESTAMP,
    employee_id INT REFERENCES employees(id)
);

CREATE TABLE IF NOT EXISTS public.bills (
    id SERIAL PRIMARY KEY,
    date_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    bill_file BYTEA,
    total_amount NUMERIC(10, 2) NOT NULL DEFAULT 0.00,
    user_id INT REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS public.orders (
    id SERIAL PRIMARY KEY,
    date_created TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    order_file BYTEA,
    total_amount NUMERIC(10, 2) NOT NULL DEFAULT 0.00,
    is_delivered BOOLEAN NOT NULL DEFAULT false,
    user_id INT REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS public.recipe_category (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS public.recipes (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    price NUMERIC,
    pic BYTEA,
    deleted_date TIMESTAMP DEFAULT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT false,
    recipe_category_id INT REFERENCES recipe_category(id)
);

CREATE TABLE IF NOT EXISTS public.ingredients (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    price NUMERIC,
    quantity_remaining NUMERIC,
    quantity_per_delivery_unit NUMERIC,
    minimum_threshold_notification NUMERIC,
    deleted_date TIMESTAMP DEFAULT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT false
);

CREATE TABLE IF NOT EXISTS public.bill_recipes (
    bill_id INT REFERENCES bills(id),
    recipe_id INT REFERENCES recipes(id),
    quantity INT,
    quantity_deleted INT,
    PRIMARY KEY (bill_id, recipe_id)
);

CREATE TABLE IF NOT EXISTS public.order_ingredients (
    order_id INT REFERENCES orders(id),
    ingredient_id INT REFERENCES ingredients(id),
    quantity INT,
    PRIMARY KEY (order_id, ingredient_id)
);

CREATE TABLE IF NOT EXISTS public.recipe_ingredients (
    recipe_id INT REFERENCES recipes(id),
    ingredient_id INT REFERENCES ingredients(id),
    quantity NUMERIC,
    PRIMARY KEY (recipe_id, ingredient_id)
);

INSERT INTO user_role (role_name) 
VALUES 
    ('Admin'), 
    ('Gérant'), 
    ('Service');

INSERT INTO recipe_category (name) 
VALUES 
    ('Entrées'), 
    ('Plats principaux'),
    ('Boissons'), 
    ('Desserts');    

-- Insertion des employés
INSERT INTO public.employees (
    birth_date, social_insurance_number, address, pic, number_hours_desired, hourly_salary
) VALUES
('1988-06-25', '123456788', '123 Rue Exemple, Ville', 'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 40, 25.50),
('1992-11-30', '987654322', '456 Avenue Démo, Ville', 'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 35, 22.00),
('1985-01-15', '111223343', '789 Boulevard Test, Ville', 'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 30, 28.75);

-- Insertion des employés
INSERT INTO public.employees (
    birth_date, social_insurance_number, address, pic, number_hours_desired, hourly_salary
) VALUES
('1988-06-25', '123456788', '123 Rue Exemple, Ville', 'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 40, 25.50),
('1992-11-30', '987654322', '456 Avenue Démo, Ville', 'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 35, 22.00),
('1985-01-15', '111223343', '789 Boulevard Test, Ville', 'base64,alksfjdasfjaslfjdofawefjopqweorihjjass', 30, 28.75);

-- Insertion des utilisateurs en récupérant les 3 derniers IDs des employés
INSERT INTO public.users (
    first_name, last_name, e_mail, user_role_id, employee_id, password_hash, password_salt, is_deleted, temp_password
) 
SELECT 
    'Carolande', 'Dupont', 'carolande.dupont@example.com', 1, id, 'JZGZcNZsijYn39GGU9XPHCrDMowcKoGt+YursgocDbU=', 'sq4oZVMkikKIGxqtxhMQ8w==', FALSE, FALSE
FROM public.employees
ORDER BY id DESC
LIMIT 1;

INSERT INTO public.users (
    first_name, last_name, e_mail, user_role_id, employee_id, password_hash, password_salt, is_deleted, temp_password
) 
SELECT 
    'Cedrick', 'Lemoine', 'cedrick.lemoine@example.com', 2, id, 'Ps7SfOSSgVF35XMBmG2w6PGuNW8qJFTXHFNCEI6U9hM=', '46Pw8uuNdXHT/v4FHApFrg==', FALSE, FALSE
FROM public.employees
ORDER BY id DESC
LIMIT 1 OFFSET 1;

INSERT INTO public.users (
    first_name, last_name, e_mail, user_role_id, employee_id, password_hash, password_salt, is_deleted, temp_password
) 
SELECT 
    'Jacob', 'Bertier', 'jacob.bertier@example.com', 3, id, '11hbkc9OZqX/HHRcfpZtuYpBph3baYiXMtoOjExEraY=', 'nQPM9TXaYigbPQZikybwtQ==', FALSE, TRUE
FROM public.employees
ORDER BY id DESC
LIMIT 1 OFFSET 2;
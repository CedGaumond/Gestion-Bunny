CREATE DATABASE bunny_db WITH ENCODING 'UTF8';

\c bunny_db

CREATE TABLE IF NOT EXISTS public.restaurant_profile (
    id SERIAL PRIMARY KEY,
    restaurant_name VARCHAR(255),
    restaurant_address VARCHAR(255),
    opening_hours VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS public.employee_role (
    id SERIAL PRIMARY KEY,
    role_name VARCHAR(255) NOT NULL
);

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
    temp_password BOOLEAN,
    number_hours_desired NUMERIC
);

CREATE TABLE IF NOT EXISTS public.schedule (
    id SERIAL PRIMARY KEY,
    shift_start TIMESTAMP,
    shift_end TIMESTAMP,
    employee_id INT REFERENCES employees(id)
);

CREATE TABLE IF NOT EXISTS public.bill_customer (
    id SERIAL PRIMARY KEY,
    order_date DATE,
    bill_file BYTEA,
    employee_id INT REFERENCES employees(id)
);

CREATE TABLE IF NOT EXISTS public.bill_provider (
    id SERIAL PRIMARY KEY,
    order_date DATE,
    bill_file BYTEA,
    is_delivered BOOLEAN
);

CREATE TABLE IF NOT EXISTS public.item_category (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS public.items (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    price NUMERIC,
    pic BYTEA,
    deleted_date TIMESTAMP,
    is_deleted BOOLEAN,
    item_category_id INT REFERENCES item_category(id)
);

CREATE TABLE IF NOT EXISTS public.ingredients (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    quantity_remaining NUMERIC,
    quantity_per_delivery_unit NUMERIC,
    minimum_threshold_notification NUMERIC,
    deleted_date TIMESTAMP,
    is_deleted BOOLEAN
);

CREATE TABLE IF NOT EXISTS public.bill_items (
    bill_customer_id INT,
    item_id INT,
    quantity NUMERIC,
    quantity_deleted INT,
    PRIMARY KEY (bill_customer_id, item_id),
    FOREIGN KEY (bill_customer_id) REFERENCES bill_customer(id),
    FOREIGN KEY (item_id) REFERENCES items(id)
);

CREATE TABLE IF NOT EXISTS public.bill_ingredients (
    bill_customer_id INT,
    ingredient_id INT,
    quantity NUMERIC,
    PRIMARY KEY (bill_customer_id, ingredient_id),
    FOREIGN KEY (bill_customer_id) REFERENCES bill_customer(id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(id)
);

CREATE TABLE IF NOT EXISTS public.item_recipe (
    item_id INT,
    ingredient_id INT,
    quantity NUMERIC,
    PRIMARY KEY (item_id, ingredient_id),
    FOREIGN KEY (item_id) REFERENCES items(id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(id)
);

INSERT INTO employee_role (role_name) 
VALUES 
    ('Admin'), 
    ('Gérant'), 
    ('Service');

INSERT INTO item_category (name) 
VALUES 
    ('Entrées'), 
    ('Plats principaux'),
    ('Boissons'), 
    ('Desserts');    

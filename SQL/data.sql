INSERT INTO roles (name) VALUES
('Admin'),
('Stower Owner'),
('Customer');


INSERT INTO users (role_id, email, first_name, last_name, hash_password, is_active)
VALUES
(1, 'admin@example.com', 'Alice', 'Admin', 'hashed_pw_1', TRUE),
(2, 'store_owner@example.com', 'Bob', 'Manager', 'hashed_pw_2', TRUE),
(3, 'customer1@example.com', 'Charlie', 'Customer', 'hashed_pw_3', TRUE);

INSERT INTO gen_lookupType (name)
VALUES
('Store Category');


INSERT INTO gen_lookup (lookup_type_id, name)
VALUES
(1, 'Gluten Free Stores'),
(1, 'Grocery'),
(1, 'Pharmacy');

INSERT INTO stores (
    store_category_id, name, description, address,
    latitude, longitude, phone_number, email,
    website_url, store_image_url, opening_time,
    closing_time, operating_days, is_verified,
    total_reviews, is_active
)
VALUES
(1, 'Tasty Bites', 'A popular local restaurant.', '123 Food Street',
 40.7128, -74.0060, '123-456-7890', 'contact@tastybites.com',
 'http://tastybites.com', 'http://tastybites.com/image.jpg', '09:00',
 '21:00', 'Mon-Sun', TRUE, 120, TRUE),

(1, 'Fresh Mart', 'Neighborhood grocery store.', '456 Market Ave',
 34.0522, -118.2437, '987-654-3210', 'support@freshmart.com',
 'http://freshmart.com', 'http://freshmart.com/store.jpg', '08:00',
 '22:00', 'Mon-Sat', TRUE, 85, TRUE)
 (3, 'HealthFirst Pharmacy', '24/7 pharmacy with delivery service.', '100 Wellness Blvd',
 40.7306, -73.9352, '212-555-0198', 'contact@healthfirst.com',
 'http://healthfirst.com', 'http://healthfirst.com/pharmacy.jpg', '00:00',
 '23:59', 'Mon-Sun', TRUE, 210, TRUE),

(1, 'The Pasta Place', 'Authentic Italian dining experience.', '200 Olive Ave',
 41.9028, 12.4964, '212-555-0123', 'info@pastaplace.com',
 'http://pastaplace.com', 'http://pastaplace.com/store.jpg', '11:00',
 '23:00', 'Mon-Sun', TRUE, 320, TRUE),

(2, 'Green Basket', 'Organic and sustainable groceries.', '321 Eco Market St',
 47.6062, -122.3321, '206-555-0199', 'support@greenbasket.com',
 'http://greenbasket.com', 'http://greenbasket.com/image.png', '08:00',
 '20:00', 'Mon-Sat', TRUE, 95, TRUE),

(1, 'Burger King Street', 'Fast food and flame-grilled burgers.', '123 Main Street',
 40.4406, -79.9959, '412-555-0147', 'bk@example.com',
 'http://burgerking.com', 'http://burgerking.com/store.jpg', '10:00',
 '23:00', 'Mon-Sun', FALSE, 400, TRUE),

(2, 'Daily Mart', 'Everything you need in one place.', '500 Convenience Rd',
 34.0522, -118.2437, '323-555-0188', 'hello@dailymart.com',
 'http://dailymart.com', 'http://dailymart.com/store.jpg', '07:00',
 '23:00', 'Mon-Sun', TRUE, 150, TRUE);


INSERT INTO customers (
    user_id, name, email, phone, address, latitude, longitude
)
VALUES
(3, 'Charlie Customer', 'charlie.customer@example.com', '555-123-4567',
 '789 Customer Lane', 37.7749, -122.4194);






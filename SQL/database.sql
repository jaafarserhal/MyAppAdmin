CREATE TABLE roles (
    role_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE
);


CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    role_id INTEGER NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    hash_password TEXT NOT NULL,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    CONSTRAINT fk_role
        FOREIGN KEY (role_id)
        REFERENCES roles(role_id)
        ON UPDATE CASCADE
        ON DELETE SET NULL
);

CREATE TABLE UsersCode (
    user_code_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    code TEXT NOT NULL,
    status_lookup_id INTEGER,
    Note TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    
    CONSTRAINT fk_users 
	    FOREIGN KEY (user_id)
        REFERENCES  users(user_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_status_lookup 
	    FOREIGN KEY (status_lookup_id)
        REFERENCES  gen_lookup(lookup_id)
        ON DELETE SET NULL
);



CREATE TABLE gen_lookupType (
    lookup_type_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE
);



CREATE TABLE gen_lookup (
    lookup_id SERIAL PRIMARY KEY,
    lookup_type_id INTEGER NOT NULL,
    name VARCHAR(100) NOT NULL,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    CONSTRAINT fk_lookup_type
        FOREIGN KEY (lookup_type_id)
        REFERENCES gen_lookupType(lookup_type_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);


CREATE TABLE stores (
    store_id SERIAL PRIMARY KEY,
    store_category_id INTEGER NOT NULL,
    name VARCHAR(150) NOT NULL,
    description TEXT,
    address TEXT,
    latitude DECIMAL(10, 7),
    longitude DECIMAL(10, 7),
    phone_number VARCHAR(20),
    email VARCHAR(255),
    website_url TEXT,
    store_image_url TEXT,
    opening_time TIME,
    closing_time TIME,
    operating_days VARCHAR(100), -- e.g., "Mon-Fri", "Mon,Wed,Fri"
    is_verified BOOLEAN DEFAULT FALSE,
    total_reviews INTEGER DEFAULT 0,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    CONSTRAINT fk_store_category
        FOREIGN KEY (store_category_id)
        REFERENCES gen_lookup(lookup_id)
        ON UPDATE CASCADE
        ON DELETE SET NULL
);

CREATE TABLE customers (
    customer_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    name VARCHAR(150) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    address TEXT,
    latitude DECIMAL(10, 7),
    longitude DECIMAL(10, 7),
    CONSTRAINT fk_user
        FOREIGN KEY (user_id)
        REFERENCES users(user_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);


CREATE INDEX idx_stores_store_category_id ON stores(store_category_id);
CREATE INDEX idx_stores_is_verified ON stores(is_verified);
CREATE INDEX idx_stores_is_active ON stores(is_active);
CREATE INDEX idx_stores_created_at ON stores(created_at);
CREATE INDEX idx_stores_lat_lon ON stores(latitude, longitude);


CREATE UNIQUE INDEX idx_customers_user_id ON customers(user_id);
CREATE INDEX idx_customers_email ON customers(email);
CREATE INDEX idx_customers_phone ON customers(phone);
CREATE INDEX idx_customers_lat_lon ON customers(latitude, longitude);


CREATE INDEX idx_stores_name_tsv ON stores USING gin(to_tsvector('english', name));
CREATE INDEX idx_stores_description_tsv ON stores USING gin(to_tsvector('english', description));





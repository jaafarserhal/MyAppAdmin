--
-- PostgreSQL database dump
--

-- Dumped from database version 14.18 (Homebrew)
-- Dumped by pg_dump version 14.18 (Homebrew)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: update_order_total(); Type: FUNCTION; Schema: public; Owner: jaafarserhal
--

CREATE FUNCTION public.update_order_total() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE "Order" 
    SET total_amount = (
        SELECT COALESCE(SUM(subtotal), 0)
        FROM OrderItem 
        WHERE order_id = COALESCE(NEW.order_id, OLD.order_id)
    )
    WHERE order_id = COALESCE(NEW.order_id, OLD.order_id);
    
    RETURN COALESCE(NEW, OLD);
END;
$$;


ALTER FUNCTION public.update_order_total() OWNER TO jaafarserhal;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: customers; Type: TABLE; Schema: public; Owner: jaafarserhal
--

CREATE TABLE public.customers (
    customer_id integer NOT NULL,
    user_id integer NOT NULL,
    name character varying(150) NOT NULL,
    email character varying(255) NOT NULL,
    phone character varying(20),
    address text,
    latitude numeric(10,7),
    longitude numeric(10,7)
);


ALTER TABLE public.customers OWNER TO jaafarserhal;

--
-- Name: customers_customer_id_seq; Type: SEQUENCE; Schema: public; Owner: jaafarserhal
--

CREATE SEQUENCE public.customers_customer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.customers_customer_id_seq OWNER TO jaafarserhal;

--
-- Name: customers_customer_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jaafarserhal
--

ALTER SEQUENCE public.customers_customer_id_seq OWNED BY public.customers.customer_id;


--
-- Name: gen_lookup; Type: TABLE; Schema: public; Owner: jaafarserhal
--

CREATE TABLE public.gen_lookup (
    lookup_id integer NOT NULL,
    lookup_type_id integer NOT NULL,
    name character varying(100) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    is_active boolean DEFAULT true
);


ALTER TABLE public.gen_lookup OWNER TO jaafarserhal;

--
-- Name: gen_lookup_lookup_id_seq; Type: SEQUENCE; Schema: public; Owner: jaafarserhal
--

CREATE SEQUENCE public.gen_lookup_lookup_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.gen_lookup_lookup_id_seq OWNER TO jaafarserhal;

--
-- Name: gen_lookup_lookup_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jaafarserhal
--

ALTER SEQUENCE public.gen_lookup_lookup_id_seq OWNED BY public.gen_lookup.lookup_id;


--
-- Name: gen_lookuptype; Type: TABLE; Schema: public; Owner: jaafarserhal
--

CREATE TABLE public.gen_lookuptype (
    lookup_type_id integer NOT NULL,
    name character varying(100) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    is_active boolean DEFAULT true
);


ALTER TABLE public.gen_lookuptype OWNER TO jaafarserhal;

--
-- Name: gen_lookuptype_lookup_type_id_seq; Type: SEQUENCE; Schema: public; Owner: jaafarserhal
--

CREATE SEQUENCE public.gen_lookuptype_lookup_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.gen_lookuptype_lookup_type_id_seq OWNER TO jaafarserhal;

--
-- Name: gen_lookuptype_lookup_type_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jaafarserhal
--

ALTER SEQUENCE public.gen_lookuptype_lookup_type_id_seq OWNED BY public.gen_lookuptype.lookup_type_id;


--
-- Name: roles; Type: TABLE; Schema: public; Owner: jaafarserhal
--

CREATE TABLE public.roles (
    role_id integer NOT NULL,
    name character varying(100) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    is_active boolean DEFAULT true
);


ALTER TABLE public.roles OWNER TO jaafarserhal;

--
-- Name: roles_role_id_seq; Type: SEQUENCE; Schema: public; Owner: jaafarserhal
--

CREATE SEQUENCE public.roles_role_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.roles_role_id_seq OWNER TO jaafarserhal;

--
-- Name: roles_role_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jaafarserhal
--

ALTER SEQUENCE public.roles_role_id_seq OWNED BY public.roles.role_id;


--
-- Name: stores; Type: TABLE; Schema: public; Owner: jaafarserhal
--

CREATE TABLE public.stores (
    store_id integer NOT NULL,
    store_category_id integer NOT NULL,
    name character varying(150) NOT NULL,
    description text,
    address text,
    latitude numeric(10,7),
    longitude numeric(10,7),
    phone_number character varying(20),
    email character varying(255),
    website_url text,
    store_image_url text,
    opening_time time without time zone,
    closing_time time without time zone,
    operating_days character varying(100),
    is_verified boolean DEFAULT false,
    total_reviews integer DEFAULT 0,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    is_active boolean DEFAULT true
);


ALTER TABLE public.stores OWNER TO jaafarserhal;

--
-- Name: stores_store_id_seq; Type: SEQUENCE; Schema: public; Owner: jaafarserhal
--

CREATE SEQUENCE public.stores_store_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.stores_store_id_seq OWNER TO jaafarserhal;

--
-- Name: stores_store_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jaafarserhal
--

ALTER SEQUENCE public.stores_store_id_seq OWNED BY public.stores.store_id;


--
-- Name: users; Type: TABLE; Schema: public; Owner: jaafarserhal
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    role_id integer NOT NULL,
    email character varying(255) NOT NULL,
    first_name character varying(100) NOT NULL,
    last_name character varying(100) NOT NULL,
    hash_password text NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    created_at timestamp with time zone DEFAULT now()
);


ALTER TABLE public.users OWNER TO jaafarserhal;

--
-- Name: users_code; Type: TABLE; Schema: public; Owner: jaafarserhal
--

CREATE TABLE public.users_code (
    user_code_id integer NOT NULL,
    user_id integer NOT NULL,
    code text NOT NULL,
    status_lookup_id integer,
    note text,
    is_active boolean DEFAULT true NOT NULL,
    created_at timestamp with time zone DEFAULT now(),
    expiration_time timestamp with time zone
);


ALTER TABLE public.users_code OWNER TO jaafarserhal;

--
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: jaafarserhal
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_user_id_seq OWNER TO jaafarserhal;

--
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jaafarserhal
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- Name: userscode_user_code_id_seq; Type: SEQUENCE; Schema: public; Owner: jaafarserhal
--

CREATE SEQUENCE public.userscode_user_code_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.userscode_user_code_id_seq OWNER TO jaafarserhal;

--
-- Name: userscode_user_code_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jaafarserhal
--

ALTER SEQUENCE public.userscode_user_code_id_seq OWNED BY public.users_code.user_code_id;


--
-- Name: customers customer_id; Type: DEFAULT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.customers ALTER COLUMN customer_id SET DEFAULT nextval('public.customers_customer_id_seq'::regclass);


--
-- Name: gen_lookup lookup_id; Type: DEFAULT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.gen_lookup ALTER COLUMN lookup_id SET DEFAULT nextval('public.gen_lookup_lookup_id_seq'::regclass);


--
-- Name: gen_lookuptype lookup_type_id; Type: DEFAULT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.gen_lookuptype ALTER COLUMN lookup_type_id SET DEFAULT nextval('public.gen_lookuptype_lookup_type_id_seq'::regclass);


--
-- Name: roles role_id; Type: DEFAULT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.roles ALTER COLUMN role_id SET DEFAULT nextval('public.roles_role_id_seq'::regclass);


--
-- Name: stores store_id; Type: DEFAULT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.stores ALTER COLUMN store_id SET DEFAULT nextval('public.stores_store_id_seq'::regclass);


--
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- Name: users_code user_code_id; Type: DEFAULT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users_code ALTER COLUMN user_code_id SET DEFAULT nextval('public.userscode_user_code_id_seq'::regclass);


--
-- Name: customers customers_pkey; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (customer_id);


--
-- Name: gen_lookup gen_lookup_pkey; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.gen_lookup
    ADD CONSTRAINT gen_lookup_pkey PRIMARY KEY (lookup_id);


--
-- Name: gen_lookuptype gen_lookuptype_name_key; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.gen_lookuptype
    ADD CONSTRAINT gen_lookuptype_name_key UNIQUE (name);


--
-- Name: gen_lookuptype gen_lookuptype_pkey; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.gen_lookuptype
    ADD CONSTRAINT gen_lookuptype_pkey PRIMARY KEY (lookup_type_id);


--
-- Name: roles roles_name_key; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_name_key UNIQUE (name);


--
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (role_id);


--
-- Name: stores stores_pkey; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.stores
    ADD CONSTRAINT stores_pkey PRIMARY KEY (store_id);


--
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- Name: users_code userscode_pkey; Type: CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users_code
    ADD CONSTRAINT userscode_pkey PRIMARY KEY (user_code_id);


--
-- Name: idx_customers_email; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_customers_email ON public.customers USING btree (email);


--
-- Name: idx_customers_lat_lon; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_customers_lat_lon ON public.customers USING btree (latitude, longitude);


--
-- Name: idx_customers_phone; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_customers_phone ON public.customers USING btree (phone);


--
-- Name: idx_customers_user_id; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE UNIQUE INDEX idx_customers_user_id ON public.customers USING btree (user_id);


--
-- Name: idx_stores_created_at; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_stores_created_at ON public.stores USING btree (created_at);


--
-- Name: idx_stores_description_tsv; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_stores_description_tsv ON public.stores USING gin (to_tsvector('english'::regconfig, description));


--
-- Name: idx_stores_is_active; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_stores_is_active ON public.stores USING btree (is_active);


--
-- Name: idx_stores_is_verified; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_stores_is_verified ON public.stores USING btree (is_verified);


--
-- Name: idx_stores_lat_lon; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_stores_lat_lon ON public.stores USING btree (latitude, longitude);


--
-- Name: idx_stores_name_tsv; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_stores_name_tsv ON public.stores USING gin (to_tsvector('english'::regconfig, (name)::text));


--
-- Name: idx_stores_store_category_id; Type: INDEX; Schema: public; Owner: jaafarserhal
--

CREATE INDEX idx_stores_store_category_id ON public.stores USING btree (store_category_id);


--
-- Name: gen_lookup fk_lookup_type; Type: FK CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.gen_lookup
    ADD CONSTRAINT fk_lookup_type FOREIGN KEY (lookup_type_id) REFERENCES public.gen_lookuptype(lookup_type_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: users fk_role; Type: FK CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT fk_role FOREIGN KEY (role_id) REFERENCES public.roles(role_id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: users_code fk_status_lookup; Type: FK CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users_code
    ADD CONSTRAINT fk_status_lookup FOREIGN KEY (status_lookup_id) REFERENCES public.gen_lookup(lookup_id) ON DELETE SET NULL;


--
-- Name: stores fk_store_category; Type: FK CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.stores
    ADD CONSTRAINT fk_store_category FOREIGN KEY (store_category_id) REFERENCES public.gen_lookup(lookup_id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: customers fk_user; Type: FK CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: users_code fk_users; Type: FK CONSTRAINT; Schema: public; Owner: jaafarserhal
--

ALTER TABLE ONLY public.users_code
    ADD CONSTRAINT fk_users FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--


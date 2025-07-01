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
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: jaafarserhal
--

COPY public.roles (role_id, name, created_at, is_active) FROM stdin;
1	Admin	2025-06-08 11:59:15.707664	t
2	Stower Owner	2025-06-08 11:59:15.707664	t
3	Customer	2025-06-08 11:59:15.707664	t
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: jaafarserhal
--

COPY public.users (user_id, role_id, email, first_name, last_name, hash_password, is_active, created_at) FROM stdin;
1	1	admin@example.com	Alice	Admin	hashed_pw_1	t	2025-06-29 20:59:25.701358+03
15	3	serhaljaafar@gmail.com	jaafar	serhal	V+8lhkdt1hJMjCFGquIuGtlAQzrKdN9bpGoxhy5N4w0=	t	2025-07-01 13:57:58.50275+03
\.


--
-- Data for Name: customers; Type: TABLE DATA; Schema: public; Owner: jaafarserhal
--

COPY public.customers (customer_id, user_id, name, email, phone, address, latitude, longitude) FROM stdin;
\.


--
-- Data for Name: gen_lookuptype; Type: TABLE DATA; Schema: public; Owner: jaafarserhal
--

COPY public.gen_lookuptype (lookup_type_id, name, created_at, is_active) FROM stdin;
1	Store Category	2025-06-08 12:00:18.261086	t
100	User Status	2025-06-28 15:24:19.582424	t
200	User Code Status	2025-06-28 21:22:50.844071	t
\.


--
-- Data for Name: gen_lookup; Type: TABLE DATA; Schema: public; Owner: jaafarserhal
--

COPY public.gen_lookup (lookup_id, lookup_type_id, name, created_at, is_active) FROM stdin;
1	1	Gluten Free Stores	2025-06-08 12:00:48.789607	t
2	1	Grocery	2025-06-08 12:00:48.789607	t
3	1	Pharmacy	2025-06-08 12:00:48.789607	t
100	100	Active	2025-06-28 15:29:26.388761	t
101	100	Blocked	2025-06-28 15:29:26.388761	t
102	100	Change Password Required	2025-06-28 15:29:26.388761	t
103	100	Deleted	2025-06-28 15:29:26.388761	t
200	200	Pending	2025-06-28 21:25:50.612442	t
201	200	Processed	2025-06-28 21:25:50.612442	t
202	200	Canceled	2025-06-28 23:04:49.148441	t
\.


--
-- Data for Name: stores; Type: TABLE DATA; Schema: public; Owner: jaafarserhal
--

COPY public.stores (store_id, store_category_id, name, description, address, latitude, longitude, phone_number, email, website_url, store_image_url, opening_time, closing_time, operating_days, is_verified, total_reviews, created_at, is_active) FROM stdin;
1	1	Tasty Bites	A popular local restaurant.	123 Food Street	40.7128000	-74.0060000	123-456-7890	contact@tastybites.com	http://tastybites.com	http://tastybites.com/image.jpg	09:00:00	21:00:00	Mon-Sun	t	120	2025-06-08 12:01:24.780386	t
2	1	Fresh Mart	Neighborhood grocery store.	456 Market Ave	34.0522000	-118.2437000	987-654-3210	support@freshmart.com	http://freshmart.com	http://freshmart.com/store.jpg	08:00:00	22:00:00	Mon-Sat	t	85	2025-06-08 12:01:24.780386	t
3	3	HealthFirst Pharmacy	24/7 pharmacy with delivery service.	100 Wellness Blvd	40.7306000	-73.9352000	212-555-0198	contact@healthfirst.com	http://healthfirst.com	http://healthfirst.com/pharmacy.jpg	00:00:00	23:59:00	Mon-Sun	t	210	2025-06-08 12:02:59.478117	t
4	1	The Pasta Place	Authentic Italian dining experience.	200 Olive Ave	41.9028000	12.4964000	212-555-0123	info@pastaplace.com	http://pastaplace.com	http://pastaplace.com/store.jpg	11:00:00	23:00:00	Mon-Sun	t	320	2025-06-08 12:02:59.478117	t
5	2	Green Basket	Organic and sustainable groceries.	321 Eco Market St	47.6062000	-122.3321000	206-555-0199	support@greenbasket.com	http://greenbasket.com	http://greenbasket.com/image.png	08:00:00	20:00:00	Mon-Sat	t	95	2025-06-08 12:02:59.478117	t
6	1	Burger King Street	Fast food and flame-grilled burgers.	123 Main Street	40.4406000	-79.9959000	412-555-0147	bk@example.com	http://burgerking.com	http://burgerking.com/store.jpg	10:00:00	23:00:00	Mon-Sun	f	400	2025-06-08 12:02:59.478117	t
7	2	Daily Mart	Everything you need in one place.	500 Convenience Rd	34.0522000	-118.2437000	323-555-0188	hello@dailymart.com	http://dailymart.com	http://dailymart.com/store.jpg	07:00:00	23:00:00	Mon-Sun	t	150	2025-06-08 12:02:59.478117	t
\.


--
-- Data for Name: users_code; Type: TABLE DATA; Schema: public; Owner: jaafarserhal
--

COPY public.users_code (user_code_id, user_id, code, status_lookup_id, note, is_active, created_at, expiration_time) FROM stdin;
10	15	266200	201	Password reset completed	t	2025-07-01 13:58:17.882938+03	2025-07-01 13:59:17.882323+03
\.


--
-- Name: customers_customer_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jaafarserhal
--

SELECT pg_catalog.setval('public.customers_customer_id_seq', 1, true);


--
-- Name: gen_lookup_lookup_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jaafarserhal
--

SELECT pg_catalog.setval('public.gen_lookup_lookup_id_seq', 3, true);


--
-- Name: gen_lookuptype_lookup_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jaafarserhal
--

SELECT pg_catalog.setval('public.gen_lookuptype_lookup_type_id_seq', 1, true);


--
-- Name: roles_role_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jaafarserhal
--

SELECT pg_catalog.setval('public.roles_role_id_seq', 3, true);


--
-- Name: stores_store_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jaafarserhal
--

SELECT pg_catalog.setval('public.stores_store_id_seq', 7, true);


--
-- Name: users_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jaafarserhal
--

SELECT pg_catalog.setval('public.users_user_id_seq', 15, true);


--
-- Name: userscode_user_code_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jaafarserhal
--

SELECT pg_catalog.setval('public.userscode_user_code_id_seq', 10, true);


--
-- PostgreSQL database dump complete
--


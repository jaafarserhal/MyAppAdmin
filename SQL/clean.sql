-- Drop all views
DO $$ 
DECLARE
    r RECORD;
BEGIN
    FOR r IN (SELECT table_name FROM information_schema.views WHERE table_schema = 'public') LOOP
        EXECUTE 'DROP VIEW IF EXISTS public.' || r.table_name || ' CASCADE';
    END LOOP;
END $$;

-- Drop all tables
DO $$ 
DECLARE
    r RECORD;
BEGIN
    FOR r IN (SELECT table_name FROM information_schema.tables WHERE table_schema = 'public') LOOP
        EXECUTE 'DROP TABLE IF EXISTS public.' || r.table_name || ' CASCADE';
    END LOOP;
END $$;

-- Drop all sequences
DO $$ 
DECLARE
    r RECORD;
BEGIN
    FOR r IN (SELECT sequence_name FROM information_schema.sequences WHERE sequence_schema = 'public') LOOP
        EXECUTE 'DROP SEQUENCE IF EXISTS public.' || r.sequence_name || ' CASCADE';
    END LOOP;
END $$;

-- Drop all functions
DO $$ 
DECLARE
    r RECORD;
BEGIN
    FOR r IN (SELECT routine_name FROM information_schema.routines WHERE routine_schema = 'public') LOOP
        EXECUTE 'DROP FUNCTION IF EXISTS public.' || r.routine_name || ' CASCADE';
    END LOOP;
END $$;

-- Drop all indexes
DO $$ 
DECLARE
    r RECORD;
BEGIN
    FOR r IN (SELECT indexname FROM pg_indexes WHERE schemaname = 'public') LOOP
        EXECUTE 'DROP INDEX IF EXISTS public.' || r.indexname || ' CASCADE';
    END LOOP;
END $$;

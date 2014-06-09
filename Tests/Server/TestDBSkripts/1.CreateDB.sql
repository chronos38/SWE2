DO $$
BEGIN
	IF EXISTS (SELECT 1 FROM pg_database WHERE datname = 'testdb') THEN
		RAISE EXCEPTION 'Datenbank wurde bereits erstellt';
	END IF;
END$$;

--Rolle mit Login ist equivalent zu User
CREATE ROLE testadmin
	PASSWORD 'test'
	NOSUPERUSER
	NOCREATEDB
	CREATEROLE
	INHERIT
	LOGIN;

CREATE DATABASE testdb
WITH
	OWNER testadmin
	TEMPLATE template1
	ENCODING 'UTF8'
	TABLESPACE pg_default
	CONNECTION LIMIT -1;
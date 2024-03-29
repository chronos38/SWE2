﻿DO $$
BEGIN
	IF EXISTS (SELECT 1 FROM pg_database WHERE datname = 'swedb') THEN
		RAISE EXCEPTION 'Datenbank wurde bereits erstellt';
	END IF;
END$$;

--Rolle mit Login ist equivalent zu User
CREATE ROLE sweadmin
	PASSWORD 'swe'
	NOSUPERUSER
	NOCREATEDB
	CREATEROLE
	INHERIT
	LOGIN;

CREATE DATABASE swedb
WITH
	OWNER sweadmin
	TEMPLATE template1
	ENCODING 'UTF8'
	TABLESPACE pg_default
	CONNECTION LIMIT -1;
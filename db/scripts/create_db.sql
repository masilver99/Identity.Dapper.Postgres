--This is run by the admin in the default database (the identity_db database will not have existed yet)
DROP OWNED BY identity_db_user;
DROP DATABASE IF EXISTS identity_db;

DROP USER IF EXISTS identity_db_user;

CREATE DATABASE identity_db;

CREATE USER identity_db_user PASSWORD 'helloworld';
GRANT ALL PRIVILEGES ON DATABASE identity_db to identity_db_user;






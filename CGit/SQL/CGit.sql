CREATE TABLE CGit.users (
	email VARCHAR (20),
	name VARCHAR (20),
	pwd VARCHAR (20),
	area VARCHAR (20),
	resume VARCHAR (100),
 CONSTRAINT pk_users PRIMARY KEY(email),
)
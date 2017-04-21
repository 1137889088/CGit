CREATE TABLE CGit.users (
	email VARCHAR (20),
	name VARCHAR (20),
	pwd VARCHAR (20),
	area VARCHAR (20),
	resume VARCHAR (100),
 CONSTRAINT pk_users PRIMARY KEY(email),
)
CREATE TABLE CGit.repository (
	repository_id int IDENTITY(1,1) primary key ,
	email varchar(20), 
	repository_name varchar(20),
	repository_describe varchar(50),
	repository_language varchar(20)
)
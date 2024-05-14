CREATE TABLE users (
	id uuid NOT NULL,
	"name" varchar(255) NOT NULL,
	email varchar(255) NOT NULL,
	"password" varchar(255) NOT NULL,
	statusinfo text NULL,
	profileimage varchar(255) NULL,
	lastsignin timestamp NULL,
	"type" int4 NOT NULL,
	status int4 NOT NULL,
	iscompleted bool NOT NULL,
	createdat timestamp NOT NULL,
	updatedat timestamp NULL,
	CONSTRAINT users_pkey PRIMARY KEY (id)
);

CREATE TABLE verifications (
	id uuid NOT NULL,
	value varchar(255) NOT NULL,
	origin varchar(255) NOT NULL,
	expiresat timestamp NOT NULL,
	"type" int4 NOT NULL,
	createdat timestamp NOT NULL,
	updatedat timestamp NULL,
	CONSTRAINT verifications_pkey PRIMARY KEY (id)
);
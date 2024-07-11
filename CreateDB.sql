CREATE TABLE Users (
    Id UUID PRIMARY KEY,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
    ProfileId UUID,
    Name VARCHAR(255) NOT NULL,
    Document VARCHAR(20),
    Email VARCHAR(255) NOT NULL UNIQUE,
    Phone VARCHAR(30),
    Password VARCHAR(255) NOT NULL,
    LastSignIn TIMESTAMP,
    Status INT NOT NULL,
    IsCompleted BOOLEAN NOT NULL,
    IsAdmin BOOLEAN NOT NULL,
    FOREIGN KEY (ProfileId) REFERENCES Profiles(Id)
);

-- Creating Profiles Table
CREATE TABLE Profiles (
    Id UUID PRIMARY KEY,
    CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
    Name VARCHAR(255) NOT NULL
);

-- Creating ProfileAreas Table
CREATE TABLE ProfileAreas (
    ProfileId UUID NOT NULL,
    Area INT NOT NULL,
    CanAdd BOOLEAN NOT NULL,
    CanUpdate BOOLEAN NOT NULL,
    CanDelete BOOLEAN NOT NULL,
    PRIMARY KEY (ProfileId, Area),
    FOREIGN KEY (ProfileId) REFERENCES Profiles(Id) ON DELETE CASCADE
);

CREATE TABLE Verifications (
	id uuid NOT NULL,
	value varchar(255) NOT NULL,
	origin varchar(255) NOT NULL,
	expiresat timestamp NOT NULL,
	"type" int4 NOT NULL,
	createdat timestamp NOT NULL,
	updatedat timestamp NULL,
	CONSTRAINT verifications_pkey PRIMARY KEY (id)
);
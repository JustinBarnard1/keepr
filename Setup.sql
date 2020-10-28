/* DROP TABLE vaultkeeps;
DROP TABLE profiles;
DROP TABLE keeps;
DROP TABLE vaults; */


CREATE TABLE IF NOT EXISTS keeps
(
    id INT AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    img VARCHAR(255),
    views INT DEFAULT 0,
    shares INT DEFAULT 0,
    keeps INT DEFAULT 0,

    PRIMARY KEY (id)
);

/* INSERT INTO keeps (creatorId, name, description, img, views, shares, keeps) VALUES ("ee795a07-9664-46f5-9054-0ed64b3711ce", "test", "this is a test", "none", 1, 3, 1); */



CREATE TABLE IF NOT EXISTS vaults
(
    id INT AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    isPrivate TINYINT DEFAULT 1,

    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS vaultkeeps
(
    id INT AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    vaultId INT,
    keepId INT,

    PRIMARY KEY (id),

    FOREIGN KEY (keepId)
        REFERENCES keeps (id)
        ON DELETE CASCADE,
    
    FOREIGN KEY (vaultId)
        REFERENCES vaults (id)
        ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS profiles
(
  id VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  name VARCHAR(255),
  picture VARCHAR(255),
  PRIMARY KEY (id)
);

/* INSERT INTO profiles (id, email, name, picture) VALUES ("12345", "t@t.com", "t@t.com", "noPic"); */
CREATE TABLE IF NOT EXISTS keeps
(
    id INT AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    img VARCHAR(255),
    views INT DEFAULT 0,
    shares INT DEFAULT 0,

    PRIMARY KEY (id)
);

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
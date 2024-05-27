CREATE TABLE Utilisateur(
    UID INT IDENTITY(1,1),
    login VARCHAR(50) NOT NULL,
    MdpUtilisateur VARCHAR(50) NOT NULL,
    PRIMARY KEY(UID),
    UNIQUE(login)
);
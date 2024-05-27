CREATE TABLE Profil(
    IDProfil INT IDENTITY(1,1),
    NumProfil TINYINT NOT NULL,
    NomProfil VARCHAR(50) NOT NULL,
    Level10Played BIT,
    Dialogue_Garde BIT,
    Dialogue_Prison BIT,
    Dialogue_Debut BIT,
    Dialogue_Inge BIT,
    TotalPieces INT,
    TotalBoulons INT,
    UID INT NOT NULL,
    PRIMARY KEY(IDProfil),
    FOREIGN KEY(UID) REFERENCES Utilisateur(UID)
);
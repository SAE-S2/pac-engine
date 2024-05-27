CREATE TABLE Evasion(
    NumEvasion INT IDENTITY(1,1),
    UtilisationPouvoirs INT,
    HPPerdus INT,
    EnnemisTues INT,
    NbPiece INT,
    NiveauEvasion INT,
    NbBoulon INT,
    Score AS (NiveauEvasion * 1000 + EnnemisTues * 500 + NbBoulon * 100 + NbPiece * 10 - UtilisationPouvoirs * 250 - HPPerdus * 300) PERSISTED,
    NumAmelioration INT,
    IDProfil INT NOT NULL,
    PRIMARY KEY(NumEvasion),
    FOREIGN KEY(NumAmelioration) REFERENCES Amelioration(NumAmelioration),
    FOREIGN KEY(IDProfil) REFERENCES Profil(IDProfil)
);
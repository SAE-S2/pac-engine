CREATE TABLE Equipement_Possede(
    IDProfil INT,
    NumAmelioration INT,
    NiveauAmelioration INT,
    PRIMARY KEY(IDProfil, NumAmelioration),
    FOREIGN KEY(IDProfil) REFERENCES Profil(IDProfil),
    FOREIGN KEY(NumAmelioration) REFERENCES Amelioration(NumAmelioration)
);
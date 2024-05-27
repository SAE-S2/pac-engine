CREATE TABLE Amelioration(
    NumAmelioration INT IDENTITY(1,1),
    Rarete TINYINT NOT NULL,
    NomAmelioration VARCHAR(50) NOT NULL,
    Description VARCHAR(200),
    EstEquipable BIT,
    PRIMARY KEY(NumAmelioration)
);

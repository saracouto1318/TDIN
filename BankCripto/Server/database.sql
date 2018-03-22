CREATE TABLE User(
    nickname varchar(50) NOT NULL PRIMARY KEY,
    firstName varchar(100) NOT NULL,
    lastName varchar(100) NOT NULL,
    password varchar(100) NOT NULL
);

CREATE TABLE Value(
    power real NOT NULL
);

CREATE TABLE Diginote(
    serialNumber int NOT NULL PRIMARY KEY,
    onwer varchar(50) NOT NULL,
    facialValue real NOT NULL DEFAULT(1),
    FOREIGN KEY (onwer) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

CREATE TABLE TransactionDiginote(
    transactionID int NOT NULL PRIMARY KEY,
    diginoteID int NOT NULL,
    seller varchar(50) NOT NULL,
    buyer varchar(50) NOT NULL,
    price real NOT NULL,
    FOREIGN KEY (diginoteID) REFERENCES Diginote(serialNumber)
        ON DELETE SET NULL
        ON UPDATE CASCADE,
    FOREIGN KEY (seller) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE, 
    FOREIGN KEY (buyer) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

CREATE TABLE Session(
    sessionID int NOT NULL PRIMARY KEY,
    nickname varchar(50) NOT NULL,
    FOREIGN KEY (nickname) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);
CREATE TABLE User(
    nickname varchar(50) NOT NULL PRIMARY KEY,
    name varchar(500) NOT NULL,
    password varchar(100) NOT NULL
);

CREATE TABLE Value(
    power real NOT NULL PRIMARY KEY DEFAULT(1),
	quantity INT NOT NULL DEFAULT(0)
);

CREATE TABLE Diginote(
    serialNumber INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    owner varchar(50) NOT NULL,
    facialValue real NOT NULL DEFAULT(1),
    FOREIGN KEY (owner) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

CREATE TABLE TransactionDiginote(
    transactionID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    diginoteID int NOT NULL,
    seller varchar(50),
    buyer varchar(50),
    price real NOT NULL,
	quantaty int NOT NULL,
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
CREATE TABLE User(
    nickname varchar(50) NOT NULL PRIMARY KEY,
    name varchar(500) NOT NULL,
    password varchar(100) NOT NULL,
	balance real NOT NULL DEFAULT(0)
);

CREATE TABLE Value(
	ID INTEGER PRIMARY KEY AUTOINCREMENT,
    power real NOT NULL DEFAULT(1),
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

CREATE TABLE Transactions(
    transactionID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    seller varchar(50),
    buyer varchar(50),
	dateTime datetime NOT NULL,
	quantity int NOT NULL,
	isTransactable int NOT NULL,
    FOREIGN KEY (seller) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE, 
    FOREIGN KEY (buyer) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

CREATE TABLE TransactionDiginote(
    transactionID INTEGER NOT NULL,
	diginoteID INT NOT NULL,
    FOREIGN KEY (diginoteID) REFERENCES Diginote(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE, 
    FOREIGN KEY (transactionID) REFERENCES Transactions(transactionID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

CREATE TABLE Session(
    sessionID int NOT NULL PRIMARY KEY,
    nickname varchar(50) NOT NULL,
    FOREIGN KEY (nickname) REFERENCES User(nickname)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);
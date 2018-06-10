CREATE TABLE pupils(pupilsSqlId int NOT NULL AUTO_INCREMENT, 
name VARCHAR(20) NOT NULL, PRIMARY KEY(pupilsSqlId));

CREATE TABLE register(
	registerSqlId INT NOT NULL AUTO_INCREMENT, 
	pupilId INT, 
	timeIn DATETIME, 
    timeOut DATETIME, 
    PRIMARY KEY(registerSqlId),
    FOREIGN KEY(pupilId) REFERENCES pupils(pupilsSqlId)
);
CREATE DATABASE Coldrun;
GO;

Use Coldrun;
GO;

CREATE TABLE Truck(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Code nvarchar(50) NOT NULL UNIQUE,
	Name nvarchar(50) NOT NULL,
	Status int NOT NULL,
	Description nvarchar(255)
);
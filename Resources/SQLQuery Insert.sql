Select * from Address

SET IDENTITY_INSERT dbo.Address OFF;  
INSERT INTO Address (Id, AddressCode, Street, AddressNumber, City, Country, Active)
VALUES (1, 'adresa1', 'Vrsacka', 22, 'Vrsac', 'Serbia', 1);

Select * from Users

SET IDENTITY_INSERT dbo.Users ON;
INSERT INTO Users(id, FirstName, LastName, JMBG, Gender, Address_ID, Email, Password, Role, Active)
		VALUES(1, 'Milan', 'Miljus', '1', 'Male', 1, 'milan@gmail.com', '1', 'Administrator', 1);


-- Create table users
IF NOT EXISTS ( 
	SELECT 1 
	FROM sys.tables 
	WHERE name = 'Users' 
	AND schema_id = SCHEMA_ID('dbo') 
) 
BEGIN 
	CREATE TABLE [Users] 
	( 
		UserId INT PRIMARY KEY IDENTITY, 
		FirstName VARCHAR(50) NOT NULL, 
		LastName VARCHAR(50) NOT NULL, 
		UserName VARCHAR(50) NOT NULL, 
		Password VARCHAR(10) NOT NULL 
	) 
END

-- Create table customers
IF NOT EXISTS ( 
	SELECT 1 
	FROM sys.tables 
	WHERE name = 'Customers' 
	AND schema_id = SCHEMA_ID('dbo') 
) 
BEGIN 
	CREATE TABLE [Customers]
	( 
		CustomerId INT PRIMARY KEY IDENTITY, 
		FullName VARCHAR(50) NOT NULL, 
		DocumentNumber VARCHAR(8) NOT NULL
	) 
END

-- Create table bookings
IF NOT EXISTS ( 
	SELECT 1 
	FROM sys.tables 
	WHERE name = 'Bookings' 
	AND schema_id = SCHEMA_ID('dbo') 
) 
BEGIN 
	CREATE TABLE [Bookings] 
	( 
		BookingId INT PRIMARY KEY IDENTITY, 
		RegisterDate DATETIME2 NOT NULL, 
		CODE VARCHAR(8) NOT NULL,
		Type VARCHAR(50) NOT NULL,
		UserId INT NOT NULL,
		CustomerId INT NOT NULL,
		FOREIGN KEY (UserId) REFERENCES [Users](UserId),
		FOREIGN KEY (CustomerId) REFERENCES [Customers](CustomerId),
	) 
END
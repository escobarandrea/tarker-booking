-- Create table user
IF NOT EXISTS ( 
	SELECT 1 
	FROM sys.tables 
	WHERE name = 'User' 
	AND schema_id = SCHEMA_ID('dbo') 
) 
BEGIN 
	CREATE TABLE [User] 
	( 
		UserId INT PRIMARY KEY IDENTITY, 
		FirstName VARCHAR(50) NOT NULL, 
		LastName VARCHAR(50) NOT NULL, 
		UserName VARCHAR(50) NOT NULL, 
		Password VARCHAR(10) NOT NULL 
	) 
END

-- Create table customer
IF NOT EXISTS ( 
	SELECT 1 
	FROM sys.tables 
	WHERE name = 'Customer' 
	AND schema_id = SCHEMA_ID('dbo') 
) 
BEGIN 
	CREATE TABLE [Customer]
	( 
		CustomerId INT PRIMARY KEY IDENTITY, 
		FullName VARCHAR(50) NOT NULL, 
		DocumentNumber VARCHAR(8) NOT NULL
	) 
END

-- Create table booking
IF NOT EXISTS ( 
	SELECT 1 
	FROM sys.tables 
	WHERE name = 'Booking' 
	AND schema_id = SCHEMA_ID('dbo') 
) 
BEGIN 
	CREATE TABLE [Booking] 
	( 
		BookingId INT PRIMARY KEY IDENTITY, 
		RegisterDay DATETIME2 NOT NULL, 
		CODE VARCHAR(8) NOT NULL,
		Type VARCHAR(50) NOT NULL,
		UserId INT NOT NULL,
		CustomerId INT NOT NULL,
		FOREIGN KEY (UserId) REFERENCES [User](UserId),
		FOREIGN KEY (CustomerId) REFERENCES [Customer](CustomerId),
	) 
END
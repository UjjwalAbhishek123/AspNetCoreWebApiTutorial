--JUST added it for reference not used here

--use ProductsDB
--Go

----declaring variables to set values
--declare @i int =1;
--declare @name varchar(50);
--declare @category varchar(50);
--declare @price int;
--declare @quantity int;

USE ProductsDB;
GO

-- Declaring variables to set values
DECLARE @i INT = 1;
DECLARE @name VARCHAR(50);
DECLARE @category VARCHAR(50);
DECLARE @price INT;
DECLARE @quantity INT;

-- Starting loop
WHILE @i <= 1000
BEGIN
    -- Generate random product name
    SET @name = CONCAT('Product ', @i);

    -- Randomly assign a category
    SET @category = CASE 
                        WHEN ABS(CHECKSUM(NEWID())) % 3 = 0 THEN 'Fruits'
                        WHEN ABS(CHECKSUM(NEWID())) % 3 = 1 THEN 'Vegetables'
                        ELSE 'Beverages' 
                    END;

    -- Generate random price between 10 and 1000
    SET @price = ABS(CHECKSUM(NEWID())) % 991 + 10;

    -- Generate random quantity between 10 and 200
    SET @quantity = ABS(CHECKSUM(NEWID())) % 191 + 10;

    -- Insert the generated data into Products table
    INSERT INTO Products (Name, Category, Price, Quantity)
    VALUES (@name, @category, @price, @quantity);

    -- Increasing counter
    SET @i = @i + 1;
END


--starting loop
--while @i<=1000
--BEGIN
--	--generate random product name
--	SET @name = CONCAT('Product ', @i);

--	--randomly assign a category
--	SET @category = CASE when abs(checksum(NEWID())) % 3 = 0 THEN 'Fruits'
--						when abs(checksum(NEWID())) % 3 = 1 THEN 'Vegetables'
--						ELSE 'Beverages' END;
	
--	--generate random price between 10 and 1000
--	SET @price = abs(CHECKSUM(NEWID())) % 991 + 10;

--	--generate random quantity between 10 and 200
--	SET @quantity = abs(CHECKSUM(NewID()))% 191 + 10;

--	--insert the generated data into Products table
--	INSERT into Products (Name, Category, Price, Quantity)
--	VALUES (@name, @category, @price, @quantity);

--	--increasing counter
--	SET @i = @i+1;
--END

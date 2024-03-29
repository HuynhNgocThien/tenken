USE [master]
GO
/****** Object:  Database [tenken]    Script Date: 5/24/2019 7:34:31 PM ******/
CREATE DATABASE [tenken]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tenken', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\tenken.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tenken_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\tenken_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [tenken] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tenken].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tenken] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tenken] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tenken] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tenken] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tenken] SET ARITHABORT OFF 
GO
ALTER DATABASE [tenken] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [tenken] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tenken] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tenken] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tenken] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tenken] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tenken] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tenken] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tenken] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tenken] SET  DISABLE_BROKER 
GO
ALTER DATABASE [tenken] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tenken] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tenken] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tenken] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tenken] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tenken] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tenken] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tenken] SET RECOVERY FULL 
GO
ALTER DATABASE [tenken] SET  MULTI_USER 
GO
ALTER DATABASE [tenken] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tenken] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tenken] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tenken] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tenken] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'tenken', N'ON'
GO
ALTER DATABASE [tenken] SET QUERY_STORE = OFF
GO
USE [tenken]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [tenken]
GO
/****** Object:  User [tenken_user]    Script Date: 5/24/2019 7:34:31 PM ******/
CREATE USER [tenken_user] FOR LOGIN [tenken_user] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Schema [tk]    Script Date: 5/24/2019 7:34:31 PM ******/
CREATE SCHEMA [tk]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_split_string_to_column]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_split_string_to_column] (
    @string NVARCHAR(MAX),
    @delimiter CHAR(1)
    )
RETURNS @out_put TABLE (
    [column_id] INT IDENTITY(1, 1) NOT NULL,
    [value] NVARCHAR(MAX)
    )
AS
BEGIN
    DECLARE @value NVARCHAR(MAX),
        @pos INT = 0,
        @len INT = 0

    SET @string = CASE 
            WHEN RIGHT(@string, 1) != @delimiter
                THEN @string + @delimiter
            ELSE @string
            END

    WHILE CHARINDEX(@delimiter, @string, @pos + 1) > 0
    BEGIN
        SET @len = CHARINDEX(@delimiter, @string, @pos + 1) - @pos
        SET @value = SUBSTRING(@string, @pos, @len)

        INSERT INTO @out_put ([value])
        SELECT LTRIM(RTRIM(@value)) AS [column]

        SET @pos = CHARINDEX(@delimiter, @string, @pos + @len) + 1
    END

    RETURN
END
GO
/****** Object:  Table [dbo].[tk_Address]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_Address](
	[Address] [nvarchar](200) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[AddressID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_Cart]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_Cart](
	[Quantity] [int] NOT NULL,
	[CartID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_CartItem]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_CartItem](
	[ProductInfoID] [int] NOT NULL,
	[CartID] [int] NOT NULL,
	[CartItemID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_Category]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_Comment]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_Comment](
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](200) NOT NULL,
	[Reply] [int] NOT NULL,
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_Order]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_Order](
	[AddressID] [int] NOT NULL,
	[DeliveryStatus] [varchar](10) NOT NULL,
	[PaymentStatus] [varchar](10) NOT NULL,
	[OrderID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_OrderItem]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_OrderItem](
	[ProductInfoID] [int] NOT NULL,
	[OrderID] [int] NULL,
	[OrderItemID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_Product]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_Product](
	[ProductName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [float] NULL,
	[StockQuantity] [int] NULL,
	[CategoryID] [int] NULL,
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] [varchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_ProductQuantityInfo]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_ProductQuantityInfo](
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductInfoID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_Rating]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_Rating](
	[UserID] [int] NOT NULL,
	[CommentID] [int] NOT NULL,
	[Rating] [int] NULL,
	[ProductID] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_User]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[CartID] [int] NOT NULL,
 CONSTRAINT [PK_tk_User] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tk_UserAddress]    Script Date: 5/24/2019 7:34:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tk_UserAddress](
	[UserID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
	[UserAddressID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [tk].[cart_item_delete]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[cart_item_delete]
@productInfoID int,
@cartID int,
@resultOut varchar(200) out,
@cartItemIDOut int out
AS
BEGIN
	DECLARE @cartItemID int = (SELECT CartItemID FROM dbo.tk_CartItem WHERE ProductInfoID = @productInfoID AND CartID = @cartID)
	IF EXISTS (SELECT TOP 1 'x' FROM dbo.tk_CartItem WHERE CartItemID = @cartItemID)
	BEGIN
		DELETE FROM dbo.tk_CartItem WHERE CartItemID = @cartItemID

		UPDATE c
		SET c.Quantity = c.Quantity - 1
		FROM dbo.tk_Cart c
		WHERE c.CartID = @cartID

		SET @resultOut = 'Success delete cart item'
		SET @cartItemIDOut = @cartItemID
	END
	ELSE
	BEGIN
		SET @resultOut = 'Error cannot find cart item'
		SET @cartItemIDOut = -1
	END
END
GO
/****** Object:  StoredProcedure [tk].[cart_item_merge]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[cart_item_merge]
@productID int,
@quantity int,
@cartID int,
@resultOut varchar(200) out,
@productInfoIdOut int out
AS
BEGIN
	DECLARE @productInfoID int = (SELECT p.ProductInfoID FROM dbo.tk_CartItem c 
													JOIN dbo.tk_ProductQuantityInfo p
													ON c.ProductInfoID =p.ProductInfoID WHERE ProductID = @productID)
	SET @productInfoID = ISNULL(@productInfoID, 0)

	IF(@productInfoID = 0)
	BEGIN
		INSERT INTO dbo.tk_ProductQuantityInfo(ProductID,Quantity)
		VALUES (@productID,@quantity)

		SET @productInfoID = SCOPE_IDENTITY();

		INSERT INTO dbo.tk_CartItem(ProductInfoID,CartID)
		VALUES (@productInfoID,@cartID)

		UPDATE dbo.tk_Cart
		SET Quantity = (Quantity + 1)
		WHERE CartID = @cartID

		SET @resultOut =  'Success insert cart item';
		SET @productInfoIdOut = @productInfoID;
		RETURN;
	END

	IF(@productInfoID > 0)
	BEGIN		
		UPDATE dbo.tk_ProductQuantityInfo
		SET Quantity = @quantity
		WHERE ProductInfoID = @productInfoID

		SET @resultOut =  'Success update cart item';
		SET @productInfoIdOut = @productInfoID;
		RETURN;
	END
END
GO
/****** Object:  StoredProcedure [tk].[category_delete]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[category_delete]
@categoryID int,
@resultOut varchar(200) out,
@categoryIdOut int out
AS
BEGIN
	IF EXISTS (SELECT TOP 1 'x' FROM dbo.tk_Category WHERE CategoryID = @categoryID)
	BEGIN
		DELETE FROM dbo.tk_Category WHERE CategoryID = @categoryID
		DELETE FROM dbo.tk_Product WHERE CategoryID = @categoryID
		SET @categoryIdOut = @categoryID
		SET @resultOut = 'Success delete category'
	END
	ELSE
	BEGIN
	SET @categoryIdOut = -1
		SET @resultOut = 'Error cannot find category'
	END
END
GO
/****** Object:  StoredProcedure [tk].[category_merge]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[category_merge]
@categoryID int = 0,
@categoryName nvarchar(100),
@resultOut varchar(200) out,
@categoryIdOut int out
AS
BEGIN
	IF(@categoryID = 0)
	BEGIN
		INSERT INTO tk_Category(CategoryName) VALUES (@categoryName)

		SET @categoryIdOut = SCOPE_IDENTITY();
		SET @resultOut =  'Success insert category';
		RETURN;
	END

	IF(@categoryID > 0)
	BEGIN
		UPDATE tk_Category
		SET CategoryName = @categoryName
		WHERE CategoryID = @categoryID

		SET @categoryIdOut = @categoryID;
		SET @resultOut = 'Success update category';
		RETURN;
	END
END
GO
/****** Object:  StoredProcedure [tk].[comment_merge]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[comment_merge]
@commentID int = 0,
@content nvarchar(200),
@userID int,
@reply int = 0,
@productID int,
@rating int = 0,
@resultOut varchar(200) out,
@commentIdOut int out
AS
BEGIN
	IF(@commentID = 0)
	BEGIN
		INSERT INTO tk_Comment(UserID,ProductID,Content,Reply)
					   VALUES (@userID,@productID,@content,@reply)
		SET @commentIdOut = SCOPE_IDENTITY();

		INSERT INTO tk_Rating (UserID,CommentID,Rating,ProductID)
					   VALUES (@userID,@commentIdOut,@rating,@productID)

		SET @resultOut = 'Success insert comment';
		RETURN;
	END

	IF(@commentID > 0)
	BEGIN
		UPDATE tk_Comment
		SET Content = @content
		WHERE CommentID = @commentID 
		SET @commentIdOut = @commentID;

		UPDATE r
		SET r.Rating = @rating
		FROM tk_Rating r 
			JOIN tk_Comment c
			ON c.CommentID = r.CommentID
		WHERE c.CommentID = @commentID

		SET @resultOut = 'Success update comment';
		RETURN;
	END
END
GO
/****** Object:  StoredProcedure [tk].[edit_user_admin]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[edit_user_admin]
@userID int,
@userName nvarchar(200),
@email varchar(100),
@userIdOut int out,
@resultOut varchar(200) out
AS
BEGIN
	IF NOT EXISTS (SELECT TOP 1 'x' FROM dbo.tk_User WHERE UserID = @userID)
	BEGIN
		SET @userIdOut = -1
		SET @resultOut = 'User not found'
		RETURN;
	END

	IF EXISTS (SELECT TOP 1 'x' FROM dbo.tk_User WHERE UserID = @userID AND UserName = @userName AND Email = @email)
	BEGIN
		SET @userIdOut = -1
		SET @resultOut = 'No info change'
		RETURN;
	END
	
	IF EXISTS (SELECT TOP 1 'x' FROM dbo.tk_User WHERE UserName = @userName)
	BEGIN
		SET @userIdOut = -1
		SET @resultOut = 'User name is used'
		RETURN;
	END

	IF EXISTS (SELECT TOP 1 'x' FROM dbo.tk_User WHERE Email = @email)
	BEGIN
		SET @userIdOut = -1
		SET @resultOut = 'Email is used'
		RETURN;
	END

	UPDATE dbo.tk_User
	SET UserName = @userName,
		Email = @email
	WHERE UserID = @userID

	SET @userIdOut = @userID
	SET @resultOut = 'Success edit user'
END
GO
/****** Object:  StoredProcedure [tk].[get_all_user]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_all_user]
AS
BEGIN
	SELECT * FROM tk_User
END
GO
/****** Object:  StoredProcedure [tk].[get_cart_item]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_cart_item]
@cartID int 
AS
BEGIN
	SELECT pq.ProductInfoID,
		   p.ProductID,
		   p.ProductName,
		   p.Price,
		   pq.Quantity,
		   (pq.Quantity * p.Price) AS PricePerProduct
	FROM dbo.tk_Cart c
		JOIN dbo.tk_CartItem ci
		ON c.CartID = ci.CartID
		JOIN dbo.tk_ProductQuantityInfo pq
		ON ci.ProductInfoID = pq.ProductInfoID
		JOIN dbo.tk_Product p 
		ON pq.ProductID = p.ProductID
	WHERE c.CartID = @cartID
END
GO
/****** Object:  StoredProcedure [tk].[get_cart_value]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [tk].[get_cart_value]
@cartID int 
AS
BEGIN
	SELECT Quantity FROM dbo.tk_Cart WHERE CartID = @cartID
END
GO
/****** Object:  StoredProcedure [tk].[get_category]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_category]
@categoryName nvarchar(100) = '',
@categoryID int = 0
AS
BEGIN
	SET @categoryName = '%' + @categoryName + '%';
	SELECT * 
	FROM tk_Category 
	WHERE (CategoryID = CASE @categoryID WHEN 0 THEN CategoryID ELSE @categoryID END) 
	AND (CategoryName LIKE @categoryName)
END
GO
/****** Object:  StoredProcedure [tk].[get_comment]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_comment]
@productID int
AS
BEGIN
	SELECT c.*,
		   u.UserName,
		   r.Rating 
	FROM tk_Comment c 
		JOIN tk_User u
		ON c.UserID = u.UserID 
		JOIN tk_Rating r
		ON c.CommentID = r.CommentID
	WHERE c.ProductID = @productID
	     AND c.Reply = 0
END
GO
/****** Object:  StoredProcedure [tk].[get_login_user]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_login_user]
@password varchar(200),
@email varchar(100)
AS
BEGIN
	SELECT * FROM tk_User WHERE Email = @email AND Password = @password
END
GO
/****** Object:  StoredProcedure [tk].[get_order]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_order]
@orderID int 
AS
BEGIN
	SELECT o.DeliveryStatus,
		   o.PaymentStatus,
		   o.OrderID,
		   a.*
	FROM dbo.tk_Order o
		JOIN dbo.tk_Address a
		ON o.AddressID = a.AddressID
	WHERE o.OrderID = @orderID
END
GO
/****** Object:  StoredProcedure [tk].[get_order_item]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [tk].[get_order_item]
@orderID int 
AS
BEGIN
	SELECT p.ProductID,
		   p.ProductName,
		   p.Price,
		   pq.Quantity,
		   (pq.Quantity * p.Price) AS PricePerProduct
	FROM dbo.tk_OrderItem o
		JOIN dbo.tk_ProductQuantityInfo pq
		ON o.ProductInfoID = pq.ProductInfoID
		JOIN dbo.tk_Product p 
		ON pq.ProductID = p.ProductID
	WHERE o.OrderID = @orderID
END
GO
/****** Object:  StoredProcedure [tk].[get_product]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_product]
@productName nvarchar(200) = '',
@productID int = 0
AS
BEGIN
	SET @productName = '%' + @productName + '%';

	SELECT p.*, c.CategoryName
	FROM tk_Product p
		JOIN tk_Category c
		ON c.CategoryID = p.CategoryID
	WHERE (ProductID = CASE @productID WHEN 0 THEN ProductID ELSE @productID END)
	AND (ProductName LIKE @productName)
END
GO
/****** Object:  StoredProcedure [tk].[get_product_by_category]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_product_by_category]
@categoryID int = 0
AS
BEGIN
	SELECT p.*, c.CategoryName
	FROM tk_Product p
		JOIN tk_Category c
		ON c.CategoryID = p.CategoryID
	WHERE p.CategoryID = CASE @categoryID WHEN 0 THEN p.CategoryID ELSE @categoryID END
END
GO
/****** Object:  StoredProcedure [tk].[get_reply]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [tk].[get_reply]
@reply int
AS
BEGIN
	SELECT c.*,
		   u.UserName,
		   r.Rating 
	FROM tk_Comment c 
		JOIN tk_User u
		ON c.UserID = u.UserID 
		JOIN tk_Rating r
		ON c.CommentID = r.CommentID
	WHERE c.Reply = @reply
END
GO
/****** Object:  StoredProcedure [tk].[get_top_product]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_top_product]
@typeOfTop int
AS
BEGIN
	IF(@typeOfTop=0)
	BEGIN
		SELECT TOP 8 p.*, c.CategoryName
		FROM tk_Product p
			JOIN tk_Category c
			ON c.CategoryID = p.CategoryID
		ORDER BY ProductID DESC
	END
END
GO
/****** Object:  StoredProcedure [tk].[get_user_info]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[get_user_info]
@userID int 
AS
BEGIN
	SELECT u.UserID, 
		   u.UserName,
		   u.Email,
		   a.* 
	FROM tk_User u
		JOIN tk_UserAddress ua
		ON u.UserID = ua.UserID
		JOIN tk_Address a
		ON ua.AddressID = a.AddressID
END
GO
/****** Object:  StoredProcedure [tk].[order_merge]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[order_merge]
@cartID int,
@orderID int = 0,
@addressID int = 0,
@address nvarchar(200) = NULL,
@phoneNumber varchar(10) = NULL,
@deliveryStatus varchar(10),
@paymentStatus varchar(10),
@resultOut varchar(200) out,
@orderIdOut int out
AS
BEGIN
	IF(@addressID = 0 AND 
	   @address IS NOT NULL AND 
	   @phoneNumber IS NOT NULL)
	BEGIN
		INSERT INTO dbo.tk_Address(Address,PhoneNumber)
		VALUES (@address,@phoneNumber)

		SET @addressID = SCOPE_IDENTITY();

		IF(@orderID = 0)
		BEGIN
			INSERT INTO dbo.tk_Order(AddressID,DeliveryStatus,PaymentStatus)
			VALUES (@addressID,@deliveryStatus,@paymentStatus)

			SET @orderID = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			UPDATE dbo.tk_Order
			SET AddressID = @addressID,
				DeliveryStatus = @deliveryStatus,
				PaymentStatus = @paymentStatus
			WHERE OrderID = @orderID
		END


		INSERT INTO dbo.tk_OrderItem(OrderID,ProductInfoID)
		SELECT @orderID,ProductInfoID
		FROM dbo.tk_CartItem
		WHERE CartID = @cartID
		
		DELETE
		FROM dbo.tk_CartItem
		WHERE CartID = @cartID

		UPDATE dbo.tk_Cart
		SET Quantity = 0
		WHERE CartID = @cartID

		SET @resultOut =  'Success insert order';
		set @orderIdOut = @orderID;
		RETURN;
	END

	IF(@addressID = 0 AND 
	   @address IS NULL AND 
	   @phoneNumber IS NULL AND
	   @orderID > 0)
	BEGIN
		UPDATE dbo.tk_Order
		SET AddressID = @addressID,
				DeliveryStatus = @deliveryStatus,
				PaymentStatus = @paymentStatus
		WHERE OrderID = @orderID

		set @orderIdOut = @orderID;
		SET @resultOut =  'Success update order';
		RETURN;
	END

	IF(@addressID > 0)
	BEGIN
		UPDATE dbo.tk_Address
		SET Address = @address,
			PhoneNumber = @phoneNumber
		WHERE AddressID = @addressID

		set @orderIdOut = @orderID;
		SET @resultOut =  'Success update order address';
		RETURN;
	END
END
GO
/****** Object:  StoredProcedure [tk].[product_delete]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[product_delete]
@productID int,
@resultOut varchar(200) out,
@productIDOut int out
AS
BEGIN
	IF EXISTS (SELECT TOP 1 'x' FROM dbo.tk_Product WHERE ProductID = @productID)
	BEGIN
		DELETE FROM dbo.tk_Product WHERE ProductID = @productID
		SET @productIDOut = @productID
		SET @resultOut = 'Success delete product'
	END
	ELSE
	BEGIN
		SET @productIDOut = -1
		SET @resultOut = 'Error cannot find product'
	END
END
GO
/****** Object:  StoredProcedure [tk].[product_merge]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[product_merge]
@productID int = 0,
@productName nvarchar(200),
@description nvarchar(500),
@price float,
@stockQuantity int,
@categoryID int,
@imageName varchar(200),
@resultOut varchar(200) out,
@productIdOut int out
AS
BEGIN
	IF(@productID = 0)
	BEGIN
		INSERT INTO tk_Product(ProductName,Description,Price,StockQuantity,CategoryID,ImageName)
					   VALUES (@productName,@description,@price,@stockQuantity,@categoryID,@imageName)
		SET @productIdOut = SCOPE_IDENTITY();
		SET @resultOut = 'Success insert product';
		RETURN;
	END

	IF(@productID > 0)
	BEGIN
		UPDATE tk_Product
		SET ProductName = @productName,
			Description = @description,
			Price = @price,
			StockQuantity = @stockQuantity,
			CategoryID = @categoryID,
			ImageName = @imageName
		WHERE ProductID = @productID 
		SET @productIdOut = @productID;
		SET @resultOut = 'Success update product';
		RETURN;
	END
END
GO
/****** Object:  StoredProcedure [tk].[register_user]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[register_user]
@userName nvarchar(200),
@password varchar(200),
@email varchar(100),
@address nvarchar(200) = NULL,
@phoneNumber varchar(10) = NULL,
@resultOut varchar(200) out,
@userIdOut int out
AS
BEGIN
	IF EXISTS (SELECT TOP 1 'x' from dbo.tk_user where UserName= @userName)
	BEGIN
		SET @userIdOut = -1;
		SET @resultOut = 'User Name is used';
		RETURN;
	END

	IF EXISTS (SELECT TOP 1 'x' from dbo.tk_user where Email= @email)
	BEGIN
		SET @userIdOut = -1;
		SET @resultOut = 'Email already register';
		RETURN;
	END

	INSERT INTO tk_Cart(Quantity)
	VALUES (0)
	DECLARE @cartID int = SCOPE_IDENTITY();

	INSERT INTO tk_Address(Address,PhoneNumber)
	VALUES (@address,@phoneNumber)
	DECLARE @addressID int = SCOPE_IDENTITY();

	INSERT INTO tk_user(UserName,Password, Email,CartID) 
	VALUES (@userName,@password,@email,@cartID)
	SET @userIdOut = SCOPE_IDENTITY();
	SET @resultOut = 'Success Create User';

	INSERT INTO tk_UserAddress(UserID,AddressID)
	VALUES (@userIdOut,@addressID)

END
GO
/****** Object:  StoredProcedure [tk].[reset_password_admin]    Script Date: 5/24/2019 7:34:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [tk].[reset_password_admin]
@userID int,
@userIdOut int out,
@resultOut varchar(200) out
AS
BEGIN
	IF NOT EXISTS (SELECT TOP 1 'x' FROM dbo.tk_User WHERE UserID = @userID)
	BEGIN
		SET @userIdOut = -1
		SET @resultOut = 'User not found'
		RETURN;
	END

	UPDATE dbo.tk_User
	SET Password='40bd0156385fc35165329ea1ff5c5ecbdbbeef'
	WHERE UserID = @userID

	SET @userIdOut = @userID
	SET @resultOut = 'Success reset password'
END
GO
USE [master]
GO
ALTER DATABASE [tenken] SET  READ_WRITE 
GO

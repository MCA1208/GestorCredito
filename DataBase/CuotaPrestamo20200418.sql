USE [master]
GO
/****** Object:  Database [cuotaPrestamo]    Script Date: 18/4/2020 17:39:09 ******/
CREATE DATABASE [cuotaPrestamo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cuotaPrestamo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\cuotaPrestamo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'cuotaPrestamo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\cuotaPrestamo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [cuotaPrestamo] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cuotaPrestamo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cuotaPrestamo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET ARITHABORT OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [cuotaPrestamo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [cuotaPrestamo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [cuotaPrestamo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [cuotaPrestamo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [cuotaPrestamo] SET  MULTI_USER 
GO
ALTER DATABASE [cuotaPrestamo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [cuotaPrestamo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [cuotaPrestamo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [cuotaPrestamo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [cuotaPrestamo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [cuotaPrestamo] SET QUERY_STORE = OFF
GO
USE [cuotaPrestamo]
GO
/****** Object:  User [admin]    Script Date: 18/4/2020 17:39:09 ******/
CREATE USER [admin] FOR LOGIN [admin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [admin]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [admin]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[dni] [nvarchar](50) NULL,
	[address] [nvarchar](100) NULL,
	[phone] [nvarchar](100) NULL,
	[creationDate] [datetime] NULL,
	[idZone] [int] NULL,
	[birthdate] [datetime] NULL,
	[married] [bit] NULL,
	[conyuge] [nvarchar](50) NULL,
	[dniMarried] [nvarchar](20) NULL,
	[situationCred] [int] NULL,
	[modifyDate] [datetime2](7) NULL,
	[modifyUser] [nvarchar](20) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuota]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuota](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[number] [int] NULL,
	[amount] [nvarchar](20) NULL,
	[paymentDate] [datetime] NULL,
	[idPrestamo] [int] NULL,
	[dayEnd] [int] NULL,
	[observation] [nvarchar](max) NULL,
	[modifyUser] [nvarchar](20) NULL,
	[modifyDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Cuota] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prestamo]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prestamo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](200) NULL,
	[date] [datetime] NULL,
	[amount] [nvarchar](20) NULL,
	[idClient] [int] NULL,
	[amountInterest] [nvarchar](20) NULL,
	[quantity] [int] NULL,
	[dateEnd] [datetime] NULL,
	[dateStart] [datetime] NULL,
	[modifyDate] [datetime2](7) NULL,
	[modifyUser] [nvarchar](20) NULL,
 CONSTRAINT [PK_Prestamo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](100) NULL,
 CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[active] [bit] NULL,
	[creationDate] [datetime] NULL,
	[idProfile] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zone]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](max) NULL,
	[modifyUser] [nvarchar](20) NULL,
	[modifyDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Zone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client] ADD  DEFAULT (NULL) FOR [modifyDate]
GO
ALTER TABLE [dbo].[Client] ADD  DEFAULT (NULL) FOR [modifyUser]
GO
ALTER TABLE [dbo].[Cuota] ADD  DEFAULT (NULL) FOR [modifyUser]
GO
ALTER TABLE [dbo].[Cuota] ADD  DEFAULT (NULL) FOR [modifyDate]
GO
ALTER TABLE [dbo].[Prestamo] ADD  DEFAULT (NULL) FOR [modifyDate]
GO
ALTER TABLE [dbo].[Prestamo] ADD  DEFAULT (NULL) FOR [modifyUser]
GO
ALTER TABLE [dbo].[Zone] ADD  DEFAULT (NULL) FOR [modifyUser]
GO
ALTER TABLE [dbo].[Zone] ADD  DEFAULT (NULL) FOR [modifyDate]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Zone] FOREIGN KEY([idZone])
REFERENCES [dbo].[Zone] ([id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Zone]
GO
ALTER TABLE [dbo].[Cuota]  WITH CHECK ADD  CONSTRAINT [FK_Cuota_Prestamo] FOREIGN KEY([idPrestamo])
REFERENCES [dbo].[Prestamo] ([id])
GO
ALTER TABLE [dbo].[Cuota] CHECK CONSTRAINT [FK_Cuota_Prestamo]
GO
ALTER TABLE [dbo].[Prestamo]  WITH CHECK ADD  CONSTRAINT [FK_Prestamo_Client] FOREIGN KEY([idClient])
REFERENCES [dbo].[Client] ([id])
GO
ALTER TABLE [dbo].[Prestamo] CHECK CONSTRAINT [FK_Prestamo_Client]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Profiles] FOREIGN KEY([idProfile])
REFERENCES [dbo].[Profiles] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Profiles]
GO
/****** Object:  StoredProcedure [dbo].[spAddPrestamo]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddPrestamo]
@cboCliente integer,
@concepto nvarchar(max),
@amount nvarchar(50),
@amountInterest nvarchar(50),
@quantity integer,
@dateStart datetime,
@dateEnd datetime,
@userLogin as nvarchar(20)

AS
BEGIN
		DECLARE @result as integer = null;
		BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO	Prestamo (idClient, [description], amount, amountInterest, [date], dateStart, dateEnd, modifyDate, modifyUser ) VALUES 
			(@cboCliente, @concepto , @amount , @amountInterest, getdate(), @dateStart, @dateEnd, getdate(), @userLogin )
			
			declare @idPrestamo as integer;
			SELECT @idPrestamo = scope_identity(); 

			DECLARE @promedio as money = cast(@amountInterest as money) / @quantity ;
			--SELECT @promedio;

			DECLARE @intFlag INT
			SET @intFlag = 1
			WHILE (@intFlag <= @quantity)
			BEGIN
				INSERT INTO cuota (number, amount, idPrestamo, modifyDate, modifyUser) VALUES (@intFlag, @promedio, @idPrestamo,getdate(), @userLogin)
				SET @intFlag = @intFlag + 1
			END
		COMMIT
		
	END TRY
	BEGIN CATCH

		ROLLBACK
		SET @result = 0
		SELECT @result as result;

	END CATCH

	SET @result = 1
	SELECT @result  as result;

END
GO
/****** Object:  StoredProcedure [dbo].[spAddZone]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddZone]
@description as varchar(50),
@userLogin as nvarchar(20)
AS
BEGIN

BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO	[Zone]([description], modifyDate, modifyUser) VALUES 
			(@description, getdate(), @userLogin )

			SELECT 1 as result
		COMMIT
	END TRY
	BEGIN CATCH

		ROLLBACK

		SELECT 0 as result

	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[spCreateclient]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---use cuotacliente

CREATE PROCEDURE [dbo].[spCreateclient]
@name as nvarchar(50),
@dni as nvarchar(50),
@address as nvarchar(200),
@phone as nvarchar(50),
@zone as integer,
@birthDate as datetime = null,
@married as bit = null,
@conyuge as nvarchar(50) = null,
@dniConyuge as nvarchar(20),
@cboSitCred as integer,
@userLogin as nvarchar(20)


AS
BEGIN

	BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO	client ([name],dni, [address], phone, creationDate, idZone,birthDate,married,conyuge, dniMarried, situationCred, modifyDate, modifyUser) 
			VALUES 
			(@name, @dni , @address , @phone, getdate(), @zone, @birthDate, @married, @conyuge, @dniConyuge, @cboSitCred, getdate(), @userLogin)
			
			SELECT 1 as result
		COMMIT
	END TRY
	BEGIN CATCH

		ROLLBACK

		SELECT 0 as result

	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteClient]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spDeleteClient]

@IdClient as integer
AS
BEGIN
	
	BEGIN TRY
		BEGIN TRANSACTION
			
			DELETE Client	WHERE id = @IdClient

			SELECT 1 as result
		COMMIT
	END TRY
	BEGIN CATCH

		ROLLBACK

		SELECT 0 as result

	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllClient]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetAllClient]
	
AS
BEGIN
	select id, [name], dni, [address], phone, (select description  from [Zone] where id = idZone) as [zone],
	case when birthdate is null then '' else  CONVERT(VARCHAR(10), birthdate, 103) end as birthdate, 
	case when married = 0 then'Soltero' else 'Casado' end as married,
	(select count(*) from Prestamo where idClient = cli.id) as cantidadPrestamo ,*  
	from client cli
	order by cli.[name]
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllPrestamo]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetAllPrestamo]

AS
BEGIN
	
	SELECT (select [name]  from client where id = Prestamo.idClient) as cliente,
	(select CONVERT(VARCHAR(50), count(number)) from cuota where idprestamo = prestamo.id) +'/'+ 
	(select CONVERT(VARCHAR(50), count(number)) from cuota where idprestamo = prestamo.id and not paymentDate is null) as cuotaPayment,
	CONVERT(varchar(10), dateEnd, 103) as dateEnd,CONVERT(varchar(10), dateStart, 103) as dateStart,*
	FROM Prestamo
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllZone]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllZone]

AS
BEGIN

SELECT *  FROM [Zone]

END
GO
/****** Object:  StoredProcedure [dbo].[spGetClient]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE [dbo].[spGetClient]

AS
BEGIN
	
	SELECT *  FROM Client
END
GO
/****** Object:  StoredProcedure [dbo].[spGetClientByDNI]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetClientByDNI]
@DNI as integer 
AS
BEGIN

	SELECT * FROM Client WHERE dni = @DNI
END
GO
/****** Object:  StoredProcedure [dbo].[spGetClientById]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetClientById]
@IdClient as integer	
AS
BEGIN
	SELECT CONVERT(char(10), birthdate,126) AS birthdate,*  FROM	Client where id = @IdClient
END
GO
/****** Object:  StoredProcedure [dbo].[spGetCuotaDetail]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetCuotaDetail]
	@IdCuota as integer 
AS
BEGIN
	SELECT CONVERT(char(10), paymentdate,126) AS paymentDate,* FROM Cuota WHERE id = @IdCuota
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPrestamoDetailById]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetPrestamoDetailById]
@IdPrestamo as integer
AS
BEGIN
	SELECT CASE WHEN paymentdate IS NULL THEN 'Pendiente' ELSE 'Pago' END as [status], 
	CONVERT(VARCHAR(10), paymentdate, 103) AS paymentDate,
	*  FROM cuota WHERE idprestamo = @IdPrestamo order by number asc
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPrestamoForId]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetPrestamoForId]
	@IdPrestamo as integer
	
AS
BEGIN
	SELECT 
	CONVERT(char(10), dateStart,126) AS dateStart,
	CONVERT(char(10), dateEnd,126) AS dateEnd,
	
	* FROM Prestamo WHERE id = @IdPrestamo 

END
GO
/****** Object:  StoredProcedure [dbo].[spGetReportCuponByClient]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetReportCuponByClient]
@idClient as integer
AS
BEGIN

SELECT p.id, z.[description],cl.[name], dni, [address],CONVERT(VARCHAR(10), dateStart, 103)as dateStart, 
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd  , round(sum(cast(cu.amount as float)),0) totalPrestamo, 
(select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) as totalPagado,
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd  , sum(cast(cu.amount as float))  - 
case when (select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) is null then
0 else (select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) end as Saldo,
Z.[description] AS [zone] ,
(select CONVERT(VARCHAR(50), count(number)) from cuota where idprestamo = p.id) +'/'+ 
(select CONVERT(VARCHAR(50), count(number)) from cuota where idprestamo = p.id and not paymentDate is null) as cuotaPayment



FROM 
Prestamo p
	inner join client cl on cl.id = p.idClient
	inner join Cuota cu on cu.idPrestamo = p.id
	inner join [Zone] z on z.id = cl.idZone
WHERE  cl.id = @idClient 
AND P.id = (SELECT MAX(id) FROM Prestamo WHERE idClient = @idClient)

GROUP BY p.id, z.[description],cl.[name], dni, [address],dateStart, dateEnd 


END
GO
/****** Object:  StoredProcedure [dbo].[spGetReportPrincipal]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetReportPrincipal]
@idClient as integer = null,
@idZone as integer = null ,
@dateFrom as datetime = null,
@dateUp as datetime = null

AS
BEGIN

SELECT cl.[name], z.[description],p.id, p.amount as amountPrestamo,p.amountInterest,c.number, c.amount as amountCuota,
case when paymentDate  is null then 'Pendiente' else 'Pagado' end as Estado ,CONVERT(VARCHAR(10), paymentDate, 103)as paymentDate, c.observation  FROM Prestamo p
	INNER JOIN cuota c on c.idPrestamo = p.id
	INNER JOIN Client cl on cl.id = p.idClient	
	INNER JOIN [Zone] z on z.id = cl.idZone
WHERE( cl.id = @idClient or @idClient is null)
	AND ( z.id = @idZone or @idZone is null)
	AND ( p.[dateStart] >= @dateFrom or @dateFrom is null)
	AND ( p.[dateEnd] <= @dateUp or @dateUp is null)
END


GO
/****** Object:  StoredProcedure [dbo].[spGetZoneById]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--use cuotacliente

CREATE PROCEDURE [dbo].[spGetZoneById]

@IdZone as integer

AS
BEGIN


select *  from [Zone] where id = @IdZone

END



GO
/****** Object:  StoredProcedure [dbo].[spModifyClient]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spModifyClient]
@IdClient as integer,	
@name as nvarchar(50), 
@dni as nvarchar(50), 
@address as nvarchar(50), 
@phone as nvarchar(50),  
@zone integer,
@birthDate as datetime = null,
@married as bit = null,
@conyuge as nvarchar(50) = null,
@dniConyuge as nvarchar(20),
@cboSitCred as integer,
@userLogin as nvarchar(20)

AS
BEGIN
	BEGIN TRY
	BEGIN TRANSACTION
			
		UPDATE 	client SET [name] = @name,dni = @dni, [address] = @address, phone = @phone, idZone = @zone,
		 birthDate= @birthDate, married = @married, conyuge = @conyuge, dniMarried = @dniConyuge, situationCred = @cboSitCred,
		 modifyDate = getdate(), modifyUser = @userLogin
		WHERE id = @IdClient 
			
		SELECT 1 as result
		COMMIT
	END TRY
	BEGIN CATCH

		ROLLBACK

		SELECT 0 as result

	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spModifyZone]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spModifyZone]
@IdZone as integer,
@Description as nvarchar(100),
@userLogin as nvarchar(20)



AS
BEGIN

	BEGIN TRY
		BEGIN TRANSACTION
			
			UPDATE 	[ZONE] set[description] = @Description, modifyDate = getdate(), modifyUser = @userLogin where id = @idZone

			SELECT 1 as result
		COMMIT
	END TRY
	BEGIN CATCH

		ROLLBACK

		SELECT 0 as result

	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[spReportCobranza]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spReportCobranza]
@IdZone as int,
@DateStart as datetime ,
@DateEnd  as datetime 
AS
BEGIN

SELECT DISTINCT (CAST(cl.id AS VARCHAR) +'-'+ CAST(p.id AS VARCHAR)) AS IdPrestamo, [name], cast( c.amount as float) as amount
FROM Prestamo p
INNER JOIN Cuota c ON c.idPrestamo = p.id 
INNER JOIN Client cl ON cl.id	= p.idClient
where @DateStart >= p.dateStart
AND @DateEnd <= P.dateEnd
AND cl.idZone = @IdZone
AND paymentDate IS NULL

END
GO
/****** Object:  StoredProcedure [dbo].[spReportCuponStatus]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spReportCuponStatus]
@idClient as integer = null,
@idZone as integer = null ,
@dateStart as datetime = null,
@dateEnd as datetime = null	
AS
BEGIN

select p.id, z.[description],cl.[name], dni, [address],CONVERT(VARCHAR(10), dateStart, 103)as dateStart, 
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd  , sum(cast(cu.amount as float)) totalPrestamo, 
(select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) as totalPagado,
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd  , sum(cast(cu.amount as float))  - 
case when (select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) is null then
0 else (select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) end as Saldo,
'Credi-To' as Copia

from 
Prestamo p
	inner join client cl on cl.id = p.idClient
	inner join Cuota cu on cu.idPrestamo = p.id
	inner join [Zone] z on z.id = cl.idZone
where ( cl.id = @idClient or @idClient is null)
	AND ( z.id = @idZone or @idZone is null)
	AND ( p.[dateStart] >= @dateStart or @dateStart is null)
	AND ( p.[dateEnd] <= @dateEnd or @dateEnd is null)
group by p.id, z.[description],cl.[name], dni, [address],dateStart, dateEnd

 UNION ALL

 select p.id, z.[description],cl.[name], dni, [address], CONVERT(VARCHAR(10), dateStart, 103)as dateStart, 
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd  ,sum(cast(cu.amount as float)) totalPrestamo, 
(select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) as totalPagado ,
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd  , sum(cast(cu.amount as float))  - 
case when (select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) is null then
0 else (select sum(cast(amount as float)) from Cuota where  paymentDate is not null and idPrestamo = p.id) end as Saldo,
'Cliente'
from 
Prestamo p
	inner join client cl on cl.id = p.idClient
	inner join Cuota cu on cu.idPrestamo = p.id
	inner join [Zone] z on z.id = cl.idZone
where ( cl.id = @idClient or @idClient is null)
	AND ( z.id = @idZone or @idZone is null)
	AND ( p.[dateStart] >= @dateStart or @dateStart is null)
	AND ( p.[dateEnd] <= @dateEnd or @dateEnd is null)
group by p.id, z.[description],cl.[name], dni, [address],dateStart, dateEnd
ORDER BY P.id

END
GO
/****** Object:  StoredProcedure [dbo].[spReportInvestmentAndProfit]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spReportInvestmentAndProfit]
@dateStart as datetime = null,
@dateEnd as datetime = null

AS
BEGIN
declare @sumaInversion as float = null ;
declare @sumaInteres as float = null;
declare @sumaCuotaPagas as float = null;
declare @gananciaEstimada as float = null;

SELECT @sumaInversion = (SELECT ROUND(sum (cast( p.amount AS float)),0)  
FROM Prestamo p 
	WHERE (dateStart >= @dateStart or @dateStart is null) 
	and (dateEnd <= @dateEnd or @dateEnd is null))

SELECT @sumaInteres	=   (SELECT ROUND(sum (cast( p.amountInterest AS float)),0)  
FROM Prestamo p 
	WHERE (dateStart >= @dateStart or @dateStart is null) 
	and (dateEnd <= @dateEnd or @dateEnd is null)) 
	      
--SELECT @sumaInteres	=   (select ROUND(sum (cast(c.amount as float)),0)  FROM cuota c 
--	   inner join Prestamo p2 on p2.id = c.idPrestamo
--	   WHERE (p2.dateStart >= @dateStart or @dateStart is null)
--	   and (p2.dateEnd <= @dateEnd or @dateEnd is null)) 

SELECT @sumaCuotaPagas	=   (select ROUND(sum (cast(c.amount as float)),0)  FROM cuota c 
	   inner join Prestamo p2 on p2.id = c.idPrestamo
	   WHERE (p2.dateStart >= @dateStart or @dateStart is null) 
	   and (p2.dateEnd <= @dateEnd or @dateEnd is null)
	   and paymentDate is not null) 

SELECT @gananciaEstimada = @sumaInteres - @sumaInversion;

SELECT CONVERT(VARCHAR(10),@dateStart,103) as dateFrom, CONVERT(VARCHAR(10),@dateEnd,103) as dateUp, 
@sumaInversion as Inversion, @sumaInteres as 'SumaIntereses', (@gananciaEstimada) as 'GananciaReal'


END
GO
/****** Object:  StoredProcedure [dbo].[spReportSummaryClient]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spReportSummaryClient]
@idClient as integer
AS
BEGIN
SELECT p.id, z.[description],cl.[name], dni, [address],CONVERT(VARCHAR(10), dateStart, 103)as dateStart,
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd , round(sum(cast(cu.amount as float)),0) totalPrestamo,
(select sum(cast(amount as float)) from Cuota where paymentDate is not null and idPrestamo = p.id) as totalPagado,
CONVERT(VARCHAR(10), dateEnd, 103)as dateEnd , sum(cast(cu.amount as float)) -
case when (select sum(cast(amount as float)) from Cuota where paymentDate is not null and idPrestamo = p.id) is null then
0 else (select sum(cast(amount as float)) from Cuota where paymentDate is not null and idPrestamo = p.id) end as Saldo,
Z.[description] AS [zone] ,
(select CONVERT(VARCHAR(50), count(number)) from cuota where idprestamo = p.id) +'/'+
(select CONVERT(VARCHAR(50), count(number)) from cuota where idprestamo = p.id and not paymentDate is null) as cuotaPayment
FROM
Prestamo p
inner join client cl on cl.id = p.idClient
inner join Cuota cu on cu.idPrestamo = p.id
inner join [Zone] z on z.id = cl.idZone
WHERE cl.id = @idClient
GROUP BY p.id, z.[description],cl.[name], dni, [address],dateStart, dateEnd
END
GO
/****** Object:  StoredProcedure [dbo].[spReportSummaryDetail]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spReportSummaryDetail]
@IdPrestamo as integer
AS
BEGIN
SELECT CASE WHEN paymentDate is null THEN 'Impago' ELSE 'Pago' END AS statusPago,
CASE WHEN paymentDate is null THEN '' ELSE CONVERT(varchar(10), paymentDate, 103) END as paymentDate,* FROM Cuota WHERE idPrestamo = @IdPrestamo
ORDER BY number
END
GO
/****** Object:  StoredProcedure [dbo].[spSaveCuotaForId]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSaveCuotaForId] 
@IdCuota as integer ,
@fecha as datetime = null,
@observation as varchar(max),
@userLogin as nvarchar(20)

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
		UPDATE 	cuota SET paymentDate = @fecha, observation = @observation, modifyDate = getdate(), modifyUser = @userLogin
		WHERE id = @IdCuota 
			
		SELECT 1 as result
		COMMIT
	END TRY
	BEGIN CATCH

		ROLLBACK

		SELECT 0 as result

	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spSavePrestamoForId]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSavePrestamoForId]
@IdPrestamo as integer,
@dateStart as datetime = null,
@dateEnd as datetime = null,
@userLogin as nvarchar(20)
AS
BEGIN
	BEGIN TRY
	BEGIN TRANSACTION
			
		UPDATE 	Prestamo set dateStart = @dateStart, dateEnd = @dateEnd, modifyDate = getdate(), modifyUser = @userLogin
		where id = @IdPrestamo

		SELECT 1 as result
	COMMIT
	END TRY
	BEGIN CATCH

		ROLLBACK

		SELECT 0 as result

	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[spUserLogin]    Script Date: 18/4/2020 17:39:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spUserLogin]
@users as varchar(50),
@password as varchar(50) 
AS
BEGIN
	
	SELECT * FROM Users WHERE [name] = @users AND [password] = @password
END
GO
USE [master]
GO
ALTER DATABASE [cuotaPrestamo] SET  READ_WRITE 
GO

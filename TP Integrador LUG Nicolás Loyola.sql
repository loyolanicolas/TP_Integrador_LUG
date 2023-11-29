USE [master]
GO
/****** Object:  Database [TP_LUG]    Script Date: 20/11/2023 14:50:36 ******/
CREATE DATABASE [TP_LUG]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TP_LUG', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TP_LUG.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TP_LUG_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TP_LUG_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TP_LUG] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TP_LUG].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TP_LUG] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TP_LUG] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TP_LUG] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TP_LUG] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TP_LUG] SET ARITHABORT OFF 
GO
ALTER DATABASE [TP_LUG] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TP_LUG] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TP_LUG] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TP_LUG] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TP_LUG] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TP_LUG] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TP_LUG] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TP_LUG] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TP_LUG] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TP_LUG] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TP_LUG] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TP_LUG] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TP_LUG] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TP_LUG] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TP_LUG] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TP_LUG] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TP_LUG] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TP_LUG] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TP_LUG] SET  MULTI_USER 
GO
ALTER DATABASE [TP_LUG] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TP_LUG] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TP_LUG] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TP_LUG] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TP_LUG] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TP_LUG] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TP_LUG] SET QUERY_STORE = OFF
GO
USE [TP_LUG]
GO
/****** Object:  User [nicolasloyola]    Script Date: 20/11/2023 14:50:36 ******/
CREATE USER [nicolasloyola] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [nicolas]    Script Date: 20/11/2023 14:50:36 ******/
CREATE USER [nicolas] FOR LOGIN [nicolas] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [nico]    Script Date: 20/11/2023 14:50:36 ******/
CREATE USER [nico] FOR LOGIN [nico] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [nicolasloyola]
GO
ALTER ROLE [db_owner] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_datareader] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [nicolas]
GO
ALTER ROLE [db_owner] ADD MEMBER [nico]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [nico]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [nico]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [nico]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [nico]
GO
ALTER ROLE [db_datareader] ADD MEMBER [nico]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [nico]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [nico]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [nico]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id_rol] [int] NOT NULL,
	[detalle_rol] [varchar](25) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Turnos]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turnos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[fecha_u_mod] [date] NOT NULL,
	[cliente_id] [int] NOT NULL,
	[fecha_turno] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[fecha_u_mod] [date] NOT NULL,
	[dni] [int] NULL,
	[nombres] [varchar](25) NOT NULL,
	[apellidos] [varchar](25) NOT NULL,
	[telefono] [varchar](25) NULL,
	[fecha_nacimiento] [date] NULL,
	[usuario] [varchar](25) NOT NULL,
	[password] [varchar](25) NOT NULL,
	[estado] [int] NOT NULL,
	[rol] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Roles] ([id_rol], [detalle_rol]) VALUES (0, N'Admin')
INSERT [dbo].[Roles] ([id_rol], [detalle_rol]) VALUES (1, N'Cliente')
GO
SET IDENTITY_INSERT [dbo].[Turnos] ON 

INSERT [dbo].[Turnos] ([id], [fecha_creacion], [fecha_u_mod], [cliente_id], [fecha_turno]) VALUES (1, CAST(N'2023-11-18' AS Date), CAST(N'2023-11-18' AS Date), 3, CAST(N'2023-12-13' AS Date))
INSERT [dbo].[Turnos] ([id], [fecha_creacion], [fecha_u_mod], [cliente_id], [fecha_turno]) VALUES (2, CAST(N'2023-11-18' AS Date), CAST(N'2023-11-18' AS Date), 3, CAST(N'2023-12-14' AS Date))
INSERT [dbo].[Turnos] ([id], [fecha_creacion], [fecha_u_mod], [cliente_id], [fecha_turno]) VALUES (3, CAST(N'2023-10-18' AS Date), CAST(N'2023-10-18' AS Date), 3, CAST(N'2023-10-19' AS Date))
INSERT [dbo].[Turnos] ([id], [fecha_creacion], [fecha_u_mod], [cliente_id], [fecha_turno]) VALUES (4, CAST(N'2023-11-19' AS Date), CAST(N'2023-11-20' AS Date), 5, CAST(N'2023-12-21' AS Date))
INSERT [dbo].[Turnos] ([id], [fecha_creacion], [fecha_u_mod], [cliente_id], [fecha_turno]) VALUES (5, CAST(N'2023-11-19' AS Date), CAST(N'2023-11-20' AS Date), 5, CAST(N'2023-12-22' AS Date))
INSERT [dbo].[Turnos] ([id], [fecha_creacion], [fecha_u_mod], [cliente_id], [fecha_turno]) VALUES (10, CAST(N'2023-11-20' AS Date), CAST(N'2023-11-20' AS Date), 8, CAST(N'2023-12-24' AS Date))
SET IDENTITY_INSERT [dbo].[Turnos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (1, CAST(N'2023-11-14' AS Date), CAST(N'2023-11-14' AS Date), 37784561, N'Nicolas', N'Loyola', N'1166490140', CAST(N'1993-10-01' AS Date), N'nico', N'nico', 0, 0)
INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (2, CAST(N'2023-11-14' AS Date), CAST(N'2023-11-14' AS Date), 37784562, N'Baja', N'Baja', N'1166490141', CAST(N'1993-10-01' AS Date), N'baja', N'baja', 1, 1)
INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (3, CAST(N'2023-11-14' AS Date), CAST(N'2023-11-14' AS Date), 11222333, N'Pablo', N'Vilaboa', N'1122224444', CAST(N'1990-01-01' AS Date), N'pablo', N'pablo', 0, 1)
INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (4, CAST(N'2023-11-19' AS Date), CAST(N'2023-11-19' AS Date), 90456789, N'Alta', N'Alta', N'1155554444', CAST(N'1990-01-10' AS Date), N'altanueva', N'altanueva', 1, 0)
INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (5, CAST(N'2023-11-19' AS Date), CAST(N'2023-11-19' AS Date), 37789456, N'Jimena', N'Nadia', N'1512345678', CAST(N'2020-02-05' AS Date), N'jnadia', N'jnadia', 0, 1)
INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (6, CAST(N'2023-11-20' AS Date), CAST(N'2023-11-20' AS Date), 37, N'pepe', N'pepe', N'45', CAST(N'2023-11-20' AS Date), N'pepe', N'pepe', 1, 0)
INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (7, CAST(N'2023-11-20' AS Date), CAST(N'2023-11-20' AS Date), 87, N'bitacora', N'auditoria', N'3734', CAST(N'2023-11-20' AS Date), N'bitacora', N'bitacora', 1, 0)
INSERT [dbo].[Usuarios] ([id], [fecha_creacion], [fecha_u_mod], [dni], [nombres], [apellidos], [telefono], [fecha_nacimiento], [usuario], [password], [estado], [rol]) VALUES (8, CAST(N'2023-11-20' AS Date), CAST(N'2023-11-20' AS Date), 34436364, N'Evelyn', N'Rumi', N'1566664444', CAST(N'1990-01-02' AS Date), N'everumi', N'everumi', 0, 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  StoredProcedure [dbo].[Borra_Turno]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Borra_Turno] 
	@id int
AS
BEGIN
	
	SET NOCOUNT ON;
	Delete 
	from Turnos
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Crear_Turno]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Turno] 
	@id int,
	@fecha_creacion date,
	@fecha_u_mod date,
	@cliente_id int,
	@fecha_turno date

AS
BEGIN
	
	SET NOCOUNT ON;

	if (@id=0) begin

    INSERT INTO Turnos([fecha_creacion],[fecha_u_mod],[cliente_id],[fecha_turno])
		VALUES(@fecha_creacion,@fecha_u_mod,@cliente_id,@fecha_turno)
	end 
	else begin

	UPDATE Turnos
		   SET [fecha_u_mod] = FORMAT(GETDATE(), 'yyyy-MM-dd')
			  ,[cliente_id] = @cliente_id
			  ,[fecha_turno] = @fecha_turno
		 WHERE id=@id

	END
END
GO
/****** Object:  StoredProcedure [dbo].[Crear_Usuario]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Crear_Usuario] 
	@id int,
	@fecha_creacion date,
	@fecha_u_mod date,
	@dni int,
	@nombres varchar(25),
	@apellidos varchar(25),
	@telefono varchar(25),
	@fecha_nacimiento date,
	@usuario varchar(25),
	@password varchar(25),
	@estado int,
	@rol int
AS
BEGIN
	SET NOCOUNT ON;

	if (@id=0) begin

	INSERT INTO Usuarios([fecha_creacion],[fecha_u_mod],[dni],[nombres],[apellidos],[telefono],[fecha_nacimiento],[usuario],[password],[estado],[rol])
		VALUES(@fecha_creacion,@fecha_u_mod,@dni,@nombres,@apellidos,@telefono,@fecha_nacimiento,@usuario,@password,@estado,@rol)

	end 
	else begin

	UPDATE Usuarios
		   SET [fecha_u_mod] = FORMAT(GETDATE(), 'yyyy-MM-dd')
			  ,[dni] = @dni
			  ,[nombres] = @nombres
			  ,[apellidos] = @apellidos
			  ,[telefono] = @telefono
			  ,[fecha_nacimiento] = @fecha_nacimiento
			  ,[usuario] = @usuario
			  ,[password] = @password
			  ,[estado] = @estado
			  ,[rol] = @rol
		 WHERE id=@id

	END
END
GO
/****** Object:  StoredProcedure [dbo].[Roles_TraerTodo]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Roles_TraerTodo] 

AS
BEGIN

	SET NOCOUNT ON;
	SELECT [id_rol], [detalle_rol]
    FROM [TP_LUG].[dbo].[Roles]

END
GO
/****** Object:  StoredProcedure [dbo].[Turnos_TraerTodo]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Turnos_TraerTodo] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT t.[id] as id_turno, t.[fecha_creacion], t.[fecha_u_mod], u.[id] as id_cliente, u.nombres, u.apellidos, t.[fecha_turno]
	FROM [TP_LUG].[dbo].[Turnos] t
	inner join Usuarios u on (t.cliente_id = u.id)
	WHERE t.[fecha_turno] > GETDATE();

END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_TraerPorUser]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Usuario_TraerPorUser]
	@usuario varchar(25)
AS
BEGIN

	SET NOCOUNT ON;

	select u.[id], u.[fecha_creacion], u.[fecha_u_mod], u.[dni], u.[nombres], u.[apellidos], u.[telefono], u.[fecha_nacimiento], 
	u.[usuario], u.[password], u.[estado], u.[rol], r.[detalle_rol]
	from Usuarios u
	inner join Roles r on (u.rol = r.id_rol)
	where usuario = @usuario 
	and estado = '0'

END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_TraerTodo]    Script Date: 20/11/2023 14:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Usuario_TraerTodo] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

    select u.[id], u.[fecha_creacion], u.[fecha_u_mod], u.[dni], u.[nombres], u.[apellidos], u.[telefono], u.[fecha_nacimiento], 
	u.[usuario], u.[password], u.[estado], u.[rol], r.[detalle_rol]
	from Usuarios u
	inner join Roles r on (u.rol = r.id_rol)
	where estado = '0'
END
GO
USE [master]
GO
ALTER DATABASE [TP_LUG] SET  READ_WRITE 
GO

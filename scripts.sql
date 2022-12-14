USE [master]
GO
/****** Object:  Database [ObligatorioP3_01]    Script Date: 26/4/2022 19:22:46 ******/
CREATE DATABASE [ObligatorioP3_01]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'database_Data', FILENAME = N'C:\Users\Andres\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\database.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'database_Log', FILENAME = N'C:\Users\Andres\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\database.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ObligatorioP3_01] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ObligatorioP3_01].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ObligatorioP3_01] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET ARITHABORT OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ObligatorioP3_01] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ObligatorioP3_01] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ObligatorioP3_01] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ObligatorioP3_01] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ObligatorioP3_01] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ObligatorioP3_01] SET  MULTI_USER 
GO
ALTER DATABASE [ObligatorioP3_01] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ObligatorioP3_01] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ObligatorioP3_01] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ObligatorioP3_01] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ObligatorioP3_01] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ObligatorioP3_01] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ObligatorioP3_01] SET QUERY_STORE = OFF
GO
USE [ObligatorioP3_01]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[IdCompra] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComprasImportadas]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComprasImportadas](
	[IdComImp] [int] NOT NULL,
	[AmericaDelSur] [bit] NOT NULL,
	[MedidasSanitarias] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdComImp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComprasPlazas]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComprasPlazas](
	[IdComPlaza] [int] NOT NULL,
	[CostoFlete] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdComPlaza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FichaCuidados]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FichaCuidados](
	[IdPlanta] [int] NOT NULL,
	[FrecuenciaRiegoUnidadTiempo] [nvarchar](20) NULL,
	[FrecuenciaRiegoCantidad] [int] NULL,
	[Temperatura] [decimal](10, 2) NULL,
	[IdIluminacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPlanta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[IdItem] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](18, 0) NOT NULL,
	[IdPlanta] [int] NOT NULL,
	[IdCompra] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListaNombresVulgares]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListaNombresVulgares](
	[IdNomVulg] [int] IDENTITY(1,1) NOT NULL,
	[NombreVulg] [nvarchar](50) NOT NULL,
	[IdPlanta] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNomVulg] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parametros]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametros](
	[IdParametros] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Valor] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Parametros] PRIMARY KEY CLUSTERED 
(
	[IdParametros] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plantas]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plantas](
	[IdPlanta] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoPlanta] [int] NOT NULL,
	[NombreCientifico] [nvarchar](50) NOT NULL,
	[AlturaMax] [int] NULL,
	[UrlFoto] [nvarchar](50) NULL,
	[Ambiente] [nvarchar](50) NULL,
	[DescripcionPlanta] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPlanta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoIluminacion]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoIluminacion](
	[IdTipoIluminacion] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoIluminacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoPlantas]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoPlantas](
	[IdTipoPlanta] [int] IDENTITY(1,1) NOT NULL,
	[NombreTipoPlanta] [nvarchar](50) NOT NULL,
	[DescripcionTipoPlanta] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoPlanta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 26/4/2022 19:22:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Contrasenia] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2041, N'Dia', 1, CAST(10.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2042, N'Dia', 3, CAST(5.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2043, N'Semana', 5, CAST(23.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2044, N'Semana', 10, CAST(20.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2045, N'Dia', 2, CAST(15.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2046, N'Dia', 2, CAST(3.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2047, N'Dia', 3, CAST(10.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2048, N'Semana', 12, CAST(23.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2049, N'Dia', 4, CAST(15.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[FichaCuidados] ([IdPlanta], [FrecuenciaRiegoUnidadTiempo], [FrecuenciaRiegoCantidad], [Temperatura], [IdIluminacion]) VALUES (2050, N'Dia', 5, CAST(16.00 AS Decimal(10, 2)), 3)
GO
SET IDENTITY_INSERT [dbo].[ListaNombresVulgares] ON 

INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1035, N'Palmera', 2041)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1036, N' cocotero', 2041)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1037, N'Jazmin', 2042)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1038, N'Boniato', 2043)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1039, N' Papa dulce', 2043)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1040, N' Batata', 2043)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1041, N' Camote', 2043)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1042, N'Girasol', 2044)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1043, N' Mirasol', 2044)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1044, N'Limonero', 2045)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1045, N' Lima', 2045)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1046, N'Papa', 2046)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1047, N' Patata', 2046)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1048, N'Menta', 2047)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1049, N' Hierba buena', 2047)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1050, N'Maiz', 2048)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1051, N' Choclo', 2048)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1052, N'Lechuga', 2049)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1053, N'Tomate', 2050)
INSERT [dbo].[ListaNombresVulgares] ([IdNomVulg], [NombreVulg], [IdPlanta]) VALUES (1054, N' Jitomate', 2050)
SET IDENTITY_INSERT [dbo].[ListaNombresVulgares] OFF
GO
SET IDENTITY_INSERT [dbo].[Parametros] ON 

INSERT [dbo].[Parametros] ([IdParametros], [Nombre], [Valor]) VALUES (7, N'Tasa Importación', CAST(21.21 AS Decimal(10, 2)))
INSERT [dbo].[Parametros] ([IdParametros], [Nombre], [Valor]) VALUES (8, N'Tasa Aranceles America del Sur', CAST(12.25 AS Decimal(10, 2)))
INSERT [dbo].[Parametros] ([IdParametros], [Nombre], [Valor]) VALUES (10, N'PlantaTopeDescrMin', CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[Parametros] ([IdParametros], [Nombre], [Valor]) VALUES (11, N'PlantaTopeDescrMax', CAST(500.00 AS Decimal(10, 2)))
INSERT [dbo].[Parametros] ([IdParametros], [Nombre], [Valor]) VALUES (12, N'TipoPlantaTopeDescrMin', CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[Parametros] ([IdParametros], [Nombre], [Valor]) VALUES (13, N'TipoPlantaTopeDescrMax', CAST(200.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Parametros] OFF
GO
SET IDENTITY_INSERT [dbo].[Plantas] ON 

INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2041, 1, N'Cocos nucifera', 5, N'Cocos_nucifera_001.jpg', N'Exterior', N'Las hojas de esta planta son de gran tamaño (de hasta 5 metros de largo) y su fruto, el coco, es el más grande que existe')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2042, 2, N'Jasminum', 1, N'Jasminum_001.jpg', N'Mixta', N'Los tallos son cuadrangulares, de color verde o grisáceo, profusamente ramificados.')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2043, 4, N'Ipomoea batatas', 1, N'Ipomoea_batatas_001.jpg', N'Interior', N'Son plantas trepadoras de hoja perenne; con tallos postrados o volubles, algo suculentos pero también delgados y herbáceos,')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2044, 12, N'Helianthus annuus', 3, N'Helianthus_annuus_001.jpg', N'Interior', N'Los tallos son generalmente erectos e hispidos. La mayoría de las hojas son caulinares, alternas, pecioladas, con base cordiforme y bordes aserrados. ')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2045, 10, N'Citrus aurantifolia', 6, N'Citrus_aurantifolia_001.jpg', N'Exterior', N'Tronco habitualmente torcido, se ramifica densamente desde muy abajo. ')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2046, 4, N'Solanum tuberosum', 1, N'Solanum_tuberosum_001.jpg', N'Exterior', N'S. tuberosum es una planta herbácea, tuberosa, perenne a través de sus tubérculos, caducifolia (ya que pierde sus hojas y tallos aéreos en la estación fría), de tallo erecto o semidecumbente, que puede medir hasta 1 m de altura')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2047, 3, N'Mentha', 0, N'Mentha_001.jpg', N'Interior', N'Poseen estolones subterráneos y superficiales que a menudo las convierten en invasivas.')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2048, 4, N'Zea mays', 5, N'Zea_mays_001.jpg', N'Exterior', N'La planta tiene dos tipos de raíz. Las primarias son fibrosas y presentan además raíces adventicias, que nacen en los primeros nudos por encima de la superficie del suelo.')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2049, 11, N'Lactuca sativa', 1, N'Lactuca_sativa_001.jpg', N'Interior', N' La raíz es pivotante y se ramifica unos 25 cm. Desarrolla una roseta basal de hojas obovadas')
INSERT [dbo].[Plantas] ([IdPlanta], [IdTipoPlanta], [NombreCientifico], [AlturaMax], [UrlFoto], [Ambiente], [DescripcionPlanta]) VALUES (2050, 1, N'Solanum lycopersicum', 5, N'Solanum_lycopersicum_001.jpg', N'Exterior', N'Tiene importancia culinaria y es utilizado como verdura. Siendo el tomate una fruta botánicamente clasificado como una baya, es comúnmente usado en arte culinario como un ingrediente vegetal o guarnición.')
SET IDENTITY_INSERT [dbo].[Plantas] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoIluminacion] ON 

INSERT [dbo].[TipoIluminacion] ([IdTipoIluminacion], [Tipo]) VALUES (1, N'Luz solar directa')
INSERT [dbo].[TipoIluminacion] ([IdTipoIluminacion], [Tipo]) VALUES (2, N'Sombra')
INSERT [dbo].[TipoIluminacion] ([IdTipoIluminacion], [Tipo]) VALUES (3, N'Media sombra')
SET IDENTITY_INSERT [dbo].[TipoIluminacion] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoPlantas] ON 

INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (1, N'Árbol', N'En síntesis, un árbol es una planta cuya información genética le faculta para desarrollarse, producir madera y crecer tanto como el ambiente se lo permita.')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (2, N'Arbusto', N'Planta leñosa , de menos de cinco metros de altura , sin un tronco preponderante , porque se ramifica a partir de la base')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (3, N'Hierba', N'Toda planta pequeña cuyo tallo es tierno y perece después de dar la simiente en el mismo año')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (4, N'Alimenticias', N'Hortalizas o verduras: Suelen ser alimentos cultivados en huertas, y pueden consumirse tanto crudas como cocidas')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (8, N'Con flores', N'Las angiospermas son plantas que sí tienen verdadera raíz, tallo, hojas, flores y fruto. Estas se reproducen por semillas que se encuentran en el interior del fruto.')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (9, N'Sin flores', N'Estas plantas primitivas dependen del agua para su crecimiento y reproducción ya que no cuentan con tallos o raíces verdaderas')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (10, N'Perennes', N'Son aquellas plantas que viven durante varias temporadas ya que cuentan con recursos para vivir con mayor facilidad y durante periodos de tiempo más largos que el resto.')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (11, N'Bianual', N'Estas plantas tienen un periodo de crecimiento que ocupa dos temporadas completas. Durante la primera la planta alcanza su máximo desarrollo ')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (12, N'Anual', N'Estas plantas son de crecimiento muy rápido y suelen germinar y florecer durante la primavera de forma general.')
INSERT [dbo].[TipoPlantas] ([IdTipoPlanta], [NombreTipoPlanta], [DescripcionTipoPlanta]) VALUES (14, N'Mata', N'Plantas de tallo leñoso cuya altura no llega a superar el metro. Suelen ser de tallo bajo y suelen vivir durante varias temporadas.')
SET IDENTITY_INSERT [dbo].[TipoPlantas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (1, N'gonza@gmail.com', N'Gonza123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (2, N'maria@gmail.com', N'Maria123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (3, N'carlos@gmail.com', N'Carlos123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (4, N'seba@gmail.com', N'Seba123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (5, N'esteban@gmail.com', N'Esteban123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (6, N'key@gmail.com', N'Key123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (7, N'fede@gmail.com', N'Fede123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (8, N'bruno@gmail.com', N'Bruno123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (9, N'andres@gmail.com', N'Andres123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (10, N'emma@gmail.com', N'Emma123')
INSERT [dbo].[Usuarios] ([IdUsuario], [Email], [Contrasenia]) VALUES (11, N'rosa@gmail.com', N'Rosa123')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Plantas__7D1E1D2315574FD1]    Script Date: 26/4/2022 19:22:47 ******/
ALTER TABLE [dbo].[Plantas] ADD UNIQUE NONCLUSTERED 
(
	[NombreCientifico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoPlan__68D863EE739F5638]    Script Date: 26/4/2022 19:22:47 ******/
ALTER TABLE [dbo].[TipoPlantas] ADD UNIQUE NONCLUSTERED 
(
	[NombreTipoPlanta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__A9D10534B16153C0]    Script Date: 26/4/2022 19:22:47 ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ComprasImportadas]  WITH CHECK ADD FOREIGN KEY([IdComImp])
REFERENCES [dbo].[Compras] ([IdCompra])
GO
ALTER TABLE [dbo].[ComprasPlazas]  WITH CHECK ADD FOREIGN KEY([IdComPlaza])
REFERENCES [dbo].[Compras] ([IdCompra])
GO
ALTER TABLE [dbo].[FichaCuidados]  WITH CHECK ADD FOREIGN KEY([IdIluminacion])
REFERENCES [dbo].[TipoIluminacion] ([IdTipoIluminacion])
GO
ALTER TABLE [dbo].[FichaCuidados]  WITH CHECK ADD FOREIGN KEY([IdPlanta])
REFERENCES [dbo].[Plantas] ([IdPlanta])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([IdCompra])
REFERENCES [dbo].[Compras] ([IdCompra])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([IdPlanta])
REFERENCES [dbo].[Plantas] ([IdPlanta])
GO
ALTER TABLE [dbo].[ListaNombresVulgares]  WITH CHECK ADD FOREIGN KEY([IdPlanta])
REFERENCES [dbo].[Plantas] ([IdPlanta])
GO
ALTER TABLE [dbo].[Plantas]  WITH CHECK ADD FOREIGN KEY([IdTipoPlanta])
REFERENCES [dbo].[TipoPlantas] ([IdTipoPlanta])
GO
ALTER TABLE [dbo].[TipoIluminacion]  WITH CHECK ADD  CONSTRAINT [CtrlTipoIluminaicion] CHECK  (([Tipo]='Media sombra' OR [Tipo]='Sombra' OR [Tipo]='Luz solar directa'))
GO
ALTER TABLE [dbo].[TipoIluminacion] CHECK CONSTRAINT [CtrlTipoIluminaicion]
GO
USE [master]
GO
ALTER DATABASE [ObligatorioP3_01] SET  READ_WRITE 
GO

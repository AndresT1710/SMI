USE [master]
GO
/****** Object:  Database [SGIS]    Script Date: 08/05/2025 09:32:33 a. m. ******/
CREATE DATABASE [SGIS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SGIS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SGIS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SGIS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SGIS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SGIS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SGIS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SGIS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SGIS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SGIS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SGIS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SGIS] SET ARITHABORT OFF 
GO
ALTER DATABASE [SGIS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SGIS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SGIS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SGIS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SGIS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SGIS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SGIS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SGIS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SGIS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SGIS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SGIS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SGIS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SGIS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SGIS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SGIS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SGIS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SGIS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SGIS] SET RECOVERY FULL 
GO
ALTER DATABASE [SGIS] SET  MULTI_USER 
GO
ALTER DATABASE [SGIS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SGIS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SGIS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SGIS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SGIS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SGIS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SGIS] SET QUERY_STORE = ON
GO
ALTER DATABASE [SGIS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SGIS]
GO
/****** Object:  Table [dbo].[ActividadLaboral]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActividadLaboral](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_ActividadLaboral] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudad]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudad](
	[id] [int] NOT NULL,
	[id_Provincia] [int] NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Ciudad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactoEmergencia]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactoEmergencia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Paciente] [int] NOT NULL,
	[nombre] [nvarchar](100) NULL,
	[telefono] [nvarchar](10) NULL,
	[correo] [nvarchar](100) NULL,
 CONSTRAINT [PK_ContactoEmergencia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoCivil]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoCivil](
	[id] [int] NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_EstadoCivil] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Genero] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrupoSanguineo]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrupoSanguineo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_GrupoSanguineo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lateralidad]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lateralidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Lateralidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NivelInstruccion]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NivelInstruccion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_NivelInstruccion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[id_Persona] [int] NOT NULL,
 CONSTRAINT [PK_Paciente] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PacienteParentesco]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PacienteParentesco](
	[id_Paciente] [int] NOT NULL,
	[id_Parentesco] [int] NULL,
 CONSTRAINT [PK_PacienteParentesco] PRIMARY KEY CLUSTERED 
(
	[id_Paciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parentesco]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parentesco](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Parentesco] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Genero] [int] NULL,
	[nombre] [nvarchar](100) NULL,
	[apellido] [nvarchar](100) NULL,
	[FechaNacimiento] [date] NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaActividadLaboral]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaActividadLaboral](
	[id_Persona] [int] NOT NULL,
	[id_ActividadLaboral] [int] NULL,
 CONSTRAINT [PK_PersonaActividadLaboral] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaDireccion]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaDireccion](
	[id_Persona] [int] NOT NULL,
	[callePrincipal] [nvarchar](100) NULL,
	[calleSecundaria1] [nvarchar](100) NULL,
	[calleSecundaria2] [nvarchar](100) NULL,
	[numeroCasa] [nvarchar](50) NULL,
	[referencia] [nvarchar](100) NULL,
 CONSTRAINT [PK_PersonaDireccion] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaDocumento]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaDocumento](
	[id_Persona] [int] NOT NULL,
	[id_TipoDocumento] [int] NULL,
	[numeroDocumento] [nchar](10) NULL,
 CONSTRAINT [PK_PersonaDocumento] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaEstadoCivil]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaEstadoCivil](
	[id_Persona] [int] NOT NULL,
	[id_EstadoCivil] [int] NULL,
 CONSTRAINT [PK_PersonaEstadoCivil] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaGrupoSanguineo]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaGrupoSanguineo](
	[id_Persona] [int] NOT NULL,
	[id_GrupoSanguineo] [int] NULL,
 CONSTRAINT [PK_PersonaGrupoSanguineo] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaInstruccion]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaInstruccion](
	[id_Persona] [int] NOT NULL,
	[id_NivelInstruccion] [int] NULL,
 CONSTRAINT [PK_PersonaInstruccion] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaLateralidad]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaLateralidad](
	[id_Persona] [int] NOT NULL,
	[id_Lateralidad] [int] NULL,
 CONSTRAINT [PK_PersonaLateralidad] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaLugarResidencia]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaLugarResidencia](
	[id_Persona] [int] NOT NULL,
	[id_Ciudad] [int] NULL,
 CONSTRAINT [PK_PersonaLugarResidencia] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaProfesion]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaProfesion](
	[id_Persona] [int] NOT NULL,
	[id_Profesion] [int] NULL,
 CONSTRAINT [PK_PersonaProfesion] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaReligion]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaReligion](
	[id_Persona] [int] NOT NULL,
	[id_Religion] [int] NULL,
 CONSTRAINT [PK_PersonaReligion] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaSeguroMedico]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaSeguroMedico](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Persona] [int] NULL,
	[id_SeguroMedico] [int] NULL,
 CONSTRAINT [PK_PersonaSeguroMedico] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonaTelefono]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonaTelefono](
	[id_Persona] [int] NOT NULL,
	[celular] [nvarchar](10) NULL,
	[convencional] [nvarchar](10) NULL,
 CONSTRAINT [PK_PersonaTelefono] PRIMARY KEY CLUSTERED 
(
	[id_Persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profesion]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profesion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Profesion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfesionalSalud]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfesionalSalud](
	[id_ProfesionalSalud] [int] NOT NULL,
	[id_TipoProfesional] [int] NULL,
	[numeroRegistro] [nvarchar](50) NULL,
 CONSTRAINT [PK_ProfesionalSalud] PRIMARY KEY CLUSTERED 
(
	[id_ProfesionalSalud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincia]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Provincia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Religion]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Religion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Religion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeguroMedico]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeguroMedico](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_SeguroMedico] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDocumento]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDocumento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_TipoDocumento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoProfesionalSalud]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoProfesionalSalud](
	[id] [int] NOT NULL,
	[nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_TipoProfesionalSalud] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 08/05/2025 09:32:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Persona] [int] NULL,
	[Correo] [nvarchar](255) NULL,
	[Clave] [nvarchar](255) NULL,
	[Activo] [bit] NULL,
	[RefreshToken] [nvarchar](200) NULL,
	[RefreshTokenExpiryTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ciudad]  WITH CHECK ADD  CONSTRAINT [FK_Ciudad_Provincia] FOREIGN KEY([id_Provincia])
REFERENCES [dbo].[Provincia] ([id])
GO
ALTER TABLE [dbo].[Ciudad] CHECK CONSTRAINT [FK_Ciudad_Provincia]
GO
ALTER TABLE [dbo].[ContactoEmergencia]  WITH CHECK ADD  CONSTRAINT [FK_ContactoEmergencia_Paciente] FOREIGN KEY([id_Paciente])
REFERENCES [dbo].[Paciente] ([id_Persona])
GO
ALTER TABLE [dbo].[ContactoEmergencia] CHECK CONSTRAINT [FK_ContactoEmergencia_Paciente]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [FK_Paciente_Persona]
GO
ALTER TABLE [dbo].[PacienteParentesco]  WITH CHECK ADD  CONSTRAINT [FK_PacienteParentesco_Paciente] FOREIGN KEY([id_Paciente])
REFERENCES [dbo].[Paciente] ([id_Persona])
GO
ALTER TABLE [dbo].[PacienteParentesco] CHECK CONSTRAINT [FK_PacienteParentesco_Paciente]
GO
ALTER TABLE [dbo].[PacienteParentesco]  WITH CHECK ADD  CONSTRAINT [FK_PacienteParentesco_Parentesco] FOREIGN KEY([id_Parentesco])
REFERENCES [dbo].[Parentesco] ([id])
GO
ALTER TABLE [dbo].[PacienteParentesco] CHECK CONSTRAINT [FK_PacienteParentesco_Parentesco]
GO
ALTER TABLE [dbo].[Persona]  WITH CHECK ADD  CONSTRAINT [FK_Persona_Genero] FOREIGN KEY([id_Genero])
REFERENCES [dbo].[Genero] ([id])
GO
ALTER TABLE [dbo].[Persona] CHECK CONSTRAINT [FK_Persona_Genero]
GO
ALTER TABLE [dbo].[Persona]  WITH NOCHECK ADD  CONSTRAINT [FK_Persona_PersonaProfesion] FOREIGN KEY([id])
REFERENCES [dbo].[PersonaProfesion] ([id_Persona])
GO
ALTER TABLE [dbo].[Persona] NOCHECK CONSTRAINT [FK_Persona_PersonaProfesion]
GO
ALTER TABLE [dbo].[PersonaActividadLaboral]  WITH CHECK ADD  CONSTRAINT [FK_PersonaActividadLaboral_ActividadLaboral] FOREIGN KEY([id_ActividadLaboral])
REFERENCES [dbo].[ActividadLaboral] ([id])
GO
ALTER TABLE [dbo].[PersonaActividadLaboral] CHECK CONSTRAINT [FK_PersonaActividadLaboral_ActividadLaboral]
GO
ALTER TABLE [dbo].[PersonaActividadLaboral]  WITH CHECK ADD  CONSTRAINT [FK_PersonaActividadLaboral_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaActividadLaboral] CHECK CONSTRAINT [FK_PersonaActividadLaboral_Persona]
GO
ALTER TABLE [dbo].[PersonaDireccion]  WITH CHECK ADD  CONSTRAINT [FK_PersonaDireccion_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaDireccion] CHECK CONSTRAINT [FK_PersonaDireccion_Persona]
GO
ALTER TABLE [dbo].[PersonaDocumento]  WITH CHECK ADD  CONSTRAINT [FK_PersonaDocumento_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaDocumento] CHECK CONSTRAINT [FK_PersonaDocumento_Persona]
GO
ALTER TABLE [dbo].[PersonaDocumento]  WITH CHECK ADD  CONSTRAINT [FK_PersonaDocumento_TipoDocumento] FOREIGN KEY([id_TipoDocumento])
REFERENCES [dbo].[TipoDocumento] ([id])
GO
ALTER TABLE [dbo].[PersonaDocumento] CHECK CONSTRAINT [FK_PersonaDocumento_TipoDocumento]
GO
ALTER TABLE [dbo].[PersonaEstadoCivil]  WITH CHECK ADD  CONSTRAINT [FK_PersonaEstadoCivil_EstadoCivil] FOREIGN KEY([id_EstadoCivil])
REFERENCES [dbo].[EstadoCivil] ([id])
GO
ALTER TABLE [dbo].[PersonaEstadoCivil] CHECK CONSTRAINT [FK_PersonaEstadoCivil_EstadoCivil]
GO
ALTER TABLE [dbo].[PersonaEstadoCivil]  WITH CHECK ADD  CONSTRAINT [FK_PersonaEstadoCivil_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaEstadoCivil] CHECK CONSTRAINT [FK_PersonaEstadoCivil_Persona]
GO
ALTER TABLE [dbo].[PersonaGrupoSanguineo]  WITH CHECK ADD  CONSTRAINT [FK_PersonaGrupoSanguineo_GrupoSanguineo] FOREIGN KEY([id_GrupoSanguineo])
REFERENCES [dbo].[GrupoSanguineo] ([id])
GO
ALTER TABLE [dbo].[PersonaGrupoSanguineo] CHECK CONSTRAINT [FK_PersonaGrupoSanguineo_GrupoSanguineo]
GO
ALTER TABLE [dbo].[PersonaGrupoSanguineo]  WITH CHECK ADD  CONSTRAINT [FK_PersonaGrupoSanguineo_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaGrupoSanguineo] CHECK CONSTRAINT [FK_PersonaGrupoSanguineo_Persona]
GO
ALTER TABLE [dbo].[PersonaInstruccion]  WITH CHECK ADD  CONSTRAINT [FK_PersonaInstruccion_NivelInstruccion] FOREIGN KEY([id_NivelInstruccion])
REFERENCES [dbo].[NivelInstruccion] ([id])
GO
ALTER TABLE [dbo].[PersonaInstruccion] CHECK CONSTRAINT [FK_PersonaInstruccion_NivelInstruccion]
GO
ALTER TABLE [dbo].[PersonaInstruccion]  WITH CHECK ADD  CONSTRAINT [FK_PersonaInstruccion_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaInstruccion] CHECK CONSTRAINT [FK_PersonaInstruccion_Persona]
GO
ALTER TABLE [dbo].[PersonaLateralidad]  WITH CHECK ADD  CONSTRAINT [FK_PersonaLateralidad_Lateralidad] FOREIGN KEY([id_Lateralidad])
REFERENCES [dbo].[Lateralidad] ([id])
GO
ALTER TABLE [dbo].[PersonaLateralidad] CHECK CONSTRAINT [FK_PersonaLateralidad_Lateralidad]
GO
ALTER TABLE [dbo].[PersonaLateralidad]  WITH CHECK ADD  CONSTRAINT [FK_PersonaLateralidad_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaLateralidad] CHECK CONSTRAINT [FK_PersonaLateralidad_Persona]
GO
ALTER TABLE [dbo].[PersonaLugarResidencia]  WITH CHECK ADD  CONSTRAINT [FK_PersonaLugarResidencia_Ciudad] FOREIGN KEY([id_Ciudad])
REFERENCES [dbo].[Ciudad] ([id])
GO
ALTER TABLE [dbo].[PersonaLugarResidencia] CHECK CONSTRAINT [FK_PersonaLugarResidencia_Ciudad]
GO
ALTER TABLE [dbo].[PersonaLugarResidencia]  WITH CHECK ADD  CONSTRAINT [FK_PersonaLugarResidencia_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaLugarResidencia] CHECK CONSTRAINT [FK_PersonaLugarResidencia_Persona]
GO
ALTER TABLE [dbo].[PersonaProfesion]  WITH NOCHECK ADD  CONSTRAINT [FK_PersonaProfesion_Profesion] FOREIGN KEY([id_Profesion])
REFERENCES [dbo].[Profesion] ([id])
GO
ALTER TABLE [dbo].[PersonaProfesion] NOCHECK CONSTRAINT [FK_PersonaProfesion_Profesion]
GO
ALTER TABLE [dbo].[PersonaReligion]  WITH CHECK ADD  CONSTRAINT [FK_PersonaReligion_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaReligion] CHECK CONSTRAINT [FK_PersonaReligion_Persona]
GO
ALTER TABLE [dbo].[PersonaReligion]  WITH CHECK ADD  CONSTRAINT [FK_PersonaReligion_Religion] FOREIGN KEY([id_Religion])
REFERENCES [dbo].[Religion] ([id])
GO
ALTER TABLE [dbo].[PersonaReligion] CHECK CONSTRAINT [FK_PersonaReligion_Religion]
GO
ALTER TABLE [dbo].[PersonaSeguroMedico]  WITH CHECK ADD  CONSTRAINT [FK_PersonaSeguroMedico_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaSeguroMedico] CHECK CONSTRAINT [FK_PersonaSeguroMedico_Persona]
GO
ALTER TABLE [dbo].[PersonaSeguroMedico]  WITH CHECK ADD  CONSTRAINT [FK_PersonaSeguroMedico_SeguroMedico] FOREIGN KEY([id_SeguroMedico])
REFERENCES [dbo].[SeguroMedico] ([id])
GO
ALTER TABLE [dbo].[PersonaSeguroMedico] CHECK CONSTRAINT [FK_PersonaSeguroMedico_SeguroMedico]
GO
ALTER TABLE [dbo].[PersonaTelefono]  WITH CHECK ADD  CONSTRAINT [FK_PersonaTelefono_Persona] FOREIGN KEY([id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[PersonaTelefono] CHECK CONSTRAINT [FK_PersonaTelefono_Persona]
GO
ALTER TABLE [dbo].[ProfesionalSalud]  WITH CHECK ADD  CONSTRAINT [FK_ProfesionalSalud_Persona] FOREIGN KEY([id_ProfesionalSalud])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[ProfesionalSalud] CHECK CONSTRAINT [FK_ProfesionalSalud_Persona]
GO
ALTER TABLE [dbo].[ProfesionalSalud]  WITH CHECK ADD  CONSTRAINT [FK_ProfesionalSalud_TipoProfesionalSalud] FOREIGN KEY([id_TipoProfesional])
REFERENCES [dbo].[TipoProfesionalSalud] ([id])
GO
ALTER TABLE [dbo].[ProfesionalSalud] CHECK CONSTRAINT [FK_ProfesionalSalud_TipoProfesionalSalud]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Persona] FOREIGN KEY([Id_Persona])
REFERENCES [dbo].[Persona] ([id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Persona]
GO
USE [master]
GO
ALTER DATABASE [SGIS] SET  READ_WRITE 
GO

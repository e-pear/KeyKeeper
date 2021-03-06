USE [master]
GO
/****** Object:  Database [KeyKeeperDataBase]    Script Date: 06.01.2021 20:18:50 ******/
CREATE DATABASE [KeyKeeperDataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KeyKeeperDataBase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\KeyKeeperDataBase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KeyKeeperDataBase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\KeyKeeperDataBase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [KeyKeeperDataBase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KeyKeeperDataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KeyKeeperDataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KeyKeeperDataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KeyKeeperDataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [KeyKeeperDataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KeyKeeperDataBase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [KeyKeeperDataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET RECOVERY FULL 
GO
ALTER DATABASE [KeyKeeperDataBase] SET  MULTI_USER 
GO
ALTER DATABASE [KeyKeeperDataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KeyKeeperDataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KeyKeeperDataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KeyKeeperDataBase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KeyKeeperDataBase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [KeyKeeperDataBase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'KeyKeeperDataBase', N'ON'
GO
ALTER DATABASE [KeyKeeperDataBase] SET QUERY_STORE = OFF
GO
USE [KeyKeeperDataBase]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 06.01.2021 20:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 06.01.2021 20:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Employee_Id] [nvarchar](4) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Surname] [nvarchar](max) NOT NULL,
	[Position] [nvarchar](max) NOT NULL,
	[Department] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[Employee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomKeys]    Script Date: 06.01.2021 20:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomKeys](
	[RoomKey_Id] [nvarchar](4) NOT NULL,
	[RoomName] [nvarchar](max) NOT NULL,
	[AssignedEmployee_Id] [nvarchar](4) NULL,
 CONSTRAINT [PK_dbo.RoomKeys] PRIMARY KEY CLUSTERED 
(
	[RoomKey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202012291510317_InitialCreate', N'KeyKeeper.Model.DataAccess.CompanyDbContext', 0x1F8B0800000000000400D559CD6EE33610BE17E83B083AB545D672B27B680D7B175B27298CDDFC204A16BD2D6869EC1095482D4905368A3E590F7DA4BE4287FA3729D9B2936CB608104424E7E3CC7066C86FF2EFDFFF8CDFADE2C8790021296713F77830741D60010F295B4EDC542D5EFDECBE7BFBFD77E3B3305E399FCA75AFF53A946472E2DE2B958C3C4F06F7101339886920B8E40B350878EC91907B27C3E12FDEF1B10708E12296E38C6F52A6680CD9077E4E390B205129892E7808912CC671C6CF509D4B12834C480013F703AC3F00242006D9DAC12951E47D108094AEF33EA20435F2215AB80E618C2BA250DFD19D045F09CE967E820324BA5D2780EB16249250D831AA97F7356978A24DF26AC1122A48A5E2F19E80C7AF0B1F79A6F8419E762B1FA217CFD0DB6AADADCE3C3971CFE224E26B40E3CDCD46D348E885B6A34B9923A79A39AA22020347FF1C39D33452A98009835409121D39D7E93CA2018ADCF23F804D581A454DDD503B9CDB18C0A16BC1115EAD6F606168FC7916BA8EB709E09908957C9B706E1DC60386B8EB5C90D547604B753F71DFB8CE395D41587E17E171C728E6038A2891E2E7251A40E61154F30DDFF98A0BF80D1808A220BC264A81C0D3BFE40C2CA50D15F5EF2DBAE19F8769B77D573F15EC4536BEE692E6B1FDB5773E8584081503535F61EF4BF24097596C185ADC4040E101C21BCE638C5EAC5D3710650BE53D4DF2123628263FBF97922E198475D69E0B1EDF70BD6FD79ACFB7442C41DBC8772CF4792A0243F5B157978CAD85A440DDA78E14222F53464A2F1C52459AB2DF6611D11ABE4C21B1A2EA099CD43797ECF478EA5C2A5364672E9549D7279750980734D3D308304BC74D479CB1D0E9AB707E020D07E039606AD1049309759AB83F597EEE015FD5961ABEF6E726FE703038B6B6C07C048115189F62F8F49398E194293B79290B6842A29EDA18F2FBBE22F441555B9A337869000B51E39E8EEFA34B6BCAD83A555B1BD56A970FC75E23C0EC1A8E320A254014DA4C799C10B63E9DEB0958A9968A8EAFE8A2A8CB2245CDC0D1C03E28C3D978B9D51960858B157D9B20F505696154116D4034ECB6705AF2BFB17C57AD300F678F4CACEC6A9A649DF61EB9D7006C38DABCD3367DD15295AA38A839979793AE929C791DEC6C7C4192042B7B83AD15238E9F53B5E92B7F7FEE12E7185E205B284CA56DB5135E966409C6AC0EE710CEA9904AD3C339D1B7CB348C5B961951DF118CE5766660DBE757C66829A1FFCEA5BA89EBA02BC06A979EA395FACD9A190C2D676F8B66EC9944446C2343531EA531EB511CBBF1F2074713281FE98F50B190264835D81FA726154DA07AB43F52932434B19AE336DAD8334ECB2AE6567C18E96A065CAF70ACEAC91345635957F70FC64EC92E3F379FD44D3F773FD377A1D9D1588FF6476ABD9B9BA0BD2EEF678E08AB849B4BAADDAB526E94EC71513E7777DDAC7A9A2F711D74DC030D752DF5D752419C05D2C0FF124D239AA549B9E08230BA00A972FEE69E0C8F4F8C86DDB7D33CF3A40CA39E1DB46FA097C51E8808EE896879B3CDF0E9B89AB87F3A3389ECEA4B0A23E7165F6ECE5F261FDB93EF355966B9FD0F3159FDF8D816D4E3C0CCB6D2E3D0EC56D13E787BF7505EBC9DF1027164762C1E77605BBA107D6CD3202367F6BBFDE89E8547CE95C02A367286DB6CB61B17CFC8FE6B0A73280FB7E8C6611D8383E8FC9637EFB3F3F6FF253F37094D3FF2BD957BE797F8C40DE71C4F3E0FE16E32D941CDB731F3B60DBAE8EF8BF076D34D9B9CAA07416F63F6CF43C3EDB71A0659E3FFA818EC99D92584FEAF2A836023BCAA3533B6E065C81B1A954B8CDA7A018A849A24084517245038ADD942D6DAFD44A23473E61CC219BB4A55922A3419E279B4E10C9D2DDBF6CF7A0D9B3A8FAF92AC93FB1426A09A144D802BF66B4AA3B0D2FBBCE5F2EE80D06958F4E6F5592ADDA35FAE2B24BB49DF0554B8AFAA1EB780418760F28AF9E4010ED1ED4EC2475892605D3EB9BB41761FC4A6DBC7A7942C0589658151CBE327C67018AFDEFE07DB1F20674E200000, N'6.4.4')
GO
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0000', N'Janusz', N'Wiśniewski', N'CEO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0001', N'Ernest', N'Baran', N'CTO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0002', N'Gniewomir', N'Szczepański', N'CIO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0003', N'Leonardo', N'Głowacki', N'CITO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0004', N'Błażej', N'Szewczyk', N'CFO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0005', N'Alfred', N'Lewandowski', N'CMO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0006', N'Alex', N'Górski', N'CSO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0007', N'Korneliusz', N'Zakrzewski', N'COO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0008', N'Kordian', N'Maciejewski', N'CRO', N'Zarząd')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0009', N'Igor', N'Krajewski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0010', N'Bogumił', N'Witkowski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0011', N'Kamil', N'Jakubowski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0012', N'Oktawian', N'Rutkowski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0013', N'Natan', N'Gajewski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0014', N'Alex', N'Baranowski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0015', N'Jacek', N'Lis', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0016', N'Dominik', N'Sokołowski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0017', N'Gracjan', N'Pawlak', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0018', N'Gniewomir', N'Stępień', N'Starszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0019', N'Aureliusz', N'Szymański', N'Starszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0020', N'Fabian', N'Wiśniewski', N'Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0021', N'Edward', N'Kubiak', N'Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0022', N'Krzysztof', N'Kalinowski', N'Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0023', N'Milan', N'Nowak', N'Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0024', N'Aleks', N'Jasiński', N'Młodszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0025', N'Alfred', N'Szymański', N'Młodszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0026', N'Joachim', N'Kubiak', N'Młodszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0027', N'Dariusz', N'Kalinowski', N'Młodszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0028', N'Julian', N'Sokołowski', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0029', N'Kuba', N'Sawicki', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0030', N'Konstanty', N'Walczak', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0031', N'Rafał', N'Borkowski', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0032', N'Damian', N'Ostrowski', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0033', N'Oliwia', N'Woźniak', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0034', N'Zuzanna', N'Chmielewska', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0035', N'Alina', N'Stępień', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0036', N'Amelia', N'Górecka', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0037', N'Małgorzata', N'Kaczmarczyk', N'Technik Reaktora', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0038', N'Julita', N'Nowak', N'Ogrodnik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0039', N'Anatolia', N'Szymańska', N'Ogrodnik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0040', N'Martyna', N'Jaworska', N'Monter', N'Wytwarzanie')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0041', N'Diana', N'Ostrowska', N'Monter', N'Wytwarzanie')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0042', N'Żaneta', N'Mazurek', N'Monter', N'Wytwarzanie')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0043', N'Weronika', N'Maciejewska', N'Monter', N'Wytwarzanie')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0044', N'Klaudia', N'Mazurek', N'Monter', N'Wytwarzanie')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0045', N'Faustyna', N'Jasińska', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0046', N'Irena', N'Ostrowska', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0047', N'Marcela', N'Lis', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0048', N'Klaudia', N'Wojciechowska', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0049', N'Arleta', N'Chmielewska', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0050', N'Honorata', N'Laskowska', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0051', N'Agnieszka', N'Lewandowska', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0052', N'Diana', N'Baran', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0053', N'Daria', N'Kubiak', N'Technik', N'Techniczny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0054', N'Klaudia', N'Urbańska', N'Pilot Kosmolotu', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0055', N'Sylwia', N'Marciniak', N'Pilot Kosmolotu', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0056', N'Klara', N'Jasińska', N'Pilot Kosmolotu', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0057', N'Otylia', N'Duda', N'Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0058', N'Weronika', N'Sokołowska', N'Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0059', N'Magdalena', N'Wasilewska', N'Młodszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0060', N'Edyta', N'Włodarczyk', N'Młodszy Maszynista', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0061', N'Ewa', N'Szewczyk', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0062', N'Oksana', N'Kamińska', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0063', N'Amanda', N'Jaworska', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0064', N'Dominika', N'Włodarczyk', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0065', N'Lidia', N'Kowalska', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0066', N'Bogdan', N'Boner', N'Łowca Demonów', N'Sekcja Paranormalna')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0067', N'Marcin', N'Łopian', N'Młodszy Łowca Demonów', N'Sekcja Paranormalna')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0068', N'Domino', N'Jahas', N'Praktykant', N'Sekcja Paranormalna')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0069', N'Janusz', N'Pień', N'Praktykant', N'Sekcja Paranormalna')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0070', N'Wilhelm', N'Blazkowski', N'Eksterminator', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0071', N'Aureliusz', N'Wiśniewski', N'Starszy Sprzątacz', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0072', N'Fabian', N'Baran', N'Starszy Sprzątacz', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0073', N'Edward', N'Szczepański', N'Starszy Sprzątacz', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0074', N'Krzysztof', N'Głowacki', N'Młodszy Sprzątacz', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0075', N'Milan', N'Szewczyk', N'Młodszy Sprzątacz', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0076', N'Aleks', N'Lewandowski', N'Młodszy Sprzątacz', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0077', N'Alfred', N'Górski', N'Praktykant', N'Operacyjny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0078', N'Joachim', N'Zakrzewski', N'Starszy Czyściciel', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0079', N'Dariusz', N'Maciejewski', N'Czyściciel', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0080', N'Julian', N'Krajewski', N'Czyściciel', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0081', N'Kuba', N'Witkowski', N'Czyściciel', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0082', N'Konstanty', N'Jakubowski', N'Młodszy Czyściciel', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0083', N'Rafał', N'Rutkowski', N'Młodszy Czyściciel', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0084', N'Damian', N'Kubiak', N'Młodszy Czyściciel', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0085', N'Diana', N'Jasińska', N'Praktykant', N'Wewnętrzny')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0086', N'Daria', N'Duda', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0087', N'Klaudia', N'Sokołowska', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0088', N'Sylwia', N'Wasilewska', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0089', N'Jan', N'Kowalski', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0090', N'Jan', N'Kowalski', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0091', N'Jan', N'Kowalski', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0092', N'Arkadiusz', N'Nowak', N'Szalony Naukowiec', N'Badawczy')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0093', N'Arkadiusz', N'Nowak', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0094', N'Arkadiusz', N'Nowak', N'Pilot Kosmolotu', N'Logistyka')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0095', N'Arkadiusz', N'Nowak', N'Monter', N'Wytwarzanie')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0096', N'Arkadiusz', N'Nowak', N'Behawiorysta', N'Wytwarzanie')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0097', N'Janusz', N'Zakrzewski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0098', N'Ernest', N'Maciejewski', N'Wartownik', N'Bezpieczeństwa')
INSERT [dbo].[Employees] ([Employee_Id], [Name], [Surname], [Position], [Department]) VALUES (N'0099', N'Gniewomir', N'Krajewski', N'Wartownik', N'Bezpieczeństwa')
GO
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0001', N'Centrum Kierowania', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0002', N'Sala Konferencyjna', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0003', N'Skrzydło Więzienne: Blok A', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0004', N'Skrzydło Więzienne: Blok B', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0005', N'Sala Nasłuchu', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0006', N'Sala Konferencyjna', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0007', N'Parowozownia', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0008', N'Obrotnica', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0009', N'Hangar I', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0010', N'Hangar II', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0011', N'Hangar III', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0012', N'Hangar IV', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0013', N'Hangar V', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0014', N'Hangar VI', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0015', N'Hangar VII', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0016', N'Hangar VIII', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0017', N'Zwrotnica A1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0018', N'Zwrotnica B1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0019', N'Zwrotnica B2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0020', N'Zwrotnica D0', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0021', N'Dyspozytornia Trakcyjna', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0022', N'Centrum Kontroli Lotów', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0023', N'Wyrzutnia 1/1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0024', N'Wyrzutnia 1/2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0025', N'Wyrzutnia 1/3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0026', N'Wyrzutnia 2/1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0027', N'Wyrzutnia 2/2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0028', N'Wyrzutnia 2/3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0029', N'Wyrzutnia 3/1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0030', N'Wyrzutnia 3/2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0031', N'Wyrzutnia 3/3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0032', N'Wyrzutnia 4/1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0033', N'Wyrzutnia 4/2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0034', N'Wyrzutnia 4/3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0035', N'Kantyna', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0036', N'Pokój nr B001', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0037', N'Pokój nr B002', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0038', N'Pokój nr B003', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0039', N'Pokój nr B004', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0040', N'Pokój nr B005', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0041', N'Pokój nr B006', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0042', N'Pokój nr B007', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0043', N'Pokój nr B008', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0044', N'Pokój nr B009', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0045', N'Pokój nr B100', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0046', N'Pokój nr B101', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0047', N'Pokój nr B102', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0048', N'Pokój nr B103', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0049', N'Pokój nr B104', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0050', N'Pokój nr B105', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0051', N'Pokój nr B106', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0052', N'Pokój nr B107', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0053', N'Pokój nr B108', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0054', N'Pokój nr B109', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0055', N'Pokój nr B201', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0056', N'Pokój nr B202', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0057', N'Pokój nr B203', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0058', N'Pokój nr B204', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0059', N'Pokój nr B205', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0060', N'Pokój nr B206', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0061', N'Pokój nr B207', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0062', N'Pokój nr B208', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0063', N'Pokój nr B209', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0064', N'Pokój nr B301', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0065', N'Pokój nr B302', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0066', N'Pokój nr B303', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0067', N'Pokój nr B304', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0068', N'Pokój nr B305', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0069', N'Pokój nr B306', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0070', N'Pokój nr B307', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0071', N'Pokój nr B308', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0072', N'Pokój nr B309', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0073', N'Pokój nr B401', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0074', N'Pokój nr B402', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0075', N'Pokój nr B403', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0076', N'Pokój nr B404', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0077', N'Pokój nr B405', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0078', N'Pokój nr B406', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0079', N'Pokój nr B407', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0080', N'Pokój nr B408', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0081', N'Pokój nr B409', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0082', N'Pokój nr B501', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0083', N'Pokój nr B502', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0084', N'Pokój nr B503', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0085', N'Pokój nr B504', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0086', N'Pokój nr B505', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0087', N'Pokój nr B506', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0088', N'Pokój nr B507', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0089', N'Pokój nr B508', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0090', N'Pokój nr B509', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0091', N'Stacja Nasłuchowa N', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0092', N'Stacja Nasłuchowa W', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0093', N'Stacja Nasłuchowa E', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0094', N'Stacja Nasłuchowa S', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0095', N'Cela nr A010', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0096', N'Cela nr A020', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0097', N'Cela nr A030', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0098', N'Cela nr A040', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0099', N'Cela nr A050', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0100', N'Cela nr A060', NULL)
GO
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0101', N'Cela nr A070', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0102', N'Cela nr A080', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0103', N'Cela nr A090', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0104', N'Cela nr B010', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0105', N'Cela nr B020', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0106', N'Cela nr B030', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0107', N'Cela nr B040', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0108', N'Cela nr B050', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0109', N'Cela nr B060', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0110', N'Cela nr B070', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0111', N'Cela nr B080', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0112', N'Cela nr B090', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0113', N'Wartownia A1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0114', N'Wartownia A2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0115', N'Wartownia A3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0116', N'Wartownia A4', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0117', N'Wartownia A5', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0118', N'Wartownia A6', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0119', N'Wartownia A7', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0120', N'Wartownia A8', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0121', N'Wartownia A9', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0122', N'Wartownia B1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0123', N'Wartownia B2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0124', N'Wartownia B3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0125', N'Wartownia B4', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0126', N'Wartownia B5', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0127', N'Wartownia B6', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0128', N'Wartownia B7', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0129', N'Wartownia B8', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0130', N'Wartownia B9', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0131', N'Wieża Obwodowa N', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0132', N'Wieża Obwodowa NE', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0133', N'Wieża Obwodowa NW', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0134', N'Wieża Obwodowa W', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0135', N'Wieża Obwodowa E', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0136', N'Wieża Obwodowa SE', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0137', N'Wieża Obwodowa SW', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0138', N'Wieża Obwodowa S', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0139', N'Serwerownia Główna', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0140', N'Serwerownia Zapasowa', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0141', N'Kanał Techniczny A1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0142', N'Kanał Techniczny A2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0143', N'Kanał Techniczny A3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0144', N'Kanał Techniczny A4', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0145', N'Kanał Techniczny A5', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0146', N'Kanał Techniczny B1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0147', N'Kanał Techniczny B2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0148', N'Kanał Techniczny B3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0149', N'Kanał Techniczny C1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0150', N'Kanał Techniczny C2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0151', N'Kanał Techniczny C3', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0152', N'Kanał Techniczny C4', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0153', N'Kanał Techniczny C5', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0154', N'Kanał Techniczny C6', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0155', N'Kanał Techniczny C7', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0156', N'Kanał Techniczny D1', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0157', N'Kanał Techniczny D2', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0158', N'Kanał Techniczny E0', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0159', N'Kanał Techniczny F0', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0160', N'Reaktor I', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0161', N'Reaktor II', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0162', N'Reaktor III', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0163', N'Tajny Reaktor', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0164', N'Sterownia Reaktora I', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0165', N'Sterownia Reaktora II', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0166', N'Sterownia Reaktora III', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0167', N'Tajna Sterownia', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0168', N'Dyspozytornia Mocy', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0169', N'Rozdzielnia A', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0170', N'Rozdzielnia B', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0171', N'Rozdzielnia C', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0172', N'Rozdzielnia D', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0173', N'Rozdzielnia F', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0174', N'Pokój Przesłuchań', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0175', N'Schowek pod schodami', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0176', N'Maszynownia 1/II', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0177', N'Maszynownia 2/II', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0178', N'Laboratorium X16', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0179', N'Laboratorium X18', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0180', N'Laboratorium 51', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0181', N'Pokój Socjalny', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0182', N'Palarnia', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0183', N'Oranżeria', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0184', N'Ogrody Naczelnika', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0185', N'Przyzakładowa strefa dla malucha', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0186', N'Arsenał Lekki', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0187', N'Arsenał Ciężki', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0188', N'Montownia Androidów - Sekcja Główna', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0189', N'Montownia Androidów - Sekcja Motoryki', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0190', N'Montownia Androidów - Sekcja Dostrojenia Behawioralnego', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0191', N'Montownia Androidów - Pokój Testów', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0192', N'Składowisko Odpadów', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0193', N'Składowisko Odpadów Niebezpiecznych', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0194', N'Składowisko Odpadów Wysoce Niebezpiecznych', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0195', N'Stacja Przetwarzania Paliwa - Sekcja Główna', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0196', N'Stacja Przetwarzania Paliwa - Sekcja Syntezy Helu', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0197', N'Stacja Przetwarzania Paliwa - Wirówki', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0198', N'Zbiornikownia', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0199', N'Magazyn A', NULL)
INSERT [dbo].[RoomKeys] ([RoomKey_Id], [RoomName], [AssignedEmployee_Id]) VALUES (N'0200', N'Magazyn B', NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Employee_Id]    Script Date: 06.01.2021 20:18:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee_Id] ON [dbo].[Employees]
(
	[Employee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AssignedEmployee_Id]    Script Date: 06.01.2021 20:18:50 ******/
CREATE NONCLUSTERED INDEX [IX_AssignedEmployee_Id] ON [dbo].[RoomKeys]
(
	[AssignedEmployee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoomKey_Id]    Script Date: 06.01.2021 20:18:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_RoomKey_Id] ON [dbo].[RoomKeys]
(
	[RoomKey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RoomKeys]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoomKeys_dbo.Employees_AssignedEmployee_Id] FOREIGN KEY([AssignedEmployee_Id])
REFERENCES [dbo].[Employees] ([Employee_Id])
GO
ALTER TABLE [dbo].[RoomKeys] CHECK CONSTRAINT [FK_dbo.RoomKeys_dbo.Employees_AssignedEmployee_Id]
GO
USE [master]
GO
ALTER DATABASE [KeyKeeperDataBase] SET  READ_WRITE 
GO

USE [master]
GO

    IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'SMSCommunication')
  BEGIN
    CREATE DATABASE [SMSCommunication]


    END
    GO
       USE [SMSCommunication]
    GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SMSCommunication].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SMSCommunication] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SMSCommunication] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SMSCommunication] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SMSCommunication] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SMSCommunication] SET ARITHABORT OFF 
GO
ALTER DATABASE [SMSCommunication] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SMSCommunication] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SMSCommunication] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SMSCommunication] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SMSCommunication] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SMSCommunication] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SMSCommunication] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SMSCommunication] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SMSCommunication] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SMSCommunication] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SMSCommunication] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SMSCommunication] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SMSCommunication] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SMSCommunication] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SMSCommunication] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SMSCommunication] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SMSCommunication] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SMSCommunication] SET RECOVERY FULL 
GO
ALTER DATABASE [SMSCommunication] SET  MULTI_USER 
GO
ALTER DATABASE [SMSCommunication] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SMSCommunication] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SMSCommunication] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SMSCommunication] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SMSCommunication] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SMSCommunication] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SMSCommunication', N'ON'
GO
ALTER DATABASE [SMSCommunication] SET QUERY_STORE = ON
GO
ALTER DATABASE [SMSCommunication] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SMSCommunication]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaseDtoOperatorDto]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaseDtoOperatorDto](
	[CasesId] [uniqueidentifier] NOT NULL,
	[SecondaryOperatorsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CaseDtoOperatorDto] PRIMARY KEY CLUSTERED 
(
	[CasesId] ASC,
	[SecondaryOperatorsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cases]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cases](
	[Id] [uniqueidentifier] NOT NULL,
	[Archived] [bit] NOT NULL,
	[ClaimNumber] [nvarchar](max) NOT NULL,
	[DateOfLoss] [nvarchar](max) NULL,
	[PolicyNumber] [nvarchar](max) NOT NULL,
	[Deductible] [int] NULL,
	[Brand] [nvarchar](max) NULL,
	[BusinessName] [nvarchar](max) NULL,
	[LineOfBusinessId] [uniqueidentifier] NULL,
	[State] [int] NOT NULL,
	[CaseType] [int] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[PrimaryContact] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[ReferenceId] [nvarchar](max) NOT NULL,
	[Privacy] [int] NULL,
	[SMSNumber] [nvarchar](max) NULL,
	[LanguagePreference] [int] NULL,
 CONSTRAINT [PK_Cases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyPhoneNumbers]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyPhoneNumbers](
	[PhoneNumber] [nvarchar](450) NOT NULL,
	[SMSProcessor] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CompanyPhoneNumbers] PRIMARY KEY CLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configurations]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configurations](
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Configurations] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[First] [nvarchar](max) NOT NULL,
	[Last] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[Mobile] [nvarchar](450) NOT NULL,
	[Role] [int] NOT NULL,
	[OptStatus] [bit] NOT NULL,
	[OptStatusDetail] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lineOfBusinesses]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lineOfBusinesses](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[SubType] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_lineOfBusinesses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageAttachmentDtoMessageDto]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageAttachmentDtoMessageDto](
	[Messagesid] [nvarchar](450) NOT NULL,
	[messageAttachmentsid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MessageAttachmentDtoMessageDto] PRIMARY KEY CLUSTERED 
(
	[Messagesid] ASC,
	[messageAttachmentsid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageAttachments]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageAttachments](
	[id] [uniqueidentifier] NOT NULL,
	[sourceUrl] [nvarchar](max) NULL,
	[name] [nvarchar](max) NULL,
	[extension] [nvarchar](max) NULL,
	[data] [varbinary](max) NULL,
 CONSTRAINT [PK_MessageAttachments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageAuthors]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageAuthors](
	[id] [nvarchar](450) NOT NULL,
	[role] [int] NOT NULL,
	[firstName] [nvarchar](max) NOT NULL,
	[lastName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MessageAuthors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[id] [nvarchar](450) NOT NULL,
	[sMSId] [nvarchar](max) NULL,
	[type] [int] NOT NULL,
	[formatting] [int] NOT NULL,
	[text] [nvarchar](max) NOT NULL,
	[status] [nvarchar](max) NOT NULL,
	[channelSource] [int] NOT NULL,
	[authorid] [nvarchar](450) NULL,
	[createdAt] [datetime2](7) NOT NULL,
	[needsAttention] [bit] NOT NULL,
	[needsAction] [bit] NOT NULL,
	[messageTemplateTemplateId] [int] NULL,
	[CaseDtoId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageTemplates]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageTemplates](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[TemplateText] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MessageTemplates] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageTemplateValues]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageTemplateValues](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[MessageTemplateDtoTemplateId] [int] NULL,
 CONSTRAINT [PK_MessageTemplateValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperatorDtoOperatorRoleDto]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatorDtoOperatorRoleDto](
	[OperatorsId] [uniqueidentifier] NOT NULL,
	[RolesId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OperatorDtoOperatorRoleDto] PRIMARY KEY CLUSTERED 
(
	[OperatorsId] ASC,
	[RolesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperatorRoles]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatorRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_OperatorRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 5/31/2024 7:23:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operators](
	[Id] [uniqueidentifier] NOT NULL,
	[First] [nvarchar](max) NOT NULL,
	[Last] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[IdentityProvider] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[OfficeNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230815210912_initialMigration', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230815210941_initialMigration', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230816171539_setSourceURLNull', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240419122014_addUniqueIndexCustomer', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240531121259_addActiveFlagToPhoneNumber', N'8.0.4')
GO
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'1111111111', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'2222222222', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'3333333333', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'4444444444', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'5555555555', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'6666666666', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'7777777777', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'8888888888', N'DemoSMS', 1)
INSERT [dbo].[CompanyPhoneNumbers] ([PhoneNumber], [SMSProcessor], [IsActive]) VALUES (N'9999999999', N'DemoSMS', 1)
GO
INSERT [dbo].[Configurations] ([Name], [Value]) VALUES (N'BreeceWorks.CommunicationWebApi', N'https://localhost:7241')
INSERT [dbo].[Configurations] ([Name], [Value]) VALUES (N'BreeceWorks.SMSCoreWebApi', N'https://localhost:7131')
INSERT [dbo].[Configurations] ([Name], [Value]) VALUES (N'BreeceWorks.SMSSimulator', N'https://localhost:7041')
INSERT [dbo].[Configurations] ([Name], [Value]) VALUES (N'CommunicationWebApiKey', N'123456789')
INSERT [dbo].[Configurations] ([Name], [Value]) VALUES (N'Twilio:AuthToken', N'')
INSERT [dbo].[Configurations] ([Name], [Value]) VALUES (N'Twilio:Client:AccountSid', N'')
INSERT [dbo].[Configurations] ([Name], [Value]) VALUES (N'Twilio:RequestValidation:Enabled', N'true')
GO
INSERT [dbo].[lineOfBusinesses] ([Id], [Type], [SubType]) VALUES (N'60df04d7-0ba8-4e2e-9dbe-b2fd5558b5df', N'personal', N'auto')
GO
SET IDENTITY_INSERT [dbo].[MessageTemplates] ON 

INSERT [dbo].[MessageTemplates] ([TemplateId], [Name], [TemplateText]) VALUES (1, N'WELCOME', N'Text messaging for claim @ReferenceId has been activated.  Please reply YES or ACCEPT to continue receiving text messages.  Please reply STOP to opt out of text messaging.')
INSERT [dbo].[MessageTemplates] ([TemplateId], [Name], [TemplateText]) VALUES (2, N'OPTED_OUT_RESPONSE', N'You have successfully been unsubscribed. You will not receive any more messages from this number. Reply START to resubscribe.')
INSERT [dbo].[MessageTemplates] ([TemplateId], [Name], [TemplateText]) VALUES (3, N'OPTED_IN_RESPONSE', N'You have been subscribed to the text messaging service.  Reply STOP to unsubscribe. Msg&Data Rates May Apply.')
INSERT [dbo].[MessageTemplates] ([TemplateId], [Name], [TemplateText]) VALUES (4, N'PRIMARY_CONTACT_ASSIGNED', N'This case has been assigned to @Email.')
INSERT [dbo].[MessageTemplates] ([TemplateId], [Name], [TemplateText]) VALUES (5, N'PRIMARY_CONTACT_UNASSIGNED', N'This case has been unassigned from the primary contact.')
SET IDENTITY_INSERT [dbo].[MessageTemplates] OFF
GO
INSERT [dbo].[MessageTemplateValues] ([Id], [Name], [MessageTemplateDtoTemplateId]) VALUES (N'222BCE53-5735-48F6-9832-F2D084612899', N'Email', 4)
INSERT [dbo].[MessageTemplateValues] ([Id], [Name], [MessageTemplateDtoTemplateId]) VALUES (N'9AFF66F0-E25C-4249-BB4F-75C792554AF8', N'ReferenceId', 1)
GO
INSERT [dbo].[OperatorRoles] ([Id], [Role]) VALUES (N'31158089-749f-4096-8e5b-36938cdfa747', N'operator')
INSERT [dbo].[OperatorRoles] ([Id], [Role]) VALUES (N'b0379ceb-80d4-46af-aa92-7d0f566bebea', N'developer')
INSERT [dbo].[OperatorRoles] ([Id], [Role]) VALUES (N'4096ac8d-9b66-4807-ace0-d8f955144ed4', N'admin')
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customers_Email_Mobile]    Script Date: 5/31/2024 7:23:30 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers_Email_Mobile] ON [dbo].[Customers]
(
	[Email] ASC,
	[Mobile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyPhoneNumbers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[CaseDtoOperatorDto]  WITH CHECK ADD  CONSTRAINT [FK_CaseDtoOperatorDto_Cases_CasesId] FOREIGN KEY([CasesId])
REFERENCES [dbo].[Cases] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CaseDtoOperatorDto] CHECK CONSTRAINT [FK_CaseDtoOperatorDto_Cases_CasesId]
GO
ALTER TABLE [dbo].[CaseDtoOperatorDto]  WITH CHECK ADD  CONSTRAINT [FK_CaseDtoOperatorDto_Operators_SecondaryOperatorsId] FOREIGN KEY([SecondaryOperatorsId])
REFERENCES [dbo].[Operators] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CaseDtoOperatorDto] CHECK CONSTRAINT [FK_CaseDtoOperatorDto_Operators_SecondaryOperatorsId]
GO
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_lineOfBusinesses_LineOfBusinessId] FOREIGN KEY([LineOfBusinessId])
REFERENCES [dbo].[lineOfBusinesses] ([Id])
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_lineOfBusinesses_LineOfBusinessId]
GO
ALTER TABLE [dbo].[MessageAttachmentDtoMessageDto]  WITH CHECK ADD  CONSTRAINT [FK_MessageAttachmentDtoMessageDto_MessageAttachments_messageAttachmentsid] FOREIGN KEY([messageAttachmentsid])
REFERENCES [dbo].[MessageAttachments] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageAttachmentDtoMessageDto] CHECK CONSTRAINT [FK_MessageAttachmentDtoMessageDto_MessageAttachments_messageAttachmentsid]
GO
ALTER TABLE [dbo].[MessageAttachmentDtoMessageDto]  WITH CHECK ADD  CONSTRAINT [FK_MessageAttachmentDtoMessageDto_Messages_Messagesid] FOREIGN KEY([Messagesid])
REFERENCES [dbo].[Messages] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageAttachmentDtoMessageDto] CHECK CONSTRAINT [FK_MessageAttachmentDtoMessageDto_Messages_Messagesid]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Cases_CaseDtoId] FOREIGN KEY([CaseDtoId])
REFERENCES [dbo].[Cases] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Cases_CaseDtoId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_MessageAuthors_authorid] FOREIGN KEY([authorid])
REFERENCES [dbo].[MessageAuthors] ([id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_MessageAuthors_authorid]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_MessageTemplates_messageTemplateTemplateId] FOREIGN KEY([messageTemplateTemplateId])
REFERENCES [dbo].[MessageTemplates] ([TemplateId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_MessageTemplates_messageTemplateTemplateId]
GO
ALTER TABLE [dbo].[MessageTemplateValues]  WITH CHECK ADD  CONSTRAINT [FK_MessageTemplateValues_MessageTemplates_MessageTemplateDtoTemplateId] FOREIGN KEY([MessageTemplateDtoTemplateId])
REFERENCES [dbo].[MessageTemplates] ([TemplateId])
GO
ALTER TABLE [dbo].[MessageTemplateValues] CHECK CONSTRAINT [FK_MessageTemplateValues_MessageTemplates_MessageTemplateDtoTemplateId]
GO
ALTER TABLE [dbo].[OperatorDtoOperatorRoleDto]  WITH CHECK ADD  CONSTRAINT [FK_OperatorDtoOperatorRoleDto_OperatorRoles_RolesId] FOREIGN KEY([RolesId])
REFERENCES [dbo].[OperatorRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OperatorDtoOperatorRoleDto] CHECK CONSTRAINT [FK_OperatorDtoOperatorRoleDto_OperatorRoles_RolesId]
GO
ALTER TABLE [dbo].[OperatorDtoOperatorRoleDto]  WITH CHECK ADD  CONSTRAINT [FK_OperatorDtoOperatorRoleDto_Operators_OperatorsId] FOREIGN KEY([OperatorsId])
REFERENCES [dbo].[Operators] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OperatorDtoOperatorRoleDto] CHECK CONSTRAINT [FK_OperatorDtoOperatorRoleDto_Operators_OperatorsId]
GO

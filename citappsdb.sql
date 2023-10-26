USE [CitAppsDb]
GO
/***** Object:  Table [dbo].[Applications]    Script Date: 24-10-2023 18:47:43 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/***** Object:  Table [dbo].[Clients]    Script Date: 24-10-2023 18:47:43 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ContactNumber] [bigint] NOT NULL,
	[ContactPerson] [nvarchar](max) NOT NULL,
	[SubscriptionKey] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/***** Object:  Table [dbo].[ClientSubscriptions]    Script Date: 24-10-2023 18:47:43 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientSubscriptions](
	[ClientSubscriptionId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[SubscriptionPlanId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[IsTerminated] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ClientSubscriptions] PRIMARY KEY CLUSTERED 
(
	[ClientSubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/***** Object:  Table [dbo].[SubscriptionFeatures]    Script Date: 24-10-2023 18:47:43 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionFeatures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionPlanId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SubscriptionFeatures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/***** Object:  Table [dbo].[SubscriptionPlans]    Script Date: 24-10-2023 18:47:43 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionPlans](
	[SubscriptionPlanId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[BillingCycleType] [nvarchar](max) NOT NULL,
	[ApplicationId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SubscriptionPlans] PRIMARY KEY CLUSTERED 
(
	[SubscriptionPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/***** Object:  Table [dbo].[Users]    Script Date: 24-10-2023 18:47:43 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Users_CreatedBy]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Users_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Users_ModifiedBy]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Users_CreatedBy]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Users_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Users_ModifiedBy]
GO
ALTER TABLE [dbo].[ClientSubscriptions]  WITH CHECK ADD  CONSTRAINT [FK_ClientSubscriptions_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[ClientSubscriptions] CHECK CONSTRAINT [FK_ClientSubscriptions_Clients_ClientId]
GO
ALTER TABLE [dbo].[ClientSubscriptions]  WITH CHECK ADD  CONSTRAINT [FK_ClientSubscriptions_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ClientSubscriptions] CHECK CONSTRAINT [FK_ClientSubscriptions_Users_CreatedBy]
GO
ALTER TABLE [dbo].[ClientSubscriptions]  WITH CHECK ADD  CONSTRAINT [FK_ClientSubscriptions_Users_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ClientSubscriptions] CHECK CONSTRAINT [FK_ClientSubscriptions_Users_ModifiedBy]
GO
ALTER TABLE [dbo].[SubscriptionFeatures]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionFeatures_SubscriptionPlans_SubscriptionPlanId] FOREIGN KEY([SubscriptionPlanId])
REFERENCES [dbo].[SubscriptionPlans] ([SubscriptionPlanId])
GO
ALTER TABLE [dbo].[SubscriptionFeatures] CHECK CONSTRAINT [FK_SubscriptionFeatures_SubscriptionPlans_SubscriptionPlanId]
GO
ALTER TABLE [dbo].[SubscriptionFeatures]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionFeatures_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SubscriptionFeatures] CHECK CONSTRAINT [FK_SubscriptionFeatures_Users_CreatedBy]
GO
ALTER TABLE [dbo].[SubscriptionFeatures]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionFeatures_Users_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SubscriptionFeatures] CHECK CONSTRAINT [FK_SubscriptionFeatures_Users_ModifiedBy]
GO
ALTER TABLE [dbo].[SubscriptionPlans]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionPlans_Applications_ApplicationId] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[SubscriptionPlans] CHECK CONSTRAINT [FK_SubscriptionPlans_Applications_ApplicationId]
GO
ALTER TABLE [dbo].[SubscriptionPlans]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionPlans_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SubscriptionPlans] CHECK CONSTRAINT [FK_SubscriptionPlans_Users_CreatedBy]
GO
ALTER TABLE [dbo].[SubscriptionPlans]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionPlans_Users_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SubscriptionPlans] CHECK CONSTRAINT [FK_SubscriptionPlans_Users_ModifiedBy]
GO
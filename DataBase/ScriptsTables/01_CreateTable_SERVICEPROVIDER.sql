USE [DB_SERVICE_PAYMENT]
GO

/****** Object:  Table [dbo].[SERVICEPROVIDER]    Script Date: 6/1/2026 01:07:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [payments].[SERVICEPROVIDER](
	[provider_id_ui] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[name_vc] [varchar](50) NOT NULL,
	[adress_vc] [varchar](100) NULL,
	[nit_in] [int] NOT NULL,
	[cell_phone_in] [int] NULL,
	[service_type_vc] [varchar](20) NOT NULL,
	[email_vc] [varchar](50) NULL,
	[user_registration_vc] [varchar](10) NOT NULL,
	[date_registration_dt] [datetime] NOT NULL,
	[user_modification_vc] [varchar](10) NOT NULL,
	[date_modification_dt] [datetime] NOT NULL,
	[state_bt] [bit] NOT NULL,
 CONSTRAINT [PK_SERVICEPROVIDER] PRIMARY KEY CLUSTERED 
(
	[provider_id_ui] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



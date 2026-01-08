USE [DB_SERVICE_PAYMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [payments].[CUSTOMER](
	[customer_id_ui] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[name_vc] [varchar](60) NOT NULL,
	[last_name_vc] [varchar](50) NOT NULL,
	[identification_number_in] [int] NOT NULL,
	[identification_extension_ch] [char](2) NOT NULL,
	[identification_complement_ch] [char](2) NOT NULL,
	[birth_date_dt] [date] NULL,
	[cell_phone_in] [int] NULL,
	[email_vc] [varchar](50) NULL,
	[user_registration_vc] [varchar](10) NOT NULL,
	[date_registration_dt] [datetime] NOT NULL,
	[user_modification_vc] [varchar](10) NOT NULL,
	[date_modification_dt] [datetime] NOT NULL,
	[state_bt] [bit] NOT NULL,
 CONSTRAINT [PK_CUSTOMER] PRIMARY KEY CLUSTERED 
(
	[customer_id_ui] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



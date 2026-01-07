USE [DB_SERVICE_PAYMENT]
GO

/****** Object:  Table [payment].[PAYMENT]    Script Date: 6/1/2026 01:07:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [payments].[PAYMENT](
	[payment_id_ui] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[customer_id_ui] [uniqueidentifier] NOT NULL,
	[provider_id_ui] [uniqueidentifier] NOT NULL,
	[amount_dc] [decimal](18, 2) NOT NULL,
	[currency_type_ch] [char](3) NOT NULL,
	[status_vc] [varchar](20) NOT NULL,
	[user_registration_vc] [varchar](10) NOT NULL,
	[date_registration_dt] [datetime] NOT NULL,
	[user_modification_vc] [varchar](10) NOT NULL,
	[date_modification_dt] [datetime] NOT NULL,
	[state_bt] [bit] NOT NULL,
 CONSTRAINT [PK_PAYMENT] PRIMARY KEY CLUSTERED 
(
	[payment_id_ui] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [payments].[PAYMENT]  WITH CHECK ADD  CONSTRAINT [FK_PAYMENT_CUSTOMER] FOREIGN KEY([customer_id_ui])
REFERENCES [payments].[CUSTOMER] ([customer_id_ui])
GO

ALTER TABLE [payments].[PAYMENT] CHECK CONSTRAINT [FK_PAYMENT_CUSTOMER]
GO

ALTER TABLE [payments].[PAYMENT]  WITH CHECK ADD  CONSTRAINT [FK_PAYMENT_SERVICEPROVIDER] FOREIGN KEY([provider_id_ui])
REFERENCES [payments].[SERVICEPROVIDER] ([provider_id_ui])
GO

ALTER TABLE [payments].[PAYMENT] CHECK CONSTRAINT [FK_PAYMENT_SERVICEPROVIDER]
GO



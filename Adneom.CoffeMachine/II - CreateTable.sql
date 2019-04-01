CREATE DATABASE [MachineDB]
 
GO

CREATE TABLE [dbo].[machine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_machine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[OperateurService](
	[ID] [int] NOT NULL,
	[Operateur] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_OperateurService] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO





CREATE TABLE [dbo].[TypeBoisson](
	[ID] [int] NOT NULL,
	[TypeBoisson] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_TypeBoisson] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiceMachine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MachineID] [int] NULL,
	[TypeBoissonID] [int] NOT NULL,
	[OperateurID] [int] NULL,
	[AvecMug] [bit] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ServiceMachine]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMachine_machine] FOREIGN KEY([MachineID])
REFERENCES [dbo].[machine] ([ID])
GO

ALTER TABLE [dbo].[ServiceMachine] CHECK CONSTRAINT [FK_ServiceMachine_machine]
GO

ALTER TABLE [dbo].[ServiceMachine]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMachine_OperateurService] FOREIGN KEY([OperateurID])
REFERENCES [dbo].[OperateurService] ([ID])
GO

ALTER TABLE [dbo].[ServiceMachine] CHECK CONSTRAINT [FK_ServiceMachine_OperateurService]
GO

ALTER TABLE [dbo].[ServiceMachine]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMachine_TypeBoisson] FOREIGN KEY([TypeBoissonID])
REFERENCES [dbo].[TypeBoisson] ([ID])
GO

ALTER TABLE [dbo].[ServiceMachine] CHECK CONSTRAINT [FK_ServiceMachine_TypeBoisson]
GO

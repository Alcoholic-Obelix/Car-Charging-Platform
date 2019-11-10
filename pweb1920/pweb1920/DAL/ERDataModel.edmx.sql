
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/10/2019 21:45:25
-- Generated from EDMX file: C:\Users\ricar\OneDrive - ISEC\ISEC\cadeiras\programacaoweb\Car-Charging-Platform\pweb1920\pweb1920\DAL\ERDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\Users\ricar\OneDrive - ISEC\ISEC\cadeiras\programacaoweb\Car-Charging-Platform\pweb1920\pweb1920\App_Data\CarChargeDb.mdf]
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ChargingModeReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_ChargingModeReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_ChargingPointChargingMode_ChargingMode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChargingPointChargingMode] DROP CONSTRAINT [FK_ChargingPointChargingMode_ChargingMode];
GO
IF OBJECT_ID(N'[dbo].[FK_ChargingPointChargingMode_ChargingPoint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChargingPointChargingMode] DROP CONSTRAINT [FK_ChargingPointChargingMode_ChargingPoint];
GO
IF OBJECT_ID(N'[dbo].[FK_ChargingPointReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_ChargingPointReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_ClientReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_StationChargingPoint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChargingPoints] DROP CONSTRAINT [FK_StationChargingPoint];
GO
IF OBJECT_ID(N'[dbo].[FK_StationCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stations] DROP CONSTRAINT [FK_StationCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_StationPriceHour]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PriceHours] DROP CONSTRAINT [FK_StationPriceHour];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ChargingModes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChargingModes];
GO
IF OBJECT_ID(N'[dbo].[ChargingPointChargingMode]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChargingPointChargingMode];
GO
IF OBJECT_ID(N'[dbo].[ChargingPoints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChargingPoints];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[PriceHours]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PriceHours];
GO
IF OBJECT_ID(N'[dbo].[Reservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservations];
GO
IF OBJECT_ID(N'[dbo].[Stations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [NIF] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Stations'
CREATE TABLE [dbo].[Stations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StreetAdress] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [District] nvarchar(max)  NOT NULL,
    [Companies_Id] int  NOT NULL
);
GO

-- Creating table 'ChargingPoints'
CREATE TABLE [dbo].[ChargingPoints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Station_Id] int  NOT NULL
);
GO

-- Creating table 'ChargingModes'
CREATE TABLE [dbo].[ChargingModes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Multiplier] float  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TimeStart] time  NOT NULL,
    [TimeFinish] time  NOT NULL,
    [ServiceCode] bigint  NOT NULL,
    [EstimatedCost] float  NOT NULL,
    [ChargingPoint_Id] int  NOT NULL,
    [Client_Id] int  NOT NULL,
    [ChargingMode_Id] int  NOT NULL
);
GO

-- Creating table 'PriceHours'
CREATE TABLE [dbo].[PriceHours] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FromTime] time  NOT NULL,
    [ToTime] time  NOT NULL,
    [Price] float  NOT NULL,
    [Station_Id] int  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [NIF] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ChargingPointChargingMode'
CREATE TABLE [dbo].[ChargingPointChargingMode] (
    [ChargingPoint_Id] int  NOT NULL,
    [ChargingModes_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stations'
ALTER TABLE [dbo].[Stations]
ADD CONSTRAINT [PK_Stations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChargingPoints'
ALTER TABLE [dbo].[ChargingPoints]
ADD CONSTRAINT [PK_ChargingPoints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChargingModes'
ALTER TABLE [dbo].[ChargingModes]
ADD CONSTRAINT [PK_ChargingModes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PriceHours'
ALTER TABLE [dbo].[PriceHours]
ADD CONSTRAINT [PK_PriceHours]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ChargingPoint_Id], [ChargingModes_Id] in table 'ChargingPointChargingMode'
ALTER TABLE [dbo].[ChargingPointChargingMode]
ADD CONSTRAINT [PK_ChargingPointChargingMode]
    PRIMARY KEY CLUSTERED ([ChargingPoint_Id], [ChargingModes_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Companies_Id] in table 'Stations'
ALTER TABLE [dbo].[Stations]
ADD CONSTRAINT [FK_StationCompany]
    FOREIGN KEY ([Companies_Id])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StationCompany'
CREATE INDEX [IX_FK_StationCompany]
ON [dbo].[Stations]
    ([Companies_Id]);
GO

-- Creating foreign key on [Station_Id] in table 'ChargingPoints'
ALTER TABLE [dbo].[ChargingPoints]
ADD CONSTRAINT [FK_StationChargingPoint]
    FOREIGN KEY ([Station_Id])
    REFERENCES [dbo].[Stations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StationChargingPoint'
CREATE INDEX [IX_FK_StationChargingPoint]
ON [dbo].[ChargingPoints]
    ([Station_Id]);
GO

-- Creating foreign key on [ChargingPoint_Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ChargingPointReservation]
    FOREIGN KEY ([ChargingPoint_Id])
    REFERENCES [dbo].[ChargingPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChargingPointReservation'
CREATE INDEX [IX_FK_ChargingPointReservation]
ON [dbo].[Reservations]
    ([ChargingPoint_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ClientReservation]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientReservation'
CREATE INDEX [IX_FK_ClientReservation]
ON [dbo].[Reservations]
    ([Client_Id]);
GO

-- Creating foreign key on [ChargingPoint_Id] in table 'ChargingPointChargingMode'
ALTER TABLE [dbo].[ChargingPointChargingMode]
ADD CONSTRAINT [FK_ChargingPointChargingMode_ChargingPoint]
    FOREIGN KEY ([ChargingPoint_Id])
    REFERENCES [dbo].[ChargingPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ChargingModes_Id] in table 'ChargingPointChargingMode'
ALTER TABLE [dbo].[ChargingPointChargingMode]
ADD CONSTRAINT [FK_ChargingPointChargingMode_ChargingMode]
    FOREIGN KEY ([ChargingModes_Id])
    REFERENCES [dbo].[ChargingModes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChargingPointChargingMode_ChargingMode'
CREATE INDEX [IX_FK_ChargingPointChargingMode_ChargingMode]
ON [dbo].[ChargingPointChargingMode]
    ([ChargingModes_Id]);
GO

-- Creating foreign key on [ChargingMode_Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ChargingModeReservation]
    FOREIGN KEY ([ChargingMode_Id])
    REFERENCES [dbo].[ChargingModes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChargingModeReservation'
CREATE INDEX [IX_FK_ChargingModeReservation]
ON [dbo].[Reservations]
    ([ChargingMode_Id]);
GO

-- Creating foreign key on [Station_Id] in table 'PriceHours'
ALTER TABLE [dbo].[PriceHours]
ADD CONSTRAINT [FK_StationPriceHour]
    FOREIGN KEY ([Station_Id])
    REFERENCES [dbo].[Stations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StationPriceHour'
CREATE INDEX [IX_FK_StationPriceHour]
ON [dbo].[PriceHours]
    ([Station_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
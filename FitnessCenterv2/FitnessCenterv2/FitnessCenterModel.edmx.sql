<<<<<<< HEAD
<<<<<<< HEAD

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/05/2015 00:52:36
-- Generated from EDMX file: D:\Drive\Projects\C#\FitnessCenterv2\FitnessCenterv2\FitnessCenterModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FitnessCenter];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Customer_Trainer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Trainer];
GO
IF OBJECT_ID(N'[dbo].[FK_PassReset_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PassReset] DROP CONSTRAINT [FK_PassReset_User];
GO
IF OBJECT_ID(N'[dbo].[FK_PassReset_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PassReset] DROP CONSTRAINT [FK_PassReset_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_Report_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Report] DROP CONSTRAINT [FK_Report_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Report_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Report] DROP CONSTRAINT [FK_Report_User1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Equipment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Equipment];
GO
IF OBJECT_ID(N'[dbo].[Manager]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manager];
GO
IF OBJECT_ID(N'[dbo].[PassReset]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PassReset];
GO
IF OBJECT_ID(N'[dbo].[Report]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Report];
GO
IF OBJECT_ID(N'[dbo].[Staff]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staff];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Trainer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainer];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [TrainerID] int  NULL,
    [RegistrationDate] datetime  NULL,
    [CreditCardNumber] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Gender] bit  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'Equipments'
CREATE TABLE [dbo].[Equipments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Quantity] int  NULL,
    [IsAvaliable] bit  NULL,
    [UnitPrice] int  NULL
);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [BirthDate] datetime  NULL,
    [Address] nvarchar(50)  NULL,
    [City] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Gender] bit  NULL,
    [Salary] decimal(18,0)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [dbo].[Staffs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Title] nvarchar(50)  NULL,
    [BirthDate] datetime  NULL,
    [Address] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Gender] bit  NULL,
    [HireDate] datetime  NULL,
    [Salary] decimal(18,0)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Trainers'
CREATE TABLE [dbo].[Trainers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [BirthDate] datetime  NULL,
    [Gender] bit  NULL,
    [HireDate] datetime  NULL,
    [Salary] decimal(18,0)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [Role] nvarchar(50)  NULL
);
GO

-- Creating table 'Reports'
CREATE TABLE [dbo].[Reports] (
    [SenderID] int  NOT NULL,
    [ReceiverID] int  NOT NULL,
    [Subject] nvarchar(50)  NULL,
    [Body] nvarchar(50)  NULL,
    [SendDate] datetime  NULL
);
GO

-- Creating table 'PassResets'
CREATE TABLE [dbo].[PassResets] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NULL,
    [EMail] nvarchar(50)  NULL,
    [AutID] nvarchar(300)  NULL,
    [isAvaliable] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Equipments'
ALTER TABLE [dbo].[Equipments]
ADD CONSTRAINT [PK_Equipments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [PK_Trainers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [SenderID], [ReceiverID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [PK_Reports]
    PRIMARY KEY CLUSTERED ([SenderID], [ReceiverID] ASC);
GO

-- Creating primary key on [ID] in table 'PassResets'
ALTER TABLE [dbo].[PassResets]
ADD CONSTRAINT [PK_PassResets]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TrainerID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_Customer_Trainer]
    FOREIGN KEY ([TrainerID])
    REFERENCES [dbo].[Trainers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Customer_Trainer'
CREATE INDEX [IX_FK_Customer_Trainer]
ON [dbo].[Customers]
    ([TrainerID]);
GO

-- Creating foreign key on [SenderID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_Report_User]
    FOREIGN KEY ([SenderID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReceiverID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_Report_User1]
    FOREIGN KEY ([ReceiverID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Report_User1'
CREATE INDEX [IX_FK_Report_User1]
ON [dbo].[Reports]
    ([ReceiverID]);
GO

-- Creating foreign key on [UserID] in table 'PassResets'
ALTER TABLE [dbo].[PassResets]
ADD CONSTRAINT [FK_PassReset_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PassReset_User'
CREATE INDEX [IX_FK_PassReset_User]
ON [dbo].[PassResets]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'PassResets'
ALTER TABLE [dbo].[PassResets]
ADD CONSTRAINT [FK_PassReset_User1]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PassReset_User1'
CREATE INDEX [IX_FK_PassReset_User1]
ON [dbo].[PassResets]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
=======
=======
>>>>>>> master

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/05/2015 00:52:36
-- Generated from EDMX file: D:\Drive\Projects\C#\FitnessCenterv2\FitnessCenterv2\FitnessCenterModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FitnessCenter];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Customer_Trainer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Trainer];
GO
IF OBJECT_ID(N'[dbo].[FK_PassReset_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PassReset] DROP CONSTRAINT [FK_PassReset_User];
GO
IF OBJECT_ID(N'[dbo].[FK_PassReset_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PassReset] DROP CONSTRAINT [FK_PassReset_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_Report_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Report] DROP CONSTRAINT [FK_Report_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Report_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Report] DROP CONSTRAINT [FK_Report_User1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Equipment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Equipment];
GO
IF OBJECT_ID(N'[dbo].[Manager]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manager];
GO
IF OBJECT_ID(N'[dbo].[PassReset]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PassReset];
GO
IF OBJECT_ID(N'[dbo].[Report]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Report];
GO
IF OBJECT_ID(N'[dbo].[Staff]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staff];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Trainer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainer];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [TrainerID] int  NULL,
    [RegistrationDate] datetime  NULL,
    [CreditCardNumber] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Gender] bit  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'Equipments'
CREATE TABLE [dbo].[Equipments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Quantity] int  NULL,
    [IsAvaliable] bit  NULL,
    [UnitPrice] int  NULL
);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [BirthDate] datetime  NULL,
    [Address] nvarchar(50)  NULL,
    [City] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Gender] bit  NULL,
    [Salary] decimal(18,0)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [dbo].[Staffs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Title] nvarchar(50)  NULL,
    [BirthDate] datetime  NULL,
    [Address] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Gender] bit  NULL,
    [HireDate] datetime  NULL,
    [Salary] decimal(18,0)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Trainers'
CREATE TABLE [dbo].[Trainers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [BirthDate] datetime  NULL,
    [Gender] bit  NULL,
    [HireDate] datetime  NULL,
    [Salary] decimal(18,0)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [EMail] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [Role] nvarchar(50)  NULL
);
GO

-- Creating table 'Reports'
CREATE TABLE [dbo].[Reports] (
    [SenderID] int  NOT NULL,
    [ReceiverID] int  NOT NULL,
    [Subject] nvarchar(50)  NULL,
    [Body] nvarchar(50)  NULL,
    [SendDate] datetime  NULL
);
GO

-- Creating table 'PassResets'
CREATE TABLE [dbo].[PassResets] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NULL,
    [EMail] nvarchar(50)  NULL,
    [AutID] nvarchar(300)  NULL,
    [isAvaliable] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Equipments'
ALTER TABLE [dbo].[Equipments]
ADD CONSTRAINT [PK_Equipments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [PK_Trainers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [SenderID], [ReceiverID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [PK_Reports]
    PRIMARY KEY CLUSTERED ([SenderID], [ReceiverID] ASC);
GO

-- Creating primary key on [ID] in table 'PassResets'
ALTER TABLE [dbo].[PassResets]
ADD CONSTRAINT [PK_PassResets]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TrainerID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_Customer_Trainer]
    FOREIGN KEY ([TrainerID])
    REFERENCES [dbo].[Trainers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Customer_Trainer'
CREATE INDEX [IX_FK_Customer_Trainer]
ON [dbo].[Customers]
    ([TrainerID]);
GO

-- Creating foreign key on [SenderID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_Report_User]
    FOREIGN KEY ([SenderID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReceiverID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_Report_User1]
    FOREIGN KEY ([ReceiverID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Report_User1'
CREATE INDEX [IX_FK_Report_User1]
ON [dbo].[Reports]
    ([ReceiverID]);
GO

-- Creating foreign key on [UserID] in table 'PassResets'
ALTER TABLE [dbo].[PassResets]
ADD CONSTRAINT [FK_PassReset_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PassReset_User'
CREATE INDEX [IX_FK_PassReset_User]
ON [dbo].[PassResets]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'PassResets'
ALTER TABLE [dbo].[PassResets]
ADD CONSTRAINT [FK_PassReset_User1]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PassReset_User1'
CREATE INDEX [IX_FK_PassReset_User1]
ON [dbo].[PassResets]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
-- --------------------------------------------------
use master;
go

drop database if exists TS;
go

create database TS;
go

use TS;
go

create table [Priority]
(
	[Id] int identity,
	[Name] nvarchar(20),
	[CreatedDate] datetime2 default getdate(),
	[EditedDate] datetime2 default null
	constraint PK_PriorityId primary key ([Id])
);
go

INSERT INTO [Priority] ([Name])
VALUES (N'Urgent'), (N'High'), (N'Medium'), (N'Low');
go

create table Severity
(
	[Id] int identity,
	[Name] nvarchar(20),
	[CreatedDate] datetime2 default getdate(),
	[EditedDate] datetime2 default null
	constraint PK_SeverityId primary key ([Id])
);
go

INSERT INTO [Severity] ([Name])
VALUES (N'Critical'), (N'Error'), (N'Warning'), (N'Normal');
go

create table TicketType
(
	[Id] int identity,
	[Name] nvarchar(50),
	[CreatedDate] datetime2 default getdate(),
	[EditedDate] datetime2 default null
	constraint PK_TicketTypeId primary key ([Id])
);
go

INSERT INTO [TicketType] ([Name])
VALUES (N'Bug'), (N'Feature Request'), (N'Test Case');
go

create table [Status]
(
	[Id] int identity,
	[Name] nvarchar(20),
	[CreatedDate] datetime2 default getdate(),
	[EditedDate] datetime2
	constraint PK_StatusId primary key ([Id])
);
go

INSERT INTO [Status] ([Name])
VALUES (N'Work in progress'), (N'Resolved'), (N'Cancelled');
go

create table [Role]
(
	[Id] int identity,
	[Name] nvarchar(50),
	[CreatedDate] datetime2 default getdate(),
	[EditedDate] datetime2
	constraint PK_RoleId primary key ([Id])
);
go

insert into [Role] ([Name])
values (N'QA'), (N'RD'), (N'PM'), (N'ADMIN');
go

create table [Account]
(
	[Id] int identity,
	[Name] nvarchar(50),
	[UserName] nvarchar(50),
	[Password] nvarchar(50),
	[RoleId] int,
	[CreatedDate] datetime2 default getdate(),
	[EditedDate] datetime2
	constraint PK_AccountId primary key ([Id]),
	constraint Unique_UserName Unique ([UserName]),
	constraint FK_AccountRoleId foreign key ([RoleId]) references [Role] ([Id])
);
go

insert into [Account] ([Name], [UserName], [Password], [RoleId])
values (N'MichaelQA', N'UserMichael', N'HQ/PPUGFGgk=', 1), (N'DianaRD', N'UserDiana', N'HQ/PPUGFGgk=', 2), (N'BrianPM', N'UserBrian', N'HQ/PPUGFGgk=', 3), (N'CatherineAdmin', N'UserCatherine', N'HQ/PPUGFGgk=', 4);
go

create table Ticket
(
	[Id] int identity,
	[TicketTypeId] int,
	[PriorityId] int,
	[SeverityId] int,
	[Description] nvarchar(max),
	[Summary] nvarchar(max),
	[StatusId] int,
	[CreatedDate] datetime2 default getdate(),
	[EditedDate] datetime2 default null,
	[CreatedBy] int,
	[EditedBy] int
	constraint PK_TicketId primary key ([Id]),
	constraint FK_TicketTypeId foreign key ([TicketTypeId]) references [TicketType] ([Id]),
	constraint FK_TicketPriorityId foreign key ([PriorityId]) references [Priority] ([Id]),
	constraint FK_TicketSeverityId foreign key ([SeverityId]) references [Severity] ([Id]),
	constraint FK_TicketStatusId foreign key ([StatusId]) references [Status] ([Id]),
	constraint FK_TicketCreatedBy foreign key ([CreatedBy]) references [Account] ([Id]),
	constraint FK_TicketEditedBy foreign key ([EditedBy]) references [Account] ([Id])
);
go

insert into Ticket ([CreatedBy], TicketTypeId, PriorityId, SeverityId, Description, Summary, StatusId)
values 
	(1, 1, 1, 1, N'Description1.',  N'Summary1.',  1),
	(1, 1, 1, 2, N'Description2.',  N'Summary2.',  2),
	(1, 1, 2, 2, N'Description3.',  N'Summary3.',  3),
	(1, 1, 2, 4, N'Description4.',  N'Summary4.',  1),
	(1, 1, 3, 1, N'Description5.',  N'Summary5.',  2),
	(1, 1, 3, 2, N'Description6.',  N'Summary6.',  3),
	(1, 1, 4, 3, N'Description7.',  N'Summary7.',  1),
	(1, 1, 4, 4, N'Description8.',  N'Summary8.',  2),
	(1, 3, 1, 1, N'Description9.',  N'Summary9.',  1),
	(1, 3, 1, 2, N'Description10.', N'Summary10.', 2),
	(1, 3, 2, 2, N'Description11.', N'Summary11.', 3),
	(1, 3, 2, 4, N'Description12.', N'Summary12.', 1),
	(1, 3, 3, 1, N'Description13.', N'Summary13.', 2),
	(1, 3, 3, 2, N'Description14.', N'Summary14.', 3),
	(1, 3, 4, 3, N'Description15.', N'Summary15.', 1),
	(1, 3, 4, 4, N'Description16.', N'Summary16.', 2),
	(3, 2, 1, 1, N'Description17.', N'Summary17.', 1),
	(3, 2, 1, 2, N'Description18.', N'Summary18.', 2),
	(3, 2, 2, 2, N'Description19.', N'Summary19.', 3),
	(3, 2, 2, 4, N'Description20.', N'Summary20.', 1),
	(3, 2, 3, 1, N'Description21.', N'Summary21.', 2),
	(3, 2, 3, 2, N'Description22.', N'Summary22.', 3),
	(3, 2, 4, 3, N'Description23.', N'Summary23.', 1),
	(3, 2, 4, 4, N'Description24.', N'Summary24.', 2);
go

SELECT
	t.[Id],
	t.[Description],
	t.[Summary],
	t.[CreatedDate],
	t.[EditedDate],
	t.[TicketTypeId],
	tp.[Name] AS TicketTypeName,
	t.[PriorityId],
	p.[Name] AS PriorityName,
	t.[SeverityId],
	sy.[Name] AS SeverityName,
	t.[StatusId],
	ss.[Name] AS StatusName,
	t.[CreatedBy],
	ca.[Name] AS CreaterName,
	cr.[Name] AS CreaterRoleName,
	t.[EditedBy],
	ea.[Name] AS EditerName,
	er.[Name] AS EditerRoleName
FROM Ticket t
INNER JOIN [TicketType] tp ON t.[TicketTypeId] = tp.Id
INNER JOIN [Priority] p ON t.PriorityId = p.Id
INNER JOIN [Severity] sy ON t.SeverityId = sy.Id
INNER JOIN [Status] ss ON t.StatusId = ss.Id
LEFT JOIN [Account] ca on t.[CreatedBy] = ca.[Id]
LEFT JOIN [Role] cr on ca.[RoleId] = cr.[Id]
LEFT JOIN [Account] ea on t.[EditedBy] = ea.[Id]
LEFT JOIN [Role] er on ea.[RoleId] = er.[Id]
ORDER BY t.Id;
go
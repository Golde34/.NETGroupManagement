
USE master
GO
alter database [Lillab] set single_user with rollback immediate
if exists (select * from sysdatabases where name='Lillab') drop database Lillab
GO

CREATE DATABASE Lillab
GO

USE Lillab
GO

CREATE TABLE [Users] (
	userID int NOT NULL identity(1,1) PRIMARY KEY,
	username nvarchar(255) NOT NULL,
    [password] nvarchar(255) NOT NULL,
	 fullname  nvarchar(255) not null,	
	email nvarchar(255),
	profileimage  nvarchar(255),	
	[status] bit,
)
Create table [Roles](
roleId int identity(1,1) primary key,
roleName nvarchar(255) not null,
[status] bit
);
Create table [Groups](
groupId int identity(1,1) Primary Key,
groupName nvarchar(255),
[description] nvarchar(255),
[status] bit
);
CREATE TABLE Menbers(
	userID int NOT NULL ,
	groupId int not null,
	roleId int,
	[status] bit,
	foreign key(userID) references [Users](userID),
	foreign key(groupId) references [Groups](groupId),
	foreign key(roleId) references [Roles](roleId)
);
Create table Projects(
projectId int identity(1,1) primary key,
projectName nvarchar(255),
description nvarchar(255),
groupId int,
[status] bit,
foreign key(groupId) references [Groups](groupId)
);
Create table Issues(
issueId int identity(1,1) primary key,
title varchar(255) not null,
dueDate datetime,
startDate datetime,
[description] nvarchar(255),
content varchar(255),
[state] int,
creator int,
projectId int,
foreign key(projectId) references [Projects](projectId)
);
create table MemberIssues(
userId int,
issueId int,
foreign key(userId) references [Users](userId),
foreign key(issueId) references [Issues](issueId)
);
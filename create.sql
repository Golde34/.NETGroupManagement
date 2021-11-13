
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
	dob datetime,
	gender int,
	bio  nvarchar(255),	
	isAdmin bit,
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
purpose nvarchar(255),
[state] int,
[status] bit
);
CREATE TABLE Members(
	userID int NOT NULL ,
	groupId int not null,
	roleId int,
	[state] int,
	[status] bit,
	foreign key(userID) references [Users](userID),
	foreign key(groupId) references [Groups](groupId),
	foreign key(roleId) references [Roles](roleId)
);
Create table Projects(
projectId int identity(1,1) primary key,
projectName nvarchar(255),
description nvarchar(255),
createdate datetime,
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
assignee int,
projectId int,
[status] bit,
foreign key(projectId) references [Projects](projectId),
foreign key(assignee) references [Users](userID)
);
create table MemberIssues(
userId int,
issueId int,
[status] bit,
foreign key(userId) references [Users](userId),
foreign key(issueId) references [Issues](issueId)
);
create table Invitation(
userId int,
groupId int,
[status] bit,
foreign key(userId) references [Users](userId),
foreign key(groupId) references [Groups](groupId)
);

INSERT INTO Users(username,password,fullname,email,dob,gender,bio,isAdmin,status) VALUES ('nam','123','Hai Nam', 'namnhh@gmail.com','',1,'I am lord','1','1')
INSERT INTO Users(username,password,fullname,email,dob,gender,bio,isAdmin,status) VALUES ('viet','123','Dong Viet', 'vietnddm@gmail.com','',1,'I am lord','1','1')
INSERT INTO Users(username,password,fullname,email,dob,gender,bio,isAdmin,status) VALUES ('hieu','123','Trung Hieu', 'hieultm@gmail.com','',1,'I am lord','1','1')
INSERT INTO Users(username,password,fullname,email,dob,gender,bio,isAdmin,status) VALUES ('thinh','123','Duc Thinh', 'thinhvd@gmail.com','',1,'I am lord','1','1')
INSERT INTO Users(username,password,fullname,email,dob,gender,bio,isAdmin,status) VALUES ('quang','123','Minh Quang', 'quangkm@gmail.com','',1,'I am lord','1','1')
INSERT INTO Users(username,password,fullname,email,dob,gender,bio,isAdmin,status) VALUES ('user1','1','User Test', 'test@gmail.com','',1,'Testing','0','1')


INSERT INTO Roles(roleName,status) VALUES ('Project Manager', 1)
INSERT INTO Roles(roleName,status) VALUES ('Leader', 1)
INSERT INTO Roles(roleName,status) VALUES ('Developer', 1)
INSERT INTO Roles(roleName,status) VALUES ('Tester', 1)
INSERT INTO Roles(roleName,status) VALUES ('BA', 1)
INSERT INTO Roles(roleName,status) VALUES ('QA', 1)
INSERT INTO Roles(roleName,status) VALUES ('Client', 1)


INSERT INTO Groups(groupName,description,purpose,state,status) VALUES ( 'Bmazon', 'Team Bmazon', 'Develop project with team Bmazon', 1, 1)
INSERT INTO Groups(groupName,description,purpose,state,status) VALUES ( 'LilLab', 'Team LilLab', 'Develop project with team LilLab', 1, 1)


INSERT INTO Members(userID,groupId,roleId,state,status) VALUES (1,1,1,1,1)
INSERT INTO Members(userID,groupId,roleId,state,status) VALUES (2,1,2,1,1)
INSERT INTO Members(userID,groupId,roleId,state,status) VALUES (3,2,1,1,1)
INSERT INTO Members(userID,groupId,roleId,state,status) VALUES (4,1,3,1,1)
INSERT INTO Members(userID,groupId,roleId,state,status) VALUES (5,2,2,1,1)



INSERT INTO Projects(projectName,description,createdate,groupId,status) VALUES ('Ecommerce','An ecommerce website for technology buyer and seller',GETDATE(),1,1)
INSERT INTO Projects(projectName,description,createdate,groupId,status) VALUES ('B-Ecommerce','An backup ecommerce website for technology buyer and seller',GETDATE(),1,1)
INSERT INTO Projects(projectName,description,createdate,groupId,status) VALUES ('LilLab','An LilLab website for group management',GETDATE(),1,2)
INSERT INTO Projects(projectName,description,createdate,groupId,status) VALUES ('B-LilLab','An backup LilLab website for group management',GETDATE(),1,2)



INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 1',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is an error about NullPointerException at line 1311 at Controller x', 'Should be fixed in 7 days after release issues',1,1,2,1,1)
INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 2',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is 2 errors about ArithmethicException at line 2001 at Controller y', 'Should be fixed in 7 days after release issues',1,1,2,1,1)
INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 1',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is an error about NullPointerException at line 1311 at Controller x', 'Should be fixed in 7 days after release issues',1,1,2,1,2)
INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 2',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is 2 errors about ArithmethicException at line 2001 at Controller y', 'Should be fixed in 7 days after release issues',1,1,2,1,2)
INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 1',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is an error about NullPointerException at line 1311 at Controller x', 'Should be fixed in 7 days after release issues',1,1,2,1,3)
INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 2',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is 2 errors about ArithmethicException at line 2001 at Controller y', 'Should be fixed in 7 days after release issues',1,1,2,1,3)
INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 1',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is an error about NullPointerException at line 1311 at Controller x', 'Should be fixed in 7 days after release issues',1,1,2,1,4)
INSERT INTO Issues(title,dueDate,startDate,description,content,state,creator,assignee,projectId,status) VALUES ('Exception 2',GETDATE(), DateAdd(DD,-7,GETDATE() ),'There is 2 errors about ArithmethicException at line 2001 at Controller y', 'Should be fixed in 7 days after release issues',1,1,2,1,4)





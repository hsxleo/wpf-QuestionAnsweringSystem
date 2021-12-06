--创建数据库QAS
create database QAS 
on primary (     
name = QAS,  
filename = 'E:\SQLServer\Data\QAS.mdf', 
size = 100,  
maxsize = unlimited, 
filegrowth = 10%)
log on(
name = QAS_log,
filename = 'E:\SQLServer\Data\QAS_log.ldf',
size = 100,  
maxsize = unlimited, 
filegrowth = 10%)

--创建用户表
create table users(
account nvarchar(30) not null,  
password nvarchar(30) not null,
name  nvarchar(30) not null,
id nvarchar(30) not null unique,
gender nvarchar(5) not null,
mail nvarchar(30),
specialty nvarchar(30) not null,
role nvarchar(30) not null,
headimage nvarchar(100),
primary key(account,role))


--创建问题表
create table question(
qid int primary key identity,
id nvarchar(30) foreign key references users(id),   
qcontent nvarchar(1000) not null)

--创建回答表
create table answering(
aid int primary key identity,
id nvarchar(30) foreign key references users(id), 
acontent nvarchar(1000) not null,
qid int not null)

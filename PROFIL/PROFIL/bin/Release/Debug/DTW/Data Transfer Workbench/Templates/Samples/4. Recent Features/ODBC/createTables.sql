DROP DATABASE OriginalDB
GO

create database OriginalDB
Go
create table OriginalDB.dbo.OCRD (CardCode Char(50) Not Null,CardName Char(50),DebitorAccount Char(50))
Go
create table OriginalDB.dbo.CRD1 (CardCode Char(50) Not Null,lineNum Int,AddressName Char(50),City Char(50),Country Char(50),Street Char(50))
Go

create table OriginalDB.dbo.OACT (FatherAccountKey Char(50) Not Null,FormatCode Char(50) Not Null,Name Char(50) NUll)
Go

insert into OriginalDB.dbo.OCRD values ('Tech1','Tech Ltd1','_SYS00000000010')
insert into OriginalDB.dbo.OCRD values ('Tech2','Tech Ltd2','_SYS00000000010')
insert into OriginalDB.dbo.OCRD values ('Tech3','Tech Ltd3','_SYS00000000010')

insert into OriginalDB.dbo.CRD1 values ('Tech1',0,'Office1','Paries1','FR','CHANNEL') 
insert into OriginalDB.dbo.CRD1 values ('Tech2',0,'Office2','Paries2','FR','CHANNEL') 
insert into OriginalDB.dbo.CRD1 values ('Tech3',0,'Office3','Paries3','FR','CHANNEL') 
insert into OriginalDB.dbo.CRD1 values ('Tech3',1,'Office3plus','Paries3','FR','CHANNEL') 
Go

insert into OriginalDB.dbo.OACT values ('Receivables','198000000100101','COAODBC1')
insert into OriginalDB.dbo.OACT values ('Receivables','197000000100101','COAODBC2')
insert into OriginalDB.dbo.OACT values ('Receivables','196000000100101','COAODBC3')

Go

create database CampFinderDb
go



create table People (
	Id uniqueidentifier primary key not null,
	FirstName nvarchar(max),
	LastName nvarchar(max),
	MailAdress nvarchar(max),
	TelephoneNumber nvarchar(max)
)

create table Places (
	Id uniqueidentifier primary key not null,
	HouseNumber int,
	Street nvarchar(max),
	City nvarchar(max),
	PostNumber nvarchar(max),
	Province nvarchar(max),
	Country nvarchar(max)
)

create table CampPlaces (
	Id uniqueidentifier primary key not null,
	Name nvarchar(max),
	Website nvarchar(max),
	AmountPersons int,
	Forest bit,
	Area float,
	Dormitories int,
	KitchenGear bit,
	Beds bit,
	DaySpaces int,
	Water bit,
	Electricity bit,
	Toilets bit,
	Discriminator bit,
	Person_Id uniqueidentifier foreign key references People(Id),
	Place_Id uniqueidentifier foreign key references Places(Id)
)

create table Reviews (
	Id uniqueidentifier primary key not null,
	Score float,
	Note nvarchar(max),
	Date datetime not null,
	PersonId uniqueidentifier foreign key references People(Id),
	CampPlace_Id uniqueidentifier foreign key references CampPlaces(Id)
)
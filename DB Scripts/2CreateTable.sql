drop table if exists PersonData cascade;
create table PersonData (
	ID serial primary key,
	Forename text not null,
	Surname text not null,
	Title text null,
	Suffix text null,
	BirthDate date null
);

drop table if exists CompanyData cascade;
create table CompanyData (
	ID serial primary key,
	Name text not null
);

drop table if exists Contact cascade;
create table Contact (
	ID serial primary key,
	fk_CompanyData integer references CompanyData(ID) null,
	fk_PersonData integer references PersonData(ID) null
);

drop table if exists Address cascade;
create table Address (
	ID serial primary key,
	Street text not null,
	StreetNumber text not null,
	PostalCode integer not null,
	City text not null
);

drop table if exists AddressType cascade;
create table AddressType (
	ID serial primary key,
	Type text not null, -- values are 'Main', 'Delivery', 'Billing'
	fk_Contact integer references Contact(ID) not null,
	fk_Address integer references Address(ID) not null
);

drop table if exists InvoiceType cascade;
create table InvoiceType (
	ID serial primary key,
	Type text not null -- either 'Sales' or 'Purchase'
);

drop table if exists ValueAddedTax cascade;
create table ValueAddedTax (
	ID serial primary key,
	Type text not null,
	Percent float not null
);

drop table if exists Invoice cascade;
create table Invoice (
	ID serial primary key,
	Date date not null,
	MaturityDate date not null,
	Comment text null,
	Message text null,
	fk_Contact integer references Contact(ID) not null,
	fk_InvoiceType integer references InvoiceType(ID) not null
);

drop table if exists InvoiceItem cascade;
create table InvoiceItem (
	ID serial primary key,
	UnitPrice float not null,
	Quantity integer not null,
	Amount float null,
	AmountNet float null,
	fk_TaxType integer references ValueAddedTax(ID) not null
);

drop table if exists InvoicePosition cascade;
create table InvoicePosition (
	ID serial primary key,
	fk_Invoice integer references Invoice(ID) not null,
	fk_IvoiceItem integer references InvoiceItem(ID) not null
);

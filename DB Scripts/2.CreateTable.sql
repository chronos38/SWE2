/*
drop table if exists PersonData cascade;
create table PersonData (
	ID serial primary key,
	Forename text null,
	Surname text null,
	Title text null,
	Suffix text null,
	BirthDate date null
);

drop table if exists CompanyData cascade;
create table CompanyData (
	ID serial primary key,
	Name text null
);
*/

drop table if exists Address cascade;
create table Address (
	ID serial primary key,
	Street text null,
	StreetNumber integer null,
	PostalCode integer null,
	City text null
);

drop table if exists Contact cascade;
create table Contact (
	ID serial primary key,
	UID text null,
	Name text null,
	Forename text null,
	Surname text null,
	Title text null,
	Suffix text null,
	BirthDate date null,
	fk_Address integer references Address(ID) null
);

drop table if exists AdditionalAddress cascade;
create table AdditionalAddress (
	ID serial primary key,
	Type text null, -- either 'Delivery' or 'Billing'
	fk_Contact integer references Contact(ID) null,
	fk_Address integer references Address(ID) null
);

drop table if exists InvoiceType cascade;
create table InvoiceType (
	ID serial primary key,
	Type text null -- either 'Sales' or 'Purchase'
);

drop table if exists ValueAddedTax cascade;
create table ValueAddedTax (
	ID serial primary key,
	Type text null,
	Percent float null
);

drop table if exists Invoice cascade;
create table Invoice (
	ID serial primary key,
	Date date null,
	MaturityDate date null,
	Comment text null,
	Message text null,
	fk_Contact integer references Contact(ID) null,
	fk_InvoiceType integer references InvoiceType(ID) null
);

drop table if exists InvoiceItem cascade;
create table InvoiceItem (
	ID serial primary key,
	UnitPrice float null,
	Quantity integer null,
	--Amount float null,
	--AmountNet float null,
	fk_TaxType integer references ValueAddedTax(ID) null
);

drop table if exists InvoicePosition cascade;
create table InvoicePosition (
	ID serial primary key,
	fk_Invoice integer references Invoice(ID) null,
	fk_InvoiceItem integer references InvoiceItem(ID) null
);

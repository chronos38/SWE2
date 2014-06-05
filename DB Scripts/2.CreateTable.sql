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
	Street text null,
	StreetNumber text null,
	PostalCode text null,
	City text null,
	Company integer references Contact(ID) null
);

drop table if exists Invoice cascade;
create table Invoice (
	ID serial primary key,
	Date date null,
	Maturity date null,
	Comment text null,
	Message text null,
	Type text null,
	ReadOnly bool default false,
	fk_Contact integer references Contact(ID) null
);

drop table if exists InvoiceItem cascade;
create table InvoiceItem (
	ID serial primary key,
	Name text null,
	UnitPrice float null,
	Quantity integer null,
	VAT float null
);

drop table if exists InvoicePosition cascade;
create table InvoicePosition (
	ID serial primary key,
	fk_Invoice integer references Invoice(ID) null,
	fk_InvoiceItem integer references InvoiceItem(ID) null
);

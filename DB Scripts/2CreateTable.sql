create table Address (
	ID serial primary key,
	Street text not null,
	StreetNumber text not null,
	PostalCode integer not null,
	City text not null
);

create table Contact (
	ID serial primary key,
	Name text,
	Title text null,
	Forename text null,
	Surname text null,
	Suffix text null,
	BirthDate text null,
	Address serial reference Address (ID),
	BillingAddress serial reference Address(ID),
	DeliveryAddress serial reference Address(ID)
);
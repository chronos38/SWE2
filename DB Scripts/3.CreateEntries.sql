-- person
insert into Contact (Forename, Surname, BirthDate, Street, StreetNumber, PostalCode, City) values ('Max', 'Mustermann', '1988-08-01', 'Musterstrasse', 1, 54321, 'Musterstadt');

-- contact
insert into Contact (UID, Name, Street, StreetNumber, PostalCode, City) values ('Dreamer', 'Dreamland', 'Dreamstreet', 2, 12345, 'Dreamcity');

-- invoice
insert into Invoice (Date, Maturity, Type, fk_Contact) values (now(), current_date + 14, 'sale', 2);
insert into Invoice (Date, Maturity, Type, fk_Contact) values (now(), current_date + 14, 'purchase', 1);

-- invoice items
insert into InvoiceItem (UnitPrice, Quantity, VAT) values (123.90, 2, 20);
insert into InvoiceItem (UnitPrice, Quantity, VAT) values (35.50, 5, 20);

-- invoice position
insert into InvoicePosition (fk_Invoice, fk_InvoiceItem) values (1, 1);
insert into InvoicePosition (fk_Invoice, fk_InvoiceItem) values (2, 2);

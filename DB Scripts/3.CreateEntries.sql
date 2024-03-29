﻿-- person
insert into Contact (Forename, Surname, BirthDate, Street, StreetNumber, PostalCode, City) values ('Max', 'Mustermann', '1988-08-01', 'Musterstrasse', 1, 54321, 'Musterstadt');

-- contact
insert into Contact (UID, Name, Street, StreetNumber, PostalCode, City) values ('Dreamer', 'Dreamland Inc', 'Dreamstreet', 2, 12345, 'Dreamcity');
insert into Contact (UID, Name, Street, StreetNumber, PostalCode, City) values ('Dreamer', 'Dreamworld Ltd', 'Dreamstreet', 45, 12345, 'Dreamcity');
insert into Contact (UID, Name, Street, StreetNumber, PostalCode, City) values ('Hater', 'Hate Machine Corporation', 'Dreamstreet', 6, 666, 'Hatecity');
insert into Contact (UID, Name, Street, StreetNumber, PostalCode, City) values ('Hater', 'Hell Ltd', 'Dreamstreet', 9, 999, 'Hellcity');

-- invoice
insert into Invoice (Date, Maturity, Type, fk_Contact) values (now(), current_date + 14, 'Incoming', 2);
insert into Invoice (Date, Maturity, Type, fk_Contact) values (now(), current_date + 14, 'Outgoing', 1);

-- invoice items
insert into InvoiceItem (Name, UnitPrice, Quantity, VAT) values ('Döner', 123.90, 2, 20);
insert into InvoiceItem (Name, UnitPrice, Quantity, VAT) values ('Kebab', 35.50, 5, 20);

-- invoice position
insert into InvoicePosition (fk_Invoice, fk_InvoiceItem) values (1, 1);
insert into InvoicePosition (fk_Invoice, fk_InvoiceItem) values (2, 2);

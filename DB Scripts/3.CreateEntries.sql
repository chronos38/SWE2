﻿-- person
insert into Address (Street, StreetNumber, PostalCode, City) values ('Musterstrasse', 1, 54321, 'Musterstadt');
insert into Contact (Forename, Surname, BirthDate, fk_Address) values ('Max', 'Mustermann', '1988-08-01', 1);

-- contact
insert into Address (Street, StreetNumber, PostalCode, City) values ('Dreamstreet', 2, 12345, 'Dreamcity');
insert into Contact (Name, fk_Address) values ('Dreamland', 2);

-- invoice type
insert into InvoiceType (Type) values ('Sale');
insert into InvoiceType (Type) values ('Purchase');

-- VAT
insert into ValueAddedTax (Type, Percent) values ('Product', 20.0);
insert into ValueAddedTax (Type, Percent) values ('Income', 20.0);
insert into ValueAddedTax (Type, Percent) values ('Sale', 20.0);

-- invoice
insert into Invoice (Date, MaturityDate, fk_Contact, fk_InvoiceType) values (now(), current_date + 14, 2, 1);
insert into Invoice (Date, MaturityDate, fk_Contact, fk_InvoiceType) values (now(), current_date + 14, 1, 2);

-- invoice items
insert into InvoiceItem (UnitPrice, Quantity, fk_TaxType) values (123.90, 2, 3);
insert into InvoiceItem (UnitPrice, Quantity, fk_TaxType) values (35.50, 5, 3);

-- invoice position
insert into InvoicePosition (fk_Invoice, fk_InvoiceItem) values (1, 1);
insert into InvoicePosition (fk_Invoice, fk_InvoiceItem) values (2, 2);

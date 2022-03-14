
USE CEPAMMCDMX
GO
/*
	Modified the table recibos
	Added a new column in order to restrict the reimpresion of tickets that actually were impressed before
*/
ALTER TABLE Recibos ADD printed int default 0;

/*
UPDATE Recibos set printed = 0;
*/

/*

NEW ADMIN PASSWORD = ADMIN987123

*/
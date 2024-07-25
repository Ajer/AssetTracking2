## AssetTracking2
AssetTracking2 is a c# console-program connected with an SQL Server DB that can Create, read , update and delete records of Assets in the form of Computers or Phones.

Each asset has details of costs , type , purchase_date and an office-location from 3 possible places: USA, Sweden or Spain and the project was created using Entity Framework Core where the Office-table is connected in a 'one-to-many'-relationship to table 'Assets'. 'Assets' is also configured as a parent Database-table to tables 'Computers' and 'Phones' 

Use the .bacpac-file in SQLDatabase_Export-folder to import the DB to SQL Server Management Studio.
### https://github.com/Ajer

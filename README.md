# _Salon Shop_

#### _Website for Darrion's Salon, 05/10/2019_

#### By _**Darrion Gering**_

## Description


## Setup/Installation Requirements

1. _Clone from GitHub_
2. _$cd HairSalon.Solution/HairSalon_
3. _$dotnet restore to install packages from the .csproj file_
4. _$dotnet run_
5. _Navigate to http://localhost:5000/ in an internet browser_

## Specs

| Behavior | Input | Output |
| ------------- |:-------------:| -----:|
| 1. The owner can add new stylists | "MC Styles" | <li>"MC Styles"</li> |
| 2. Employees can add new clients for a given stylist | "DJ Client" | <li>"DJ Client"</li> |
| 3. Employees can click a stylist and see details and a list of clients | Click | <p>details</p> <li>"Hair Beats"</li> <li>"Curly Qs"</li> |
| 4. Clients can't be added if no stylists have been added | navigate to "/home/stylists/stylistID/clients/new" | "No stylists" |
| 5. Employees can delete stylists | click delete | Stylist has been removed from the list |
| 6. Employees can delete clients | click delete | Client has been removed from the list |
| 7. Employees can edit stylists name| click edit name | "New name"|
| 7. Employees can edit a clients name and stylist id| click edit id | Client now appears on a different stylist list|
| 8. Employees can add a specialty to a stylist | "Afroman" | Specialty - Afros |
| 9. Employees can add new specialties to the list | color | color is now on the list of specialties|
| 10. Employyes can remove a specialty | color | color is no longer on the list of specialties |

#Database Config
Option 1: Use the darrion_gering.sql file

Option 2: Follow these steps.
1. CREATE DATABASE darrion_gering;
2. USE darrion_gering;
3. CREATE TABLE stylists;
4. ALTER TABLE `stylists` ADD `id` INT NOT NULL AUTO_INCREMENT AFTER `name`, ADD PRIMARY KEY (`id`);
5. ALTER TABLE `stylists` ADD `name` VARCHAR(255) NOT NULL AFTER `name`;
6. ALTER TABLE `stylists` ADD `description` VARCHAR(255) NULL DEFAULT NULL AFTER `name`;
7. ALTER TABLE `stylists` ADD `hire_date` DATETIME NULL DEFAULT NULL AFTER `description`;
8. CREATE TABLE clients(id serial PRIMARY KEY, name VARCHAR(255);)
9. ALTER TABLE `clients` ADD `stylist_id` INT;
10. CREATE TABLE specialties(id serial PRIMARY KEY, specialty_type VARCHAR(255);)
11. 10. CREATE TABLE stylists_specialties(id serial PRIMARY KEY, stylist_id INT, specialty_id INT;)

## Support and contact details

darrionkg@gmail.com

## Technologies Used

* C#
* MySql
* html
* css

### License

*MIT License*

Copyright (c) 2019 **_Darrion Gering_**

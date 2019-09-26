# UDTList
## Introduction:
This Project contains a User Defined List Type, which can hold a list of other UDT. This type has functions 
to perform delete, retrieve , add Operations and to get Entire data of UDT as a string/XML.

## Installation
This package contains a core assembly UDTList.dll which can be included and used directly from a
.Net application or can be registered and used from within SQL Server.

### To Register:
1. Note the path to UDTList.dll file which is inside bin>debug.
2. Edit the CREATE ASSEMBLY COMMAND to insert the path to UDTList.dll.
3. Execute CREATE ASSEMBLY COMMAND to register the DLL to Your Database.
4. Now Execute CREATE TYPE Command to Create the type in you database.
### To Unregister:
Execute DROP TYPE command first and DROP ASSEMBLY command to Unregister.

## Features:
This UDTList Type have 6 functions and 4 attributes to make use of.

### Some useful functions are:
## string ToXML()
To get the Data Type data in xml format.
## string ToString()
To get the Data Type list data in string format.
## AddItem()
To add item to the list.
## RemoveItem()
To remove an item from the list

## Usage:
1. First register and create type.
2. Declate a variable of Type UDTList.
3. Execute the Null() or Parse() function to initialize the datatype;
4. Perform the above functions according to the need.

### Cheers :)

# cw2

Trail App API Program
=============================

Introduction: This API will allow you to add a user and specify the trail they completed along with thier completion time and completion status.


API Connectors Included in the GitHub Repository:

/api/Users/login
As there is JWT ecryption enabled for this API, you will need to retrive an encryption key from this site first before being able to utilize any of the CRUD functions within POSTMAN or Insomnia.

Once you have the JWT Token you will need to do one of the following two for this API to work:

1. In POSTMAN or Insomnia, add a header with the following values; Authorization: Bearer <JWT Token Code>

2. In POSTMAN or Insomnia there is a tab for Authorization, you will select this and proceed to select Bearer Token as the mode of authentication. From here you will enter the JWT Token in the Token field.


For testing purposes this API comes equipped with a testing account for authentication purposes, this needs to be changed before this API is placed in production mode.

The fields have been modfied for a normal login, you will need to enter a username and a password to gain access to the JWT token.


/api/Users

Select this link to perform CRUD operations on all accounts.

Use the GET method in POSTMAN or Insomnia to get an overview of all accounts.

Use POST to add an account with the required parameters.

The required parameters for adding a user are:

{
  "firstName":
  "lastName":
  "username":
  "password":
}

Please note that all passwords are encrypted with BCrypt


/api/Users/{id}

Use this method to perform GET and PUT functions of specified accounts.

If you are wanting to pull a specified account the link would be /api/Users/1001 or any of the userID's that are within the database.

For a streamlined approach, userID numbers start with 1000 and increment by 1 for each user that is added.


/api/Users/{userId}/explorations

This will allow you view the following information about the user:

{
		"explorationID": "",
		"firstName": "",
		"lastName": "",
		"email": "",
		"trailID": "",
		"trailName": "",
		"trailLocation": "",
		"completionDate": "",
		"completionStatus": 
}


Security Protocols:

All useer passwords are encrypted with one way hashing using BCrypt.

Modern Authentication using JWT is eabled which will require the use of a JWT token to access the API functions.





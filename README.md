# Hiring Coding Test App(Online Test)
## Introduction
This project is built on the .net core 3.1 webapi template and uses PostgresSql Server for persisting data. It uses the Clean Architecture with Command and Query Responsibility Segregation(CQRS) Pattern which is further backed by using seperate ORMs for reading and writing. EF Core is used for writing to the database while Dapper ORM is used for reading from the database. The Project uses the latest functionalities and coding patterns of the .net core and uses MediatR, FulentAPi validations along with other leading packages to optimise the performance with less code and complexities. Microsoft Identity Framework is used for the User Access Controls.
## Project Details

Its an online test backend solution that can be used to create and manage online test with mutiple choice answers. It has admin control where admin can create Subjects(Exams) and Create a Test(Exam Group) with the available Subjects(Exams). Admin will further create questions for the selected subject with correct option for the answer. Admin can add as many questions as possible for any subject. Once Questions are created, admin can create a test schedule with available tests and specify the starting datetime and ending datetime along with number of questions and timer. Once a Schedule is created admin can send the details of the test to the user by creating a user account and sending user credentials and schedule details. The same can be achieved at the client level using a link sent to the user's email.

User can login the page and start the test at the prescribed time. once test is completed, Result can be displayed or mailed to the user.

Please keep it in mind that its a sample backend solution that can be further enhanced based on the requirements from the UI side development.

## Features
- Create an Admin and User
- Create and Manage Exam(Subject) with Description
- Create and Mange Exam Group(Test) with available subjects
- Create and Mange an Test Schedule
- Test page can be created based on the schedule details
- Result can be evaluated programatically and api can provide it to the client side

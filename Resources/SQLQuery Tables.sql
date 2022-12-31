CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[FirstName] VARCHAR(20) NOT NULL,
	[LastName] VARCHAR(20) NOT NULL,
	[JMBG] VARCHAR(20) NOT NULL,
	[Gender] VARCHAR(10) NOT NULL,
	[Address_ID] INT NOT NULL,	
	[Email] VARCHAR(20) NOT NULL,
	[Password] VARCHAR(20) NOT NULL,
	[Role] VARCHAR(15) NOT NULL,
	[Active] BIT NOT NULL,
	CONSTRAINT users_address_fk FOREIGN KEY (Address_ID) REFERENCES dbo.Address(ID)
)

CREATE TABLE [dbo].[Instructors]
(
	[Id] INT NOT NULL PRIMARY KEY,
	CONSTRAINT FK_User_Instructor_Id
	FOREIGN KEY(ID) REFERENCES dbo.Users(ID)
)

CREATE TABLE [dbo].[Administrators]
(
	[Id] INT NOT NULL PRIMARY KEY,
	CONSTRAINT FK_User_Administrator_Id
	FOREIGN KEY(ID) REFERENCES dbo.Users(ID)
)

CREATE TABLE [dbo].[Attendees]
(
	[Id] INT NOT NULL PRIMARY KEY,
	CONSTRAINT FK_User_Attendee_Id
	FOREIGN KEY(ID) REFERENCES dbo.Users(ID)
)

CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[AddressCode] VARCHAR(20) NOT NULL,
	[Street] VARCHAR(20) NOT NULL,
	[AddressNumber] VARCHAR(20) NOT NULL,
	[City] VARCHAR(20) NOT NULL,
	[Country] VARCHAR(20) NOT NULL,
	[Active] BIT NOT NULL
)

CREATE TABLE [dbo].[FitnessCentre]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[FitnessCentreCode] VARCHAR(20) NOT NULL,
	[CentreName] VARCHAR(20) NOT NULL,
	[Address_ID] INT NOT NULL,
	[Active] BIT NOT NULL,
	CONSTRAINT fitnesscentre_address_fk FOREIGN KEY (Address_ID) REFERENCES dbo.Address(ID)
)

CREATE TABLE [dbo].[Workouts]
(
	[id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[WorkoutCode] VARCHAR(20) NOT NULL,
	[WorkoutDate] DATE NOT NULL,
	[WorkoutStartTime] VARCHAR(15) NOT NULL,
	[WorkoutLength] VARCHAR(15) NOT NULL,
	[WorkoutStatus] VARCHAR(15) NOT NULL,
	[AppointedInstructor_ID] INT,
	[ReservedForAttendee_ID] INT,
	[Active] BIT NOT NULL,
	CONSTRAINT workouts_instructor_fk FOREIGN KEY (AppointedInstructor_ID) REFERENCES dbo.Instructors(ID),
	CONSTRAINT workouts_attendee_fk FOREIGN KEY (ReservedForAttendee_ID) REFERENCES dbo.Attendees(ID)
)
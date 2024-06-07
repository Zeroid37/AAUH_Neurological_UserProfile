--Normal tables

--zipCity
create table ZipCity (
	zip varchar(6) not null primary key,
	city varchar(20) not null
)

--Address
create table Address (
	id int IDENTITY(1,1) primary key,
	street varchar(16) not null,
	houseNo varchar(8) not null,
	zip_FK varchar(6) not null foreign key references ZipCity(zip),
)

--Person
create table Person(
	id int IDENTITY(1,1) primary key,
	firstName varchar(12) not null,
	lastName varchar(20) not null,
	addressId_FK int not null foreign key references Address(id),
	phone varchar(16) not null unique,	
	dateOfBirth dateTime not null,
	email varchar(30) not null unique
)

--Patient
create table Patient(
	patientNo int IDENTITY(381943, 1) primary key,
	email_FK varchar(30) not null unique foreign key references Person(email),
	cpr varchar(10) not null
)

--Medical Secretary
create table MedicalSecretary (
	employeeNo int IDENTITY(381943, 1) primary key,
	department varchar(50),
	email_FK varchar(30) not null unique foreign key references Person(email)
)

--Clinic Professional
create table ClinicProfessional(
	employeeNo int IDENTITY(381943, 1) primary key,
	profession varchar(20),
	email_FK varchar(30) not null unique foreign key references Person(email)
)

--Flag
create table Flag(
	id int IDENTITY(1, 1) primary key,
	flagName varchar(30) not null,
	flagDescription varchar(100) not null,
	flagRaised bit not null,
	flagAlertLevel varchar(1) not null
)

--Questionnaire
create table Questionnaire(
	id int IDENTITY(1,1) primary key,
	title varchar(50) not null,
)

--Question
create table Question(
	id int IDENTITY(1,1) primary key,
	questionDescription varchar(200) not null,
	questionnaireID_FK int not null foreign key references Questionnaire(id)
)

--Answer
create table Answer(
	id int IDENTITY(1, 1) primary key,
	answerText varchar(100) not null,
	isChosen bit not null,
	answerValue int not null,
	questionID_FK int not null foreign key references Question(id)
)

--Join Tables

--QuestionnaireFlag
create table QuestionnaireFlag(
	questionnaireID_FK int not null foreign key references Questionnaire(id),
	flagID_FK int not null foreign key references Flag(id),
	PRIMARY KEY(questionnaireID_FK, flagID_FK)
)

--PatientAnswer
create table PatientAnswer(
	patientNo_FK int not null foreign key references Patient(patientNo),
	answerID_FK int not null foreign key references Answer(id),
	answerUpdated DATE not null,
	PRIMARY KEY(patientNo_FK, answerID_FK)
)

--PatientFlag
create table PatientFlag(
	patientNo_FK int not null foreign key references Patient(patientNo),
	flagID_FK int not null foreign key references Flag(id),
	flagStage int not null,
	PRIMARY KEY(patientNo_FK, flagID_FK)
)

--PatientQuestionnaire
create table PatientQuestionnaire(
    patientNo_FK int not null foreign key references Patient(patientNo),
    questionnaireID_FK int not null foreign key references Questionnaire(id),
    PRIMARY KEY(patientNo_FK, questionnaireID_FK)
)
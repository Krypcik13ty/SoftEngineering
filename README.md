# SoftEngineering - Timetable App
>ASP.NET MVC C#

>.NET Framework 4.7.2

SQL Script:
```
create database scheduledb;
use scheduledb;
CREATE TABLE ACCOUNTS(
	Login varchar(25) NOT NULL,
	Password varchar(24) NOT NULL,
	Pesel BIGINT(11) NOT NULL UNIQUE,
	Typ varchar(24) NOT NULL,
	PRIMARY KEY (Login))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE ACCOUNTS_DICTIONARY(
	ID int NOT NULL AUTO_INCREMENT,
	Name varchar(25) NOT NULL,
	Description text NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE ADRESSES_DICTIONARY(
	ID int NOT NULL AUTO_INCREMENT,
	Country varchar(25) NOT NULL,
	City varchar(50) NOT NULL,
	Adress varchar(50) NOT NULL,
	Building varchar(25) NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE CALENDAR(
	IdDate date NOT NULL,
	IsHoliday bit NOT NULL,
	IsHolidayText varchar(25),
	PRIMARY KEY (IdDate))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE CLASS_AVAILABILITY(
	ID int NOT NULL AUTO_INCREMENT,
	ClassNR varchar(4) NOT NULL,
	Date date NOT NULL,
	TimeFrom time(6) NOT NULL,
	TimeTo time(6) NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE CLASSES(
	ClassNR varchar(4) NOT NULL,
	Type varchar(25) NOT NULL,
	Adress varchar(64) NOT NULL,
	PRIMARY KEY (ClassNR))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE GROUPS(
	GroupID int NOT NULL AUTO_INCREMENT,
	GroupName varchar(50) NOT NULL,
	PRIMARY KEY (GroupID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE HOURS_DICTIONARY(
	ID int NOT NULL AUTO_INCREMENT,
	Hour time(6) NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE LECTURER_AVAILABILITY(
	ID int NOT NULL AUTO_INCREMENT,
	Login varchar(25) NOT NULL,
	Date date NOT NULL,
	TimeFrom time(6) NOT NULL,
	TimeTo time(6) NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE LECTURERS(
	Login varchar(25) NOT NULL,
	Name varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL,
	Pesel BIGINT(11) NOT NULL UNIQUE,
	City varchar(50) NOT NULL,
	Adress varchar(50) NOT NULL,
	Building varchar(25) NOT NULL,
	Phone_number varchar(9) NOT NULL,
	PRIMARY KEY (Login))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE STUDENTS(
	Login varchar(25) NOT NULL,
	Name varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL,
	Pesel BIGINT(11) NOT NULL UNIQUE,
	City varchar(50) NOT NULL,
	Adress varchar(50) NOT NULL,
	Building varchar(25) NOT NULL,
	Phone_number varchar(9) NOT NULL,
	PRIMARY KEY (Login))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE OLDPASS(
	ID int NOT NULL AUTO_INCREMENT,
	Login varchar(25) NOT NULL,
	Pass varchar(24) NOT NULL,
	PassDate date NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE SCHEDULE(
	ID int NOT NULL AUTO_INCREMENT,
	DateId date NOT NULL,
	Subject int NOT NULL,
	Lecturer varchar(25) NOT NULL,
	Class varchar(4) NOT NULL,
	Groups int NOT NULL,
	Start_hour time(6) NOT NULL,
	End_hour time(6) NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE STUDENTS_MEMBERSHIPS(
	ID int NOT NULL AUTO_INCREMENT,
	Student varchar(25) NOT NULL,
	Groups int NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE SUBJECTS_DICTIONARY(
	ID int NOT NULL AUTO_INCREMENT,
	Subject varchar(100) NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE SUBJECTS_HOURS(
	ID int NOT NULL AUTO_INCREMENT,
	Subject int NOT NULL,
	Type int NOT NULL,
	Quantity int NOT NULL,
	Lecturer varchar(25) NOT NULL,
	Groups int NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE TYPES_DICTIONARY(
	Typ varchar(25) NOT NULL,
	PRIMARY KEY (Typ))ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE EMAIL_DICTIONARY(
	ID int NOT NULL AUTO_INCREMENT,
	Login varchar(25) NOT NULL,
	E_mail varchar(50) NOT NULL,
	PRIMARY KEY (ID))ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE ACCOUNTS  ADD  CONSTRAINT FK_ACCOUNTS_TYPES_DICTIONARY FOREIGN KEY(Typ)
REFERENCES TYPES_DICTIONARY (Typ);

ALTER TABLE ACCOUNTS_DICTIONARY ADD  CONSTRAINT FK_ACCOUNTS_DICTIONARY_ACCOUNTS FOREIGN KEY(Name)
REFERENCES ACCOUNTS (Login);

ALTER TABLE CLASS_AVAILABILITY ADD  CONSTRAINT FK_RoomAvailability_CLASSES FOREIGN KEY(ClassNR)
REFERENCES CLASSES (ClassNR);

ALTER TABLE LECTURER_AVAILABILITY ADD  CONSTRAINT FK_LecturerAvailability_ACCOUNTS1 FOREIGN KEY(Login)
REFERENCES LECTURERS (Login);

ALTER TABLE LECTURERS ADD  CONSTRAINT FK_TEACHERS_ACCOUNTS FOREIGN KEY(Login)
REFERENCES ACCOUNTS (Login);

ALTER TABLE OLDPASS ADD  CONSTRAINT FK_OLDPASS_ACCOUNTS FOREIGN KEY(Login)
REFERENCES ACCOUNTS (Login);

ALTER TABLE SCHEDULE ADD  CONSTRAINT FK_SCHEDULE_CLASSES FOREIGN KEY(Class)
REFERENCES CLASSES (ClassNR);

ALTER TABLE SCHEDULE ADD  CONSTRAINT FK_SCHEDULE_Date FOREIGN KEY(DateId)
REFERENCES CALENDAR (IdDate);

ALTER TABLE SCHEDULE ADD  CONSTRAINT FK_SCHEDULE_GROUPS FOREIGN KEY(Groups)
REFERENCES GROUPS (GroupID);

ALTER TABLE SCHEDULE ADD  CONSTRAINT FK_SCHEDULE_SUBJECTS FOREIGN KEY(Subject)
REFERENCES SUBJECTS_HOURS (ID);

ALTER TABLE STUDENTS_MEMBERSHIPS ADD  CONSTRAINT FK_STUDENTS_MEMBERSHIPS_GROUPS FOREIGN KEY(Groups)
REFERENCES GROUPS (GroupID);

ALTER TABLE SUBJECTS_HOURS ADD  CONSTRAINT FK_SUBJECTS_GROUPS FOREIGN KEY(Groups)
REFERENCES GROUPS (GroupID);

ALTER TABLE SUBJECTS_HOURS ADD  CONSTRAINT FK_SUBJECTS_HOURS_SUBJECTS_DICTIONARY FOREIGN KEY(Subject)
REFERENCES SUBJECTS_DICTIONARY (ID);

ALTER TABLE SUBJECTS_HOURS ADD  CONSTRAINT FK_SUBJECTS_TEACHERS FOREIGN KEY(Lecturer)
REFERENCES LECTURERS (Login);

-- typy userów
INSERT INTO TYPES_DICTIONARY VALUES('admin');
INSERT INTO TYPES_DICTIONARY VALUES('student');
INSERT INTO TYPES_DICTIONARY VALUES('dziekanat');
INSERT INTO TYPES_DICTIONARY VALUES('informatyk');
INSERT INTO TYPES_DICTIONARY VALUES('wykladowca');
INSERT INTO TYPES_DICTIONARY VALUES('wykład');
-- wprowadzanie godzin, zeby potem mozna bylo latwiej wybierac podczas dodawania zajeć do planu
INSERT INTO HOURS_DICTIONARY(Hour)VALUES('9:45');
INSERT INTO HOURS_DICTIONARY(Hour)VALUES('11:15');
INSERT INTO HOURS_DICTIONARY(Hour)VALUES('8:00');
INSERT INTO HOURS_DICTIONARY(Hour)VALUES('9:30');
-- tabela ze wszystkimi przedmiotami
INSERT INTO SUBJECTS_DICTIONARY(Subject)VALUES('Plastyka dla Informatyków');
INSERT INTO SUBJECTS_DICTIONARY(Subject)VALUES('Geografia dla Informatyków');
INSERT INTO SUBJECTS_DICTIONARY(Subject)VALUES('Muzyka dla Informatyków');
INSERT INTO SUBJECTS_DICTIONARY(Subject)VALUES('WF dla Informatyków');
-- wprowadzenie usera do bazy
INSERT INTO ACCOUNTS(Login,Pesel,Password,Typ)VALUES('KGorn','12345678901','skjhabdashi','wykladowca');
INSERT INTO ACCOUNTS(Login,Pesel,Password,Typ)VALUES('MKandul','12345678903','hthtef','wykladowca');
INSERT INTO ACCOUNTS(Login,Pesel,Password,Typ)VALUES('AStach','12345678904','skkjfwef','wykladowca');
INSERT INTO ACCOUNTS(Login,Pesel,Password,Typ)VALUES('KMaluch','12345678905','uretwe','wykladowca');
INSERT INTO ACCOUNTS(Login,Pesel,Password,Typ)VALUES('student','12345678906','djnsad','student');
INSERT INTO ACCOUNTS(Login,Pesel,Password,Typ)VALUES('student2','12345678907','njin','student');
-- wprowadzenie imienia i nazwiska danego użytkownika
INSERT INTO ACCOUNTS_DICTIONARY (Name,Description)VALUES('student','Pierwszy Student');
INSERT INTO ACCOUNTS_DICTIONARY (Name,Description)VALUES('student2','Drugi Student');
INSERT INTO ACCOUNTS_DICTIONARY (Name,Description)VALUES('KGorn','Krzysztof Górnisiewicz');
INSERT INTO ACCOUNTS_DICTIONARY (Name,Description)VALUES('AStach','Alfred Stach');
INSERT INTO ACCOUNTS_DICTIONARY (Name,Description)VALUES('MKandul','Maciej Kandulski');
INSERT INTO ACCOUNTS_DICTIONARY (Name,Description)VALUES('KMaluch','Kuba Małuch');
-- wprowadzanie danych do tabeli słownikowej z adresami
INSERT INTO ADRESSES_DICTIONARY(Country,City,Adress,Building)Values('Polska','Piła','ul.Miła 6','12');
INSERT INTO ADRESSES_DICTIONARY(Country,City,Adress,Building)Values('Polska','Piła','ul.Zamkowa 89','1a');
INSERT INTO ADRESSES_DICTIONARY(Country,City,Adress,Building)Values('Polska','Piła','ul.Wiejska 9','122a');
INSERT INTO ADRESSES_DICTIONARY(Country,City,Adress,Building)Values('Polska','Piła','ul.Ulana 15','5b');
-- wprowadzanie dni wolnych format daty to: RRRR-MM-DD, isHoliday 0 albo 1. 1 to znaczy, że jest wolne
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-02-12','1','sesja');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-02-11','1','sesja');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-02-10','1','sesja');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-02-09','1','sesja');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-02-13','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-07','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-08','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-09','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-10','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-13','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-15','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-17','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-20','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-21','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-22','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-23','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-24','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-27','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-29','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-30','0','');
INSERT INTO CALENDAR(idDate,IsHoliday,IsHolidayText)Values('2020-01-31','0','');
-- wprowadzanie informacji o salach trzecia wartosc to adres czyli id adresu z tabeli adresses_dictionary
INSERT INTO CLASSES VALUES('1.02','laboratorium','1');
INSERT INTO CLASSES VALUES('1.03','wykładowa','2');
INSERT INTO CLASSES VALUES('1.04','komputerowa','3');
INSERT INTO CLASSES VALUES('1.05','aula','4');
-- informacje o tym kiedy sala jest zajeta, można użyć podczas wprowadzania zajęć do planu
INSERT INTO CLASS_AVAILABILITY(ClassNR,Date,TimeFrom,TimeTo)VALUES('1.02','2020-02-11','08:00','09:30');
INSERT INTO CLASS_AVAILABILITY(ClassNR,Date,TimeFrom,TimeTo)VALUES('1.03','2020-02-12','08:00','09:30');
INSERT INTO CLASS_AVAILABILITY(ClassNR,Date,TimeFrom,TimeTo)VALUES('1.04','2020-02-10','09:45','11:15');
INSERT INTO CLASS_AVAILABILITY(ClassNR,Date,TimeFrom,TimeTo)VALUES('1.05','2020-02-09','09:45','11:15');
-- grupa ma id które jest cyfra i nazwe - podczas tworzenia grupy w tabeli podajemy nazwe i id bo po id będziemy potem te grupy wyszukiwać
INSERT INTO GROUPS(GroupName) VALUES('IT1');
INSERT INTO GROUPS(GroupName) VALUES('IT2');
INSERT INTO GROUPS(GroupName) VALUES('IT3');
INSERT INTO GROUPS(GroupName) VALUES('IT4');
-- informacje o wykladowcach, przechowuje login, imie, nazwisko, przedmioty wykładowcy. Jeden wykładowca może mieć wiele rekordów, 
-- zależy od tego ile przedmiotów prowadzi. jeżeli będzie sie tworzyło konto to login i hasło wykładowcy trzeba też wprowadzić to tabeli accounts, insert jest wyżej
INSERT INTO LECTURERS(Login,Name,Pesel,Surname,City,Adress,Building,Phone_Number)VALUES('KGorn','Krzysztof','12345678901','Górnisiewicz','Poznań','ul.JakaśTam 10a','13','999888777');
INSERT INTO LECTURERS(Login,Name,Pesel,Surname,City,Adress,Building,Phone_Number)VALUES('MKandul','Maciej','12345678903','Kandulski','Poznań','ul.GdzieśTam 2c','130','111222333');
INSERT INTO LECTURERS(Login,Name,Pesel,Surname,City,Adress,Building,Phone_Number)VALUES('AStach','Alfred','12345678904','Stach','Poznań','ul.Szpitalna 14','1','246513289');
INSERT INTO LECTURERS(Login,Name,Pesel,Surname,City,Adress,Building,Phone_Number)VALUES('KMaluch','Kuba','12345678905','Maluch','Piła','ul.Quadowska 11','1','482157953');

INSERT INTO STUDENTS(Login,Name,Pesel,Surname,City,Adress,Building,Phone_Number)VALUES('student','Pierwszy','12345678906','Student','Poznań','ul.Akademicka 11a','3','478954245');
INSERT INTO STUDENTS(Login,Name,Pesel,Surname,City,Adress,Building,Phone_Number)VALUES('student2','Drugi','12345678907','Student','Poznań','ul.Uczelniana 5c','1','478975245');
-- przypisanie studenta do grupy. login studenta i id grupy. jedens tudent moze być w kilku grupach
INSERT INTO STUDENTS_MEMBERSHIPS(Student,Groups)VALUES('student','1');
INSERT INTO STUDENTS_MEMBERSHIPS(Student,Groups)VALUES('student2','2');
INSERT INTO STUDENTS_MEMBERSHIPS(Student,Groups)VALUES('student','3');
INSERT INTO STUDENTS_MEMBERSHIPS(Student,Groups)VALUES('student2','4');
-- informacje ile godzina danego przedmiotu ma grupa, odrazu dolaczone sa id grupy, przedmiotu, wykladowcy
INSERT INTO SUBJECTS_HOURS(Subject,Type,Quantity,Lecturer,Groups)VALUES('1','wykład','30','MKandul','1');
INSERT INTO SUBJECTS_HOURS(Subject,Type,Quantity,Lecturer,Groups)VALUES('2','wykład','30','AStach','2');
INSERT INTO SUBJECTS_HOURS(Subject,Type,Quantity,Lecturer,Groups)VALUES('3','wykład','30','MKandul','3');
INSERT INTO SUBJECTS_HOURS(Subject,Type,Quantity,Lecturer,Groups)VALUES('4','wykład','30','AStach','4');
-- 1tydzien
-- pon
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-07','1','AStach','1.02','1','8:15','10:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-07','2','AStach','1.02','1','10:45','13:15');
-- sr
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-08','1','MKandul','1.05','1','8:00',':9:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-08','2','MKandul','1.05','1','9:45','11:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-08','3','MKandul','1.05','1','11:30','13:00');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-08','4','MKandul','1.05','1','13:15','14:30');
-- czw
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-09','1','KMaluch','1.04','1','16:15','17:45');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-09','2','KMaluch','1.04','1','18:00','19:30');
-- pt
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-10','1','KGorn','1.02','1','8:15','9:45');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-10','2','KGorn','1.03','2','10:00','11:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-10','3','KGorn','1.04','1','11:45','13:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-10','4','KGorn','1.05','2','13:30','15:00');


-- 2tydzien
-- pon
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-13','1','KMaluch','1.02','1','11:30','13:45');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-13','2','KMaluch','1.02','1','14:00','16:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-13','3','KMaluch','1.02','1','16:30','18:00');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-13','4','KMaluch','1.02','1','18:15','19:45');
-- sr
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-15','1','MKandul','1.05','1','8:00',':9:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-15','2','MKandul','1.05','1','9:45','11:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-15','3','MKandul','1.05','2','11:30','13:00');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-15','4','MKandul','1.05','2','13:15','14:30');
-- pt
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-17','1','KGorn','1.02','1','8:15','9:45');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-17','2','KGorn','1.03','2','10:00','11:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-17','3','KGorn','1.04','1','11:45','13:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-17','4','KGorn','1.05','2','13:30','15:00');

-- 3tydzien
#pon
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-20','1','AStach','1.02','1','11:30','13:45');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-20','2','AStach','1.02','1','14:00','16:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-20','3','KMaluch','1.04','1','16:30','18:00');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-20','4','KMaluch','1.04','1','18:15','19:45');
-- wt
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-21','1','AStach','1.02','1','8:15','10:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-21','2','AStach','1.02','1','10:45','13:15');
-- sr
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-22','1','MKandul','1.05','1','8:00',':9:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-22','2','MKandul','1.05','1','9:45','11:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-22','3','MKandul','1.05','1','11:30','13:00');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-22','4','MKandul','1.05','1','13:15','14:30');
-- czw
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-23','1','KMaluch','1.04','1','16:15','17:45');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-23','2','KMaluch','1.04','1','18:00','19:30');
-- pt
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-24','1','KGorn','1.03','1','10:00','11:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-24','2','KGorn','1.03','1','11:45','13:15');

-- 4tydzien
-- pon
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-27','1','KMaluch','1.05','1','8:00',':9:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-27','2','KMaluch','1.05','1','9:45','11:15');
-- sr
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-29','1','MKandul','1.05','1','8:00',':9:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-29','2','MKandul','1.05','1','9:45','11:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-29','3','MKandul','1.05','1','11:30','13:00');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-29','4','MKandul','1.05','1','13:15','14:30');
-- czw
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-30','1','KMaluch','1.04','1','16:15','17:45');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-30','2','KMaluch','1.04','1','18:00','19:30');
-- pt
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-31','1','KGorn','1.03','1','10:00','11:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-01-31','2','KGorn','1.03','1','11:45','13:15');

-- informacje o tym kiedy wykladowca jest zajety, można użyć podczas wprowadzania zajęć do planu oraz przed, żeby wiedzieć kiedy wykładowca nie może prowadzić zajęć
INSERT INTO LECTURER_AVAILABILITY(Login,Date,TimeFrom,TimeTo)VALUES('KGorn','2020-02-12','8:00','12:00');
INSERT INTO LECTURER_AVAILABILITY(Login,Date,TimeFrom,TimeTo)VALUES('MKandul','2020-02-11','13:00','23:00');
INSERT INTO LECTURER_AVAILABILITY(Login,Date,TimeFrom,TimeTo)VALUES('AStach','2020-02-10','16:00','23:00');
INSERT INTO LECTURER_AVAILABILITY(Login,Date,TimeFrom,TimeTo)VALUES('KMaluch','2020-02-09','01:00','23:59');
-- przechowuje informacje o planie, zazwyczaj informacje wprowadzane do tej tabeli będą wybierane z formularza dlatego w niektórych przypadkach będą wprowadzane ID zamiast nazw bo 
-- to będzie zrobione w selecie, żeby ładnie wyglądało a w bazie ma być prosto
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-02-13','1','MKandul','1.02','1','8:00','9:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-02-13','2','AStach','1.03','2','8:00','9:30');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-02-13','3','MKandul','1.04','3','9:45','11:15');
INSERT INTO SCHEDULE(DateId,Subject,Lecturer,Class,Groups,Start_hour,End_hour)VALUES('2020-02-13','4','AStach','1.05','4','9:45','11:15');

```          

How to connect to DataBase:

             1. Install program "xampp".
             
             2. Turn on Apache and MySQL module.
             
![xamp](https://user-images.githubusercontent.com/26824257/74232098-629f5200-4cc8-11ea-9fe2-4f611d46e86f.png)

             3. If Apache module doesn't work, delete skype program or change port in "Apache (httpd.conf)" from Listen 80 to Listen 84.
![wycinek](https://user-images.githubusercontent.com/26824257/74232445-19033700-4cc9-11ea-863a-25cd8f4bcbe1.PNG)
![dsa](https://user-images.githubusercontent.com/26824257/74232589-67183a80-4cc9-11ea-9f22-39296026d57a.png)

             4. Click in "Admin" button in MySQL section.
             
             5. If you changed port in point number 3, change link "http://localhost/phpmyadmin/" on http://localhost:84/phpmyadmin/
             
             6. Now go to SQL section in phpMyAdmin and paste sql script visible above. Execute it.
             
             7. Refresh left panel and check scheduledb database.
             
             8. Make sure that you have the correct "Metoda porównywania napisów". It should be "utf8_general_ci"
![aaaaa](https://user-images.githubusercontent.com/26824257/74233937-ffafba00-4ccb-11ea-9494-c8d3605ad199.png)

             9. If don't change it in Ustawienia -> funkcje -> Bazy danych.
![sadasdasd](https://user-images.githubusercontent.com/26824257/74234145-7e0c5c00-4ccc-11ea-80c2-c8aa4da86d23.png)

             10. Find "my.ini" file in xampp/bin folder and change these lines
	    character-set-server=utf8
            collation-server=utf8_general_ci
	     
	     11. Ready! Now you should be connected to DataBase using our application.
             
    

             



           

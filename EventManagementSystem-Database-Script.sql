DROP DATABASE IF EXISTS `EventManagementSystem`;
CREATE DATABASE `EventManagementSystem`; 
USE `EventManagementSystem`;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231204162032_InitialModel', '7.0.14');

COMMIT;

START TRANSACTION;

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` varchar(50) NOT NULL,
    `Email` varchar(255) NOT NULL,
    `PasswordHash` varchar(255) NOT NULL,
    `FirstName` varchar(255) NOT NULL,
    `LastName` varchar(255) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE UNIQUE INDEX `IX_Users_Email` ON `Users` (`Email`);

CREATE UNIQUE INDEX `IX_Users_Username` ON `Users` (`Username`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231204162106_AddUsersTable', '7.0.14');

COMMIT;

START TRANSACTION;

CREATE TABLE `Events` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) NOT NULL,
    `Description` varchar(2048) NOT NULL,
    `Location` varchar(255) NOT NULL,
    `Date` datetime(6) NOT NULL,
    `AvailableTickets` int NOT NULL,
    `UserId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Events_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Events_UserId` ON `Events` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231205120800_AddEventsTable', '7.0.14');

COMMIT;

START TRANSACTION;

ALTER TABLE `Events` MODIFY `AvailableTickets` int unsigned NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231205174741_EditDataTypeForAvailableTicketsColumnInEventsTable', '7.0.14');

COMMIT;

START TRANSACTION;

CREATE TABLE `Participations` (
    `UserId` int NOT NULL,
    `EventId` int NOT NULL,
    `NumOfTicket` int NOT NULL,
    PRIMARY KEY (`EventId`, `UserId`),
    CONSTRAINT `FK_Participations_Events_EventId` FOREIGN KEY (`EventId`) REFERENCES `Events` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Participations_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Participations_UserId` ON `Participations` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20231205184517_AddParticipationsTable', '7.0.14');

COMMIT;


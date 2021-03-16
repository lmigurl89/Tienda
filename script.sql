CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `AspNetRoles` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Nombre` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Apellidos` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
    `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
);

CREATE TABLE `Productos` (
    `Id` varchar(50) NOT NULL,
    `Nombre` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Slug` longtext CHARACTER SET utf8mb4 NULL,
    `Precio` double NOT NULL,
    `Cantidad` int NOT NULL,
    `CreadoPor` longtext CHARACTER SET utf8mb4 NULL,
    `FechaCreado` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00',
    CONSTRAINT `PK_Productos` PRIMARY KEY (`Id`)
);

CREATE TABLE `Trazas` (
    `Id` varchar(50) NOT NULL,
    `UserName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `NombreAccion` varchar(255) CHARACTER SET utf8mb4 NULL,
    `NombrePC` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ObjetoCreado` json NULL,
    `ObjetoAntesModificar` json NULL,
    `ObjetoModificado` json NULL,
    `ObjetoEliminado` json NULL,
    `FechaCreado` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00',
    CONSTRAINT `PK_Trazas` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Ordenes` (
    `Id` varchar(50) NOT NULL,
    `Fecha` Date NOT NULL,
    `Cantidad` int NOT NULL,
    `Estado` int NOT NULL,
    `UsuarioId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProductoId` varchar(50) NOT NULL,
    `FechaCreado` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00',
    CONSTRAINT `PK_Ordenes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Ordenes_AspNetUsers_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Ordenes_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `Productos` (`Id`) ON DELETE RESTRICT
);

INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Descripcion`, `Discriminator`, `Name`, `NormalizedName`)
VALUES ('91e0f2a3-3a4c-46e4-83a6-94356cd46725', 'dc0aeb28-2e47-483d-9a1a-1396f6aa374d', 'Administradores de la tienda', 'Rol', 'Administrador', NULL);
INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Descripcion`, `Discriminator`, `Name`, `NormalizedName`)
VALUES ('f56dc294-76e6-4277-9685-b4ca524f61d8', 'b6ef9fe3-415b-4688-9fec-64a46c0a20e9', 'Vendedor de la tienda', 'Rol', 'Vendedor', NULL);
INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Descripcion`, `Discriminator`, `Name`, `NormalizedName`)
VALUES ('49efd009-ced5-4fb9-868d-cf365a34765f', 'b1327f26-fce3-4e93-bc2a-8edd332bdc78', 'Cliente de la tienda', 'Rol', 'Cliente', NULL);

INSERT INTO `AspNetUsers` (`Id`, `AccessFailedCount`, `Apellidos`, `ConcurrencyStamp`, `Email`, `EmailConfirmed`, `LockoutEnabled`, `LockoutEnd`, `Nombre`, `NormalizedEmail`, `NormalizedUserName`, `PasswordHash`, `PhoneNumber`, `PhoneNumberConfirmed`, `SecurityStamp`, `TwoFactorEnabled`, `UserName`)
VALUES ('f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0', 0, 'Administrador', '44c8c6e9-f046-4af2-92ba-eb91b232b110', 'administrador@tienda.com', FALSE, TRUE, NULL, 'Usuario', 'ADMINISTRADOR@TIENDA.COM', 'ADMINISTRADOR', 'AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==', NULL, FALSE, 'NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY', FALSE, 'administrador');
INSERT INTO `AspNetUsers` (`Id`, `AccessFailedCount`, `Apellidos`, `ConcurrencyStamp`, `Email`, `EmailConfirmed`, `LockoutEnabled`, `LockoutEnd`, `Nombre`, `NormalizedEmail`, `NormalizedUserName`, `PasswordHash`, `PhoneNumber`, `PhoneNumberConfirmed`, `SecurityStamp`, `TwoFactorEnabled`, `UserName`)
VALUES ('549889bf-fec3-4403-a536-133cd8cdeac5', 0, 'Vendedor', '44c8c6e9-f046-4af2-92ba-eb91b232b110', 'vendedor@tienda.com', FALSE, TRUE, NULL, 'Usuario', 'VENDEDOR@TIENDA.COM', 'VENDEDOR', 'AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==', NULL, FALSE, 'NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY', FALSE, 'vendedor');
INSERT INTO `AspNetUsers` (`Id`, `AccessFailedCount`, `Apellidos`, `ConcurrencyStamp`, `Email`, `EmailConfirmed`, `LockoutEnabled`, `LockoutEnd`, `Nombre`, `NormalizedEmail`, `NormalizedUserName`, `PasswordHash`, `PhoneNumber`, `PhoneNumberConfirmed`, `SecurityStamp`, `TwoFactorEnabled`, `UserName`)
VALUES ('787d0bc9-048c-4e9f-b6c3-df267442a3aa', 0, 'Cliente', '44c8c6e9-f046-4af2-92ba-eb91b232b110', 'cliente@tienda.com', FALSE, TRUE, NULL, 'Usuario', 'CLIENTE@TIENDA.COM', 'CLIENTE', 'AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==', NULL, FALSE, 'NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY', FALSE, 'cliente');

INSERT INTO `Productos` (`Id`, `Cantidad`, `CreadoPor`, `Descripcion`, `FechaCreado`, `Nombre`, `Precio`, `Slug`)
VALUES ('5d1a6250-4bdc-46c0-8fb7-b965dad45d79', 220, 'vendedor', 'MotherBoard', '2021-03-03 00:00:00', 'Motherboard', 20.989999999999998, NULL);
INSERT INTO `Productos` (`Id`, `Cantidad`, `CreadoPor`, `Descripcion`, `FechaCreado`, `Nombre`, `Precio`, `Slug`)
VALUES ('4b5a5caa-8858-4faa-9e86-c5023d1119cd', 614, 'vendedor', 'Mouse', '2021-03-03 00:00:00', 'Mouse', 52.0, NULL);
INSERT INTO `Productos` (`Id`, `Cantidad`, `CreadoPor`, `Descripcion`, `FechaCreado`, `Nombre`, `Precio`, `Slug`)
VALUES ('4a7b536f-7777-4af9-bec8-e9a9530d630d', 20, 'vendedor', 'Monitor', '2021-03-03 00:00:00', 'Monitor', 65.650000000000006, NULL);
INSERT INTO `Productos` (`Id`, `Cantidad`, `CreadoPor`, `Descripcion`, `FechaCreado`, `Nombre`, `Precio`, `Slug`)
VALUES ('1df535e8-9f12-4b9a-a780-f32a21c123a9', 220, 'vendedor', 'RAM', '2021-03-03 00:00:00', 'Ram', 20.0, NULL);
INSERT INTO `Productos` (`Id`, `Cantidad`, `CreadoPor`, `Descripcion`, `FechaCreado`, `Nombre`, `Precio`, `Slug`)
VALUES ('6c875378-7bf3-4ca3-b056-bc360d65ac8c', 220, 'vendedor', 'KeyBoard', '2021-03-03 00:00:00', 'Keyboard', 10.99, NULL);

INSERT INTO `AspNetUserRoles` (`RoleId`, `UserId`, `Discriminator`)
VALUES ('91e0f2a3-3a4c-46e4-83a6-94356cd46725', 'f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0', 'Rol_Usuarios');
INSERT INTO `AspNetUserRoles` (`RoleId`, `UserId`, `Discriminator`)
VALUES ('f56dc294-76e6-4277-9685-b4ca524f61d8', '549889bf-fec3-4403-a536-133cd8cdeac5', 'Rol_Usuarios');
INSERT INTO `AspNetUserRoles` (`RoleId`, `UserId`, `Discriminator`)
VALUES ('49efd009-ced5-4fb9-868d-cf365a34765f', '787d0bc9-048c-4e9f-b6c3-df267442a3aa', 'Rol_Usuarios');

INSERT INTO `Ordenes` (`Id`, `Cantidad`, `Estado`, `Fecha`, `ProductoId`, `UsuarioId`)
VALUES ('40ea0e5d-c412-45f7-94f6-3e0051350f96', 10, 0, '2021-03-12', '1df535e8-9f12-4b9a-a780-f32a21c123a9', 'f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0');
INSERT INTO `Ordenes` (`Id`, `Cantidad`, `Estado`, `Fecha`, `ProductoId`, `UsuarioId`)
VALUES ('72690b9a-0afb-4b3c-98c8-26809816bd7d', 5, 0, '2021-03-12', '1df535e8-9f12-4b9a-a780-f32a21c123a9', 'f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0');
INSERT INTO `Ordenes` (`Id`, `Cantidad`, `Estado`, `Fecha`, `ProductoId`, `UsuarioId`)
VALUES ('f2f4a187-3f67-4eb3-8bc8-b1550bd9ffe8', 2, 2, '2021-03-12', '1df535e8-9f12-4b9a-a780-f32a21c123a9', 'f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0');
INSERT INTO `Ordenes` (`Id`, `Cantidad`, `Estado`, `Fecha`, `ProductoId`, `UsuarioId`)
VALUES ('4cd7a29e-1e21-4697-b3c9-b7f8ea30a72d', 20, 1, '2021-03-12', '6c875378-7bf3-4ca3-b056-bc360d65ac8c', 'f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0');
INSERT INTO `Ordenes` (`Id`, `Cantidad`, `Estado`, `Fecha`, `ProductoId`, `UsuarioId`)
VALUES ('0d4d759f-58f6-4dd2-a890-079f2ffef68c', 20, 1, '2021-03-12', '6c875378-7bf3-4ca3-b056-bc360d65ac8c', 'f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0');

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE UNIQUE INDEX `Id_UNIQUE` ON `Ordenes` (`Id`);

CREATE INDEX `IX_Ordenes_Fecha` ON `Ordenes` (`Fecha`);

CREATE INDEX `IX_Ordenes_ProductoId` ON `Ordenes` (`ProductoId`);

CREATE INDEX `IX_Ordenes_UsuarioId` ON `Ordenes` (`UsuarioId`);

CREATE UNIQUE INDEX `Id_UNIQUE` ON `Productos` (`Id`);

CREATE UNIQUE INDEX `Nombre_UNIQUE` ON `Productos` (`Nombre`);

CREATE INDEX `FK_NombreAccion_Trazas_idx` ON `Trazas` (`NombreAccion`);

CREATE UNIQUE INDEX `Id_UNIQUE` ON `Trazas` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210316023832_migration1', '5.0.4');

COMMIT;


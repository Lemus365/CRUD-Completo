CREATE DATABASE IF NOT EXISTS escuela
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE escuela;

CREATE TABLE IF NOT EXISTS alumnos (
    id          INT AUTO_INCREMENT PRIMARY KEY,
    carnet      VARCHAR(20)  NOT NULL UNIQUE,
    nombre      VARCHAR(60)  NOT NULL,
    apellido    VARCHAR(60)  NOT NULL,
    correo      VARCHAR(120),
    carrera     VARCHAR(80),
    edad        INT,
    fecha_alta  TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO alumnos (carnet, nombre, apellido, correo, carrera, edad) VALUES
    ('2024001', 'Ana',   'Lopez',    'ana.lopez@correo.com',    'Ingenieria en Sistemas', 20),
    ('2024002', 'Carlos','Mendez',   'carlos.mendez@correo.com','Administracion',         22),
    ('2024003', 'Maria', 'Ramirez',  'maria.ramirez@correo.com','Contaduria',             19)
ON DUPLICATE KEY UPDATE carnet = VALUES(carnet);

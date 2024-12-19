# Aplicación de Gestión de Reservas

Esta aplicación permite gestionar reservas para salas, utilizando .NET Framework 4.8, procedimientos almacenados para la base de datos, Dapper como micro-ORM, y Bootstrap para la interfaz de usuario.

## Características Principales

- **CRUD de Salas**: Crear, leer, actualizar y eliminar salas.
- **CRUD de Reservas**: Crear, leer, actualizar y eliminar reservas.
- **Filtro de Reservas**: Filtrar reservas por rango de fechas y nombre de la sala.
- **Interfaz Amigable**: Uso de Bootstrap para una experiencia de usuario moderna.
- **Diseño por Capas**: Separación de responsabilidades en controladores, repositorios, modelos y vistas.

---

## Tecnologías y Herramientas

- **Backend**: ASP.NET Framework 4.8
- **Base de Datos**: SQL Server con procedimientos almacenados
- **ORM**: Dapper
- **Frontend**: Razor Views con Bootstrap

---

## Estructura del Proyecto

- **Controllers**: Manejan la lógica de las acciones y gestionan las vistas.
- **Repositories**: Encapsulan el acceso a la base de datos utilizando Dapper.
- **Models**: Representan las entidades de negocio (Rooms, Reservations).
- **Views**: Contienen la interfaz de usuario renderizada con Razor y Bootstrap.

---

## Instalación y Configuración

### Prerrequisitos

1. SQL Server instalado y configurado.
2. Visual Studio 2019 o superior.
3. .NET Framework 4.8 instalado.

### Pasos para Instalar

1. **Clonar el Repositorio**:
   ```bash
   git clone https://github.com/johanS123/reservationSystem.git
   ```
2. **Configurar la Base de Datos**:
   - Crear una base de datos en SQL Server.
   - Ejecutar los scripts SQL proporcionados para crear las tablas y los procedimientos almacenados.

3. **Configurar la Cadena de Conexión**:
   - Abrir el archivo `web.config`.
   - Actualizar el valor de `connectionString` con los datos de tu servidor SQL Server.

   ```xml
   <connectionStrings>
       <add name="DefaultConnection" connectionString="Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;Trusted_Connection=True;" providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

4. **Ejecutar la Aplicación**:
   - Abrir el proyecto en Visual Studio.
   - Establecer como proyecto de inicio y ejecutar con `Ctrl + F5`.

---

## Procedimientos Almacenados

### Crear Salas
```sql
CREATE PROCEDURE CreateRoom
    @Name NVARCHAR(100),
    @Capacity INT,
	@Availability BIT
AS
BEGIN
    INSERT INTO Rooms(Name, Capacity, Availability)
    VALUES (@Name, @Capacity, @Availability);
    SELECT SCOPE_IDENTITY() AS Id;
END
```

### Crear Reservas
```sql
CREATE PROCEDURE CreateReservation
    @ReservedBy VARCHAR(100),
    @RoomId INT,
    @ReservaDate DATETIME,
    @Duration INT
AS
BEGIN
    INSERT INTO Reservations(ReservedBy, RoomId, ReservaDate, Duration)
    VALUES (@ReservedBy, @RoomId, @ReservaDate, @Duration);

	-- Actualiza disponibilidad
	UPDATE Rooms
	SET Availability = 0
	WHERE Id = @RoomId;

    SELECT SCOPE_IDENTITY() AS Id;
END
```

### Filtrar Reservas
```sql
CREATE PROCEDURE ListReservations
	@RoomName VARCHAR(100) = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SELECT 
		R.Id,				 
		R.ReservaDate,		 
		R.ReservedBy,		
		R.Duration,			
		R.RoomId AS RoomId, 
		S.*				
    FROM Reservations R
    INNER JOIN Rooms S ON R.RoomId = S.Id
	WHERE (@RoomName IS NULL OR S.Name LIKE '%' + @RoomName + '%')
              AND (@StartDate IS NULL OR r.ReservaDate >= @StartDate)
              AND (@EndDate IS NULL OR r.ReservaDate <= @EndDate);
END;
```

los demás procediemientos han sido compartidos por correo a las personas indicadas.

---

## Uso de la Aplicación

1. **Gestor de Salas**:
   - Accede a `/Room` para gestionar las salas.
   - Usa las opciones de crear, editar y eliminar.

2. **Gestor de Reservas**:
   - Accede a `/Reservation` para gestionar las reservas.
   - Filtra por nombre de sala y rango de fechas utilizando el formulario en la parte superior.

---

## Mejoras Futuras

- Agregar autenticación y autorización.
- Diseño responsivo avanzado con Bootstrap.

---

## Licencia

Este proyecto está bajo la licencia MIT. Puedes usarlo libremente respetando los términos de la licencia.













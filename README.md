# clientes-api

Backend para la gestion de clientes. Este proyecto expone una API REST con autenticacion JWT, operaciones CRUD para clientes y documentacion con Swagger. Esta pensado para consumirse desde un frontend separado que vive en otro repositorio.

## Objetivo del proyecto

Este repositorio contiene el backend de la aplicacion. Su responsabilidad es:

- autenticar usuarios mediante JWT
- exponer endpoints para consultar, crear, actualizar y eliminar clientes
- conectarse a SQL Server mediante procedimientos almacenados
- servir como API para un frontend independiente

## Tecnologias usadas

- ASP.NET Core Web API
- .NET 10
- SQL Server / SQL Server Express
- JWT para autenticacion
- Swagger para documentacion y pruebas
- ADO.NET con procedimientos almacenados

## Estructura general

```text
Clientes/
  Clientes.Api/             API principal
  Clientes.Aplicacion/      Servicios y logica de aplicacion
  Clientes.Dominio/         Entidades e interfaces
  Clientes.Infraestructura/ Acceso a datos y repositorios
database/
  ClientesDb.sql            Script de base de datos
```

## Requisitos previos

Antes de ejecutar el proyecto, asegurarse de tener instalado lo siguiente:

- .NET SDK 10
- SQL Server o SQL Server Express
- Una herramienta para ejecutar scripts SQL, por ejemplo SQL Server Management Studio o Azure Data Studio

## Clonar el repositorio

```bash
git clone <URL_DEL_REPOSITORIO>
cd clientes-api
```

## Configuracion de la base de datos

Este proyecto no usa migraciones. La base de datos se inicializa con el script:

`database/ClientesDb.sql`

### Pasos

1. Crear una base de datos llamada `ClientesDb` en la instancia de SQL Server.
2. Abrir y ejecutar el script `database/ClientesDb.sql`.
3. El script crea las tablas, restricciones, procedimientos almacenados y algunos datos iniciales.

### Datos iniciales

El script inserta:

- clientes de ejemplo
- un usuario llamado `admin`

Nota: la contrasena del usuario esta almacenada hasheada. Para realizar pruebas, reemplazar ese registro por uno generado en el entorno o actualizarlo directamente en la base de datos.

## Configuracion del proyecto

La configuracion principal se encuentra en:

`Clientes/Clientes.Api/appsettings.json`

### Connection string

Por defecto el proyecto apunta a esta instancia:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ClientesDb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=True;"
}
```

Ajustar ese valor con el nombre de instancia y el tipo de autenticacion del entorno antes de ejecutar la API.

### JWT

En el mismo archivo se configura JWT:

```json
"Jwt": {
  "Key": "EstaEsUnaClaveSeguraParaJwtDeClientesApi2026",
  "Issuer": "Clientes.Api",
  "Audience": "Clientes.Web",
  "ExpirationMinutes": 60
}
```

Estos valores corresponden a la configuracion local actual. En despliegues o entornos compartidos, mover estas credenciales a variables de entorno o a configuracion segura.

## Ejecutar la API

Desde la raiz del repositorio, iniciar la API con:

```bash
cd Clientes/Clientes.Api
dotnet restore
dotnet run
```

Segun `launchSettings.json`, la API queda disponible en:

- `http://localhost:5220`
- `https://localhost:7149`

Swagger queda disponible en:

- `http://localhost:5220/swagger`
- `https://localhost:7149/swagger`

## Flujo de uso con el frontend

Como el frontend vive en otro repositorio, la integracion con este backend requiere:

1. Configurar en el frontend la URL base de esta API.
2. Consumir `POST /auth/login` para obtener el token JWT.
3. Guardar el token en el frontend.
4. Enviar el header `Authorization: Bearer <token>` en las peticiones protegidas.
5. Consumir los endpoints de `clientes` para el CRUD.

## Endpoints principales

### Autenticacion

- `POST /auth/login`

Body esperado:

```json
{
  "nombreUsuario": "admin",
  "contrasena": "TU_CONTRASENA"
}
```

Respuesta esperada:

```json
{
  "id": 1,
  "nombreUsuario": "admin",
  "token": "jwt_aqui"
}
```

### Clientes

Todos los endpoints de esta seccion requieren JWT.

- `GET /clientes` -> obtener todos los clientes
- `GET /clientes/{id}` -> obtener un cliente por id
- `POST /clientes` -> crear un cliente
- `PUT /clientes/{id}` -> actualizar un cliente
- `DELETE /clientes/{id}` -> eliminar un cliente

Ejemplo de body para crear o actualizar un cliente:

```json
{
  "nombre": "Juan Perez",
  "correoElectronico": "juan.perez@correo.com",
  "telefono": "5551234567",
  "estatus": true
}
```

## CORS

Actualmente la API tiene CORS abierto para cualquier origen, header y metodo:

- `AllowAnyOrigin()`
- `AllowAnyHeader()`
- `AllowAnyMethod()`

Esto se hizo para evitar bloqueos de CORS durante el desarrollo y facilitar la conexion con el frontend aunque este corra en otro puerto, dominio o repositorio.

## Recomendaciones para quien clone el proyecto

Para clonar y dejar listo este backend, seguir este orden:

1. Clonar el repositorio.
2. Crear la base de datos `ClientesDb`.
3. Ejecutar `database/ClientesDb.sql`.
4. Revisar y ajustar `Clientes/Clientes.Api/appsettings.json`.
5. Conectar el frontend usando la URL base de esta API.

## Notas importantes

- El proyecto depende de SQL Server y de los procedimientos almacenados creados por el script SQL.
- Un cambio en la instancia de SQL Server obliga a actualizar la cadena de conexion.
- Los endpoints de clientes estan protegidos con JWT.
- CORS esta abierto para simplificar la integracion con el frontend.

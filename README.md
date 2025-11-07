# 🌐 WebApi HU3 – Sistema de Gestión de Usuarios y Estudiantes

## 📘 Descripción General

**WebApi HU3** es una aplicación desarrollada en **ASP.NET Core** que implementa una arquitectura por capas (Domain, Application, Infrastructure y API).  
El sistema permite la **gestión de usuarios y estudiantes**, con autenticación mediante **JSON Web Tokens (JWT)** para proteger los endpoints.  
Está diseñado con fines **académicos y profesionales**, siguiendo buenas prácticas de programación y patrones de diseño.

### 🎯 Objetivos del Sistema
- Gestionar usuarios (registro, autenticación, roles).
- Administrar estudiantes (creación, actualización, eliminación y consulta).
- Proteger las operaciones mediante autenticación JWT.
- Implementar un entorno modular y escalable.

---

## 🏗️ Arquitectura del Proyecto

El proyecto sigue una **arquitectura limpia (Clean Architecture)**, separando las responsabilidades en distintas capas:

```
WebApi-HU3-develop/
│
├── WebApi-HU3.Api/ → Capa de presentación (controladores, configuración de JWT, endpoints)
├── WebApi-HU3.Application/ → Lógica de negocio (servicios, DTOs, validaciones)
├── WebApi-HU3.Domain/ → Entidades principales e interfaces de repositorio
├── WebApi-HU3.Infraestructure/ → Acceso a datos, contexto EF Core, repositorios
└── Assets/ → Diagramas y documentación (Casos de uso, ERD, JWT, etc.)
```

Cada capa comunica solo lo necesario con la siguiente, asegurando bajo acoplamiento y alta cohesión.

---

## 🛠️ Tecnologías Utilizadas

- **.NET 8 / ASP.NET Core Web API**
- **Entity Framework Core** (acceso a datos y migraciones)
- **JWT (JSON Web Token)** para autenticación
- **C# 12**
- **SQL Server** (base de datos)
- **Visual Studio / Rider / VS Code**
- **Swagger** para documentación de endpoints

---

## ⚙️ Configuración y Ejecución

### 🔹 Requisitos Previos
- .NET SDK 8.0 o superior
- SQL Server o base de datos compatible
- Herramienta de desarrollo: Rider, Visual Studio o VS Code

### 🔹 Pasos de Instalación

1. **Clonar el repositorio:**
```bash
   git clone https://github.com/tuusuario/WebApi-HU3.git
   cd WebApi-HU3-develop
```
2. **Configurar la cadena de conexión** en el archivo:

```bash
WebApi-HU3.Api/appsettings.json
```

3. Aplicar migraciones y crear la base de datos:

```bash
cd WebApi-HU3.Infraestructure
dotnet ef database update
```

4. Ejecutar el proyecto:

```bash
cd ../WebApi-HU3.Api
dotnet run
```

5. Abrir en el navegador:

```bash
https://students-web-fb5f86739d1b.herokuapp.com/index.html
```

🔐 Autenticación JWT

El sistema utiliza JWT Bearer Tokens para autenticar y autorizar usuarios.
🔸 Flujo Básico:

1. El usuario se registra o inicia sesión mediante /api/Auth/login.

2. El servidor genera un token JWT firmado.

3. El cliente incluye el token en el encabezado de cada petición:

```bash
    Authorization: Bearer {token}
```

---

## 🧾 Endpoints Principales

Los ficheros fuente están en:
`WebApi-HU3-develop/WebApi-HU3.Api/Controllers/`

---

## 🧩 **AuthController**
**Ruta base:** `/api/Auth`

### `POST /api/Auth/Login`
**Propósito:** autenticar y devolver `AuthResponseDto` con Token + User.  
**Autorización:** público (no requiere token).

---

### `POST /api/Auth/Register`
**Propósito:** crear un nuevo usuario (acepta `UserRegisterDto` con `Username`, `Email`, `Password`, `Role`).  
**Autorización:** público (no requiere token).

> 📝 **Nota:** Actualmente el cliente puede indicar `Role` en el body (ver DTO `UserRegisterDto.Role`).

---

## 👤 **UserController**
**Ruta base:** `/api/User`

### `GET /api/User`
**Propósito:** listar todos los usuarios.  
**Autorización:** `[Authorize(Roles = "Admin")]` → solo **Admin**.

---

### `GET /api/User/{id}`
**Propósito:** obtener un usuario por ID.  
**Autorización:** `[Authorize]` → cualquier usuario autenticado (**Admin** o **User**).

---

### `PUT /api/User/{id}`
**Propósito:** actualizar un usuario existente.  
**Autorización:** `[Authorize(Roles = "Admin")]` → solo **Admin**.

---

### `DELETE /api/User/{id}`
**Propósito:** eliminar un usuario.  
**Autorización:** `[Authorize(Roles = "Admin")]` → solo **Admin**.

📂 Estos atributos se encuentran en  
`WebApi-HU3.Api/Controllers/UserController.cs`.

---

## 🎓 **StudentController**
**Ruta base:** `/api/Student`

| Método | Endpoint | Descripción | Autorización |
|---------|-----------|-------------|---------------|
| `GET` | `/api/Student` | Listar estudiantes. | Pública |
| `GET` | `/api/Student/{id}` | Obtener estudiante por ID. | Pública |
| `POST` | `/api/Student` | Crear un nuevo estudiante. | Pública |
| `PUT` | `/api/Student/{id}` | Actualizar estudiante. | Pública |
| `DELETE` | `/api/Student/{id}` | Eliminar estudiante. | Pública |

> ⚠️ En el código actual **no hay ningún `[Authorize]`** en la clase ni en los métodos de `StudentController`,  
> por tanto, **todos los endpoints son públicos** (no requieren token).

---

## 🧾 **Roles definidos en el dominio**

**Archivo:**  
`WebApi-HU3.Domain/Entities/UserRole.cs`

```csharp
public enum UserRole
{
    Admin,
    User
}
```
---

## 🧩 Diagramas y Documentación

### Diagrama Entidad-Relación (ER)

![Diagrama ER](./Assets/Images/Entidad_Relacion.png)

---

### Diagrama de Clases

![Diagrama de Clases](./Assets/Images/Clases.png)

---

## Casos de Uso

![Casos de Uso](./Assets/Images/Casos_Uso.png)

---

## Secuencias

### Generación de Token JWT

![Generación de Token JWT](./Assets/Images/Login_JWT.png)

---

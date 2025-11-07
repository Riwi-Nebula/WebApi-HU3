# 📘 Documentación de Endpoints — WebApi-HU3

---

## 🔐 Autenticación y Usuarios

### 🟢 **POST** `/api/auth/register`

**Descripción:**  
Registra un nuevo usuario en el sistema.

**Body (JSON):**
```json
{
  "userName": "sara",
  "email": "sara@correo.com",
  "password": "12345",
  "role": "Admin"
}
```

**Respuestas posibles:**

| Código | Descripción | Ejemplo |
|--------|-------------|---------|
| 201 Created | Usuario creado exitosamente | `{ "message": "User created successfully" }` |
| 400 Bad Request | Datos inválidos o usuario ya existe | `{ "error": "Email already registered" }` |


**Permisos:** Público (no requiere token)

---

### 🟢 POST `/api/auth/login`

**Descripción:**
Permite a un usuario autenticarse y obtener un token JWT.

**Body (JSON):**

```json
{
  "email": "sara@correo.com",
  "password": "12345"
}
```

**Respuesta exitosa (200 OK):**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "userName": "sara",
    "email": "sara@correo.com",
    "role": "Admin"
  }
}
```

**Permisos:** Público (no requiere token)

---

### 🔵 GET `/api/users`

**Descripción:**
Devuelve la lista de todos los usuarios registrados.

**Headers:**

```
Authorization: Bearer <tu_token_jwt>
```

**Respuesta exitosa (200 OK):**

```json
[
  {
    "id": 1,
    "userName": "sara",
    "email": "sara@correo.com",
    "role": "Admin"
  },
  {
    "id": 2,
    "userName": "miguel",
    "email": "miguel@correo.com",
    "role": "User"
  }
]
```

**Permisos:** 🔒 Requiere token JWT válido. Solo accesible para rol Admin.

---

### 🔵 GET `/api/users/{id}`

**Descripción:**
Obtiene la información de un usuario específico por su ID.

**Headers:**

```
Authorization: Bearer <tu_token_jwt>
```

**Ejemplo:** `/api/users/2`

**Respuesta (200 OK):**

```json
{
  "id": 2,
  "userName": "miguel",
  "email": "miguel@correo.com",
  "role": "User"
}
```

**Errores posibles:**

```json
404 Not Found → Usuario no encontrado

401 Unauthorized → Token inválido o faltante
```

**Permisos:** Admin

---

### 🟠 PUT `/api/users/{id}`

**Descripción:**
Actualiza la información de un usuario existente.

**Headers:**

```
Authorization: Bearer <tu_token_jwt>
```

**Body (JSON):**

```json
{
  "userName": "miguel_updated",
  "email": "miguel@correo.com",
  "role": "User"
}
```

**Respuesta (200 OK):**

```json
{
  "message": "User updated successfully"
}
```

**Permisos:** Admin

---

### 🔴 DELETE `/api/users/{id}`

**Descripción:**
Elimina un usuario existente por ID.

**Headers:**

```json
Authorization: Bearer <tu_token_jwt>
```

**Ejemplo:** `/api/users/3`

**Respuesta (204 No Content):**
Usuario eliminado sin contenido adicional.

**Permisos: Admin**

---

## 🎓 Estudiantes
### 🟢 POST `/api/students`

**Descripción:**
Crea un nuevo estudiante en la base de datos.

**Headers:**

```json
Authorization: Bearer <tu_token_jwt>
```

**Body (JSON):**

```json
{
  "fullName": "Camila López",
  "email": "camila@escuela.com"
}
```

**Respuesta (201 Created):**

```json
{
  "id": 1,
  "fullName": "Camila López",
  "email": "camila@escuela.com"
}
```

**Permisos:** Requiere token JWT (rol Admin o User)

---

### 🔵 GET `/api/students`

**Descripción:**
Lista todos los estudiantes registrados.

**Headers:**

```json
Authorization: Bearer <tu_token_jwt>
```

**Respuesta (200 OK):**

```json
[
  {
    "id": 1,
    "fullName": "Camila López",
    "email": "camila@escuela.com"
  },
  {
    "id": 2,
    "fullName": "Juan Pérez",
    "email": "juan@escuela.com"
  }
]
```

**Permisos:** Requiere token JWT (Admin o User)

---

### 🔵 GET `/api/students/{id}`

**Descripción:**
Obtiene los datos de un estudiante por ID.

**Ejemplo:**  `/api/students/1`

**Respuesta (200 OK):**

```json
{
  "id": 1,
  "fullName": "Camila López",
  "email": "camila@escuela.com"
}
```

**Errores posibles:**

``404 Not Found`` → Estudiante no existe

``401 Unauthorized`` → Token inválido

---

### 🟠 PUT `/api/students/{id}`

**Descripción:**
Actualiza los datos de un estudiante existente.

**Body (JSON):**

```json
{
  "fullName": "Camila López García",
  "email": "camila@escuela.com"
}
```

**Respuesta (200 OK):**

```json
{
  "message": "Student updated successfully"
}
```

**Permisos:** JWT requerido (Admin o User)

---

### 🔴 DELETE `/api/students/{id}`

**Descripción:**
Elimina un estudiante del sistema.

**Headers:**

```json
Authorization: Bearer <tu_token_jwt>
```

**Respuesta (204 No Content):**
Estudiante eliminado correctamente.

**Permisos:** JWT requerido (Admin o User)

---

⚙️ ## ⚙️ Códigos de estado comunes

| Código | Descripción |
|--------|-------------|
| 200 OK | Solicitud exitosa |
| 201 Created | Recurso creado |
| 204 No Content | Eliminado correctamente |
| 400 Bad Request | Datos inválidos |
| 401 Unauthorized | Token ausente o inválido |
| 403 Forbidden | Rol sin permisos |
| 404 Not Found | Recurso no encontrado |
| 500 Internal Server Error | Error interno del servidor |

---

### 🧾 Notas importantes para el equipo Frontend

Token JWT:
Todos los endpoints protegidos deben incluir el encabezado:

```json
Authorization: Bearer <token>
```

**Roles:**

- **Admin:** acceso total a usuarios y estudiantes.

- **User:** solo puede gestionar estudiantes.

**Flujo de uso:**

1. `POST /api/auth/register` — Crear usuario

2. `POST /api/auth/login` — Obtener token JWT

3. Usar el token en los endpoints protegidos

**Formato de datos:**
Todo debe enviarse en formato `application/json`.
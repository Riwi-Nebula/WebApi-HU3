# Documentación de la API

**URL Base de la API (Producción):**  
`https://students-web-fb5f86739d1b.herokuapp.com`

Esta API permite la gestión de **Usuarios** y **Estudiantes**.  
El sistema maneja Roles:

| Rol           | Permisos |
| ----          |---------|
| **Admin**     | Puede gestionar **Usuarios** y **Estudiantes** |
| **User**      | Puede gestionar solo **Estudiantes** |

Los roles se asignan al momento del registro de un User.

---

## Autenticación (Auth)

### Login

**POST** `/api/Auth/Login`

**Body (JSON):**

```json
{
  "email": "string",
  "password": "string"
}
```

**Respuesta exitosa:**
Devuelve un **JWT token** que debes enviar en el `Authorization Header`:

```json
{
  "Authorization": "Bearer <token>"
}
```

---

### Register

**POST** `/api/Auth/Register`

**Body (JSON):**

```json
{
  "username": "string",
  "email": "string",
  "password": "string",
  "role": "Admin" | "User"
}
```

---

## Estudiantes (Student)

> Accesible por **Admin** y **User**

### Obtener todos los estudiantes

**GET** `/api/Student`

**Respuesta:** Lista de estudiantes.

---

### Crear un estudiante

**POST** `/api/Student`

**Body (JSON):**

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

---

### Obtener estudiante por ID

**GET** `/api/Student/{id}`

---

### Actualizar estudiante

**PUT** `/api/Student/{id}`

**Body (JSON):**

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

---

### Eliminar estudiante

**DELETE** `/api/Student/{id}`

---

## Usuarios (User)

> **Solo Admin** puede usar estos endpoints.

### Obtener todos los usuarios

**GET** `/api/User`

---

### Obtener usuario por ID

**GET** `/api/User/{id}`

---

### Actualizar usuario

**PUT** `/api/User/{id}`

**Body (JSON):**

```json
{
  "id": 0,
  "username": "string",
  "email": "string",
  "role": "Admin" | "User"
}
```

---

### Eliminar usuario

**DELETE** `/api/User/{id}`

---

## Uso del Token en Frontend

Enviar el token en cada request que requiera autenticación:

```json
fetch(url, {
  method: "GET",
  headers: {
    "Authorization": "Bearer <TOKEN>"
  }
})
```

---

## Notas Finales

- `Student` es una tabla de registro simple.
- El **rol** determina el alcance del usuario.
- Un **Admin** puede administrar todo.
- Un **User** solo maneja estudiantes.

---

![Casos de uso Api](./Assets/Images/XPJ1QiCm38RlVWgHqtOesTXzD6oX30eBJRAxYRNCpB63ey1AoRlFQSlgjdO78GJzdwHV4cGv4BSqTegH98MG5M-mCGe7s4AkHM-afW4My24T22lKQBbYRFYMkkUMTs2n8QvRXILjNRurenOeF43W9nyLLVX3cPAjxb0JDEw5rgTw2OzF3upzrOF4UKbWnm3SuuT7-e8NeyaQfT0U482xUqYAzxA2bFSodQ5qRLgfavYwmTODvGeuO6BdFK.png)

Fin de documento.

# 📘 School Management API v1

API para la gestión de **usuarios** y **estudiantes**.  
Permite el registro, autenticación y administración de estudiantes y usuarios (roles).

---

## 🌐 Base URL

```
http://localhost:5268
```

 ¡¡ **ANTES DE DESPLIEGUE** !!

---

## 🔐 Autenticación (`/api/auth/Auth`)

### **POST** `/api/auth/Auth/login`

Inicia sesión con correo y contraseña.

#### 🧾 Request body
```json
{
  "email": "string",
  "password": "string"
}
```

#### 📤 Response (200 OK)
- Devuelve un **token JWT** que debe incluirse en los headers de las siguientes peticiones:
  ```
  Authorization: Bearer <token>
  ```

#### ❌ Errores posibles
| Código | Descripción |
|--------|--------------|
| 400 | Bad Request |
| 401 | Unauthorized |
| 500 | Internal Server Error |

---

### **POST** `/api/auth/Auth/register`

Registra un nuevo usuario con rol.

#### 🧾 Request body
```json
{
  "username": "string",
  "email": "string",
  "password": "string",
  "role": "string"
}
```

#### 📤 Response
| Código | Descripción |
|--------|--------------|
| 200 | OK |
| 400 | Bad Request |
| 500 | Internal Server Error |

---

## 👨‍🎓 Student (`/api/Student`)

### **GET** `/api/Student`

Obtiene la lista de todos los estudiantes.  
**Requiere autenticación (Bearer token)**

#### 📤 Response (200 OK)
```json
[
  {
    "id": 1,
    "firstName": "string",
    "lastName": "string",
    "email": "string"
  }
]
```

---

### **POST** `/api/Student`

Crea un nuevo estudiante.

#### 🧾 Request body
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

#### 📤 Response
| Código | Descripción |
|--------|--------------|
| 200 | OK |

---

### **GET** `/api/Student/{id}`

Obtiene los datos de un estudiante por su ID.

#### 🔸 Parámetros
| Nombre | Tipo | Ubicación | Requerido |
|---------|------|------------|------------|
| id | integer | path | ✅ |

#### 📤 Response
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

---

### **PUT** `/api/Student/{id}`

Actualiza la información de un estudiante.

#### 🧾 Request body
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

---

### **DELETE** `/api/Student/{id}`

Elimina un estudiante por su ID.

#### 🔸 Parámetros
| Nombre | Tipo | Ubicación | Requerido |
|---------|------|------------|------------|
| id | integer | path | ✅ |

---

## 👤 User (`/api/User`)

### **POST** `/api/User/register`

Crea un nuevo usuario.

#### 🧾 Request body
```json
{
  "username": "string",
  "email": "string",
  "password": "string",
  "role": "string"
}
```

---

### **POST** `/api/User/login`

Inicia sesión de usuario (retorna token JWT).

#### 🧾 Request body
```json
{
  "email": "string",
  "password": "string"
}
```

---

### **GET** `/api/User`

Obtiene todos los usuarios (requiere rol autorizado).

---

### **GET** `/api/User/{id}`

Obtiene la información de un usuario por ID.

#### 🔸 Parámetros
| Nombre | Tipo | Ubicación | Requerido |
|---------|------|------------|------------|
| id | integer | path | ✅ |

---

### **PUT** `/api/User/{id}`

Actualiza la información de un usuario.

#### 🧾 Request body
```json
{
  "id": 0,
  "username": "string",
  "email": "string",
  "role": "string"
}
```

---

### **DELETE** `/api/User/{id}`

Elimina un usuario por su ID.

#### 🔸 Parámetros
| Nombre | Tipo | Ubicación | Requerido |
|---------|------|------------|------------|
| id | integer | path | ✅ |

---

## 📦 Esquemas (DTOs)

| Nombre | Campos |
|--------|---------|
| `StudentCreateDto` | firstName, lastName, email |
| `StudentUpdateDto` | firstName, lastName, email |
| `UserDto` | id, username, email, role |
| `UserLoginDto` | email, password |
| `UserRegisterDto` | username, email, password, role |

---

## ⚙️ Notas para Frontend

- Todas las peticiones protegidas requieren incluir el **token JWT** en el header:
  ```
  Authorization: Bearer <token>
  ```
- Las rutas `/User` y `/Student` usan formato **JSON**.
- Los roles pueden usarse para definir permisos en el frontend según el `role` recibido al iniciar sesión.
- Endpoint del Swagger:  
  [http://localhost:5268/swagger/index.html](http://localhost:5268/swagger/index.html)

---

© 2025 School Management API
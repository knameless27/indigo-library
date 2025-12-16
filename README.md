# 📚 Indigo Library – Backend (.NET 8)

Backend desarrollado en **.NET 8 con C#** para automatizar el proceso de **préstamo de libros** en una biblioteca, cumpliendo reglas de negocio específicas según el tipo de usuario.

Este proyecto fue construido como **prueba técnica**, aplicando principios de **Clean Architecture, SOLID y Clean Code**, con énfasis en mantenibilidad, testabilidad y buenas prácticas profesionales.

---

## 🚀 Tecnologías

* **.NET 8**
* **ASP.NET Core Web API**
* **C#**
* **Entity Framework Core (InMemory)**
* **xUnit + Moq** (pruebas unitarias)
* **Swagger / OpenAPI**

---

## 🧱 Arquitectura

El proyecto sigue **Clean Architecture**, separando responsabilidades en capas:

```
indigoLibrary
│
├── indigoLibrary.API            → Controllers, Middlewares
├── indigoLibrary.Application    → DTOs, Interfaces, Services (Lógica de negocio)
├── indigoLibrary.Domain         → Entidades, Enums, Reglas de dominio
├── indigoLibrary.Infrastructure → EF Core, Repositories, DbContext
├── indigoLibrary.Tests          → Pruebas unitarias (xUnit)
```

### Principios aplicados

* Separación de responsabilidades
* Inversión de dependencias
* Lógica de negocio aislada de infraestructura
* Código asincrónico
* Pruebas sin acceso a base de datos real

---

## 📦 Modelo de Dominio

### Entidades principales

#### 📘 Libro

* `Id`
* `Isbn`
* `Titulo`
* `Autor`
* `CantidadDisponible`

#### 📄 Préstamo

* `Id`
* `Isbn`
* `IdentificacionUsuario`
* `TipoUsuario`
* `FechaMaximaDevolucion`
* `Estado`

---

## 👤 Tipos de Usuario

| Código | Tipo     | Días préstamo |
| ------ | -------- | ------------- |
| 1      | Afiliado | 10 días       |
| 2      | Empleado | 8 días        |
| 3      | Invitado | 7 días        |

📌 *Los días se calculan excluyendo sábados y domingos y usando la zona horaria local del servidor.*

---

## 📜 Reglas de Negocio

* Un **usuario invitado** solo puede tener **un préstamo activo**
* Un libro **no puede prestarse** si su cantidad disponible es `0`
* El préstamo cuenta el **día actual** como día hábil
* Validaciones de dominio mediante **DataAnnotations**
* Estados del préstamo manejados por **enum**

---

## 🔌 Endpoints

### 📘 Crear Libro

`POST /api/books`

```json
{
  "isbn": "978-8432225631",
  "titulo": "Clean Code",
  "autor": "Robert C. Martin",
  "cantidad": 5
}
```

---

### 📄 Crear Préstamo

`POST /api/prestamo`

```json
{
  "isbn": "978-8432225631",
  "identificacionUsuario": "ABC123",
  "tipoUsuario": 1
}
```

📌 Errores controlados con HTTP 400 y mensajes descriptivos.

---

### 🔍 Consultar Préstamo

`GET /api/prestamo/{id}`

Respuesta exitosa:

```json
{
  "id": "guid",
  "isbn": "978-8432225631",
  "identificacionUsuario": "ABC123",
  "tipoUsuario": 1,
  "fechaMaximaDevolucion": "2025-01-20"
}
```

---

## 🧪 Pruebas Unitarias

Las pruebas se encuentran en el proyecto `indigoLibrary.Tests` y cubren:

* Usuario invitado con préstamo activo
* Libro sin stock disponible
* Cálculo correcto de fechas de devolución
* Consulta de préstamo inexistente

Ejecutar tests:

```bash
dotnet test
```

---

## ▶️ Ejecución del Proyecto

### Requisitos

* .NET SDK 8

### Ejecutar API

```bash
dotnet restore
dotnet run --project indigoLibrary.API
```

Swagger:

```
https://localhost:{puerto}/swagger
```

---

## 🗄️ Base de Datos

* **EF Core InMemory**
* Persistencia mientras la aplicación esté en ejecución
* No requiere configuración adicional

---

## 📐 Diagrama Entidad–Relación

📎 *El diagrama ER se adjunta en el repositorio como imagen.*

---

## ✨ Extras Implementados

* Middleware global de manejo de errores
* Swagger documentado
* Código 100% async
* Validaciones de dominio

---

## 👨‍💻 Autor

**Andrés González**
Backend / Fullstack Developer

---

## 📄 Licencia

Proyecto desarrollado con fines educativos y de evaluación técnica.

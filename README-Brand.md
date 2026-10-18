# Funcionalidad Brand

## 1. Descripción

Se incorporó la funcionalidad **Brand (Marca)** al proyecto Clean Architecture. Esta funcionalidad permite registrar marcas y consultar una marca por su identificador.

La entidad Brand contiene los datos de la marca, como su nombre, país y año de fundación. Además, se integra con la persistencia mediante Entity Framework Core.

## 2. Reglas de negocio

La entidad Brand cumple con las siguientes reglas:

- El nombre es obligatorio y puede tener hasta 100 caracteres.
- El país es obligatorio.
- El año de fundación debe ser igual o posterior a 1800 y no puede superar el año actual.
- La marca se identifica mediante `BrandId`.
- El identificador utilizado para consultar una marca debe ser mayor que cero.

## 3. Endpoints

### 3.1. Crear una marca válida

**Método:** `POST`

**Ruta:** `/Brands`

**Body de la solicitud:**

```json
{
  "name": "Marca de ejemplo",
  "country": "Argentina",
  "foundedYear": 2020
}
```

**Resultado esperado:** `201 Created`. La marca se crea correctamente.

### 3.2. Consultar una marca existente

**Método:** `GET`

**Ruta:** `/Brands/1`

**Resultado esperado:** `200 OK`, si existe una marca con ese identificador.

### 3.3. Consultar una marca inexistente

**Método:** `GET`

**Ruta:** `/Brands/999`

**Resultado esperado:** `404 Not Found`, porque no existe una marca con ese identificador.

### 3.4. Crear una marca con datos inválidos

**Método:** `POST`

**Ruta:** `/Brands`

**Body de la solicitud:**

```json
{
  "name": "",
  "country": "Argentina",
  "foundedYear": 2020
}
```

**Resultado esperado:** `400 Bad Request`, porque el nombre es obligatorio.

## 4. Pruebas realizadas

Se realizaron pruebas de los endpoints mediante Bruno para verificar los siguientes casos:

- Creación de una marca con datos válidos.
- Consulta de una marca existente.
- Consulta de una marca inexistente.
- Intento de creación de una marca con datos inválidos.

# CoffeeMaker

- David González Villanueva C13388

## Descripción

CoffeeMaker es un sistema web completo de una máquina expendedora de café. Permite a los usuarios seleccionar diferentes tipos de café, realizar pagos en monedas, y recibir cambio de forma automática. El sistema está dividido en un frontend moderno con Vue.js y un backend robusto con ASP.NET Core.

---

## Arquitectura del Proyecto

El proyecto está organizado en dos partes principales:

### 📁 **Frontend** (`/frontend/frontendcoffee`)
Aplicación web interactiva construida con **Vue 3**, **Vuetify** y **Vuex**.

### 📁 **Backend** (`/backend/CoffeeMaker`)
API REST desarrollada con **ASP.NET Core 8** siguiendo el patrón arquitectónico en capas.

---

## 🎨 Frontend - Características Detalladas

### Tecnologías
- **Vue 3**: Framework progresivo para interfaces de usuario
- **Vue Router 4**: Enrutamiento
- **Vuex 4**: Gestión centralizada del estado
- **Vuetify 3**: Componentes UI Material Design
- **Axios**: Cliente HTTP para comunicación con el backend

### Estructura de Componentes

#### 1. **HomePage** (`src/view/HomePage.vue`)
- Página principal que organiza la interfaz en dos columnas:
  - **Columna izquierda (70%)**: Listado de cafés disponibles
  - **Columna derecha (30%)**: Resumen de selección y pagos

#### 2. **CoffeeList** (`src/components/CoffeeList.vue`)
- Muestra todos los cafés disponibles en cards
- Cada card contiene:
  - Imagen del café
  - Nombre y precio en colones (₡)
  - Stock disponible
  - Botones +/- para seleccionar cantidad
  - Botón "Agregar al pedido" para añadir al carrito
- Obtiene los datos del backend mediante GET a `/api/Coffee`
- Inicializa la cantidad en 0 para cada café

#### 3. **CoffeeSelection** (`src/components/CoffeeSelection.vue`)
- Panel lateral que muestra:
  - Lista de cafés seleccionados con sus cantidades
  - Cálculo automático del total a pagar
  - Botón "Realizar Pago" para abrir el diálogo de pago
- Accede al estado global de Vuex para obtener los cafés seleccionados

#### 4. **PaymentInput** (`src/components/PaymentInput.vue`)
- Diálogo modal para el proceso de pago
- Botones para seleccionar monedas de ₡1000, ₡500, ₡100, ₡50 y ₡25
- Muestra:
  - Monto total pagado en tiempo real
  - Total a pagar
  - Desglose de monedas seleccionadas
- El botón "Pagar" se habilita cuando el pago es suficiente
- Envía POST a `/api/Purchase` con:
  - Lista de cafés seleccionados
  - Desglose de monedas pagadas
- Procesa la respuesta y muestra el diálogo de cambio

#### 5. **ChangeDisplay** (`src/components/ChangeDisplay.vue`)
- Diálogo que muestra el resultado de la transacción:
  - Monto de la compra
  - Monto pagado
  - Cambio total
  - Desglose del cambio (monedas y cantidades)
  - Mensaje de confirmación o error
- Permite cerrar y actualizar la interfaz después de la compra

### Gestión de Estado (Vuex)

`src/store.js` maneja:

```javascript
state: {
  selectedCoffees: [] // Cafés seleccionados con cantidad
}

mutations: {
  ADD_TO_CART       // Añade o actualiza cantidad de café
  CLEAR_CART        // Limpia el carrito después de compra exitosa
}
```

---

## 🖥️ Backend - Características Detalladas

### Tecnologías
- **ASP.NET Core 8**: Framework web de alto rendimiento
- **C# 12**: Lenguaje de programación
- **Swagger/OpenAPI**: Documentación interactiva de APIs

### Arquitectura en Capas

#### 1. **Presentation Layer** (Controladores)

**CoffeeController** (`Presentation/Controllers/CoffeeController.cs`)
- Endpoint `GET /api/coffee`
- Retorna lista de cafés disponibles con sus propiedades

**PurchaseController** (`Presentation/Controllers/PurchaseController.cs`)
- Endpoint `POST /api/purchase`
- Recibe solicitud de compra (`PurchaseRequest`)
- Retorna respuesta de cambio (`ChangeResponse`)

**Modelos de Request/Response:**
- `PurchaseRequest`: Contiene cafés seleccionados y monedas pagadas
- `ChangeResponse`: Retorna cambio, desglose y mensaje

#### 2. **Application Layer** (Servicios)

**CoffeeService** (`Application/Services/CoffeeServices.cs`)

Métodos:
- `GetAvailableCoffees()`: Retorna lista completa de cafés
- `UpdateCoffeeStock(List<Coffee>)`: Reduce el stock después de compra
- `CalculateTotalCost(List<Coffee>)`: Calcula el costo total de una orden

Datos iniciales:
- **Americano**: ₡950, Stock: 10
- **Capuchino**: ₡1200, Stock: 8
- **Latte**: ₡1350, Stock: 10
- **Mocachino**: ₡1500, Stock: 15

**PaymentService** (`Application/Services/PaymentServices.cs`)

Métodos principales:
- `MakePurchase(PurchaseRequest)`: Orquesta todo el proceso de compra

Métodos privados:
- `ValidateData()`: Valida que la solicitud tenga datos válidos
- `CalculateTotalPayment()`: Suma monedas pagadas
- `UpdateCoins()`: Actualiza el inventario de monedas de la máquina
- `CalculateChange()`: Calcula el cambio óptimo usando algoritmo greedy
- `UpdateCoffeeStock()`: Reduce stock después de compra

Stock de monedas inicial:
- ₡500: 20 unidades
- ₡100: 30 unidades
- ₡50: 50 unidades
- ₡25: 25 unidades

#### 3. **Domain Layer** (Entidades)

**Coffee** (`Domains/Entities/Coffee.cs`)
```
- Name: string
- Price: int (en colones)
- Stock: int (cantidad disponible)
- Quantity: int (cantidad solicitada)
```

**Coin** (`Domains/Entities/Coin.cs`)
```
- Value: int (denominación)
- Quantity: int (cantidad)
```

---

## 🔄 Flujo de la Aplicación

### 1. **Selección de Café**
1. Usuario abre la aplicación
2. Frontend solicita GET `/api/Coffee`
3. Se muestran 4 tipos de café con botones +/-
4. Usuario selecciona cantidad y presiona "Agregar al pedido"
5. Café se añade al estado Vuex

### 2. **Pago**
1. Usuario presiona "Realizar Pago"
2. Se abre diálogo con opciones de monedas
3. Usuario selecciona monedas
4. Se habilita botón "Pagar" cuando monto ≥ total
5. Frontend POST a `/api/Purchase` con:
   - Cafés seleccionados
   - Monedas pagadas

### 3. **Procesamiento en Backend**
1. Backend valida datos
2. Calcula total de pago y total de costo
3. Verifica si hay suficiente dinero
4. Calcula cambio óptimo
5. Actualiza stock de cafés
6. Actualiza stock de monedas
7. Retorna respuesta con desglose de cambio

### 4. **Confirmación**
1. Frontend recibe respuesta
2. Muestra diálogo con desglose de cambio
3. Limpia carrito (CLEAR_CART)
4. Usuario puede cerrar y volver a comprar

---

## ✅ Testing

El proyecto incluye pruebas unitarias en `CoffeeMakerTests/`:

### **CoffeeServiceTests** (`UnitTestCoffee.cs`)
- ✓ Obtener lista de cafés
- ✓ Actualizar stock correctamente
- ✓ Calcular costo total

### **PaymentServiceTests** (`UnitTestPayment.cs`)
- ✓ Compra exitosa con cambio
- ✓ Datos inválidos
- ✓ Pago insuficiente
- ✓ Imposibilidad de dar cambio

---

## Mockup

![MockUp](https://github.com/user-attachments/assets/8ba600c9-a126-4c0b-b3c4-d32fec43c80b)

## UML

![UML](https://github.com/user-attachments/assets/da7d117b-ff5d-45d4-8008-aa7827ceec9c)

---

## 🚀 Cómo Ejecutar

### Backend
```bash
cd backend/CoffeeMaker
dotnet run --launch-profile https
# Acceder a https://localhost:7058/swagger
```

### Frontend
```bash
cd frontend/frontendcoffee
npm install
npm run serve
# Acceder a http://localhost:8080
```

---

## 📦 Dependencias Principales

### Frontend
- vue@^3.5.13
- vuetify@^3.7.4
- vuex@^4.1.0
- axios@^1.7.8

### Backend
- .NET 8.0
- Swashbuckle.AspNetCore 6.6.2

---

## 🔐 Configuración CORS

El backend permite solicitudes desde `http://localhost:8080` (frontend en desarrollo).

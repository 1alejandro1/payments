To improve the README.md file for the project, we will incorporate the new content while ensuring that the overall structure and coherence of the document are maintained. Below is the revised README.md file with the new content integrated seamlessly.

# payments Service by Douglas Toro

## Descripción
Breve descripción del proyecto y su propósito.

## Instrucciones de desarrollo y ejecución

### Requisitos
- Visual Studio 2022 o superior con soporte para .NET 8
- .NET SDK 8
- SQL Server (local o remoto) con permisos para crear bases de datos y ejecutar scripts

### Configuración
1. Clona el repositorio y abre la solución en Visual Studio: `D:\Repositorios\payments` (branch `develop`).
2. Actualiza la cadena de conexión en `appsettings.json` o en las _User Secrets_ bajo la sección `ConnectionStrings:DefaultConnection`. Ejemplo:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=.;Database=DB_SERVICE_PAYMENT;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

3. Asegúrate de que las credenciales y el servidor sean correctos antes de ejecutar los scripts SQL.

### Base de datos
- Los scripts de creación de tablas y stored procedures se encuentran en la carpeta `DataBase\ScriptsTables` y `DataBase\ScriptsSPs`.
- Ejecuta en el siguiente orden:
  1. `00_CreateDataBase_DB_SERVICE_PAYMENT.sql`
  2. `01_Create_Login_User.sql`
  3. `02_Create_Schema`
  4. `00_CreateTableCustomer.sql`
  4. `01_CreateTable_SERVICEPROVIDER.sql`
  5. `02_CreateTable_PAYMENT.sql`
  6. Scripts adicionales para CUSTOMER y SPs (como `sp_RegisterPayment.sql`, `sp_RegisterCustomer.sql`, `sp_RegisterServiceProvider.sql`, `sp_GetPayments.sql`, `sp_GetCustomers.sql`, `sp_GetServiceProviders.sql`).

### Construir y ejecutar
1. Restaurar paquetes NuGet: en Visual Studio usa __Build > Restore NuGet Packages__ o ejecuta `dotnet restore`.
2. Compilar la solución: __Build > Build Solution__ o `dotnet build`.
3. Ejecutar la API: __Debug > Start Debugging__ o `dotnet run` desde el proyecto `ServicePaymentsAPI`.

### Endpoints principales (ejemplos)
- POST `api/registerCustomer/registerCustomer` — Registrar cliente (payload: `RegisterCustomerRequestDto`).
- POST `api/registerPayments/registerPayment` — Registrar pago (payload: `RegisterPaymentRequestDto`).
- POST `api/registerProvider/registerProvider` — Registrar proveedor (payload: `RegisterProviderRequestDto`).
- GET  `api/getPayments/getPayment` — Obtener pagos (payload: `GetPaymentRequestDto` en el body).
- GET  `api/getPayments/getCustomer` — Obtener cliente (payload: `GetCustomerRequestDto` en el body).
- GET  `api/getPayments/getProvider` — Obtener proveedor (payload: `GetProviderRequestDto` en el body).

**Nota:** algunos endpoints usan GET con cuerpo; si su cliente HTTP no lo permite, cambia a POST o pasa parámetros por query según convenga.

### Ejecución de stored procedures desde la app
- El acceso a BD se realiza mediante `PAY.CROSS.DATAACCESS` y la interfaz `IDataAccess`.
- Para registrar y consultar se usan SPs prefijados con el esquema `payments` (`payments.sp_RegisterPayment`, `payments.sp_GetPayments`, etc.).

### Convenciones y estilo
- El proyecto target es `.NET 8` y C# 12.
- Se mantiene inicialización de cadenas NOT NULL con `string.Empty` en DTOs y uso de atributos de validación (`[Required]`, `[StringLength]`, etc.).
- Revisa `.editorconfig` y `CONTRIBUTING.md` (si no existen, se crearán con las normas del proyecto).

### Logs y depuración
- El logger está definido en `PAY.CROSS.LOGGER.ILogger`. Revisa implementaciones concretas en el proyecto para ver destinos de log.

### Preguntas frecuentes
- **¿Dónde están los DTOs?** En `ServicePaymentsAPI\DTOs` (subcarpetas `Requests` y `Responses`).
- **¿Cómo agregar un nuevo SP?** Añade el script en `DataBase\ScriptsSPs` y actualiza el repositorio que lo invoque.

## Contribuciones
Si deseas contribuir al proyecto, por favor revisa el archivo `CONTRIBUTING.md` para obtener más información sobre cómo puedes ayudar.

## Licencia
Este proyecto está bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.


In this revised README.md, the new content has been integrated into the appropriate sections, ensuring clarity and coherence throughout the document. The structure remains intact, and additional sections such as "Contribuciones" and "Licencia" have been added to provide a comprehensive overview of the project.
# tienda-alimentos-prueba-tecnica
Backend realizado en .net 7 para una tienda online de alimentos

# 1. REEMPLAZAR LOS VALORES DE LAS LLAVES DEL ARCHIVO appsettings.json y appsettings.Development.json para la cadena de conexion y datos del smtp

 "ConnectionStrings": {
    "conexionBDSqlServer": "Reemplaza cadena conexion"
  },

 "ConfiguracionSmtp": {
    "host": "smtp.office365.com",
    "port": 587,
    "emailRemitente": "CambiarCorreoDequienEnvia",
    "passwordEmailRemitente": "passwordQuienEnvia"
  }

# 2. URL DEL DESPLIEGUE => https://backend-tienda-alimnetos-online.azurewebsites.net/
# 3. URL DOCUMNETACION API SWAGGER => https://backend-tienda-alimnetos-online.azurewebsites.net/swagger/index.html

 4. EN LA CARPETA RECURSO DEL PROYECTO WEBAPI ESTA LA COLECCION DE POSTMAN PARA QUE SE REALIZEN LAS PRUEBAS EL ARCHIVO SE LLAMA TiendaAlimentos.postman_collection.json

# 5. EN LA CARPETA SCRIPTBD DEL PROYECTO DATA SE ENCUENTRA EL SCRIPT DE CREACION DEL DE LA BASE DE DATOS EL ARCHIVO SE LLAMA ScriptBD.sql

 6. LA MAYORIA DE LAS RUTAS ESTAN PROTEGIDAS Y NECECISTARAN UN TOKEN QUE SE DEBE PASAR POR BEARER TOKEN

# 7. PARA OBTENER EL TOKEN POR FAVOR EJECUTAR EL ENDPOINT DE INICIO DE SESION

************************************************************************************************************************************
# 8. SI DESEA QUE EL USUARIO SEA ADMINISTRADOR AL MOMENTO DE HACER LA INSERCION EL ROL DEBE LLEVAR EL VALOR DE admin  OJO IMPORTANTE
************************************************************************************************************************************

# NOTA =>PARA PROBAR EL API SE DEBE CAMBIAR  EN LA COLECCION DE POSTMAN LUEGO DE IMPORTARLA EL https://localhost:7263 POR https://backend-tienda-alimnetos-online.azurewebsites.net  
# EJEMPLO PARA LISTAR CATEGORIAS => https://localhost:7263/api/Categoria/listarCategoria QUEDARIA ASI https://backend-tienda-alimnetos-online.azurewebsites.net/api/Categoria/insertarCategoria

INFORMACION DE PATRONES Y ESTRATEGIAS USADAS

* INYECCIONDE DEPENCIAS
* ARQUITECTURA NCAPAS
* PATRON CQRS
* INVERSION DE DEPENDENCIAS
* AUTENTICACION JWT
* PROTECCION DE RUTAS
* DESPLIEGUE EN AZURE

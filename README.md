# Programacion-Avanzada-2023
Sistema de inventario en .Net Core


### Instalación

```Iniciar las migraciones
Crear una base de datos llamada Proyecto_2023 o bien cambiar la linea 9 del appsettings.json en los folders ui y api, sin embargo, si usted cambia este archivo debe excluirlo del commit.
Ir al package manager console, seleccionar persistence y escribir en el command prompt lo siguiente Update-Database -Context ApplicationDbContext, luego seleccionar Identity y correr  Update-Database -Context ApplicationIdentityDbContext 

Si usted crea otro modelo debe crear la migracion con Add-Migration <NombreModelo>ToDb (debe cambiar NombreModelo por el nombre del modelo que creaste) si solicita el context entonces se le agrega -Context ApplicationDbContext
```


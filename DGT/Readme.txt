Comentarios

  * He creado una BBDD local para el almacenamiento de datos. No he creado la relaciones entre tablas por falta de tiempo
  * Se carga swagger para probar la API
  * Uso el patrón de Unit of Work y repository para la gestión de las tablas
  * Tengo un proyecto con los Modelos de la BBDD y el Datacontext, otro con el acceso a datos y por último la API
  * No uso una capa de negocio. Donde trabajaba los cambios eran muy continuos, y esa capa era más un impedimento
  * Tampoco he hecho pruebas unitarias por el mismo motivo
  * No he creado ningún log. Se escapa del alcance del proyecto
  * La logica la suelo hacer con procedmientos almacenados y no con linq
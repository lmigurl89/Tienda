Indicaciones para ejecutar el API

1. Crear la Base Datos. Se recomienda usar el nombre "tienda", en caso de emplear otro nombre
se debe modificar el nombre de la base datos en la cadena de conexion en el archivo appsettings.json

2. Ejecutar el script contenido en el archivo script.sql

3. Una vez ejecutada la aplicación se puede logiar mediante los usuarios 'administrador', 'vendedor' o
'cliente' usando en todos los casos la contraseña 'Abc123456*'

4. Una vez logiado satisfactoriamente un usuario es necesario copiar el token de autenticación y el mismo 
debe ser insertado en la opcion Authorize antecedido de la palabra Bearer seguido de un espacio

Ej. Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluaXN0cmFkb3IiLCJqdGkiOiJhMDM3MD
c3ZC0xOWE0LTQ0MzctYTliYS1mMTdiM2JkNDNkOGQiLCJBZG1pbmlzdHJhZG9yIjoiQWRtaW5pc3RyYWRvciIsImV4cCI6MTg1NTU0OTMz
OCwiaXNzIjoidGllbmRhLmNvbSIsImF1ZCI6InRpZW5kYS5jb20ifQ.JjeZfA9P1mZM-QCVrX9LHMIa6_RkzKHryBUDAaussnA

5. Se debe tener en cuanta que al editar un elemento debe coincidir el Id tanto en el campo como en el JSON

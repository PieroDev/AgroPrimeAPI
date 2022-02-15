<div id="top"></div>
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Tabla de contenido</summary>
  <ol>
    <li>
      <a href="#Acerca-del-proyecto">Acerca del proyecto</a>
    </li>
    <li>
      <a href="#Comenzando">Comenzando</a>
      <ul>
        <li><a href="#Instalación">Instalación</a></li>
      </ul>
    </li>
    <li><a href="#Contacto">Contacto</a></li>
    <li><a href="#Documentación">Documentación</a></li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## Acerca del proyecto

En este ejercicio se pidió crear una API con .NET Core con las siguientes requerimientos:

* Se incluya un CRUD a una entidad Trabajador siendo que el mismo posee Primer Nombre, Segundo Nombre, Primer Apellido, Segundo Apellido, Número de Documento y Fecha de Nacimiento.
* Todos los campos son requeridos excepto el Segundo nombre.
* El servicio de obtener todos los trabajadores debe entregar los nombres y apellidos enuna sola propiedad llamada Nombre Completo.
* El Número de Documento es único y no se edita.
* Los trabajadores deben tener una edad igual o superior a los 18 años.
* Cree una migración que permita la creación de tabla Trabajador.

Además, se añadio el siguiente requerimiento: 

* Usando la Base de Datos antes creada cree un procedimiento almacenado que me entregue dado una fecha todos los trabajadores nacidos antes de dicha fecha. (El código del procedimiento almacenado puede incluirlo en el proyecto de la API como una migración o como un archivo .sql)

<p align="right">(<a href="#top">Volver al inicio</a>)</p>

<!-- GETTING STARTED -->
## Comenzando

### Instalación

1. Clona el repositorio
   ```sh
   git clone https://github.com/PieroDev/AgroPrimeAPI.git
   ```

2. Verificar el string de conexión en AgroPrimeContext.cs y editarla si es necesario para indicar el servidor al cual quieres conectar
   La string a editar es la siguiente en AgroPrimeContext.cs :

   ```sh
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(local);Database=AgroPrimeAPI;Integrated Security=True");
            }
        }
   ```

3. Para crear la tabla de Trabajador, ejecute el siguiente comando desde Package Manager Console
   ```sh
   update-database
   ```
   Este comando creará la base de datos AgroPrimeAPI y su tabla de trabajador, además de generar el procedimiento de almacenado solicitado en esta misma base.

4. Para iniciar la aplicación ejecutar:
```sh
   dotnet run
   ```

4. Ahora podrás revisar la api desde [http://localhost:5001](http://localhost:5000)


<p align="right">(<a href="#top">Volver al inicio</a>)</p>


<!-- CONTACT -->
## Contacto

Piero Zúñiga - pierozudev@gmail.com

Link proyecto: [https://github.com/PieroDev/AgroPrimeAPI](https://github.com/PieroDev/AgroPrimeAPI)

<p align="right">(<a href="#top">Volver al inicio</a>)</p>

<!-- Documentation -->

## Documentación

Para facilitar el testeo de la API, se ha creado una pequeña documentación en postman junto a distintos casos de uso establecidos.

Link a Documentación: [https://documenter.getpostman.com/view/13100703/UVeNo3SY](https://documenter.getpostman.com/view/13100703/UVeNo3SY)




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/othneildrew/Best-README-Template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/othneildrew/Best-README-Template/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/othneildrew/Best-README-Template/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/othneildrew/Best-README-Template/issues
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=for-the-badge
[license-url]: https://github.com/othneildrew/Best-README-Template/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/othneildrew
[product-screenshot]: images/screenshot.png
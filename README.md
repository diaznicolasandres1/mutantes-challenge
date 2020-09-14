# Mutantes-challenge
```
https://docs.diaznicolasandres.com/melichallenge
https://api.diaznicolasandres.com/melichallenge/api
```
![.NET Core](https://github.com/diaznicolasandres1/mutantes-challenge/workflows/.NET%20Core/badge.svg) [![Codacy Badge](https://app.codacy.com/project/badge/Grade/ef0d090df39c4aa1b4e2d8334a0be4a9)](https://www.codacy.com?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=diaznicolasandres1/mutantes-challenge&amp;utm_campaign=Badge_Grade)



## Tabla de contenidos
Este challenge es parte del proceso de seleccion en la empresa Mercado Libre.
* [Consignas](#consignas)   
* [Resolucion](#resolucion)

* [DocumentacionApi](#documentacionapi)
* [Instalacion](#instalacion)





## Consignas
### Parte 1
Se pide crear un proyecto en el cual contenga un metodo / funcion que en base a un adn recibido por parametro se pueda determinar si es un humano o mutante.

- Crear un metodo con la siguiente firma: 
```c#
boolean isMutant(String[] dna);
```
 
Donde cada elemento de ``` String[] dna ``` corresponde a una fila de la matriz: <img src="https://latex.codecogs.com/gif.latex?A=\Re^{nxn}" /> , las cuales representan cada base nitrogenada del ADN
Ej:

![Alt text](https://github.com/diaznicolasandres1/mutantes-challenge/blob/master/Doc/Imagenes/matrices.png?raw=true  "Title")

Sabremos si es un humano o mutante, si encontramos mas de una secuencia de cuatro letras iguales, de forma oblicua, horizontal o vertical. Solo pueden ser posibles: (A,T,C,G)

### Parte 2

Crear una API REST, hostear esa API en un cloud computing libre (Google App Engine,Amazon AWS, etc).

Crear el servicio “/mutant/” en donde se pueda detectar si un humano es mutante enviando la secuencia de ADN mediante un HTTP POST con un Json el cual tenga elsiguiente formato:

```javascript
POST → /mutant/
{
“dna”:["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
}
```
En caso de verificar un mutante, debería devolver un **HTTP 200-OK**, en caso contrario un
**403-Forbidden**

### Parte 3

Anexar una base de datos, la cual guarde los ADN’s verificados con la API.
Solo 1 registro por ADN.

Exponer un servicio extra “/stats” que devuelva un Json con las estadísticas de las
verificaciones de ADN:
```javascript
{“count_mutant_dna”:40, “count_human_dna”:100: “ratio”:0.4}
```
Tener en cuenta que la API puede recibir fluctuaciones agresivas de tráfico (Entre 100 y 1
millón de peticiones por segundo).


## Resolucion

### Parte 1

El algoritmo se abordo de la siguiente manera:

 Recorrer la matriz de manera iterativa, al pararnos en cada coordenada analizamos su al rededor con un metodo recurisivo.
 Se lo resolvio de esta manera ya que nos permite dividir el problema en sub problemas mas sencillos, en este caso basto que cuando se realiza el primer apilamiento en el stack, hacer un chequeo de sus al rededores(diagonal superior, lado derecho, diagonal inferior, por debajo), y a partir del segundo apilamiento calcula desde que direccion fue llamado y recursivamente chequea sus al rededores en esa direccion.
 
 Se lo considera eficiente ya que en el peor de los casos la cantidad de llamadas por coordenada son pocas:  4 posibles direcciones, x 4 repeticiones de palabra. Es un numero infimo para la capacidad de ejecucion de nuestro problema.
 En el proyecto este algoritmo esta resuelto en: Mutantes.Core/Utilities/DnaAnalyzerAlgorithm.cs
 
 ### Parte 2 & 3.
 
El desarrollo de la API se abordo de la siguiente manera:
- Se utilizo la arquitectura: Clean architecture.

La regla clave detrás de la arquitectura limpia es: La regla de dependencia . La esencia de esto es simplemente que las dependencias están encapsuladas en cada "anillo" del modelo de arquitectura y estas dependencias solo pueden apuntar hacia adentro.

 Se mantiene la parte web y base de datos en las capas externas, ya que son las mas propopensas al cambio.
 
 En el proyecyo ***Mutantes.Infraestructure***:  Aquí definimos entidades de datos, acceso a bases de datos. Bajo el patron de diseño Repository. Utilizamos SQLServer y Entitiframework core.
 
 *Eric Evans define el patron Repository como un "mecanismo para encapsular el comportamiento de almacenamiento, obtención y búsqueda, de una forma similar a una colección de objetos*
  
 
 Por otro lado en ***Mutantes.CORE*** se implemtna la lógica de dominio, la menos propopensa a cambiar. Con esta arquitectura nuestra implementacion  no depende de frameworkos ni implementaciones externas. Se utiliza el patron de diseño Service layer pattern, manteniendo los objetos con unicas responsabilidades.
Ademas se desarrolla contra interfaces, el framework nos provee su propio container para utilizar  inyeccion de dependencias.

***Mutantes.Controller***: Aca se encuentran nuestros dos controladores Mutant y Stats, se encargan de expones nuestros metodos a travez del protocoloco HTTP. 
Estos controladores utilizan los services expuestos por la capa CORE.

Para tener una mejor performance usamos redis como base de datos cache, no tiene sentido volver a analizar el mismo adn una y otra vez si podemos almacenarlo en la cache y hacer que sea mas performante. Lo mismo ocurre cuando se quieren consultar los stats, si se analizo previamente un adn, se actualizo el valor y evitamos tener que hacer una consulta a la base de datos
 
 
<img src="https://github.com/diaznicolasandres1/mutantes-challenge/blob/master/Doc/Imagenes/cleanArchitecture.png?raw=true" width="40%">
<img src="https://github.com/diaznicolasandres1/mutantes-challenge/blob/master/Doc/Imagenes/clean2.png?raw=true width="20%">




## DocumentacionApi


 
<img src="https://github.com/diaznicolasandres1/mutantes-challenge/blob/master/Doc/Imagenes/Api.png?raw=true" width="40%">

La documentacion de los endpoits, request y response se encuentra en:
[Mutants-API-Swagger](https://api.diaznicolasandres.com/melichallenge/swagger)


```
http://api.diaznicolasandres.com/melichallenge/api/mutant

```
```
http://api.diaznicolasandres.com/melichallenge/api/stats
```


## Instalacion
Es necesario tener instalado   [NET CORE 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.402-windows-x64-installer)

1. Clonar el repo.
```sh
git clone https://github.com/diaznicolasandres1/mutantes-challenge.git
```

2. En la carpeta root buildear el proyecto
```sh
dotnet build

```
3. Nos movemos a la carpeta Mutantes.API
```sh
 cd Mutantes.API
 dotnet run
```

4. Abrir en un navegador
```
http://localhost:5001
```
5. Si queremos ejecutar unit tests, volvemos a la carpeta anterior
```sh
cd ..
cd Mutantes.Test\Mutantes.UnitTests
dotnet test
```
6. Ejecutar tests de integracion
```sh
cd ..
cd Mutantes.IntegrationTests
dotnet test



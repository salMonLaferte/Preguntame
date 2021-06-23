# Pregúntame
Pregúntame es un software pensado para servir como una herramienta de estudio para quienes busquen prepararse para exámenes con preguntas de opción múltiple o  que simplemente busquen memorizar teoría sobre algún tema a través de responder preguntas. 

El software, de manera aleatoria, toma preguntas contenidas en un archivo de texto (.txt) con determinado formato y selecciona al azar respuestas correctas e incorrectas para que el usuario identifique la respuesta correcta y revise su respuesta.

Estás preguntas pueden clasificarse en temas y el usuario puede elegir qué temas quiere estudiar durante sus sesiones. 

![image](https://user-images.githubusercontent.com/50729585/123034894-08c89500-d3b0-11eb-8159-ecb381f1d080.png)

# Manual de usuario. 
En el siguiente manual se especifican todas las opciones que ofrece el programa, para que el usuario pueda aprovecharlo al máximo. A medida que surjan actualizaciones tambien se actualizará este manual.

## Formato de las preguntas.
Las preguntas deben estar contenidas en archivos  .txt dentro de la carpeta “pregu” en la carpeta de instalación del programa. El formato para cada pregunta es el siguiente.
```
{
[Contenido de la pregunta]
[Opción incorrecta1|Opción Incorrecta2|Opción Incorrecta3|Opcion Incorrecta 4|Opcion Incorrecta 5]
[Opción correcta1|Opción correcta2|Opción correcta3]
[Tema] 
}
```
Nótese que:
* Las llaves “{“ y “}” marcan el inicio y el final de la pregunta.
* En primer lugar se escribe el contenido de la pregunta  entre corchetes “[Contenido de la pregunta]”.
* En segundo lugar se escriben entre corchetes las opciones incorrectas separadas por el carácter “|”.
* En tercer lugar, de igual manera, se escriben entre corchetes las opciones correctas separadas por el carácter “|”
* Por último se escribe el tema al cual pertenece la pregunta.

La pregunta anterior podrá ser visualizada en el programa de la siguiente manera.

![image](https://user-images.githubusercontent.com/50729585/123035852-b5efdd00-d3b1-11eb-8999-8d8e78450090.png)

El programa seleccionará al azar la opción correcta y las opciones incorrectas, el orden de dichas opciones también será al azar. Así que la pregunta anterior también podría aparecer de la siguiente manera.

![image](https://user-images.githubusercontent.com/50729585/123163724-a61adc00-d437-11eb-86ff-cdf1a04b6542.png)

Nótese que ahora la opción correcta que seleccionó el programa es “Opción correcta2” y corresponde al inciso B) en está ocasión. De esta manera el estudiante que practique con estas preguntas no memorizará incisos sino contenidos. La respuesta correcta a seleccionar no siempre es la misma y las opciones incorrectas a evaluar tampoco serán las mismas.

## Opciones del programa.

Para una explicación de las opciones del programa considere la siguiente pregunta:

```
{
[¿Cuál de los siguientes números es múltiplo de 6?]
[76|601|904|15|23]
[6|12|606|666|48]
[MAT]
}
```

![image](https://user-images.githubusercontent.com/50729585/123164066-11fd4480-d438-11eb-95c0-9ddaf838b744.png)

![image](https://user-images.githubusercontent.com/50729585/123164166-3527f400-d438-11eb-8047-828780b13a59.png)

### Total de opciones por pregunta y número de opciones correctas por pregunta.

La cantidad de opciones totales por pregunta y opciones correctas por pregunta puede establecerse dando click en “Opciones”, desde luego el número de opciones incorrectas será la diferencia del total de opciones y la cantidad de opciones correctas por pregunta.

![image](https://user-images.githubusercontent.com/50729585/123164269-5983d080-d438-11eb-80a6-23c3b42ff6c5.png)

Si en el ejemplo establecemos dos opciones correctas  y seis opciones por pregunta.

![image](https://user-images.githubusercontent.com/50729585/123164330-6b657380-d438-11eb-83b8-e3f4edc3835d.png)

La pregunta anterior podrá ser mostrada como: 

![image](https://user-images.githubusercontent.com/50729585/123164380-7c15e980-d438-11eb-93a4-0fe417e09a25.png)

En donde el inciso A) y C) son respuestas correctas y las otras cuatro opciones son respuestas incorrectas. 
El programa siempre intentará seleccionar el  número indicado de opciones correctas e incorrectas, sin embargo cuando la pregunta no tiene definidas suficientes respuestas correctas para seleccionar, el programa tomará todas las opciones correctas y completará el resto con opciones incorrectas. Consideremos la siguiente configuración:

![image](https://user-images.githubusercontent.com/50729585/123164438-8f28b980-d438-11eb-9b8f-b0d279bc6389.png)

Como la pregunta solo tiene definidas cinco opciones correctas, el programa nos seleccionará las cinco opciones correctas más cuatro incorrectas para mostrarnos nueve opciones en total como lo establecimos. El programa NO seleccionará siete opciones correctas y dos incorrectas pues es imposible según los datos de la pregunta.

![image](https://user-images.githubusercontent.com/50729585/123164518-ac5d8800-d438-11eb-882a-e94de389d377.png)

Ahora, sí establecemos el total de opciones en 15, como la pregunta no tiene suficientes opciones entonces mostrará todas las opciones correctas e incorrectas, en este caso 10.

### Número aleatorio de opciones correctas.

Si marcamos la opción “Número aleatorio de opciones correctas” entonces para cada pregunta visualizada se seleccionara un número entre 1 y “Total de opciones por pregunta” de opciones correctas.

### Modo de respuesta válida.

En el ejemplo anterior, si marcamos ambas opciones correctas y damos click en “revisar respuesta” el programa nos marcará respuesta correcta. Si solo marcamos una de varias respuestas correctas y damos “revisar respuesta” el programa marcará respuesta incorrecta a menos que tengamos seleccionado el “modo de respuesta válida” en “se selecciona al menos una opción correcta”.  

![image](https://user-images.githubusercontent.com/50729585/123165319-b16f0700-d439-11eb-863a-f4c829eb07a6.png)

### Repetir preguntas.

![image](https://user-images.githubusercontent.com/50729585/123165347-bb910580-d439-11eb-8827-9062b78a9680.png)

*	Si seleccionamos “No” ninguna pregunta se repetirá en la sesión actual.
*	Si seleccionamos “Sólo las respondidas incorrectamente” entonces una vez que respondamos bien una pregunta esta no volverá a aparecer en la sesión actual.
*	Si seleccionamos “Todas las preguntas se pueden repetir”  entonces cualquier pregunta se puede repetir en nuestra sesión cualquier número de veces.

### Colocar imágenes en una pregunta y en sus opciones.

Pregúntame tiene la opción de adjuntar una imagen junto con el contenido de la pregunta. Para ello coloque la imagen en la carpeta “pregu”. Dentro del contenido de la pregunta en el archivo de texto escriba al final el carácter “&” seguido del nombre de la imagen con su extensión. 

#### Ejemplo

```
{
[¿Cuál de las siguientes afirmaciones es verdadera? &conicas.jpg] 
[Las figuras 2 y 3 son circunferencias| La figura 1 es un elipse y la figura 2 una parábola | Las figuras 1 y 4 son parábolas, mientras que las figuras 2 y 3 son circunferencias]
[La figura 1 es una parábola y la figura 2 es una elipse| La figura 3 es una circunferencia y la figura 4 es una hipérbola]
[CONICAS]
} 
```

La siguiente imagen “conicas.jpg” deberá estar contenida dentro de la carpeta pregu.

![image](https://user-images.githubusercontent.com/50729585/123166080-9ea90200-d43a-11eb-885e-5a3279c5bb0a.png)

Así la pregunta se visualizará de la siguiente manera.

![image](https://user-images.githubusercontent.com/50729585/123166142-b2546880-d43a-11eb-86fc-bd4d6b2761ae.png)

Podemos poner la imagen “conicas.jpg” en subcarpetas de la carpeta “pregu” en ese caso deberemos escribir la ruta relativa a la carpeta pregu en el contenido de la pregunta. 
Por ejemplo, si nuestra imagen se encuentra en “pregu\imagenesDeMate\conicas.jpg” deberemos escribir en el contenido de la pregunta lo siguiente:

```
{
[¿Cuál de las siguientes afirmaciones es verdadera &imagenesDeMate\conicas.jpg] 
[Las figuras 2 y 3 son circunferencias| La figura 1 es un elipse y la figura 2 una parábola | Las figuras 1 y 4 son parábolas, mientras que las figuras 2 y 3 son circunferencias]
[La figura 1 es una parábola y la figura 2 es una elipse| La figura 3 es una circunferencia y la figura 4 es una hipérbola]
[CONICAS]
}
```

De manera similar también podemos adjuntar una imagen con cada opción añadiendo “&” + el nombre de la imagen y su extensión


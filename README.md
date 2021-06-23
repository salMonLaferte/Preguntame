# Pregúntame
Pregúntame es un software pensado para servir como una herramienta de estudio para quienes busquen prepararse para exámenes con preguntas de opción múltiple o  que simplemente busquen memorizar teoría sobre algún tema a través de responder preguntas. 

El software, de manera aleatoria, toma preguntas contenidas en un archivo de texto (.txt) con determinado formato y selecciona al azar respuestas correctas e incorrectas para que el usuario identifique la respuesta correcta y revise su respuesta.

Estás preguntas pueden clasificarse en temas y el usuario puede elegir qué temas quiere estudiar durante sus sesiones. 

![image](https://user-images.githubusercontent.com/50729585/123034894-08c89500-d3b0-11eb-8159-ecb381f1d080.png)

# Manual de usuario. 

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



# GuíaFormatting

## Generalidades

+ **Identación consistente:** Usar 4 espacios para identación, y 8 para continuaciones de línea
+ **Largo de línea:** Usar hasta 100 caracteres por línea, pero flexible para aumentar la legibilidad
+ **Espacios:**
    + Usar espacios alrededor de operadores (+, -, *, /, %, etc) `a + b / c * d`
    + Espacio antes de abrir un paréntesis o llave. 
    + Espacio después de cerrar un paréntesis, llave, coma. `(a + b) / c * d`
    + Usar paréntesis sólo cuando sea necesario, se debe conocer la precedencia de los operadores.
+ **Estilo de llaves:** 
    + Seguir el estilo K&R (Kernighan & Ritchie) para estructuras de control: abrir las llaves en la misma línea, cerrarla en una nueva línea
    ```C#
        if (a == b) {
            // hacer algo
        }

        foreach(var e in elems) {
            // hacer algo
        }

        using (var s = new StreamReader(path)) {
        }
    ```
    + Abrir y cerrar en lineas separadas para:
        1. Clases
        2. Métodos
        3. Propiedades, `get` y `set`
    ```C#
        class Prueba
        {
            public void Metodo()
            {
                var variable = "";
                Console.WriteLine("Valor {0}", variable);
            }

            private string m_Field;
            public string Field
            {
                get => m_Field;
                set => m_Field = value;
            }

            public int FieldAsInt
            {
                get
                {
                    var valor_int = 0;
                    if (int.TryParse(m_Field, valor)) {
                        return valor_int;
                    }

                    return -1;
                }
            }
    ```

+ **switch:** para los switchs, la apertura de las llaves en la misma línea, y cada case va identado a la misma altura que la palabra clave `switch`
```
switch(val) {
case 1:
    break;
case 2:
    // hacer algo
    break;
case 3:
case 4:
    // algo entre 3 y 4
    break;
default:
    // nada
}
```
+ **Líneas en blanco:** Usar una línea en blanco para separar secciones.
+ **Namespace:** luego de las instrucciones `using`, en una sóla línea. Cada archivo debe contener la definición de un sólo namespace.

## Convención de nombres

+ **PascalCase:** para clases, struct, interfaces, enums (definitions), funciones, propiedades, namespace.
+ **camelCase:** para variables.
+ **Prefijo:** `Is` para booleanos para claridad
+ **Prefijo:** `m_` para campos globales de clases
+ **Prefijo:** `I` para nombre de interfaces
+ **UPPER_SNAKE_CASE:** para constantes.
+ **Prefiera** el uso de `string` y `object`, int, long, short, bool, sus variantes en minúsculas.

## Ejemplo:

```C#
using System;

namespace COYALab.Entidades;

public class Paciente
{
    public int Codigo { get; set; }

    public short CodigoTipoDocumento { get; set; }
    public int NumeroDocumento { get; set; }

    public string ApellidoYNombre { get; set; }
    public string Sexo { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string HC { get; set; }

    public string Email {  get; set; }
    public string Provincia { get; set; }
    public string Localidad { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; } 
    public string OtrosDatos { get; set; }

    public static int? CalcularEdad(DateTime? fecha_nacimiento, DateTime fecha_actual)
    {
        if (!fecha_nacimiento.HasValue) {
            //  no tiene fecha cierta 
            return null;
        }

        if (EdadSePuedeCalcularEnAnios(fecha_nacimiento.Value, fecha_actual)) {
            return DiferenciaEnAnios(fecha_nacimiento.Value, fecha_actual); 
        }

        if (EdadSePuedeCalcularEnMeses(fecha_nacimiento.Value, fecha_actual)) {
            return DiferenciaEnMeses(fecha_nacimiento.Value, fecha_actual); 
        }

        return DiferenciaEnDias(fecha_nacimiento.Value, fecha_actual); 
    }

    public int? Edad
    {
        get => CalcularEdad(FechaNacimiento, DateTime.Today);
    }

    public string UnidadMedidaEdad
    {
        get => Utilidades.UnidadMedidaEdad(FechaNacimiento, DateTime.Today);
    }

    public TipoDocumento TipoDocumento { get; set; }

    public ICollection<Entrada> Entradas { get; set; }
}
```

## Namespace

+ Utilizar declaración de **namespace de archivo por sobre la tradicional** 
### Namespace por Archivo ("file-scoped namespace"):
```C#
namespace COYALab.Entidades;

class UnaClase
{
}

```
### Namespace Tradicional ("feo, horrible"):
```C#
namespace MiEspacio
{
    // agrega un nivel de identación innecesaria a todo el archivo
    class MiClase
    {
    }
}
```
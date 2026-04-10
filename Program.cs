using System.IO;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

public class Programa
{
    public static void Main(string[] args)
    {
        // Inicio del programa -> limpiar consola
        Console.Clear();

        // Lista de tareas utilizada en el programa
        List<Tarea> listaTareas = new List<Tarea>();


        // Inicio del bucle para el menú
        int op;

        do
        {
            // Imprimir menú
            ImprimirMenu();

            // Pedir al usuario que introduzca una opción
            Console.Write("\nIntroduce una opción: ");

            // Registrar la opción
            while(!int.TryParse(Console.ReadLine(), out op))
            {
                // Input invalido, pedir que vuelva a introducir una opción
                MensajeError("Debes introducir una opción valida");
                Console.Write("Vuelve a intentarlo: ");
            }

            // Gestionar opción elegida
            switch(op)
            {
                // Crear tarea
                case 1:
                    CrearTarea(listaTareas);
                    break;
                
                // Buscar tarea
                case 2:
                    BuscarTarea(listaTareas);
                    break;

                // Eliminar tarea
                case 3:
                    EliminarTarea(listaTareas);
                    break;

                // @todo: Exportar tareas
                case 4:
                    ExportarTareas(listaTareas);
                    break;

                // @todo: Importar tarea
                case 5:
                    ImportarTareas(listaTareas);
                    break;

                // Salir del programa
                case 6:
                    Console.Clear();
                    ImprimirTitulo("Salir");
                    Console.WriteLine("Saliendo del programa...");
                    break;
                
                // Opción invalida!
                default:
                    MensajeError("La opción que has introducido no existe, intentalo de nuevo...");
                    break;
            }

        } while(op != 6);



        // Fin del programa
        FinPrograma();
    }

    /*** MÉTODOS DE MENÚ ***/

    // ImprimirMenu -> muestra el menu
    public static void ImprimirMenu()
    {
        Console.Clear(); // Limpiar la consola
        Console.WriteLine("|| MENÚ PRINCIPAL ||");
        Console.WriteLine("1. Crear tarea");
        Console.WriteLine("2. Buscar tarea");
        Console.WriteLine("3. Eliminar tarea");
        Console.WriteLine("4. Exportar tareas");
        Console.WriteLine("5. Importar tareas");
        Console.WriteLine("6. Salir");
    }

    // Crear tarea
    public static void CrearTarea(List<Tarea> listaTareas)
    {
        // Imprimir titulo de la opción
        ImprimirTitulo("crear tarea");

        // Pedir el nombre de la tarea
        Console.Write("\nIntroduce el nombre de la tarea (max 20 caracteres): ");

        // Almacenar el nombre de la tarea
        string nombre = Console.ReadLine();

        // Validar el nombre
        while(nombre == null || nombre.Equals("") || nombre.Length > 20)
        {
            // Debe de tener un nombre
            if(nombre == null || nombre.Equals(""))
            {
                MensajeError("El nombre no puede estar vacío");
            }

            // Menos de 20 caracteres
            if(nombre.Length > 20)
            {
                MensajeError("El nombre no puede tener más de 20 caracteres");
            }

            // Pedir de nuevo el nombre correctamente
            Console.Write("Introduce el nombre correctamente: ");
            nombre = Console.ReadLine();
        }

        // Imprimir titulo de la opción
        ImprimirTitulo("crear tarea");

        // Pedir la descripción de la tarea
        Console.Write("Introduce la descripción (max 100 caracteres): ");

        // Almacenar y validar la descripción
        string desc = Console.ReadLine();

        while(desc == null || desc.Trim().Equals("") || desc.Length > 100)
        {
            // Descripción vacía o nula
            if(desc == null || desc.Trim().Equals(""))
            {
                MensajeError("La descripción no puede estar vacía");
            }

            // Descripción demasiado larga < 100
            if(desc.Length > 100)
            {
                MensajeError("La descripción es demasiado larga, máximo 100 caracteres");
            }

            // Pedir la descrpicón nueva correctaemte
            Console.Write("Introduce la descripción correctamente: ");
            desc = Console.ReadLine();

        }

        // Imprimir titulo de la opción
        ImprimirTitulo("crear tarea");

        // Pedir el tipo
        Console.WriteLine("Tipo de tarea");
        EnumerarTipos();
        
        // Almacenar el tipo y validarlo
        int tipoNumero;
        Console.Write("\nIntroduce el tipo (1 - 3): " );
        while(!int.TryParse(Console.ReadLine(), out tipoNumero) || tipoNumero < 1 || tipoNumero > 3)
        {
            MensajeError("Debes introducir un número válido entre 1 y 3");
            Console.Write("Introduce el tipo de nuevo (1 - 3): ");
        }

        // Asignar tipo correspondiente
        TipoTarea tipoTarea = AsignarTipoTarea(tipoNumero);

        // Imprimir titulo de la opción
        ImprimirTitulo("crear tarea");

        // Preguntar si tiene prioridad
        Console.Write("¿Esta tarea tiene prioridad (SI/NO)?: ");
        string confTexto = Console.ReadLine();
        bool tienePrioridad = false;

        if(confTexto.Trim().ToLower().Equals("si"))
        {
            tienePrioridad = true;
        }


        // Imprimir titulo de la opción
        ImprimirTitulo("crear tarea");

        // Añadir a la lista una instancia de la tarea
        Tarea t = new Tarea(nombre, desc, tipoTarea, tienePrioridad);
        listaTareas.Add(t);

        // Confirmación
        Separador();
        Console.WriteLine("\n[ + ] Se ha creado una nueva tarea");

        t.MostrarDatos();
        Console.WriteLine("\n");

        Separador();
        Console.WriteLine("\n");

        // Fin de la opción
        TeclaContinuar();
    }

    // Buscar tarea
    public static void BuscarTarea(List<Tarea> listaTareas)
    {
        // Imprimir titulo de la opción
        ImprimirTitulo("buscar tarea");

        // Pedir el tipo de tareas
        Console.WriteLine("Tipo de tareas:");
        EnumerarTipos();

        // Almacenar el tipo de tarea
        int tipoNumero;
        Console.Write("Introduce un tipo de tarea para listarlas (1 - 3): ");
        
        // Tarea invalida!
        while(!int.TryParse(Console.ReadLine(), out tipoNumero) || tipoNumero < 1 || tipoNumero > 3)
        {
            MensajeError("Tienes que poner un tipo de tarea que exista");
            Console.Write("Introduce un tipo de tarea para listarlas (1 - 3): ");
        }

        TipoTarea tipo = AsignarTipoTarea(tipoNumero);

        // Desplegar las tareas de ese tipo
        ImprimirTitulo("Tareas de tipo " + ObtenerNombreTipo(tipoNumero));

        // Comprobar si esta vacía
        if(TipoTareaEnLista(listaTareas, tipo))
        {
            // La lista esta vacía
            Separador();
            Console.WriteLine("\nLa lista de tareas esta vacía!\n");
        } else
        {
            // Imprimir la lista de tareas de ese tipo
            for(int i = 0; i < listaTareas.Count; i++)
            {
                if(listaTareas[i].Tipo == tipo)
                {
                    Separador();
                    listaTareas[i].MostrarDatos();
                }
            }
        }

        Separador();
        

        // Fin de la opción
        TeclaContinuar();
    }

    // Eliminar tarea
    public static void EliminarTarea(List<Tarea> listaTareas)
    {
        // Imprimir titulo de la opción
        ImprimirTitulo("eliminar tarea");

        // Pedir el ID de la tarea
        Console.Write("Introduce el ID de la tarea a eliminar (ej: 102): ");

        // Validar el número
        int idTareaEliminar;
        while(!int.TryParse(Console.ReadLine(), out idTareaEliminar) || idTareaEliminar < 101)
        {
            // ID invalido
            MensajeError("Ese ID no es valido!");
            Console.Write("Introduce ID valido (ej: 104): ");
        }

        // Ver si esa tarea existe
        bool tareaExiste = false;
        for(int i = 0; i < listaTareas.Count; i++)
        {
            if(listaTareas[i].Id == idTareaEliminar)
            {
                tareaExiste = true;

                // Preguntarle al usuario si la quiere eliminar
                ImprimirTitulo("eliminar tarea");

                Console.WriteLine("Se ha encontrado la siguiente tarea: ");
                listaTareas[i].MostrarDatos();

                Console.Write("\n¿Estas seguro de que quieres eliminarla? (SI/NO): ");

                // Validar el input
                string conf = Console.ReadLine();
                if(conf.Trim().ToUpper().Equals("SI"))
                {
                    // Eliminar la tarea
                    ImprimirTitulo("eliminar tarea");
                    string nombreEliminada = listaTareas[i].NombreTarea;
                    bool eliminada = listaTareas.Remove(listaTareas[i]);

                    // Mostrar mensaje de confirmación
                    if(eliminada)
                    {
                        Console.WriteLine("¡Se ha eliminado la tarea " + nombreEliminada + " correctamente!\n");
                    } else
                    {
                        MensajeError("Ha ocurrido un error inesperado, lo sentimos");
                    }
                } else
                {
                    // Eliminar la tarea
                    ImprimirTitulo("eliminar tarea");

                    // Cancelar operación
                    Console.WriteLine("Cancelando operación...\n");
                }
                
            }
        }

        // Si la tarea no existe, motrar mensaje de error
        if(!tareaExiste)
        {
            Console.WriteLine("\n");
            MensajeError("La tarea con el ID " + idTareaEliminar + " no existe! Intentalo de nuevo...");
        }

        // Fin de la opción
        TeclaContinuar();
    }

    // Exportar tarea
    public static void ExportarTareas(List<Tarea> listaTareas)
    {
        // Imprimir titulo de la opción
        ImprimirTitulo("exportar tareas");

        // Comprobar que la lista NO este vacía
        if(listaTareas.Count == 0)
        {
            Console.WriteLine("\n[ 0 ] La lista de tareas esta vacía, nada que exportar...");
            TeclaContinuar();
            return; // parar la ejecución del método
        }

        // Continuar con la ejecución del método
        Console.WriteLine("\nActualmente cuentas con: " + listaTareas.Count + " tareas en tu lista");
        Console.Write("\n¿Deseas exportar todas las tareas (SI/NO): ");
        string conf = Console.ReadLine();

        // Gestionar la confirmación
        if(conf.Trim().ToUpper() != "SI")
        {
            Console.WriteLine("\nCancelando operación...");
            TeclaContinuar();
            return;
        }

        // Organizar ruta
        string path = "logs";
        string ruta = Path.Combine(path, "tareas.csv");

        // Comprobar que el directorio exista -> sino, crearlo
        if(!Directory.Exists(path))
        {
            // Crear carpeta/directorio
            Directory.CreateDirectory(path);
        }

        // Intentar exportar las tareas
        try
        {
            // Generar el contenido
            string contenido = GenerarOutputTareas(listaTareas);

            // Escribir el fichero
            File.WriteAllText(ruta, contenido);

            // Mensaje de success
            Console.WriteLine("\n[ + ] Archivo exportado correctamente en: " + ruta);
        } catch(Exception e)
        {
            MensajeError("Error al exportar el archivo");
            Console.WriteLine(e.Message);
        }

        // Fin de la opción
        TeclaContinuar();
    }


    // Importar tarea
    public static void ImportarTareas(List<Tarea> listaTareas)
    {
        // Imprimir titulo de la opción
        Console.Clear();
        ImprimirTitulo("importar tarea");

        // Confirmar
        Console.WriteLine("[ ! ] ATENCIÓN: Se borraran todas las tareas existentes");
        Console.Write("¿Estas seguro de que quieres importar las tareas (SI/NO)?: ");
        string conf = Console.ReadLine();

        // Descartarlo
        if(conf.Trim().ToUpper() != "SI")
        {
            Console.WriteLine("\nCancelando operación...");
            TeclaContinuar();
            return;
        }

        // Vaciar la lista actual)
        ImprimirTitulo("importar tarea");

        // Configurando directorio
        string path = "logs";
        string ruta = Path.Combine(path, "tareas.csv");

        if(!File.Exists(ruta))
        {
            MensajeError("No existe el archivo tareas.csv en la carpeta logs");
            TeclaContinuar();
            return;
        }

        // Vaciar la lista de tareas
        listaTareas.Clear();

        // Intentar importar el archivo csv
        try
        {
            // Declarar array de lineas
            string[] lineas = File.ReadAllLines(ruta);

            // Comprobar que tenga contenido
            if(lineas.Length <= 1)
            {
                MensajeError("El archivo esta vacío o solo contiene el header");
                TeclaContinuar();
                return;
            }

            // Contador de tareas importadas
            int importadas = 0; // 0 hasta que se demuestre lo contrario

            // Empezamos en 1 para saltar la linea de cabecera
            for(int i = 1; i < lineas.Length; i++)
            {
            
                // Ignorar las lineas vacías
                if(lineas[i].Trim() == "")
                {
                    continue; // saltar la iteración del bucle
                }

                // Crear un array para dividir las partes
                string[] partes = lineas[i].Split(',');

                // Validar que haya 5 columnas
                if(partes.Length != 5)
                {
                    MensajeError("Línea inválida en el CSV: " + lineas[i]);
                    continue;
                }

                // Columnas
                int id = int.Parse(partes[0].Trim());
                string nombre = partes[1].Trim();
                string desc = partes[2].Trim();
                string tipoTexto = partes[3].Trim();
                string prioridadTexto = partes[4].Trim();

                // Convertir el tipo
                TipoTarea tipo;
                bool tipoValido = Enum.TryParse<TipoTarea>(tipoTexto, true, out tipo);

                if(!tipoValido)
                {
                    MensajeError("Tipo inválido en la línea: " + lineas[i]);
                    continue;
                }

                // Convertir prioridad
                bool prioridad;
                bool prioridadValida = bool.TryParse(prioridadTexto, out prioridad);

                if(!prioridadValida)
                {
                    MensajeError("Prioridad inválida en la línea: " + lineas[i]);
                    continue;
                }

                // Crear tarea nueva
                Tarea tareaNueva = new Tarea(id, nombre, desc, tipo, prioridad);
                listaTareas.Add(tareaNueva); // añadir la tarea a la lista
                importadas++; // incrementar el contador
            }
            
            // Mensaje de SUCCESS
            Console.WriteLine("\n[ + ] Se han importado " + importadas + " correctamente ");

        } catch(Exception e)
        {
            MensajeError("Ha ocurrido un error al importar el archivo");
            Console.WriteLine(e.Message);

        }
        // Fin de la opción
        TeclaContinuar();
    }



    /*** MÉTODOS DE UTILIDAD ***/
    
    // ImprimirTitulo -> imprime el titulo para la opción seleccionada
    public static void ImprimirTitulo(string titulo)
    {
        Console.Clear();
        Console.WriteLine("|| " + titulo.ToUpper());
    }

    // TeclaContinuar -> pide al usuario que introduzca una tecla para continuar
    public static void TeclaContinuar()
    {
        Console.WriteLine("Introduce una tecla para continuar...");
        Console.ReadKey();
    }

    // MensajeError -> muestra un mensaje de error por consola
    public static void MensajeError(string mensaje)
    {
        Console.WriteLine("ERROR!: " + mensaje);
    }

    // Fin del programa (mensaje)
    public static void FinPrograma()
    {
        Console.WriteLine("\nPresiona cualquier tecla para finalizar...");
        Console.ReadKey();
        Console.WriteLine("[ - ] Adios!");
    }

    // EnumerarTipos -> enumera los tipos de tarea que hay
    public static void EnumerarTipos()
    {
        List<string> tipos = new List<string> { "Persona", "Trabajo", "Ocio" };

        if(tipos.Count > 0)
        {
            // Imprimirlos
            for(int i = 0; i < tipos.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + tipos[i]);
            }
        } else
        {
            MensajeError("No existen tipos de tareas actualmente");
        }
    }

    // Obtener nombre bruto de la tarea
    public static string ObtenerNombreTipo(int numeroTipo)
    {
        string nombre;

        switch(numeroTipo)
        {
            case 1:
                nombre = "Persona";
                break;
            case 2:
                nombre = "Trabajo";
                break;
            case 3:
                nombre = "Ocio";
                break;
            default: // no debería ocurrir núnca si todo se ha configurado bien!
                nombre = "ERROR";
                break;
        }

        return nombre;
    }

    // Asignar Tipo Tarea
    public static TipoTarea AsignarTipoTarea(int numeroTipo)
    {
        TipoTarea tipo;

        switch(numeroTipo)
        {
            case 1:
                tipo = TipoTarea.Persona;
                break;
            case 2:
                tipo = TipoTarea.Trabajo;
                break;
            case 3:
                tipo = TipoTarea.Ocio;
                break;
            default:
                tipo = TipoTarea.Persona; // no deberia ocurrir núnca!
                break;    
        }

        return tipo;
    }

    // Comprobar si la lista tiene tipo de tarea
    public static bool TipoTareaEnLista(List<Tarea> lista, TipoTarea tipo)
    {
        bool estaVacia = true;
        for(int i = 0; i < lista.Count; i++)
        {
            if(lista[i].Tipo == tipo)
            {
                estaVacia = false;
                break;
            }
        }

        return estaVacia;
    }

    // Separador -> para dividir las tareas
    public static void Separador()
    {
        for(int i = 0; i < 60; i++)
        {
            Console.Write("-");
        }

        // Salto de línea
        Console.WriteLine();
    }

    // (EXPORTAR Helper) Generar output de tareas
    /*
        Al final, lo he decidido hacer en CSV
    */
    public static string GenerarOutputTareas(List<Tarea> listaTareas)
    {
        string output = "Id,Nombre,Descripcion,Tipo,Prioridad";

        foreach(Tarea t in listaTareas)
        {
            // Anidar al output el string generado
            output += $"\n{t.Id},{t.NombreTarea},{t.Desc},{t.Tipo},{t.Prioridad}";
        }

        return output;

    }
}
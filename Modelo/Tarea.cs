public class Tarea
{
    /*** ESTATICOS ***/
    private static int _id = 101; // empieza con el 101, para darle más claridad y formato

    /*** ATRIBUTOS ***/
    private string _nombreTarea;
    private string _desc;

    /*** PROPIEDADES ***/

    // ID
    public int Id { get; private set; }

    // NombreTarea: propiedad getter/setter
    public string NombreTarea
    {
        get => _nombreTarea;

        set
        {
            if(value != null && !value.Trim().Equals("") && value.Length <= 20)
            {
                _nombreTarea = value;
            }
        }
    }

    // Desc: descripción de la tarea
    public string Desc
    {
        get => _desc;

        set
        {
            if(value != null && !value.Trim().Equals("") && value.Length <= 100)
            {
                _desc = value;
            }
        }
    }

    // TipoTarea: propiedad getter/setter
    public TipoTarea Tipo { get; set; }

    // Prioridad
    public bool Prioridad { get; set; }

    /*** CONSTRUCTORES ***/

    // constructor vacío
    public Tarea()
    {
        // Asignar el ID de la tarea
        Id = _id;

        // Incrementar ID
        _id++;
    }

    // constructor solo con nombre, descripción y tipo
    public Tarea(string nombreTarea, string desc, TipoTarea tipo) : this()
    {
        NombreTarea = nombreTarea;
        Desc = desc;
        Tipo = tipo;
    }

    // constructor con nombre, descripción, tipo y prioridad
    public Tarea(string nombre,string desc, TipoTarea tipo, bool prioridad)
    : this(nombre, desc, tipo)
    {
        Prioridad = prioridad;
    }

    /*** MÉTODOS ***/
    
    // MostrarDatos: muestra los datos de la tarea
    public void MostrarDatos()
    {
        Console.WriteLine("ID: " + Id);
        Console.WriteLine("Nombre: " + NombreTarea);
        Console.WriteLine("Descripción: " + Desc);
        Console.WriteLine("Tipo de tarea: " + Tipo);

        Console.WriteLine("¿Tiene prioridad?: " + (Prioridad ? "SI" : "NO"));
    }
}
using MantenimientoAlumnos.Datos;
using MantenimientoAlumnos.Modelos;

namespace MantenimientoAlumnos.Vista
{
    public class MenuConsola
    {
        private readonly AlumnoRepositorio _repo = new();

        public void Iniciar()
        {
            int opcion;
            do
            {
                MostrarMenu();
                opcion = LeerEntero("Seleccione una opcion: ");
                EjecutarOpcion(opcion);
            }
            while (opcion != 0);

            Console.WriteLine("\nPrograma finalizado. Hasta pronto.");
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("\n==============================================");
            Console.WriteLine("       MANTENIMIENTO DE ALUMNOS");
            Console.WriteLine("==============================================");
            Console.WriteLine("  1. Crear alumno");
            Console.WriteLine("  2. Listar alumnos");
            Console.WriteLine("  3. Actualizar alumno");
            Console.WriteLine("  0. Salir");
            Console.WriteLine("==============================================");
        }

        private void EjecutarOpcion(int opcion)
        {
            try
            {
                switch (opcion)
                {
                    case 1: CrearAlumno(); break;
                    case 2: ListarAlumnos(); break;
                    case 3: ActualizarAlumno(); break;
                    case 0: break; // salir
                    default:
                        Console.WriteLine(">> Opcion no valida. Intente de nuevo.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(">> Error de base de datos: " + ex.Message);
            }
        }

        private void CrearAlumno()
        {
            Console.WriteLine("\n--- NUEVO ALUMNO ---");
            var a = new Alumno
            {
                Carnet = LeerTexto("Carnet: "),
                Nombre = LeerTexto("Nombre: "),
                Apellido = LeerTexto("Apellido: "),
                Correo = LeerTexto("Correo: "),
                Carrera = LeerTexto("Carrera: "),
                Edad = LeerEntero("Edad: ")
            };

            int id = _repo.Crear(a);
            if (id > 0)
                Console.WriteLine($">> Alumno creado correctamente con id = {id}");
            else
                Console.WriteLine(">> No se pudo crear el alumno.");
        }

        private void ListarAlumnos()
        {
            Console.WriteLine("\n--- LISTADO DE ALUMNOS ---");
            var alumnos = _repo.Listar();

            if (alumnos.Count == 0)
            {
                Console.WriteLine("No hay alumnos registrados.");
                return;
            }

            Console.WriteLine($"{"ID",-4} {"CARNET",-10} {"NOMBRE",-15} {"APELLIDO",-15} " +
                              $"{"CORREO",-25} {"CARRERA",-22} {"EDAD",-4}");
            Console.WriteLine(new string('-', 100));

            foreach (var a in alumnos)
            {
                Console.WriteLine($"{a.Id,-4} {a.Carnet,-10} {a.Nombre,-15} {a.Apellido,-15} " +
                                  $"{a.Correo,-25} {a.Carrera,-22} {a.Edad,-4}");
            }
        }

        private void ActualizarAlumno()
        {
            Console.WriteLine("\n--- ACTUALIZAR ALUMNO ---");
            int id = LeerEntero("Ingrese el id del alumno a actualizar: ");

            var actual = _repo.BuscarPorId(id);
            if (actual == null)
            {
                Console.WriteLine(">> No existe un alumno con ese id.");
                return;
            }

            Console.WriteLine("Datos actuales: " + actual);
            Console.WriteLine("Deje vacio cualquier campo para conservar el valor actual.\n");

            actual.Carnet = LeerTextoOpcional("Carnet", actual.Carnet);
            actual.Nombre = LeerTextoOpcional("Nombre", actual.Nombre);
            actual.Apellido = LeerTextoOpcional("Apellido", actual.Apellido);
            actual.Correo = LeerTextoOpcional("Correo", actual.Correo);
            actual.Carrera = LeerTextoOpcional("Carrera", actual.Carrera);
            actual.Edad = LeerEnteroOpcional("Edad", actual.Edad);

            if (_repo.Actualizar(actual))
                Console.WriteLine(">> Alumno actualizado correctamente.");
            else
                Console.WriteLine(">> No se pudo actualizar el alumno.");
        }

        private static string LeerTexto(string etiqueta)
        {
            Console.Write(etiqueta);
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        private static string LeerTextoOpcional(string etiqueta, string valorActual)
        {
            Console.Write($"{etiqueta} [{valorActual}]: ");
            string entrada = (Console.ReadLine() ?? string.Empty).Trim();
            return string.IsNullOrEmpty(entrada) ? valorActual : entrada;
        }

        private static int LeerEntero(string etiqueta)
        {
            while (true)
            {
                Console.Write(etiqueta);
                string entrada = (Console.ReadLine() ?? string.Empty).Trim();
                if (int.TryParse(entrada, out int valor))
                    return valor;
                Console.WriteLine(">> Debe ingresar un numero entero valido.");
            }
        }

        private static int LeerEnteroOpcional(string etiqueta, int valorActual)
        {
            Console.Write($"{etiqueta} [{valorActual}]: ");
            string entrada = (Console.ReadLine() ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(entrada))
                return valorActual;
            if (int.TryParse(entrada, out int valor))
                return valor;
            Console.WriteLine(">> Valor no numerico, se conserva el actual.");
            return valorActual;
        }
    }
}

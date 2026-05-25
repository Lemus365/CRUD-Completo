using MantenimientoAlumnos.Config;
using MantenimientoAlumnos.Modelos;
using MySql.Data.MySqlClient;

namespace MantenimientoAlumnos.Datos
{
    public class AlumnoRepositorio
    {
        public int Crear(Alumno a)
        {
            const string sql =
                "INSERT INTO alumnos (carnet, nombre, apellido, correo, carrera, edad) " +
                "VALUES (@carnet, @nombre, @apellido, @correo, @carrera, @edad); " +
                "SELECT LAST_INSERT_ID();";

            using var con = ConexionBD.ObtenerConexion();
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@carnet", a.Carnet);
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apellido", a.Apellido);
            cmd.Parameters.AddWithValue("@correo", a.Correo);
            cmd.Parameters.AddWithValue("@carrera", a.Carrera);
            cmd.Parameters.AddWithValue("@edad", a.Edad);

            object? resultado = cmd.ExecuteScalar();
            return resultado != null ? Convert.ToInt32(resultado) : -1;
        }

        public List<Alumno> Listar()
        {
            const string sql =
                "SELECT id, carnet, nombre, apellido, correo, carrera, edad " +
                "FROM alumnos ORDER BY id;";

            var lista = new List<Alumno>();

            using var con = ConexionBD.ObtenerConexion();
            using var cmd = new MySqlCommand(sql, con);
            using var lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(Mapear(lector));
            }
            return lista;
        }

        public Alumno? BuscarPorId(int id)
        {
            const string sql =
                "SELECT id, carnet, nombre, apellido, correo, carrera, edad " +
                "FROM alumnos WHERE id = @id;";

            using var con = ConexionBD.ObtenerConexion();
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            using var lector = cmd.ExecuteReader();
            if (lector.Read())
            {
                return Mapear(lector);
            }
            return null;
        }


        public bool Actualizar(Alumno a)
        {
            const string sql =
                "UPDATE alumnos SET carnet = @carnet, nombre = @nombre, apellido = @apellido, " +
                "correo = @correo, carrera = @carrera, edad = @edad WHERE id = @id;";

            using var con = ConexionBD.ObtenerConexion();
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@carnet", a.Carnet);
            cmd.Parameters.AddWithValue("@nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@apellido", a.Apellido);
            cmd.Parameters.AddWithValue("@correo", a.Correo);
            cmd.Parameters.AddWithValue("@carrera", a.Carrera);
            cmd.Parameters.AddWithValue("@edad", a.Edad);
            cmd.Parameters.AddWithValue("@id", a.Id);

            return cmd.ExecuteNonQuery() > 0;
        }
        private static Alumno Mapear(MySqlDataReader lector)
        {
            return new Alumno
            {
                Id = lector.GetInt32("id"),
                Carnet = lector.GetString("carnet"),
                Nombre = lector.GetString("nombre"),
                Apellido = lector.GetString("apellido"),
                Correo = lector.IsDBNull(lector.GetOrdinal("correo"))
                            ? string.Empty : lector.GetString("correo"),
                Carrera = lector.IsDBNull(lector.GetOrdinal("carrera"))
                            ? string.Empty : lector.GetString("carrera"),
                Edad = lector.IsDBNull(lector.GetOrdinal("edad"))
                            ? 0 : lector.GetInt32("edad")
            };
        }
    }
}

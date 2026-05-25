using MySql.Data.MySqlClient;

namespace MantenimientoAlumnos.Config
{
    public static class ConexionBD
    {
        private static readonly string CadenaConexion =
            "server=localhost;" +
            "port=3306;" +
            "database=escuela;" +
            "user=root;" +
            "password=tu_password_aqui;" +
            "SslMode=None;" +
            "AllowPublicKeyRetrieval=true;";

        public static MySqlConnection ObtenerConexion()
        {
            var conexion = new MySqlConnection(CadenaConexion);
            conexion.Open();
            return conexion;
        }
    }
}

namespace MantenimientoAlumnos.Modelos
{

    public class Alumno
    {
        public int Id { get; set; }
        public string Carnet { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public int Edad { get; set; }

        public override string ToString()
        {
            return $"Alumno{{Id={Id}, Carnet='{Carnet}', Nombre='{Nombre} {Apellido}', " +
                   $"Correo='{Correo}', Carrera='{Carrera}', Edad={Edad}}}";
        }
    }
}

namespace ProyectoHDP.Models
{
    public class Empleados
    {
        public int iD { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string tipoContrato { get; set; } = string.Empty;
        public string pais { get; set; } = string.Empty;
        public string empresa { get; set; } = string.Empty;
        public double salario { get; set; }
        public DateTime fechaContrato { get; set; }
        public DateTime fechaRenuncia { get; set; }
        public DateTime fechaEmision { get; set; }
        public int mesesTrabajo { get; set; }
        public string cargo { get; set; } = string.Empty;
        public string DUI { get; set; } = string.Empty;
        public string nISSS { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string nTelefono { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;

        public string direccionEmpresa { get; set; } = string.Empty;
        public string telefonoEmpresa { get; set; } = string.Empty;
        public string correoEmpresa { get; set; } = string.Empty;



        public Empleados()
        {
            
        }
    }
}

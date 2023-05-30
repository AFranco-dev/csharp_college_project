using System.ComponentModel;

namespace ProyectoHDP.Models
{
    public class Empleados
    {
        [DisplayName("ID")]
        public int iD { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; } = string.Empty;
        [DisplayName("Tipo de Contrato")]
        public string tipoContrato { get; set; } = string.Empty;
        [DisplayName("País")]
        public string pais { get; set; } = string.Empty;
        [DisplayName("Empresa")]
        public string empresa { get; set; } = string.Empty;
        [DisplayName("Salario")]
        public double salario { get; set; }
        [DisplayName("Fecha de Contrato")]
        public DateTime fechaContrato { get; set; }
        [DisplayName("Fecha de Renuncia")]
        public DateTime fechaRenuncia { get; set; }
        [DisplayName("Fecha de Emisión")]
        public DateTime fechaEmision { get; set; }
        [DisplayName("Meses de Trabajo")]
        public int mesesTrabajo { get; set; }
        [DisplayName("Cargo")]
        public string cargo { get; set; } = string.Empty;
        [DisplayName("DUI")]
        public string DUI { get; set; } = string.Empty;
        [DisplayName("Número ISSS")]
        public string nISSS { get; set; } = string.Empty;
        [DisplayName("Dirección")]
        public string direccion { get; set; } = string.Empty;
        [DisplayName("Número de Teléfono")]
        public string nTelefono { get; set; } = string.Empty;
        [DisplayName("Correo")]
        public string correo { get; set; } = string.Empty;

        [DisplayName("Dirección de la Empresa")]
        public string direccionEmpresa { get; set; } = string.Empty;
        [DisplayName("Teléfono de la Empresa")]
        public string telefonoEmpresa { get; set; } = string.Empty;
        [DisplayName("Correo de la Empresa")]
        public string correoEmpresa { get; set; } = string.Empty;



        public Empleados()
        {
            
        }
    }
}

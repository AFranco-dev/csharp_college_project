﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoHDP.Data;
using ProyectoHDP.Models;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Web;
using iText.Kernel.Geom;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Microsoft.AspNetCore.Authorization;
//using IronPdf;

namespace ProyectoHDP.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Empleados != null ? 
                          View(await _context.Empleados.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
        }
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        // GET: Empleados/ShowBuscarYearForm
        public async Task<IActionResult> ShowBuscarYearForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowBuscarYearResults(DateTime SearchYear)
        {
            return _context.Empleados != null ?
                          View("Index", await _context.Empleados.Where(x => x.fechaEmision.Year == SearchYear.Year).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
        }
        // GET: Empleados/ShowBuscarYearForm
        public async Task<IActionResult> ShowBuscarEmpresaForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowBuscarEmpresaResults(string SearchEmpresa)
        {
            return _context.Empleados != null ?
                          View("Index", await _context.Empleados.Where(x => x.empresa.Contains(SearchEmpresa)).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
        }

        public IActionResult DownloadTextFile(int id)
        {
            var empleados = _context.Empleados.FirstOrDefault(m => m.iD == id);
            if (empleados == null)
            {
                return NotFound();
            }

            // Generate the text content for the file using the model information
            string fileContent =    $"ID: {empleados.iD}\n" +
                                    $"Nombre: {empleados.nombre}\n" +
                                    $"Tipo de Contrato: {empleados.tipoContrato}\n" +
                                    $"País: {empleados.pais}\n" +
                                    $"Empresa: {empleados.empresa}\n" +
                                    $"Salario: {empleados.salario}\n" +
                                    $"Fecha de Contrato: {empleados.fechaContrato}\n" +
                                    $"Fecha de Renuncia: {empleados.fechaRenuncia}\n" +
                                    $"Fecha de Emisión: {empleados.fechaEmision}\n" +
                                    $"Meses de Trabajo: {empleados.mesesTrabajo}\n" +
                                    $"Cargo: {empleados.cargo}\n" +
                                    $"DUI: {empleados.DUI}\n" +
                                    $"Número de ISSS: {empleados.nISSS}\n" +
                                    $"Dirección: {empleados.direccion}\n" +
                                    $"Teléfono: {empleados.nTelefono}\n" +
                                    $"Correo Electrónico: {empleados.correo}\n" +
                                    $"Dirección de la Empresa: {empleados.direccionEmpresa}\n" +
                                    $"Teléfono de la Empresa: {empleados.telefonoEmpresa}\n" +
                                    $"Correo Electrónico de la Empresa: {empleados.correoEmpresa}";

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent);
            string fileName = $"Empleado_{empleados.iD}.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }

        public IActionResult DownloadAllUsersTextFile()
        {
            var empleadosList = _context.Empleados.ToList();
            if (empleadosList.Count == 0)
            {
                return NotFound();
            }

            // Generate the text content for the file
            var fileContent = new StringBuilder();
            foreach (var empleado in empleadosList)
            {
                fileContent.AppendLine($"ID: {empleado.iD}");
                fileContent.AppendLine($"Nombre: {empleado.nombre}");
                fileContent.AppendLine($"Tipo de Contrato: {empleado.tipoContrato}");
                fileContent.AppendLine($"País: {empleado.pais}");
                fileContent.AppendLine($"Empresa: {empleado.empresa}");
                fileContent.AppendLine($"Salario: {empleado.salario}");
                fileContent.AppendLine($"Fecha de Contrato: {empleado.fechaContrato}");
                fileContent.AppendLine($"Fecha de Renuncia: {empleado.fechaRenuncia}");
                fileContent.AppendLine($"Fecha de Emisión: {empleado.fechaEmision}");
                fileContent.AppendLine($"Meses de Trabajo: {empleado.mesesTrabajo}");
                fileContent.AppendLine($"Cargo: {empleado.cargo}");
                fileContent.AppendLine($"DUI: {empleado.DUI}");
                fileContent.AppendLine($"Número de ISSS: {empleado.nISSS}");
                fileContent.AppendLine($"Dirección: {empleado.direccion}");
                fileContent.AppendLine($"Teléfono: {empleado.nTelefono}");
                fileContent.AppendLine($"Correo Electrónico: {empleado.correo}");
                fileContent.AppendLine($"Dirección de la Empresa: {empleado.direccionEmpresa}");
                fileContent.AppendLine($"Teléfono de la Empresa: {empleado.telefonoEmpresa}");
                fileContent.AppendLine($"Correo Electrónico de la Empresa: {empleado.correoEmpresa}");
                fileContent.AppendLine();
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent.ToString());
            string fileName = "AllUsers.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }

        public IActionResult DownloadAllUsersByEmpresaTextFile(string empresa)
        {
            var query = _context.Empleados.AsQueryable();

            if (!string.IsNullOrEmpty(empresa))
            {
                query = query.Where(x => x.empresa.Contains(empresa));
            }

            var empleadosList = query.ToList();

            if (empleadosList.Count == 0)
            {
                return NotFound();
            }

            // Generate the text content for the file
            var fileContent = new StringBuilder();
            foreach (var empleado in empleadosList)
            {
                fileContent.AppendLine($"ID: {empleado.iD}");
                fileContent.AppendLine($"Nombre: {empleado.nombre}");
                fileContent.AppendLine($"Tipo de Contrato: {empleado.tipoContrato}");
                fileContent.AppendLine($"País: {empleado.pais}");
                fileContent.AppendLine($"Empresa: {empleado.empresa}");
                fileContent.AppendLine($"Salario: {empleado.salario}");
                fileContent.AppendLine($"Fecha de Contrato: {empleado.fechaContrato}");
                fileContent.AppendLine($"Fecha de Renuncia: {empleado.fechaRenuncia}");
                fileContent.AppendLine($"Fecha de Emisión: {empleado.fechaEmision}");
                fileContent.AppendLine($"Meses de Trabajo: {empleado.mesesTrabajo}");
                fileContent.AppendLine($"Cargo: {empleado.cargo}");
                fileContent.AppendLine($"DUI: {empleado.DUI}");
                fileContent.AppendLine($"Número de ISSS: {empleado.nISSS}");
                fileContent.AppendLine($"Dirección: {empleado.direccion}");
                fileContent.AppendLine($"Teléfono: {empleado.nTelefono}");
                fileContent.AppendLine($"Correo Electrónico: {empleado.correo}");
                fileContent.AppendLine($"Dirección de la Empresa: {empleado.direccionEmpresa}");
                fileContent.AppendLine($"Teléfono de la Empresa: {empleado.telefonoEmpresa}");
                fileContent.AppendLine($"Correo Electrónico de la Empresa: {empleado.correoEmpresa}");
                fileContent.AppendLine();
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent.ToString());
            string fileName = "AllUsers.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }

        public IActionResult DownloadAllUsersByYearTextFile(DateTime SearchYear)
        {
            var query = _context.Empleados.AsQueryable();

            if (SearchYear != DateTime.MinValue)
            {
                query = query.Where(x => x.fechaEmision.Year == SearchYear.Year);
            }

            var empleadosList = query.ToList();

            if (empleadosList.Count == 0)
            {
                return NotFound();
            }

            // Generate the text content for the file
            var fileContent = new StringBuilder();
            foreach (var empleado in empleadosList)
            {
                fileContent.AppendLine($"ID: {empleado.iD}");
                fileContent.AppendLine($"Nombre: {empleado.nombre}");
                fileContent.AppendLine($"Tipo de Contrato: {empleado.tipoContrato}");
                fileContent.AppendLine($"País: {empleado.pais}");
                fileContent.AppendLine($"Empresa: {empleado.empresa}");
                fileContent.AppendLine($"Salario: {empleado.salario}");
                fileContent.AppendLine($"Fecha de Contrato: {empleado.fechaContrato}");
                fileContent.AppendLine($"Fecha de Renuncia: {empleado.fechaRenuncia}");
                fileContent.AppendLine($"Fecha de Emisión: {empleado.fechaEmision}");
                fileContent.AppendLine($"Meses de Trabajo: {empleado.mesesTrabajo}");
                fileContent.AppendLine($"Cargo: {empleado.cargo}");
                fileContent.AppendLine($"DUI: {empleado.DUI}");
                fileContent.AppendLine($"Número de ISSS: {empleado.nISSS}");
                fileContent.AppendLine($"Dirección: {empleado.direccion}");
                fileContent.AppendLine($"Teléfono: {empleado.nTelefono}");
                fileContent.AppendLine($"Correo Electrónico: {empleado.correo}");
                fileContent.AppendLine($"Dirección de la Empresa: {empleado.direccionEmpresa}");
                fileContent.AppendLine($"Teléfono de la Empresa: {empleado.telefonoEmpresa}");
                fileContent.AppendLine($"Correo Electrónico de la Empresa: {empleado.correoEmpresa}");
                fileContent.AppendLine();
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent.ToString());
            string fileName = "AllUsers.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }


        public IActionResult GeneratePDF(int id)
        {
            var empleados = _context.Empleados.FirstOrDefault(m => m.iD == id);
            if (empleados == null)
            {
                return NotFound();
            }
            // Define your custom HTML string
            var fileContent = new StringBuilder();
            fileContent.Append("<html>\r\n<body>");
            fileContent.Append("<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"100\" height=\"100\" viewBox=\"275.991 555.002 297.474 299\"><title>Starbucks Corporation Logo</title><path d=\"M573.465 704.502c0 82.566-66.592 149.5-148.736 149.5-82.146 0-148.737-66.934-148.737-149.5s66.592-149.5 148.737-149.5c82.144 0 148.736 66.934 148.736 149.5z\" fill=\"#fff\"/><path d=\"M423.486 608.151c5.236 3.018 10.791 6.902 15.018 11.019l-1.66 1.668c-8.344-.887-17.131.221-25.041.343-.881-.89-2.658-2.228-1.004-3.346l12.687-9.684zm147.522 103.638c.111 3.765.465 6.999-1.004 10.854-15.242 4.336-25.254-11.802-40.043-9.369l1.672 8.365c13.682.45 22.229 13.571 36.363 13.007-2.008 4.01.896 14.578-8 10.354-9.236-2.22-15.914-10.354-26.033-10.354-1.221 2.241-.447 5.345-.662 7.998 10.676 1.674 18.355 10.698 29.027 12.364-1.004 3.668-1.998 7.775-5.006 10.011-8.682-2.113-15.465-9.911-24.709-9.688l-1.645 7.681c8.783.226 14.461 7.998 22.355 10.332.217 3.239-2.678 6.135-4.348 9.026-7.008-2.104-12.352-8.126-20.361-7.681-.111-3.018 1.68-6.443 1.004-9.67-8.994 2.676-15.025-5.681-21.691-10.695-.191-16.404-17.461-27.167-18.373-42.534-.176-3.065.291-6.335 1.688-9.851 5.676-13.123 3.893-30.162-2.33-42.398-1.893-3.221-5.242-5.762-8.365-7.656 4.568 9.684 10.137 21.125 7.357 33.027-.322 9.19-5.746 18.018-5.807 26.878-.021 3.159.654 6.318 2.471 9.508 7.234 13.031 20.252 25.368 15.357 41.712-1.346 5.02-5.352 8.901-6.354 14.01 8.465 7.011 16.469 14.912 27.701 15.356 4.002-2.556 1.779-13.891 10.012-8.662l12.021 6.013c-.875 3.217-3.57 6.101-6.01 8.317-4.561-2.219-9.01-4.881-14.697-4.664-1.676 1.78-3.119 3.777-2.992 6.33 4.002.659 7.674 2.347 11.016 4.688-1.553 3.343-4.578 5.437-7.359 7.998l-9.324-3.334c-1.557 2.228-4.791 4.568-3.656 7.014 2.105.9 4.438 2.118 5.986 3.679-4.115 4.449-9.332 7.312-14.352 10.331-4.324-14.133-18.572-24.041-17.348-40.385l-.662-.662c-3.348 3.766-2.23 9.786-2.014 14.673 4.008 10.236 13.568 18.143 15.018 29.369-1.674 2.89-5.447 3.466-8.344 4.688.338-20.239-30.365-35.59-10.676-56.063 6.566-10.025 17.568-19.592 13.67-33.394-4.002-13.327-18.158-21.584-18.033-36.522l.023-1.166c3.223-15.578 13.018-32.155 5.006-48.728-2.234-6.666-7.223-14.241-14.008-17.027 10.117 11.579 14.127 30.473 8.34 46.055-2.918 6.199-6.934 13.582-7.336 20.868h-.025c-.215 3.805.561 7.59 3.018 11.177 7.68 12.465 21.023 26.155 13.348 42.398-8.676 13.01-23.805 26.244-15.355 43.381 6.117 10.233 15.582 20.445 13.027 33.026l-10.01 3.335c6.674-23.466-26.037-37.254-12.342-60.729 8.453-12.569 23.574-24.364 15.014-41.047-5.117-10.337-15.689-16.455-15.357-29.372l.365-2.17c3.445-18.32 17.373-35.663 7.633-55.24-2.559-5.782-7.672-11.675-13.348-15.015l-.342.342c11.352 11.013 14.805 29.134 8.684 43.7-2.855 8.546-9.123 17.059-10.396 26.216-.607 4.319-.096 8.794 2.398 13.508 7.23 11.449 19.91 23.594 11.998 38.396-8.332 12.225-22.242 24.122-16.686 40.021 6.121 13.91 18.898 27.162 12 44.409l-10.332 2.333c8.229-16.808-4.109-33.488-11.338-48.065-8.793-20.916 18.891-30.931 19.336-50.052-.105-12.343-13.227-19.351-15.998-30.696-5.68-4.792-11.566 1.904-18.355.661-5.011 1.007-12.709-6.899-15.701 1.672-3.562 12.564-19.795 21.8-12.662 36.703 6.789 13.243 23.125 22.926 17.668 40.065-4.886 16.007-21.027 31.249-11.016 49.712-4.113-1.332-11.791 1.219-11.68-5.351-6.338-22.358 20.69-35.385 12.342-57.732-3.885-13.688-21.586-21.249-17.689-37.713 2.881-13.552 18.088-23.026 15.955-37.871a21.988 21.988 0 0 0-.916-3.84c-5.676-16.019-17.256-32.043-10.354-50.739 1.895-5.339 5.68-9.899 9.006-14.672-11.014 5.893-16.57 18.356-16.342 31.035-.48 13.788 9.705 25.599 11.154 38.216.57 5.092-.264 10.307-3.818 15.838-7.225 9.803-16.674 20.14-12.342 33.049 4.119 13.021 20.359 20.91 19.359 36.044-.111 17.904-20.914 29.05-15.018 49.073l-10.354-3.359c-4.111-17.793 14.797-26.895 15.014-43.358 2.676-18.93-21.469-25.954-20.02-44.409-.104-14.021 12.238-22.901 17.689-34.695.934-2.87 1.246-5.581 1.121-8.182-.668-13.04-12.418-23.234-10.468-37.896.656-10.79 4.327-20.249 11.338-28.363-9.673 3.885-13.669 15.02-16.341 24.366-4.637 14.987 6.432 28.467 7.541 41.893.492 5.93-.956 11.825-6.535 17.852-8.014 9.443-16.141 21.36-10.355 34.053 6.127 14.005 25.376 24.906 17.027 42.373-4.555 11.016-15.462 19.353-15.014 32.365-2.89-1.224-5.888-2.444-8.344-4.664 0-16.571 23.574-26.922 12.342-44.387-.995 3.445-1.209 8.129-1.988 12.022-3.898 10.128-12.238 18.234-15.359 28.705-5.219-2.014-10.676-6.246-15.017-10.696l6.013-3.679c-.668-2.23-2.445-4.119-4-6.01-3.904-.548-6.468 2.014-9.688 3.018-2.781-2.453-5.465-4.678-7.682-7.682 1.996-5.684 17.592-.984 9.691-10.331-5.334-3.334-10.688 1.43-15.359 3.654l-6.332-7.998c5.451-3.334 10.888-8.018 17.668-6.674 1.671 3.009 1.787 7.433 5.006 9.004 11.46-.222 19.58-8.891 28.365-15.679-6.123-9.11-11.107-21.688-5.668-32.707 4.357-11.04 15.713-20.34 16.227-32.546.117-2.777-.316-5.7-1.531-8.846-6.004-15.465-8.123-36.367 3.337-50.055-6.231 1.674-9.905 9.441-12.022 15.337-6.889 15.101 1.824 29.092 2.125 43.563.041 2.604-.17 5.212-.801 7.84-5.898 11.902-15.454 22.345-17.006 36.041-6.788 4.33-12.361 11.902-22.033 9.347-.34 2.556.439 6.452 1.326 9.348-7.67-.323-12.9 5.331-19.357 8-3.441-1.104-4.336-5.551-5.668-8.662 7.559-2.553 13.012-10.131 21.691-10.012 1.006-2.795-.564-5.356-1.007-8.023-10.796-4.791-27.909 23.688-29.348-1.004 10.788-.667 17.681-10.682 28.366-11.68l.319-7.998c-12.125-1.224-19.925 11.24-32.045 10.012l-2.673-10.674c14.02.876 22.373-11.688 35.725-12.364 1.774-2.333 1.988-5.781 2.33-9.004-14.574-1.9-24.574 14.252-40.043 9.347 0 0-.533-6.722-.822-10.513h.021a108.072 108.072 0 0 0-.205-2.514c13.793 5.351 25.912-4.556 37.712-9.669 3.001-.898 7.681.659 9.026-2.676-.115-2.559 4.551-5.551.666-7.336-17.251-2.342-28.711 18.466-46.4 9.005l-1.327-2.676c3.677-50.277 32.031-98.883 79.083-122.802 19.055-10.89 42.729-16.44 66.604-16.638 28.812-.24 57.928 7.319 79.586 22.65 21.031 12.9 41.148 34.161 52.066 55.744 10.443 18.911 15.248 40.13 16.684 62.719-18.248 13.788-30.49-12.989-49.072-6.996 1 2.897 1.555 5.896 3.68 8.343 16.469-.117 27.809 17.128 45.049 10.331l.018 2.516zM361.396 649.87c-11.572-7.566-26.242-.441-37.026 3.68-1.001-10.128-5.114-19.355-13.69-26.029-.832-.415-1.982-1.285-3.018-1.35-.62-.039-1.21.212-1.668 1.004 3.887 23.914-11.658 40.6-22.674 58.74 3.668 2.997 9.561 2.55 14.01 1.668 11.678-4.669 22.802-12.798 36.707-10.012 4.113-7.008 11.021-13.796 16.686-19.357 3.462-2.996 8.216-4.897 10.673-8.344zm35.381 15.018c-.223 1.44.111 4.008 2.331 3.015 3.454-4.783 9.569-.998 13.349-.662v-1.346c-2.074-3.257-6.34-4.892-10.674-4.73-1 .036-2.016.181-2.994.409-1.449.322-2.459 1.648-2.012 3.314zm54.397-14.33c-3.107-6.899-10.227-9.691-16.686-11.36-3.168-.39-6.455-.653-9.715-.685-.611-.006-1.221-.014-1.83 0-9.118.184-17.831 2.525-23.154 10.031-.764 2.119-4.123 5.095-1.004 6.994 7.009-.447 15.007-1.326 19.679 4.688 0 5.346 3.452 12.456-2.331 15.015-4.791-4.436-10.465 1.889-15.678-1.671-2.451-1.449-4.002.241-5.006 2.355-1.449 13.79 5.773 29.011 18.674 36.684 5.229 1.886 12.258 3.679 17.715 1.004 13.125-6.112 18.562-20.12 21.008-33.026-.328-2.456-.213-7.242-3.658-7.359-3.891 2.462-9.566 1.007-13.689 0-2.439.787-2.551 4.355-5.326 5.031l-1.691-1.349c-1.449-6.11-2.342-14.789 3.338-19.017 6.346-2.559 13.799-3.676 20.361-1.668 1.225-2.114-.665-3.777-1.007-5.667zm-6.697 10.968c-.441.012-.869.034-1.305.048-2.33 1.555-9.123.437-6.67 5.667 4.229-3.345 10.125-1.001 14.352.662 1.562-.775 1.225-2.333 1.326-3.677-1.358-2.525-4.6-2.761-7.703-2.7zm48.433-47.035c-8.234-.437-15.129 4.238-22.695 6.352-.658-6.438 4.232-13.237 5.348-19.7-12.232.787-21.922 8.151-32.043 14.033a5081.318 5081.318 0 0 1-7.016-17.028l13.35-12.684.342-1.672-18.033-1.004c-3.105-5.442-4.33-11.997-8-17.003-3.004 5.674-5.022 11.678-8.023 17.345l-18.01 1.007v1.327l13.669 12.342c-1.671 6.344-3.224 13.237-7.337 18.031-8.008-4.797-15.904-10.017-24.684-13.349-2.113-.681-5.125-1.79-7.016-.342 2.777 6.121 6.666 12.704 6.012 19.384-3.674-.676-6.561-3.031-10.012-4.021-4.127-1.127-9.023-3.326-13.027-1.988 6.676 9.338 12.125 19.113 14.01 30.352 22.147-10.69 47.975-16.569 74.785-12 12.568 1.999 24.123 7.119 35.361 11.335.439-11.784 7.89-21.253 13.019-30.717zm38.033 64.085c11.338 3 22.711 15.354 34.717 7.339-10.457-17.351-26.02-33.375-21.691-56.064.453-1.549-.549-3.117-1.988-3.337-6.783 2.772-12.037 9.455-14.721 16.023-1.104 3.886-1.756 8.015-3.311 11.678-8.127-4.23-16.797-8.326-26.697-7.337-3.572.445-8.688.679-9.348 4.345 11.234 6.004 20.473 16.023 27.359 26.694 4.796-.781 10.675-.114 15.68.659zm-118.806 15.682l.32 2.011c4.347 2.895 5.684 7.803 11.703 7.358 5.336.316 9.34-4.684 11.998-8.686-7.22-.998-16.01.545-24.021-.683zm6.009-9.005c.217.89-.551 2.225.662 2.676 3.561-.773 10.01 1.894 11.998-2.334-1.389-1.555-3.654-2.261-6.033-2.261-2.377 0-4.843.692-6.627 1.919z\" fill=\"#106e33\"/><path d=\"M412.145 681.392h24.393v10.021h-24.393v-10.021z\" opacity=\".99\" fill=\"#fff\"/><path d=\"M416.65 686.751c3.152.746 3.932 1.863 6.422 2.176 2.65-.047 2.268-.585 6.885-2.179.305-.107.721-.777.205-1.29-2.869-2.846-4.852-.812-6.648-.758-2.387.025-4.623-2.042-6.953.646-.397.545-.604 1.082.089 1.405z\" fill=\"#106e33\"/><g fill=\"#106e33\"><path d=\"M531.887 851.174v-14.273h-5.098v-2.906h13.652v2.906h-5.086v14.273h-3.468zM542.645 851.174v-17.18h5.191l3.117 11.719 3.082-11.719h5.203v17.18h-3.223v-13.523l-3.41 13.523h-3.34l-3.398-13.523v13.523h-3.222z\"/></g></svg>");
            fileContent.Append("<p>");
            fileContent.Append($"<div>{empleados.fechaRenuncia}</div>");
            fileContent.Append("</p>");
            fileContent.Append("<p>");
            fileContent.Append($"<div>{empleados.nombre}</div>");
            fileContent.Append($"<div>{empleados.cargo}</div>");
            fileContent.Append($"<div>{empleados.DUI}</div>");
            fileContent.Append($"<div>{empleados.nISSS}</div>");
            fileContent.Append($"<div>{empleados.direccion}</div>");
            fileContent.Append($"<div>{empleados.nTelefono}</div>");
            fileContent.Append($"<div>{empleados.correo}</div>");
            fileContent.Append("</p>");
            fileContent.Append("<p>");
            fileContent.Append($"<div>{empleados.empresa}</div>");
            fileContent.Append($"<div>{empleados.direccionEmpresa}</div>");
            fileContent.Append($"<div>{empleados.telefonoEmpresa}</div>");
            fileContent.Append($"<div>{empleados.correoEmpresa}</div>");
            fileContent.Append("</p>");
            fileContent.Append("<p>");
            fileContent.Append($"<div>Estimado empleador de {empleados.empresa},</div>");
            fileContent.Append("</p>");

            if (string.Equals(empleados.tipoContrato, "eventual", StringComparison.OrdinalIgnoreCase))
            {
                fileContent.Append("<p>");
                fileContent.Append($"<div>Por medio de la presente, yo, {empleados.nombre}, con número de DUI {empleados.DUI} y número de ISSS {empleados.nISSS}, deseo presentar mi renuncia voluntaria al puesto de {empleados.cargo} en {empleados.empresa}.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Mi contrato de trabajo tiene una duración determinada y está próximo a su finalización. He tomado la decisión de no renovar mi contrato y continuar mi trayectoria profesional en otros horizontes.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Agradezco sinceramente la oportunidad que se me ha brindado de formar parte de la empresa durante este periodo y de contribuir con mis habilidades y conocimientos al equipo de trabajo. Ha sido una experiencia enriquecedora que valoraré siempre.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Durante mi periodo de preaviso, me comprometo a finalizar todas las tareas y responsabilidades asignadas, así como a colaborar en la transición para asegurar una continuidad operativa sin contratiempos.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Deseo expresar mi gratitud hacia usted y hacia todo el equipo de {empleados.empresa} por su apoyo, orientación y compañerismo a lo largo de mi contrato. Ha sido un placer trabajar con profesionales tan comprometidos y talentosos.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Agradezco nuevamente la oportunidad que se me ha brindado y le deseo a usted y a [Nombre de la Empresa] todo lo mejor en el futuro. Estoy abierto(a) a cualquier instrucción adicional o requerimiento que deba seguir para concluir este proceso de renuncia de manera adecuada.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Atentamente empleado temporal,</div>");
                fileContent.Append("</p>");
            }
            else
            {
                fileContent.Append("<p>");
                fileContent.Append($"<div>Por medio de la presente, yo, {empleados.nombre}, con número de DUI {empleados.DUI} y número de ISSS {empleados.nISSS}, deseo presentar mi renuncia voluntaria al puesto de {empleados.cargo} en {empleados.empresa}.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Mi contrato de trabajo tiene una duración determinada y está próximo a su finalización. He tomado la decisión de no renovar mi contrato y continuar mi trayectoria profesional en otros horizontes.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Agradezco sinceramente la oportunidad que se me ha brindado de formar parte de la empresa durante este periodo y de contribuir con mis habilidades y conocimientos al equipo de trabajo. Ha sido una experiencia enriquecedora que valoraré siempre.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Durante mi periodo de preaviso, me comprometo a finalizar todas las tareas y responsabilidades asignadas, así como a colaborar en la transición para asegurar una continuidad operativa sin contratiempos.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Deseo expresar mi gratitud hacia usted y hacia todo el equipo de {empleados.empresa} por su apoyo, orientación y compañerismo a lo largo de mi contrato. Ha sido un placer trabajar con profesionales tan comprometidos y talentosos.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Agradezco nuevamente la oportunidad que se me ha brindado y le deseo a usted y a {empleados.empresa} todo lo mejor en el futuro. Estoy abierto(a) a cualquier instrucción adicional o requerimiento que deba seguir para concluir este proceso de renuncia de manera adecuada.</div>");
                fileContent.Append("</p>");
                fileContent.Append("<p>");
                fileContent.Append($"<div>Atentamente empleado permanente,</div>");
                fileContent.Append("</p>");
            }

            fileContent.Append("<p>");
            fileContent.Append($"<div>Firma: __________________</div>");
            fileContent.Append($"<div>{empleados.nombre}</div>");
            fileContent.Append($"<div>{empleados.cargo}</div>");
            fileContent.Append("</p>");
            fileContent.Append("</body>\r\n</html>");
            string htmlString = fileContent.ToString();

            // Create a memory stream to hold the PDF document
            MemoryStream stream = new MemoryStream();

            // Create an iText7 PdfWriter
            PdfWriter writer = new PdfWriter(stream);
            iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer);

            // Create an iText7 ConverterProperties object
            ConverterProperties properties = new ConverterProperties();

            // Create an iText7 Document
            Document document = new Document(pdf);

            // Convert the HTML string to PDF and save it to the document
            HtmlConverter.ConvertToPdf(htmlString, pdf, properties);

            // Close the document and writer, which will flush the content to the stream
            document.Close();
            writer.Close();

            // Create a new memory stream from the existing stream
            MemoryStream newStream = new MemoryStream(stream.ToArray());

            // Return the PDF file as a download
            return File(newStream, "application/pdf", "custom.pdf");
        }

        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        // GET: Empleados/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados
                .FirstOrDefaultAsync(m => m.iD == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // GET: Empleados/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("iD,nombre,tipoContrato,pais,empresa,salario,fechaContrato,fechaRenuncia,fechaEmision,mesesTrabajo,cargo,DUI,nISSS,direccion,nTelefono,correo,direccionEmpresa,telefonoEmpresa,correoEmpresa")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados.FindAsync(id);
            if (empleados == null)
            {
                return NotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("iD,nombre,tipoContrato,pais,empresa,salario,fechaContrato,fechaRenuncia,fechaEmision,mesesTrabajo,cargo,DUI,nISSS,direccion,nTelefono,correo,direccionEmpresa,telefonoEmpresa,correoEmpresa")] Empleados empleados)
        {
            if (id != empleados.iD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadosExists(empleados.iD))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados
                .FirstOrDefaultAsync(m => m.iD == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
            }
            var empleados = await _context.Empleados.FindAsync(id);
            if (empleados != null)
            {
                _context.Empleados.Remove(empleados);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadosExists(int id)
        {
          return (_context.Empleados?.Any(e => e.iD == id)).GetValueOrDefault();
        }
    }
}

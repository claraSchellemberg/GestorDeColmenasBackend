using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;

// =============================================================
// Script para hashear contraseñas existentes en texto plano
// Usa PBKDF2-SHA256 con el mismo formato que la clase Hash
// del proyecto principal (iteraciones.salt.key)
//
// Paquete NuGet necesario:
//   dotnet add package Microsoft.Data.SqlClient
//
// IMPORTANTE: Hacé un backup de la base antes de ejecutar esto.
// =============================================================

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

// Modificá este connection string según tu configuración
const string connectionString =
    "Server=(localdb)\\MSSQLLocalDB;Database=GestorApiariosDB;Trusted_Connection=True;";

bool autoConfirm = args is not null && (Array.Exists(args, a => a == "-y" || a == "--yes"));
Console.WriteLine("=== Hasheador de contraseñas - GestorApiariosDB ===\n");

var usuarios = ObtenerUsuarios(connectionString);

if (usuarios.Count == 0)
{
    Console.WriteLine("No se encontraron usuarios para actualizar.");
    return;
}

Console.WriteLine($"Se encontraron {usuarios.Count} usuario(s).\n");

foreach (var (id, nombre, contrasenaActual) in usuarios)
{
    if (string.IsNullOrEmpty(contrasenaActual))
    {
        Console.WriteLine($"  ID: {id} | Nombre: {nombre} | [SIN CONTRASEÑA - SE OMITE]");
        continue;
    }

    bool yaEstaHasheada = EsHashPBKDF2(contrasenaActual);
    string estado = yaEstaHasheada ? "[YA HASHEADA - SE OMITE]" : "[PENDIENTE]";
    Console.WriteLine($"  ID: {id} | Nombre: {nombre} | {estado}");
}

if (!autoConfirm)
{
    Console.Write("\nConfirmar hasheo? (s/n): ");
    var confirmacion = Console.ReadLine()?.Trim().ToLower();
    if (confirmacion != "s")
    {
        Console.WriteLine("Operación cancelada.");
        return;
    }
}
else
{
    Console.WriteLine("\nAuto-confirmado (-y). Ejecutando...");
}

int actualizados = 0;
int omitidos = 0;
int errores = 0;

foreach (var (id, nombre, contrasenaActual) in usuarios)
{
    if (string.IsNullOrEmpty(contrasenaActual) || EsHashPBKDF2(contrasenaActual))
    {
        omitidos++;
        continue;
    }

    try
    {
        string hash = HashPassword(contrasenaActual);
        ActualizarContrasena(connectionString, id, hash);
        actualizados++;
        Console.WriteLine($"  OK Usuario '{nombre}' (ID: {id}) - Contraseña hasheada correctamente.");
    }
    catch (Exception ex)
    {
        errores++;
        Console.WriteLine($"  ERROR Usuario '{nombre}' (ID: {id}) - {ex.Message}");
    }
}

Console.WriteLine($"\n=== Resultado ===");
Console.WriteLine($"  Actualizados: {actualizados}");
Console.WriteLine($"  Omitidos (ya hasheados / sin contraseña): {omitidos}");
Console.WriteLine($"  Errores: {errores}");
Console.WriteLine($"  Total procesados: {usuarios.Count}");

// =====================
// Funciones auxiliares
// =====================

static string HashPassword(string password)
{
    const int saltSize = 16;   // 128 bits
    const int keySize = 32;    // 256 bits
    const int iterations = 100_000;

    var salt = new byte[saltSize];
    RandomNumberGenerator.Fill(salt);

    using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
    var key = pbkdf2.GetBytes(keySize);

    // Formato: iteraciones.salt.key
    return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
}

static bool EsHashPBKDF2(string valor)
{
    if (string.IsNullOrEmpty(valor)) return false;

    var parts = valor.Split('.', 3);
    if (parts.Length != 3) return false;
    if (!int.TryParse(parts[0], out _)) return false;

    try
    {
        Convert.FromBase64String(parts[1]);
        Convert.FromBase64String(parts[2]);
        return true;
    }
    catch (FormatException)
    {
        return false;
    }
}

static List<(int Id, string Nombre, string Contrasena)> ObtenerUsuarios(string connStr)
{
    var usuarios = new List<(int, string, string)>();

    using var connection = new SqlConnection(connStr);
    connection.Open();

    // Usar corchetes para nombres que contienen caracteres especiales
    using var command = new SqlCommand(
        "SELECT Id, Nombre, [Contraseña] FROM [dbo].[Usuarios]", connection);

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        int id = reader.GetInt32(0);
        string nombre = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
        string contrasenia = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
        usuarios.Add((id, nombre, contrasenia));
    }

    return usuarios;
}

static void ActualizarContrasena(string connStr, int id, string hashNuevo)
{
    using var connection = new SqlConnection(connStr);
    connection.Open();

    using var command = new SqlCommand(
        "UPDATE [dbo].[Usuarios] SET [Contraseña] = @hash WHERE Id = @id", connection);

    var pHash = command.Parameters.Add("@hash", SqlDbType.NVarChar, -1);
    pHash.Value = hashNuevo;
    var pId = command.Parameters.Add("@id", SqlDbType.Int);
    pId.Value = id;

    int affected = command.ExecuteNonQuery();
    if (affected != 1)
    {
        throw new InvalidOperationException($"No se pudo actualizar el usuario Id={id} (filas afectadas: {affected}).");
    }
}
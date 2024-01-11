using System;
using System.Security.Cryptography;
using System.Text;

namespace LoginWebApp.Service
{
    public static class UtilityService
    {
        public static string ConvertirSHA256(string texto)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                // Obtener el hash del texto recibido
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Convertir el array byte en cadena de texto
                foreach (byte b in hashValue)
                    hash += $"{b:X2}";
            }
            return hash;
        }

        public static string GenerarToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}

using System;
using System.Linq;
using System.Text;

namespace VetClinic.UI1
{
    public static class AuthUtil
    {
        // Görünmeyen/garip boşluk karakterlerini temizler + trimler
        public static string Clean(string input)
        {
            if (input == null) return string.Empty;

            // Normal boşluklar + NBSP + zero-width + BOM vb. temizle
            var s = input
                .Replace("\u00A0", " ")   // NBSP
                .Replace("\u200B", "")    // zero-width space
                .Replace("\u200C", "")    // zero-width non-joiner
                .Replace("\u200D", "")    // zero-width joiner
                .Replace("\uFEFF", "");   // BOM

            return s.Trim();
        }

        public static string NormalizeEmail(string email)
            => Clean(email).ToLowerInvariant();

        public static string NormalizePassword(string password)
            => Clean(password); // şifreyi küçük harfe çevirmiyoruz
    }
}

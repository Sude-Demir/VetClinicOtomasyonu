using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetClinic.UI1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Veritabanını başlangıçta hazırla
            try
            {
                using (var db = new VetClinic.UI1.Data.VetClinicContext())
                {
                    db.EnsureSeeded();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Veritabanı başlatma hatası:\n{ex.Message}\n\n{ex.InnerException?.Message}\n\n{ex.StackTrace}",
                    "Başlatma Hatası",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // FrmRoleSelection ile başlatıyoruz
            Application.Run(new FrmRoleSelection());
        }
    }
}

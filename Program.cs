using System;
using System.Windows.Forms;

namespace crc8__calculate   // sizin namespace’iniz buradaysa iki alt çizgiyle de olur
{
    static class Program
    {
        [STAThread]  // Form’ların doğru şekilde çalışması için önemli
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());  // Form1’i başlat
        }
    }
}

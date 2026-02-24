using System;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace crc8__calculate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // Kullanıcıdan gelen hex veriyi parse et
                byte[] body = textBox1.Text
                    .Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(b => Convert.ToByte(b, 16))
                    .ToArray();

                // Başlangıç ve bitiş baytlarını ekle
                byte start = 0xAA;
                byte end = 0x55;

                // CRC hesapla (body dizisi üzerinden)
                byte crc = CRC8_Compute(body);

                // Tam komutu oluştur
                byte[] fullCmd = new byte[body.Length + 3];
                fullCmd[0] = start;
                Array.Copy(body, 0, fullCmd, 1, body.Length);
                fullCmd[body.Length + 1] = crc;
                fullCmd[body.Length + 2] = end;

                // Ekranda göster
                textBox2.Text = string.Join(" ", fullCmd.Select(b => b.ToString("X2")));
            }
            catch
            {
                MessageBox.Show("Geçersiz giriş! Örnek: 00 10 02 01 01");
            }
        }

        
        private byte CRC8_Compute(byte[] data)
        {
            byte crc = 0x00;
            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x80) != 0)
                        crc = (byte)((crc << 1) ^ 0x07);
                    else
                        crc <<= 1;
                }
            }
            return crc;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                Clipboard.SetText(textBox2.Text);
                MessageBox.Show("Komut panoya kopyalandı ✅", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

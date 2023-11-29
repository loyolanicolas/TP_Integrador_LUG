using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UI
{
    public partial class Auditoria : Form
    {
        public Auditoria()
        {
            InitializeComponent();
        }

        private void Auditoria_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Name = "UnicaCol";

            string buscoArchivo = "Bitacora.txt";

            if (File.Exists(buscoArchivo))
            {
                string[] lineas = File.ReadAllLines(buscoArchivo);

                foreach (string linea in lineas)
                {
                    dataGridView1.Rows.Add(linea.Split(';'));
                }
                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("El archivo no existe.");
            }
        }
    }
}

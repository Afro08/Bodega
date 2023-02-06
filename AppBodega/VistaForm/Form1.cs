using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BD;
using MaterialSkin;

namespace VistaForm
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        CRUDSql crud = new CRUDSql();
        private bool guardar = false;
        private string codigo = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SkinManager.Theme = MaterialSkinManager.Themes.DARK;
            SkinManager.ColorScheme = new ColorScheme(Primary.Green900,Primary.Green800, Primary.Green800,Accent.Red700,TextShade.WHITE);
            Mostrar();
            Botones();
        }


        #region Eventos
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fecha = DateTime.Now;
                crud.Insertar(textBoxDescripcion.Text, fecha, Convert.ToInt32(textBoxValor.Text), Convert.ToInt32(textBoxStock.Text), Convert.ToInt32(comboBoxBodega.Text));
                MessageBox.Show("Inserción correcta!");
                limpiar();
                Mostrar();
                btnAgregar.Enabled = false;
            }catch (Exception ex)
                {
                    MessageBox.Show("No se pudo insertar por : " + ex);
                }
                    

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                crud.Editar(textBoxDescripcion.Text, Convert.ToInt32(textBoxValor.Text), Convert.ToInt32(textBoxStock.Text), Convert.ToInt32(this.comboBoxBodega.GetItemText(this.comboBoxBodega.SelectedItem)), Convert.ToInt32(codigo));
                MessageBox.Show("Edición correcta!");
                guardar = false;
                limpiar();
                Mostrar();
                btnAgregar.Enabled = false;
                Botones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo insertar por : " + ex);
            }


        }
        private void btnEditar_Click(object sender, EventArgs e)
        {

            guardar = true;
            if (guardar)
            {
                btnAgregar.Enabled = false;
            }
            if (dataGridView1.SelectedRows.Count == 1)
                {
                    textBoxDescripcion.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                    textBoxValor.Text = dataGridView1.CurrentRow.Cells["Valor"].Value.ToString();
                    textBoxStock.Text = dataGridView1.CurrentRow.Cells["Stock_Minimo"].Value.ToString();
                    codigo = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                }
            else
                {
                    MessageBox.Show("Seleccione una fila para editar!");
                }
         }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                codigo = dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString();
                crud.Eliminar(Convert.ToInt32(codigo));
                MessageBox.Show("Eliminación correcta!");
                Mostrar();
                Botones();
            }
            else
                MessageBox.Show("Seleccione una fila para eliminar!");
        }
        #endregion
        #region validaciones
        //TextBox Descripcion
        private void textBoxDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            Botones();
        }
        //TextBox Valor
        private void textBoxValor_KeyUp(object sender, KeyEventArgs e)
        {
            Botones();
        }

        private void textBoxValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //TextBox Stock
        private void textBoxStock_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void textBoxStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //ComboBox

        private void comboBoxBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            Botones();
        }



        #endregion
        #region Utilidad
        private void Mostrar()
        {
            CRUDSql mostrar = new CRUDSql();
            dataGridView1.DataSource = mostrar.Mostrar();
            if(!guardar)
            {
                btnAgregar.Enabled = true;
            }
        }
        private void Botones()
        {
            if(string.IsNullOrEmpty(textBoxDescripcion.Text) | string.IsNullOrEmpty(textBoxValor.Text)| string.IsNullOrEmpty(textBoxStock.Text) | string.IsNullOrEmpty(comboBoxBodega.Text))
            {
                btnAgregar.Enabled = false;
                btnGuardar.Enabled = false;
            }
            else
            {
                btnAgregar.Enabled = true;
            }
            if(guardar)
            {
                btnGuardar.Enabled = true;
                btnAgregar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = false;
            }

        }
        private void limpiar()
        {
            textBoxDescripcion.Clear();
            textBoxValor.Clear();
            textBoxStock.Clear();
            comboBoxBodega.SelectedItem = null;
        }
        #endregion

    }
}

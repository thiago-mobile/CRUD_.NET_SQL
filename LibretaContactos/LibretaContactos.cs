using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibretaContactos
{
    public partial class LibretaContactos : Form
    {
        public LibretaContactos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre= txtNombre.Text;
            string correo=txtCorreo.Text;
            string telefono=txtTelefono.Text;
            string tipo=cbxTipo.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(tipo))
            {
                MessageBox.Show("debe completar todos los campos");
            }
            else 
            {
                MessageBox.Show("Validaciones correctas");
                Contacto nuevoContacto = new Contacto(0,nombre,correo,telefono,tipo);
                int filas= nuevoContacto.agregarContacto();
                if (filas >0)
                {
                    MessageBox.Show("Registro se agrego correctamente, filas afectadas: " + filas, "exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarText();
                }
                else 
                {
                    MessageBox.Show("Ocurrio un problema al agregar el registro","error",MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        public void limpiarText()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            cbxTipo.Text = "Seleccione una opcion";
        }

        private void LibretaContactos_Load(object sender, EventArgs e)
        {
            listarContactos();
        }

        public void listarContactos()
        {
            Contacto contacto = new Contacto();
            contacto.cargarContactos(dtgContactos);
            
        }

        private void dtgContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            if (indice == -1 || dtgContactos.SelectedCells[1].Value.ToString() == "")
            {
                limpiarForm();
            }
            else
            {
                txtId.Text = dtgContactos.SelectedCells[0].Value.ToString();
                txtNombre.Text = dtgContactos.SelectedCells[1].Value.ToString();
                txtCorreo.Text = dtgContactos.SelectedCells[2].Value.ToString();
                txtTelefono.Text = dtgContactos.SelectedCells[3].Value.ToString();
                cbxTipo.Text = dtgContactos.SelectedCells[4].Value.ToString();

                btnAgregar.Enabled= false;
                btnEliminar.Enabled= true;
                btnModificar.Enabled = true;
            }
        }

        private void limpiarForm()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            cbxTipo.Text = "Seleccione Tipo";

            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            txtNombre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id= Convert.ToInt32(txtId.Text);
            DialogResult confirmar = MessageBox.Show("¿Desea eliminar?", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (confirmar == DialogResult.OK)
            {
                MessageBox.Show("Confirmar eliminacion");
                Contacto contacto = new Contacto(id);
                int filas=contacto.eliminarContactos();
                if (filas > 0) 
                {
                    MessageBox.Show("Contacto eliminado, fueron: " + filas + " Afectadas");
                    limpiarForm();
                    listarContactos();
                } 
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id= Convert.ToInt32(txtId.Text);
            string nombre=txtNombre.Text;
            string correo=txtCorreo.Text;
            string tipo=cbxTipo.Text;
            string telefono = txtTelefono.Text;

            DialogResult modificar = MessageBox.Show("¿Desea modificar este contacto?","Mensaje",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if(modificar == DialogResult.OK)
            {
                MessageBox.Show("Confirmar modificacion");
                Contacto contacto = new Contacto(id,nombre,correo,telefono,tipo);
                int filas =contacto.editarContacto();
                if (filas > 0) 
                {
                    MessageBox.Show("Contacto modificado, fueron: " + filas + " Afectadas");
                    limpiarForm();
                    listarContactos();
                }

            }
        }
    }
}

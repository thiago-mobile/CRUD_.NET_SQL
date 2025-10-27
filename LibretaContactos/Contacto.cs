using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace LibretaContactos
{
    class Contacto
    {
        private int id;
        private string nombre;
        private string correo;
        private string telefono;
        private string tipo;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-1DFGB59\\SQLEXPRESS;Initial Catalog=BD_Contactos;Integrated Security=True;Encrypt=False");

        public Contacto(int id,string name, string correo, string telefono, string tipo)
        {
            this.id = id;
            this.nombre = name;
            this.correo = correo;
            this.telefono = telefono;
            this.tipo = tipo;
        }

        public Contacto()
        {

        }

        public Contacto(int id)
        {
            this.id= id;
        }

        public int agregarContacto() {
            con.Open();
            SqlCommand consulta = new SqlCommand("INSERT INTO tb_contactos VALUES(@nombre,@correo,@telefono,@tipo)",con);
            consulta.Parameters.AddWithValue("nombre", nombre);
            consulta.Parameters.AddWithValue("correo", correo);
            consulta.Parameters.AddWithValue("telefono", telefono);
            consulta.Parameters.AddWithValue("tipo", tipo);

            int filasAfectadas=consulta.ExecuteNonQuery();
            con.Close();
            return filasAfectadas;
        }

        public void cargarContactos(DataGridView dtg)
        {
            string consulta = "SELECT * FROM tb_contactos";
            con.Open();
            SqlDataAdapter data = new SqlDataAdapter(consulta,con);
            DataTable dt= new DataTable(); // tabla virtual para dar datos mediante el objeto data
            data.Fill(dt);
            dtg.DataSource = dt;

        }

        public int eliminarContactos()
        {
        con.Open();
            SqlCommand delete = new SqlCommand("DELETE FROM tb_contactos WHERE Id= @codigo",con);
            delete.Parameters.AddWithValue("codigo", id);
            int filasAfectadas= delete.ExecuteNonQuery();
            con.Close();
            return filasAfectadas;
            
        }

        public int editarContacto() 
        {
            int filasAfectadas;
            con.Open();
            SqlCommand edit = new SqlCommand("UPDATE tb_contactos SET nombre=@nombreContacto, correo=@email, telefono=@telefono, tipo=@tipo WHERE id=@codigo",con);
            edit.Parameters.AddWithValue("codigo",id);
            edit.Parameters.AddWithValue("nombreContacto", nombre);
            edit.Parameters.AddWithValue("email", correo);
            edit.Parameters.AddWithValue("telefono", telefono);
            edit.Parameters.AddWithValue("tipo", tipo);
            filasAfectadas = edit.ExecuteNonQuery();
            con.Close();
            return filasAfectadas;


        }

        public int Id { get => id; set => id = value; }
        public string Name { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Tipo { get => tipo; set => tipo = value; }


    }
}

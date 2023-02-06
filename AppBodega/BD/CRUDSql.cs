using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BD
{
    public class CRUDSql
    {
        private ConexionBD conexion = new ConexionBD();

        SqlDataReader leer;
        DataTable table = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable Mostrar()
        {
            comando.Connection = conexion.ConectarBD();
            comando.CommandText = "MostrarArticulos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            table.Load(leer);
            conexion.DesconectarBD();
            return table;
        }
        public void Insertar(string descripcion, DateTime fecha, int valor, int stock,int codigoBodega)
        {
            comando.Connection = conexion.ConectarBD();
            comando.CommandText = "InsertarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Descripcion", descripcion);
            comando.Parameters.AddWithValue("@Fecha", fecha);
            comando.Parameters.AddWithValue("@Valor", valor);
            comando.Parameters.AddWithValue("@Stock", stock);
            comando.Parameters.AddWithValue("@CodigoBodega", codigoBodega);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.DesconectarBD();
        }
        public void Editar(string descripcion, int valor, int stock, int codigoBodega,int codigo)
        {
            comando.Connection = conexion.ConectarBD();
            comando.CommandText = "EditarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Descripcion", descripcion);
            comando.Parameters.AddWithValue("@Valor", valor);
            comando.Parameters.AddWithValue("@Stock", stock);
            comando.Parameters.AddWithValue("@Codigo_Bodega", codigoBodega);
            comando.Parameters.AddWithValue("@Codigo", codigo);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.DesconectarBD();
        }
        public void Eliminar(int codigo)
        {
            comando.Connection = conexion.ConectarBD();
            comando.CommandText = "EliminarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Codigo", codigo);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.DesconectarBD();
        }

    }
}

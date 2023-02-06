using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace BD
{
    class ConexionBD
    {   

        public SqlConnection Conexion = new SqlConnection("Data Source = DESKTOP-54OMFLA\\SQLEXPRESS; Initial Catalog = Bodega; User = sa;Password = 123456");

        public SqlConnection ConectarBD()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection DesconectarBD()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }

}

using System.ComponentModel.DataAnnotations;

namespace Denoy_INFASS2.Models
{
    public class Users
    {
        
        [Key]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;


        public string GenerateSQL(string TableName, string[] Fields, object[] Values)
        {

            string sql = "";

            if (Fields.Length != Values.Length)
            {
                return "Fields and Values must have the same length.";
            }

            sql += "INSERT INTO " + TableName + " (";



            for (int i = 0; i < Fields.Length; i++)
            {
                sql += Fields[i];

                if (i < Fields.Length - 1)
                {
                    sql += ", ";
                }
                else
                {
                    sql += ") ";
                }
            }

            sql += "VALUES (";


            for (int v = 0; v < Values.Length; v++)
            {
                if (Values[v] != null)
                {

                    if (int.TryParse(Values[v].ToString(), out int _) || double.TryParse(Values[v].ToString(), out double _) || decimal.TryParse(Values[v].ToString(), out decimal _))
                    {
                        sql += Values[v];
                    }
                    else
                    {
                        sql += "'";
                        Values[v] = Values[v].ToString();
                        sql += Values[v];
                        sql += "'";
                    }
                }

                if (v < Values.Length - 1)
                {
                    sql += ", ";
                }
                else
                {
                    sql += "); ";
                }
            }


            return sql;


        }


        ////
        ////VIEW
        ////

        public string ViewSQL(string tablename, string[] fields)
        {
            string view = "SELECT ";

            if (tablename != null)
            {
                return "SELECT " + string.Join(", ", fields) + " FROM " + tablename + ";";
            }

            return view;
        }


        ////
        ////UPDATE
        ////

        //public string UpdateSQL(string tablename, string[] Field, object[] newValue, string conditionfield, object[] conditionvalue)
        //{
        //    string update = "UPDATE " + tablename + "\nSET ";

        //    for (int i = 0; i < Field.Length; i++)
        //    {
        //        update += Field[i] + " = '" + newValue[i] + "'";

        //        if (i < Field.Length - 1)
        //        {
        //            update += ", ";
        //        }
        //    }

        //    update += "\nWHERE " + conditionfield + " = '" + conditionvalue[0] + "';";

        //    return update;
        //}



        ////}

        ////
        ////DELETE
        ////

        //public string DeleteSQL(string tablename, string[] conditionfield, object[] conditionvalue)
        //{
        //    string sql = "DELETE FROM " + tablename + "\nWHERE ";

        //    for (int i = 0; i < conditionfield.Length; i++)
        //    {
        //        sql += conditionfield[i] +
        //               " = '" +
        //               conditionvalue[i] +
        //               "'";

        //        if (i < conditionfield.Length - 1)
        //        {
        //            sql += " AND ";
        //        }
        //    }

        //    sql += ";";

        //    return sql;
        //}



    }
}

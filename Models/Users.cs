namespace Denoy_INFASS2.Models
{
    public class Users
    {
        //public string _sql(string Username, string Email, string Password, string ConfPass)
        //{

        //    return "From Model: Username: " + Username + "\nEmail: " + Email + "\nPassword: " + Password + "\nConfirm Password: " + ConfPass;
        //}



        public string GenerateSQL(string TableName, string[] Fields, object[] Values)
        {

            string sql = "";

            if (Fields.Length != Values.Length)
            {
                return "Fields and Values must have the same length.";
            }

            sql += "INSERT INTO " + TableName +" (";



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

            sql += ") VALUES (";


            for (int v = 0; v < Values.Length; v++)
            {
                if (Values[v] != null)
                {

                    if (int.TryParse(Values[v].ToString(), out int result))
                    {
                        Values[v] = result.ToString();
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



    }
}

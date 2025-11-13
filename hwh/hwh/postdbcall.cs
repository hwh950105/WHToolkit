using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHToolkit.Database;

namespace hwh
{
     static class postdbcall
    {
        public static void postdbcallmain()
        {
            var connString = "Host=172.28.24.239;Port=5432;Username=postgres;Password=ghkd8077!@;Database=postgres;";

            using (NpgHelper db = new NpgHelper(connString))
            {
                List<test> tests = db.ExecuteList<test>(System.Data.CommandType.Text,"select * from test");
                test? test = tests.FirstOrDefault(a => a.id == 3);

                List<test> tests1 = tests.Where(a => a.id > 3).ToList();

                if(test.id == 1)
                {

                }


            }
        }


    }

    public class test
    {
        public string name { get; set; }
        public int id { get; set; }
    }
}

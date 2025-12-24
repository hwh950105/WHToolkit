using System.Linq;
using WHToolkit.Database;

namespace hwh
{
    static class PostDbCall
    {
        public static void Run()
        {
            var connString = "Host=172.28.24.239;Port=5432;Username=postgres;Password=ghkd8077!@;Database=postgres;";

            using (NpgHelper db = new NpgHelper(connString))
            {
                List<TestRecord> tests = db.ExecuteList<TestRecord>(System.Data.CommandType.Text, "select * from test");
                TestRecord? selected = tests.FirstOrDefault(a => a.id == 3);
                List<TestRecord> tests1 = tests.Where(a => a.id > 3).ToList();

                if (selected?.id == 1)
                {
                    // TODO: handle match
                }
            }
        }
    }

    public class TestRecord
    {
        public string name { get; set; } = string.Empty;
        public int id { get; set; }
    }
}

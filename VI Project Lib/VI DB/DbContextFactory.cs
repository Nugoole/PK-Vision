using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_DB
{
    class DbContextFactory
    {
        private const string FilePath = @"d:\EF.log";

        private static readonly string[] PrefixList = { "SELECT", "INSERT", "UPDATE", "DELETE" };

        private static void Write(string log)
        {
            string contents;
            if (File.Exists(FilePath))
                contents = File.ReadAllText(FilePath);
            else
                contents = "";

            string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string formattedLog = $"[{time}]\r\n{log}\r\n------------------------------\r\n\r\n";

            contents = formattedLog + contents; // 성능 구림

            File.WriteAllText(FilePath, contents);
        }

        public static PCBVIEntities Create()
        {
            PCBVIEntities context = new PCBVIEntities();

            context.Database.Log = Write;

            return context;
        }
    }
}

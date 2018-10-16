using DbFieldToWorld.GenerteWord;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace DbFieldToWorld
{
    class Program
    {
        
        //生成的文件名称
        private static string fileName = @"数据库字段说明";
        
        //字段，默认值，类型，可为空，描述，后期可以使用反射标注到类名
        private static string[] wordFieldName = new string[] { "字段", "数据类型", "可为空", "描述" };

        #region MyRegion

        #endregion

        public static string _tableFilesSql = $"select COLUMN_NAME,DATA_TYPE,'' as IsNullAble,COLUMN_COMMENT from information_schema.COLUMNS where table_name =";
        public static string _getTablesSql = "show tables;";

        static void Main(string[] args)
        {
            try
            {
                //传入mysql获取库的语句，
                Console.WriteLine("数据库说明生成中...");
                MySqlTablesToWord mySqlTablesToWord = new MySqlTablesToWord(_getTablesSql, _tableFilesSql);
                mySqlTablesToWord.CreateTablesToWord(fileName, wordFieldName);
                Console.WriteLine("数据库说明生成完毕...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库说明生成失败..."+"\n");
                Console.WriteLine(ex.ToString());
            }
            Console.ReadKey();
        }


    }
}



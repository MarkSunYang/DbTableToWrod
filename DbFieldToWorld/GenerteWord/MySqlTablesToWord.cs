using Dapper;
using DbFieldToWorld.ConectionFactory;
using DbFieldToWorld.Models;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;

namespace DbFieldToWorld.GenerteWord
{
    public class MySqlTablesToWord : TablesToWord
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        public DbConnection con;

        public static string _tableFilesSql= $"select COLUMN_NAME,DATA_TYPE,'' as IsNullAble,COLUMN_COMMENT from information_schema.COLUMNS where table_name =";
        public static string _getTablesSql = "show tables;";
        public MySqlTablesToWord(string getTablesSql,string tableFilesSql)
        {
            con = new ConnectionFactory().MySqlDbConnection();
            _tableFilesSql = tableFilesSql;
            _getTablesSql = getTablesSql;
        }

        /// <summary>
        /// 将表生成字段
        /// </summary>
        /// <param name="fileName">生成文件的名称</param>
        /// <param name="sql">获取数据库所有表 SELECT name FROM tmc..sysobjects Where xtype='U' ORDER BY name </param>
        /// <param name="WordFieldName"> Word中列的名称</param>
        public override void CreateTablesToWord(string fileName, string[] wordFieldName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                var m_Docx = CreateXWPFDocument(fileName);
                XWPFParagraph p0 = m_Docx.CreateParagraph();
                XWPFRun r0 = p0.CreateRun();
                r0.SetText("DOCX表");

                //获取数据源  
                var tables = con.Query<TablesName>(_getTablesSql).ToList();

                for (int i = 0; i < tables.Count; i++)
                {
                    //获取 数据源
                    var FieldNames = GetTableFileds(tables[i].Tables_in_Tmc);

                    XWPFTable table = m_Docx.CreateTable(2, 4);//创建一行四列表

                    //每一行的中文名称 比如 字段 数据类型 可为空 描述
                    for (int icol = 0; icol < 4; icol++)
                    {
                        table.GetRow(1).GetCell(icol).SetText(wordFieldName[icol]);
                        table.GetRow(1).GetCell(icol).SetColor("#BABABA");
                    }

                    //  数据库中的字段名称，类型备注信息
                    for (int j = 1; j < FieldNames.Count; j++)
                    {
                        XWPFTableRow m_Row = table.CreateRow();
                        m_Row.GetCell(0).SetText(FieldNames[j].COLUMN_NAME);
                        m_Row.GetCell(1).SetText(FieldNames[j].DotNet_DATA_TYPE);
                        m_Row.GetCell(2).SetText(FieldNames[j].IsNullAble);
                        m_Row.GetCell(3).SetText(FieldNames[j].COLUMN_COMMENT);
                    }

                    //标注表名，如果放在放在上面，只能生成一列,这样88行会报错
                    table.GetRow(0).MergeCells(0, 3);
                    table.GetRow(0).GetCell(0).SetText(tables[i].Tables_in_Tmc);
                    table.GetRow(0).GetCell(0).SetColor("#98FB98");
                    table.GetRow(0).GetCTRow().AddNewTrPr().AddNewTrHeight().val = (ulong)500;
                   
                    //创建一个空白行分隔每个表,并添加每一行标题
                    CT_P newLine = m_Docx.Document.body.AddNewP();
                    newLine.AddNewPPr().AddNewJc().val = ST_Jc.center;//段落水平居中
                    XWPFParagraph gp = new XWPFParagraph(newLine, m_Docx); //创建XWPFParagraph
                    XWPFRun runTitle = gp.CreateRun();
                    runTitle.IsBold = true;
                    runTitle.SetText(i.ToString()+"."+ tables[i].Tables_in_Tmc);
                    runTitle.FontSize = 16;
                    runTitle.SetFontFamily("宋体", FontCharRange.None);//设置雅黑字体  
                  
                }

                MemoryStream ms = new MemoryStream();
                m_Docx.Write(ms);
                ms.Flush();
                SaveToFile(ms, "d:\\Tmc数据库表字段说明.docx");
            }
        }

        public override List<TableModel> GetTableFileds(string tableName)
        {
            Console.WriteLine(tableName);
            string sql = _tableFilesSql + $"'{tableName}'";
            var tablesFields = con.Query<TableModel>(sql);
            return tablesFields.ToList();
        }
    }
}

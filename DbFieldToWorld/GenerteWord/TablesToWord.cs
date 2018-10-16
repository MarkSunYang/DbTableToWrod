using DbFieldToWorld.Models;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DbFieldToWorld.GenerteWord
{
    /// <summary>
    /// 将数据库Table生成到数据库
    /// </summary>
    public abstract class TablesToWord
    {
        /// <summary>
        /// 获取数据库中的表格，不同的数据库获取方式不一样
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract List<TableModel> GetTableFileds(string tableName);

        /// <summary>
        ///  根据表格，生成到Word中，sql语句中的字段要和TableFieldName中的字段匹配
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="sql">sql 语句,用于获取数据库中的所有表</param>
        /// <param name="TableFieldName">sql语句中的字段</param>
        public abstract void CreateTablesToWord(string fileName, string[] WordFieldName);


        /// <summary>
        /// 创建Word文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XWPFDocument CreateXWPFDocument(string name)
        {
            XWPFDocument doc = new XWPFDocument();
            var p0 = doc.CreateParagraph();
            p0.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r0 = p0.CreateRun();
            r0.FontFamily = "microsoft yahei";
            r0.FontSize = 18;
            r0.IsBold = true;
            r0.SetText("");
            var p1 = doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.LEFT;
            p1.IndentationFirstLine = 500;
            XWPFRun r1 = p1.CreateRun();
            r1.FontFamily = "·ÂËÎ";
            r1.FontSize = 12;
            r1.IsBold = true;
            return doc;
        }

        /// <summary>
        /// 保存Word文件
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="fileName"></param>
        public static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();
                data = null;
            }
        }
    }
}

using DbFieldToWorld.TypeMapper;
using System;
using static DbFieldToWorld.TypeMapper.DbToDotNet;

namespace DbFieldToWorld.Models
{
    /// <summary>
    /// 接受数据库字段
    /// </summary>
    public class TableModel
    {
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }

        public string DotNet_DATA_TYPE
        {
            get
            {
                var str = Enum.Parse(typeof(MySqlDbTypeToDotNet), DATA_TYPE.ToUpper());
                var aa = (MySqlDbTypeToDotNet)str;
                return aa.GetEnumDesc();
            }
        }

        // public string COLUMN_TYPE { get; set; }
        public string COLUMN_COMMENT { get; set; }

        public string IsNullAble { get; set; }
    }
}

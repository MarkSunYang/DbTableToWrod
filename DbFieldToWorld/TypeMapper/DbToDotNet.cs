using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace DbFieldToWorld.TypeMapper
{
    public class DbToDotNet
    {
        /// <summary>
        /// 数据库字段类型
        /// </summary>
        public enum MySqlDbTypeToDotNet
        {
            [Description("Int")]
            INT = 0,
            [Description("Boolean")]
            BOOLEAN = 1,
            [Description("String")]
            VARCHAR = 2,
            [Description("Boolean")]
            BIT = 3,
            [Description("DateTime")]
            TIMESTAMP = 4,
            [Description("String")]
            LONGTEXT = 5,
            [Description("DateTime")]
            DATETIME = 6,
            [Description("String")]
            CHAR = 7,
            [Description("Decimal")]
            DECIMAL=8,
            [Description("long")]
            BIGINT=9,
            [Description("float")]
            FLOAT = 10


        }
    }
}

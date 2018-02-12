using System;
using System.Collections.Generic;

namespace KELEI.PM.DBService.DBAccess
{
    public class BusinessModel
    {
        public string modelCode { get; set; }
        public string tableName { get; set; }
        public string dbConnection { get; set; }
        public List<Column> column { get; set; }
        public Type modelObj { get; set; }
    }

    public class Column
    {
        public string cloumnid { get; set; }
        public BindingcloumnType cloumnType { get; set; }
        public string dataCloumn { get; set; }
        public bool isPrimaryKey { get; set; } = false;
        public string sourceModel { get; set; }
        public string mappingKeys { get; set; }
        public string defaultValue { get; set; }
        public bool isLazy { get; set; } = true;
    }

    public class AnalysisModel
    {
        public string id { get; set; }
        public string selectSql { get; set; }
        public string insertSql { get; set; }
        public string updateSql { get; set; }
        public string deleteSql { get; set; }
        public List<UpdateColumn> keyColumnList { get; set; }
        public List<SelectColumn> selectColumnList { get; set; }
        public List<UpdateColumn> updateColumnList { get; set; }
        public List<UpdateColumn> insertColumnList { get; set; }
    }

    public class SelectColumn
    {
        public string columnCode { get; set; }

        public string columnTxt { get; set; }
        public string relationTxt { get; set; }
        public string relationTableName { get; set; }
    }

    public class UpdateColumn
    {
        public string columnCode { get; set; }
        public bool isPrimaryKey { get; set; }
        public string columnTxt { get; set; }
    }

    public class WhereEntitile
    {
        //string filedName:查询字段, dynamic oValue：查询值, string op操作符
        public string filedName { get; set; }
        public dynamic oValue { get; set; }
        public string op { get; set; }
        public WhereEntitile orCorrelation { get; set; }
    }
}

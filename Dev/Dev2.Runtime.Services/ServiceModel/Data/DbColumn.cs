﻿using System;
using System.Data;

namespace Dev2.Runtime.ServiceModel.Data
{
    public class DbColumn
    {
        SqlDbType _sqlDataType;

        public DbColumn()
        {
        }

        public DbColumn(DataColumn dataColumn)
        {
            ColumnName = dataColumn.ColumnName;
            DataType = dataColumn.DataType;
            MaxLength = dataColumn.MaxLength;
        }

        public string ColumnName { get; set; }

        public Type DataType { get; set; }

        public SqlDbType SqlDataType
        {
            get
            {
                return _sqlDataType;
            }
            set
            {
                _sqlDataType = value;
                DataType = ConvertSqlDbType(value);
            }
        }

        public int MaxLength { get; set; }

        public string DataTypeName
        {
            get
            {
                if(SqlDataType == SqlDbType.VarChar || SqlDataType == SqlDbType.Char || SqlDataType == SqlDbType.NVarChar || SqlDataType == SqlDbType.NChar)
                {
                    return string.Format("{0} ({1})", SqlDataType, MaxLength).ToLower();
                }

                return SqlDataType.ToString().ToLower();

            }
        }

        public static Type ConvertSqlDbType(SqlDbType sqlDbType)
        {
            // http://msdn.microsoft.com/en-us/library/system.data.sqldbtype.aspx
            switch(sqlDbType)
            {
                case SqlDbType.BigInt:
                    return typeof(long);
                case SqlDbType.Binary:
                    return typeof(byte[]);
                case SqlDbType.Bit:
                    return typeof(bool);
                case SqlDbType.Char:
                    return typeof(string);
                case SqlDbType.DateTime:
                    return typeof(DateTime);
                case SqlDbType.Decimal:
                    return typeof(decimal);
                case SqlDbType.Float:
                    return typeof(double);
                case SqlDbType.Image:
                    return typeof(byte[]);
                case SqlDbType.Int:
                    return typeof(int);
                case SqlDbType.Money:
                    return typeof(decimal);
                case SqlDbType.NChar:
                    return typeof(string);
                case SqlDbType.NText:
                    return typeof(string);
                case SqlDbType.NVarChar:
                    return typeof(string);
                case SqlDbType.Real:
                    return typeof(Single);
                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid);
                case SqlDbType.SmallDateTime:
                    return typeof(DateTime);
                case SqlDbType.SmallInt:
                    return typeof(short);
                case SqlDbType.SmallMoney:
                    return typeof(decimal);
                case SqlDbType.Text:
                    return typeof(string);
                case SqlDbType.Timestamp:
                    return typeof(byte[]);
                case SqlDbType.TinyInt:
                    return typeof(byte);
                case SqlDbType.VarBinary:
                    return typeof(byte[]);
                case SqlDbType.VarChar:
                    return typeof(string);
                case SqlDbType.Variant:
                    return typeof(object);
                case SqlDbType.Xml:
                    return typeof(string);
                case SqlDbType.Udt:
                    return typeof(object);
                case SqlDbType.Structured:
                    return typeof(object);
                case SqlDbType.Date:
                    return typeof(DateTime);
                case SqlDbType.Time:
                    return typeof(DateTime);
                case SqlDbType.DateTime2:
                    return typeof(DateTime);
                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset);
            }

            return typeof(object);
        }
    }
}
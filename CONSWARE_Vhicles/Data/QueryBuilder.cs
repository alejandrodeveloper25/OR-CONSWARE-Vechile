using System;
using System.Text;

namespace CONSWARE_Vhicles.Data
{
    public class QueryBuilder
    {
        private StringBuilder _query;

        public QueryBuilder()
        {
            _query = new StringBuilder();
        }

        public QueryBuilder Select(string columns)
        {
            _query.Append($"SELECT {columns} ");
            return this;
        }

        public QueryBuilder From(string tableName)
        {
            _query.Append($"FROM {tableName} ");
            return this;
        }

        public QueryBuilder Where(string condition)
        {
            _query.Append($"WHERE {condition} ");
            return this;
        }

        public QueryBuilder And(string condition)
        {
            _query.Append($"AND {condition} ");
            return this;
        }

        public QueryBuilder Or(string condition)
        {
            _query.Append($"OR {condition} ");
            return this;
        }

        public QueryBuilder InsertInto(string tableName)
        {
            _query.Append($"INSERT INTO {tableName} ");
            return this;
        }

        public QueryBuilder Columns(string columns)
        {
            _query.Append($"({columns}) ");
            return this;
        }

        public QueryBuilder Values(string values)
        {
            _query.Append($"VALUES ({values}) ");
            return this;
        }

        public QueryBuilder Update(string tableName)
        {
            _query.Append($"UPDATE {tableName} ");
            return this;
        }

        public QueryBuilder Set(string values)
        {
            _query.Append($"SET {values} ");
            return this;
        }

        public string Build()
        {
            return _query.ToString();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.AdoNET;


namespace Core.Extensions
{
    public static class SqlExtensions
    {
        public static List<T> ToList<T>(this DataTable dt)
        {

            if (dt == null)
            {
                return null;
            }
            var propList = typeof(T).GetProperties();
            //T instance = Activator.CreateInstance<T>();


            Dictionary<string, PropertyInfo> map = new Dictionary<string, PropertyInfo>();
            foreach (var prp in propList)
            {
                if (dt.Columns[prp.Name] != null && !map.Keys.Contains(prp.Name))
                {
                    //int index = dt.Columns.IndexOf(dt.Columns[prp.Name]);
                    map.Add(prp.Name, prp);
                }
            }
            List<T> collection = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T data = Activator.CreateInstance<T>();
                foreach (var item in map)
                {
                    if (row.Table.Columns[item.Key] == null)
                        continue;
                    if (row[item.Key] is DBNull)
                        item.Value.SetValue(data, null);
                    else
                        item.Value.SetValue(data, row[item.Key]);
                }
                collection.Add(data);
            }
            return collection;
        }
        public static List<T> ToOneTypeList<T>(this DataTable dt)
        {

            if (dt == null)
            {
                return null;
            }

            List<T> collection = new List<T>();
            foreach (DataRow row in dt.Rows)
            {

                if (!(row[0] is DBNull) && !(row[0] == null))
                    collection.Add((T)row[0]);


            }
            return collection;
        }

        public static List<T> GetQuery<T>(this string query)
        {

            SqlCommand cmd = new GenericDataAccess().CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Clear();
            return new GenericDataAccess().ExecuteSelectCommand(cmd).ToList<T>();

        }
        //public static ResultObj<T> GetQuery<T>(this string query)
        //{
        //    try
        //    {
        //        ResultObj<T> resultObj = new ResultObj<T>();
        //        SqlCommand cmd = new GenericDataAccess().CreateCommand();
        //        cmd.CommandText = query;
        //        cmd.Parameters.Clear();
        //        return resultObj new GenericDataAccess().ExecuteSelectCommand(cmd).ToList<T>();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<T>();
        //    }
        //} 
        public static List<T> GetQuery<T>(this string query, string connectiontype)
        {
            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand(connectiontype);
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                return new GenericDataAccess().ExecuteSelectCommand(cmd).ToList<T>();
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }


        public static dynamic GetDynamicQuery(this string query, string connectiontype)
        {
            var dns = new List<dynamic>();
            try
            {
                SqlCommand cmd = new GenericDataAccess().CreateCommand(connectiontype);
                cmd.CommandText = query;

                var table = new GenericDataAccess().ExecuteSelectCommand(cmd);


                for (int i = 0; i < table.Rows.Count; i++)
                {
                    // Expando objects are IDictionary<string, object>
                    IDictionary<string, object> dn = new System.Dynamic.ExpandoObject();

                    foreach (var column in table.Columns.Cast<DataColumn>())
                    {
                        dn[column.ColumnName] = table.Rows[i][column.ColumnName];
                    }

                    dns.Add(dn);
                }

                return dns;
            }
            catch (Exception ex)
            {
                return dns;
            }
        }
        public static dynamic GetDynamicQuery(this string query)
        {
            var dns = new List<dynamic>();

            SqlCommand cmd = new GenericDataAccess().CreateCommand();
            cmd.CommandText = query;

            var table = new GenericDataAccess().ExecuteSelectCommand(cmd);


            for (int i = 0; i < table.Rows.Count; i++)
            {
                // Expando objects are IDictionary<string, object>
                IDictionary<string, object> dn = new System.Dynamic.ExpandoObject();

                foreach (var column in table.Columns.Cast<DataColumn>())
                {
                    dn[column.ColumnName] = table.Rows[i][column.ColumnName];
                }

                dns.Add(dn);
            }

            return dns;

        }

        public static dynamic GetDynamicFromDataTable(this DataTable table)
        {
            var dns = new List<dynamic>();
            try
            {

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    // Expando objects are IDictionary<string, object>
                    IDictionary<string, object> dn = new System.Dynamic.ExpandoObject();

                    foreach (var column in table.Columns.Cast<DataColumn>())
                    {
                        dn[column.ColumnName] = table.Rows[i][column.ColumnName];
                    }

                    dns.Add(dn);
                }

                return dns;
            }
            catch (Exception ex)
            {
                return dns;
            }
        }

        //map to metodunda atlanması gereken tüm property tiplerini buraya yazılacak.
        static string[] ignoredPropertyTypes = { "String" };
        public static T MapTo<T>(this object source, T result = null) where T : class
        {
            if (source == null)
                return null;
            return (T)MapTo(typeof(T), source.GetType(), source, result);
        }
        static object MapTo(Type TargetType, Type SourceType, object source, object result = null)
        {
            if (source == null) return null;
            if (TargetType.Name.Contains("List") || TargetType.Name.Contains("Collection"))
                return null;
            var tp = TargetType.GetProperties();
            var sp = SourceType.GetProperties();

            result = result ?? Activator.CreateInstance(TargetType);
            Parallel.For(0, tp.Length, i =>
            {
                var t = tp[i];
                PropertyInfo s = sp.FirstOrDefault(x => x.Name == t.Name);
                if (s != null && t.GetSetMethod() != null)
                {
                    Type stype = s.PropertyType;


                    if (!ignoredPropertyTypes.Contains(stype.Name) && (stype.IsClass || stype.IsInterface))
                    {
                        t.SetValue(result, MapTo(t.PropertyType, s.PropertyType, s.GetValue(source)));
                    }
                    else
                    {
                        t.SetValue(result, s.GetValue(source));
                    }
                }
            });

            //foreach (var t in tp)
            //{
            //    PropertyInfo s = sp.FirstOrDefault(x => x.Name == t.Name);
            //    if (s != null && t.GetSetMethod() != null)
            //    {
            //        Type stype = s.PropertyType;


            //        if (!ignoredPropertyTypes.Contains(stype.Name) && (stype.IsClass || stype.IsInterface))
            //        {
            //            t.SetValue(result, MapTo(t.PropertyType, s.PropertyType, s.GetValue(source)));
            //        }
            //        else
            //        {
            //            t.SetValue(result, s.GetValue(source));
            //        }
            //    }
            //}
            return result;
        }

        public static object GetField(this DataRow row, string field)
        {

            if (!(row[field] is DBNull))
                return row[field];
            else
                return null;

        }

        /// <summary>
        /// Verilen Listeyi istenilen sayıda bölümlere ayırır
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Liste</param>
        /// <param name="size">Bölüm sayısı</param>
        /// <returns></returns>
        public static List<T>[] Partition<T>(this List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            List<T>[] partitions = new List<T>[totalPartitions];

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }

        //public static string userIP()
        //{
        //    string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    return ip;
        //}

        //public static Dictionary<string, object> requestKeyValue()
        //{
        //    Dictionary<string, object> result = new Dictionary<string, object>();
        //    foreach (var item in getContext.Request.Unvalidated.Form.AllKeys)
        //        result.Add(item, getContext.Request.Unvalidated.Form[item]);
        //    return result;
        //}

        //public static DateTime ToDateTime(this string text, string format = null)
        //{
        //    DateTime date;
        //    if (string.IsNullOrEmpty(format))
        //        format = Helper.dateFormat;
        //    return Convert.ToDateTime(text);
        //}
        //public static System.Web.HttpContext getContext
        //{
        //    get
        //    {
        //        return System.Web.HttpContext.Current;
        //    }
        //}

        //public static List<T> GetLanguageList<T>(List<int> langID = null)
        //{
        //    //item1.GetType().Name + "" + property.Name + "-" + item.ID + "-" + MyExtensions.getGuid,
        //    string tableName = typeof(T).Name;
        //    var listInstance = Activator.CreateInstance<List<T>>();
        //    foreach (var item in langID)
        //    {
        //        var instance = Activator.CreateInstance<T>();
        //        foreach (var key in requestKeyValue().Where(x => x.Key.Split('-').Length == 4 && x.Key.Split('-')[2] == item.ToString() && x.Key.Split('-')[0] == tableName))
        //        {
        //            if (string.IsNullOrEmpty(key.Value.ToString()))
        //                continue;
        //            var property = instance.GetType().GetProperty(key.Key.Split('-')[1]);
        //            var objectValue = Convert.ChangeType(key.Value, property.PropertyType);
        //            property.SetValue(instance, objectValue);
        //        }
        //        listInstance.Add(instance);
        //    }
        //    return listInstance;
        //}
        public static SqlDbType ToSqlDbType(this Type type)
        {
            if (type == typeof(string))
                return SqlDbType.NVarChar;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type);

            var param = new SqlParameter("", Activator.CreateInstance(type));
            return param.SqlDbType;
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static Type ToClrType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long?);

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool?);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime?);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal?);

                case SqlDbType.Float:
                    return typeof(double?);

                case SqlDbType.Int:
                    return typeof(int?);

                case SqlDbType.Real:
                    return typeof(float?);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid?);

                case SqlDbType.SmallInt:
                    return typeof(short?);

                case SqlDbType.TinyInt:
                    return typeof(byte?);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }

        //public static string ToJson(this object obj)
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    serializer.MaxJsonLength = 2147483644;
        //    return serializer.Serialize(obj);
        //}

    }
}

using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.AdoNET;
using Core.Extensions;


namespace Core.DataTables
{
    public class DatatablesJS
    {
        public class DataTablesObjectResult
        {
            public DataTablesObjectResult()
            {
                data = new List<object>();
            }
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<object> data { get; set; }

            public DataTablesObjectResult getresults(DatatablesObject filters, string tablename, string privatewhere)
            {
                DataTablesObjectResult result = new DataTablesObjectResult();
                result.draw = filters.draw;


                tablename = string.Format("( {0} ) t", tablename);

                //string privatewhere = QueryHelper.GetFilter(filters.dataqueryname, filters.additionalvalues);

                List<string> fields = new List<string>();
                fields = filters.columns.Where(r => !string.IsNullOrEmpty(r.data)).Select(res => res.data).ToList();


                int counter = 0;
                string order = "";
                for (int i = 0; i < filters.order.Length; i++)
                {
                    try
                    {

                        if (counter > 0)
                            order += ",";
                        order += string.Format("{0} {1}", filters.columns[filters.order[i].column].data, filters.order[i].dir);
                        counter++;

                    }
                    catch { }
                }

                counter = 0;
                string searchwhere = "";
                for (int i = 0; i < filters.columns.Length; i++)
                {
                    try
                    {

                        if (!string.IsNullOrEmpty(filters.search.value) && filters.columns[i].searchable && !filters.columns[i].search.regex && !string.IsNullOrEmpty(filters.columns[i].data))
                        {
                            if (counter > 0)
                            {
                                searchwhere += " OR ";
                            }
                            if (filters.columns[i].name == "date")
                                searchwhere += "CONVERT(datetime, " + filters.columns[i].data + ", 104)" + " LIKE " + string.Format("'%{0}%'", CleanTexts(filters.search.value));
                            //else if (context.Request.Form["columns[" + i + "][name]"].ToString() == "datetime")
                            //    searchwhere += "CONVERT(VARCHAR(25), " + context.Request.Form["columns[" + i + "][data]"].ToString() + ", 104) " + "CONVERT(VARCHAR(5), " + context.Request.Form["columns[" + i + "][data]"].ToString() + ", 108)" + " LIKE " + string.Format("'{0}%'", Utilities.CleanTexts(search));
                            else
                                searchwhere += filters.columns[i].data + " LIKE " + string.Format("'%{0}%'", CleanTexts(filters.search.value));

                            counter++;
                        }
                    }
                    catch { }
                }

                string columnsearchvalue = "";
                string columnwhere = "";
                counter = 0;
                for (int i = 0; i < filters.columns.Length; i++)
                {
                    try
                    {
                        if (filters.columns[i].search.regex && !string.IsNullOrEmpty(filters.columns[i].data))
                        {
                            //if (fields.Length > 0)
                            //    fields += ",";
                            //fields += context.Request.Form["columns[" + i + "][data]"].ToString();
                            if (!string.IsNullOrEmpty(filters.columns[i].search.value))
                            {
                                if (counter > 0)
                                {
                                    columnwhere += " AND ";
                                }

                                columnsearchvalue = filters.columns[i].search.value.Replace("^", "").Replace("$", "");

                                if (filters.columns[i].name == "date")
                                    columnwhere += "CONVERT(datetime, " + filters.columns[i].data + ", 104)" + " LIKE " + string.Format("'%{0}%'", columnsearchvalue.Replace("\\", ""));
                                //else if (context.Request.Form["columns[" + i + "][name]"].ToString() == "datetime")
                                //    columnwhere += "CONVERT(VARCHAR(25), " + context.Request.Form["columns[" + i + "][data]"].ToString() + ", 104) " + "CONVERT(VARCHAR(5), " + context.Request.Form["columns[" + i + "][data]"].ToString() + ", 108)" + " LIKE " + string.Format("'{0}%'", Utilities.CleanTexts(search));
                                else
                                    columnwhere += filters.columns[i].data + " LIKE " + string.Format("'%{0}%'", columnsearchvalue.Replace("\\", ""));
                                counter++;

                            }
                        }
                    }
                    catch { }
                }

                string where = "";
                int pagenumber = Convert.ToInt32(filters.start / filters.length);
                string customwhere = string.Empty;
                pagenumber++;
                where = !string.IsNullOrEmpty(privatewhere) ? privatewhere.Replace("[", "").Replace("]", "") : "";



                if (filters.search.value.Length > 0)
                    customwhere += " AND (" + searchwhere + ")";

                if (columnwhere.Length > 0)
                    customwhere += " AND (" + columnwhere + ")";


                if (!string.IsNullOrEmpty(where))
                    customwhere = customwhere + " AND " + where;

                string tmpSQL = "";

                tmpSQL = tablename;
                //if (!string.IsNullOrEmpty(customwhere) && !string.IsNullOrEmpty(where))
                //    tmpSQL = tmpSQL + " WHERE  " + where + " " + customwhere;
                //else if (string.IsNullOrEmpty(customwhere) && string.IsNullOrEmpty(where))
                //    tmpSQL = tablename;

                //table = new GenericDataAccess().ExecuteScalar(countSQL);


                try
                {
                    SqlCommand cmdCount = new GenericDataAccess().CreateCommand(filters.dbtype);
                    cmdCount.CommandText = "SELECT COUNT(1) as toplam FROM " + tablename + " WHERE 1=1 " + customwhere;
                    result.recordsFiltered = Convert.ToInt32(new GenericDataAccess().ExecuteScalar(cmdCount));
                    result.recordsTotal = result.recordsFiltered;
                }
                catch (Exception ex) { }


                //tmpSQL = "SELECT * FROM (" + tmpSQL + ") tab ";

                tmpSQL = @"WITH TempResult as(
		SELECT ROW_NUMBER() OVER(ORDER BY $$ORDERBY$$) as RowNumber,
			$$FIELDS$$
			FROM $$TABLENAME$$
		where  1=1 $$FILTER$$
	)
	SELECT  TempResult.*
	   FROM TempResult
	WHERE RowNumber > $$STARTFROM$$ AND RowNumber <= $$LASTREC$$";

                tmpSQL = tmpSQL.Replace("$$TABLENAME$$", tablename);
                tmpSQL = tmpSQL.Replace("$$FIELDS$$", string.Join(",", fields));

                if (!string.IsNullOrEmpty(customwhere))
                {
                    //tmpSQL = tmpSQL + " WHERE 1=1 " + customwhere;
                    tmpSQL = tmpSQL.Replace("$$FILTER$$", customwhere);
                }
                else
                    tmpSQL = tmpSQL.Replace("$$FILTER$$", "");


                //tmpSQL = "SELECT * FROM (" + tmpSQL + ") tab ";

                if (!string.IsNullOrEmpty(order))
                {
                    //tmpSQL += " ORDER BY " + order;

                    tmpSQL = tmpSQL.Replace("$$ORDERBY$$", order);

                }
                else
                    tmpSQL = tmpSQL.Replace("$$ORDERBY$$", "id");


                if (filters.length > 0)
                {
                    //tmpSQL += " LIMIT " + filters.start.ToString() + "," + filters.length.ToString();

                    tmpSQL = tmpSQL.Replace("$$STARTFROM$$", filters.start.ToString()).Replace("$$LASTREC$$", (filters.start + filters.length).ToString());
                }
                else
                {
                    tmpSQL = tmpSQL.Replace("$$STARTFROM$$", "0").Replace("$$LASTREC$$", "100000").ToString();
                }
                result.data = tmpSQL.GetDynamicQuery(filters.dbtype);

                return result;
            }

            public object getresults(DatatablesObject requestobj)
            {
                throw new NotImplementedException();
            }

            public string CleanTexts(string txtResponse)
            {
                txtResponse = txtResponse.Replace("'", "&#39;");
                txtResponse = txtResponse.Replace("=", "&#61;");
                txtResponse = txtResponse.Replace("-", "&minus;");
                txtResponse = txtResponse.Replace("<", "&lt;");
                txtResponse = txtResponse.Replace(">", "&gt;");

                return txtResponse;
            }

            public DataTablesObjectResult getresults(DatatablesObject requestobj, string query)
            {
                throw new NotImplementedException();
            }
        }

        public class DatatablesObject
        {
            public string dbtype { get; set; }
            public int draw { get; set; }
            public dtcolumn[] columns { get; set; }
            public dtorder[] order { get; set; }
            public int start { get; set; }
            public int length { get; set; }
            public string dataqueryname { get; set; }
            public dtsearch search { get; set; }
            public string sortorder { get; }
            public IEnumerable<string> additionalvalues { get; set; }

            public class dtcolumn
            {
                //public dtcolumn();

                public string data { get; set; }
                public string name { get; set; }
                public bool searchable { get; set; }
                public bool orderable { get; set; }
                public dtsearch search { get; set; }
            }

            public class dtorder
            {
                //public dtorder();

                public int column { get; set; }
                public dtorderdir dir { get; set; }
            }

            public class dtsearch
            {
                //public dtsearch();

                public string value { get; set; }
                public bool regex { get; set; }
            }

            public enum dtorderdir
            {
                asc = 0,
                desc = 1
            }
        }
    }
}

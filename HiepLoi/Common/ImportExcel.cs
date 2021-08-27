using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace ClsCommon
{
   public static class ImportExcel
    {
        //start import data from excel
        private static int GetEndCol(int intBeginRow, int intBeginCol, Aspose.Cells.Worksheet ws)
        {

            try
            {
                int intCol = intBeginCol;
                int intColNull = 0;
                while (true)
                {
                    if (ws.Cells[intBeginRow, intCol].Value == null)
                    {
                        if (intColNull == 10)
                        {
                            break;
                        }
                        intColNull += 1;
                        intCol += 1;
                    }
                    else
                    {
                        intCol += 1;
                        intColNull = 0;
                    }
                }
                return intCol;
            }
            catch
            {
                return 0;
            }
        }

        private static int GetEndRow(int intBeginRow, int intBeginCol, Aspose.Cells.Worksheet ws)
        {
            try
            {
                int intRow = intBeginRow + 1;
                while (true)
                {
                    if (ws.Cells[intRow, intBeginCol].Value == null || ws.Cells[intRow, intBeginCol].Value.ToString() == "")
                    {
                        break;
                    }
                    else
                    {
                        intRow += 1;
                    }
                }
                return intRow;
            }
            catch
            {
                return 0;
            }
        }

        [Obsolete]
        public static DataTable ReadExcelToDT(String strFilePath, int intBeginRow, int intBeginCol, ref String strErr, int iRowEndVal = 0, int iColEndVal = 0, int indexWorksheets = 0)
        {
            if (!System.IO.File.Exists(strFilePath))
            {
                strErr = "FileNotFound";
            }
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(strFilePath);
            Aspose.Cells.Worksheet ws = wb.Worksheets[indexWorksheets];

            DataTable dt = new DataTable();

            int iRowEnd = iRowEndVal;
            int iColEnd = iColEndVal;

            iRowEnd = GetEndRow(intBeginRow, intBeginCol, ws);
            iColEnd = GetEndCol(intBeginRow, intBeginCol, ws);

            try
            {
                for (int j = intBeginCol; j <= iColEnd - 1; j++)
                {
                    string strDataColumn = "";

                    if (ws.Cells[intBeginRow, j].Value != null)
                    {
                        strDataColumn = ws.Cells[intBeginRow, j].Value.ToString().Trim();
                    }

                    dt.Columns.Add(new DataColumn(strDataColumn, System.Type.GetType("System.String")));
                }
                //get Data
                int intStep = 0;
                for (int i = intBeginRow + 1; i <= iRowEnd - 1; i++)
                {
                    intStep = 0;
                    DataRow dr = dt.NewRow();
                    for (int j = intBeginCol; j <= iColEnd - 1; j++)
                    {
                        string strValue = "";
                        if (ws.Cells[i, j].Value == null)
                        {
                            strValue = "";
                        }
                        else
                        {
                            strValue = ws.Cells[i, j].Value.ToString();
                        }
                        dr[intStep] = strValue;
                        intStep += 1;
                    }
                    dt.Rows.Add(dr);
                }

                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}

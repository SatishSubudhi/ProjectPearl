
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using LumenWorks.Framework.IO.Csv;
//using Microsoft.VisualBasic.FileIO;
//using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using PearlFramework.Utilities;
using System.Text.RegularExpressions;
//using Microsoft.VisualStudio.Tools.Applications.Runtime;


namespace SatishTests.Utilities
{

    /*
    public static class GetData
    {
        static string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

       private static IEnumerable<string[]> GetTestData_Smoke()
        {
            string relativePath = "\\..\\..\\TestData\\CheckoutTests_Smoke.csv";
            string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);


            using (var csv = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(absolutePath), true))
            {
                while (csv.ReadNextRecord())
                {
                    string Accpt = csv[0];
                    string CCType = csv[1];
                    string CC = csv[2];
                    string CVC = csv[3];
                    string ExpM = csv[4];
                    string ExpY = csv[5];

                    yield return new[] { Accpt, CCType, CC, CVC, ExpM, ExpY };
                }
            }
        }
        */

    /*     private static IEnumerable<string[]> GetTestData_Billing()
         {
             string relativePath = "\\..\\..\\TestData\\Billing.csv";
             string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);

             using (var csv = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(absolutePath), true))
             {
                 while (csv.ReadNextRecord())
                 {
                     string FirstName = csv[0];
                     string LastName = csv[1];
                     string Email = csv[2];
                     string Country = csv[3];
                     string StateProvince = csv[4];
                     string City = csv[5];
                     string Address = csv[6];
                     string PostalCode = csv[7];
                     string Phone = csv[8];
                     string ShortDesc = csv[9];

                     yield return new[] { FirstName, LastName, Email, Country, StateProvince, City, Address, PostalCode, Phone, ShortDesc };
                 }
             }
         }
         */
    /* private static IEnumerable<string[]> GetTestData_CreateAccount()
     {
         string relativePath = "\\..\\..\\TestData\\CreateAccount.csv";
         string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);

         using (var csv = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(absolutePath), true))
         {
             while (csv.ReadNextRecord())
             {
                 string FirstName = csv[0];
                 string LastName = csv[1];
                 string Email = csv[2];
                 string Country = csv[3];
                 string StateProvince = csv[4];
                 string City = csv[5];
                 string Address = csv[6];
                 string PostalCode = csv[7];
                 string Phone = csv[8];
                 string Password = csv[9];
                 string ConfirmPassword = csv[10];
                 string ShortDesc = csv[11];


                 yield return new[] { FirstName, LastName, Email, Country, StateProvince, City, Address, PostalCode, Phone, Password, ConfirmPassword, ShortDesc };
             }
         }
     }
    */
    /*       private static IEnumerable<string[]> GetTestData_Payments()
           {
               string relativePath = "\\..\\..\\TestData\\Payments_Stripe.csv";
               string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);

               using (var csv = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(absolutePath), true))
               {
                   while (csv.ReadNextRecord())
                   {
                       string CCType = csv[0];
                       string CC = csv[1];
                       string CCIssue = csv[2];
                       string CVC = csv[3];
                       string CVCIssue = csv[4];
                       string ExpM = csv[5];
                       string ExpY = csv[6];
                       string ShortDesc = csv[7];

                       yield return new[] { CCType, CC, CCIssue, CVC, CVCIssue, ExpM, ExpY, ShortDesc };
                   }
               }
           }

    */
    /*
            private static IEnumerable<string[]> GetTestData_SearchSimple_NL()
            {

                string relativePath = "\\..\\..\\TestData\\SearchSimple_NL.txt";
                string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);
                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                Encoding iso = Encoding.GetEncoding("iso-8859-1");
                TextFieldParser parser = new TextFieldParser((@absolutePath), iso);
                using (parser)
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    while (!parser.EndOfData)
                    {
                        //Process row

                        string[] csv = parser.ReadFields();
                        string TValue = csv[0];
                        yield return new[] { TValue };
                    }
                }
            }

        */
    /*
            private static IEnumerable<string[]> GetTestData_SearchComp_NL()
            {
                string relativePath = "\\..\\..\\TestData\\SearchComp_NL.txt";
                string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);
                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                Encoding iso = Encoding.GetEncoding("iso-8859-1");
                TextFieldParser parser = new TextFieldParser((@absolutePath), iso);
                using (parser)
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    while (!parser.EndOfData)
                    {
                        //Process row

                        string[] csv = parser.ReadFields();
                        string val1 = csv[0];
                        string keyword = csv[1];
                        string val2 = csv[2];
                        yield return new[] { val1, keyword, val2 };
                    }
                }
            }
    */
    /*
            private static IEnumerable<string[]> GetTestData_SearchSimple_UK()
            {

                string relativePath = "\\..\\..\\TestData\\SearchSimple_UK.txt";
                string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);
                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                Encoding iso = Encoding.GetEncoding("iso-8859-1");
                TextFieldParser parser = new TextFieldParser((@absolutePath), iso);
                using (parser)
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    while (!parser.EndOfData)
                    {
                        //Process row

                        string[] csv = parser.ReadFields();
                        string TValue = csv[0];
                        yield return new[] { TValue };
                    }
                }
            }

    */
    /*
            private static IEnumerable<string[]> GetTestData_SearchComp_UK()
            {
                string relativePath = "\\..\\..\\TestData\\SearchComp_UK.txt";
                string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);
                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                Encoding iso = Encoding.GetEncoding("iso-8859-1");
                TextFieldParser parser = new TextFieldParser((@absolutePath), iso);
                using (parser)
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    while (!parser.EndOfData)
                    {
                        //Process row

                        string[] csv = parser.ReadFields();
                        string val1 = csv[0];
                        string keyword = csv[1];
                        string val2 = csv[2];
                        yield return new[] { val1, keyword, val2 };
                    }
                }
            }
    */
    /*
            public static class ReadandWriteDeleteExcel
            {
                public static void ReadExcel(string file, int qty, string amount, string date)
                {

                    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(file);
                    Microsoft.Office.Interop.Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
                    //iterate over the rows and columns and print to the console as it appears in the file
                    //excel is not zero based!!
                    //int rowCount = xlWorksheet.Columns.Count;
                    //int colCount = xlWorksheet.Rows.Count;
                    int rowCount = xlWorksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                    int colCount = xlWorksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Column;
                    Console.WriteLine("Rows.Count: " + rowCount);
                    Console.WriteLine("Cols.Count: " + colCount);
                    Assert.IsTrue(rowCount == qty + 1, "the number of vouchers is not correct");
                    Assert.IsTrue(colCount == 3, "the number of columns in Excel is not correct");

                    for (int i = 2; i <= rowCount; i++)
                    {
                        Console.WriteLine(xlRange.Cells[i, 2].Value2.ToString() + "\t");
                        Console.WriteLine(xlRange.Cells[i, 3].Value2.ToString() + "\t");
                        Assert.IsTrue(xlRange.Cells[i, 2].Value2.ToString().Equals(amount), "Excel amount is not correct");
                        Assert.IsTrue(xlRange.Cells[i, 3].Value2.ToString().Equals(date), "Excel expiry date is not correct");
                    }

                    //close and release
                    xlWorkbook.Close();
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlWorksheet);
                    Marshal.ReleaseComObject(xlWorkbook);
                    Marshal.ReleaseComObject(xlApp);
                }
    */
    /*
                public static void DeleteExcel(string file)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
    */
    /*
                public static void CleanVoucherFiles()
                {
                    string[] files = Directory.GetFiles(@KortextGlobals.pathDownloads, "vouchers-" + DateTime.Now.ToString("yyyyMMdd") + "*.xlsx");
                    if (files.Length > 0)
                    {
                        foreach (string file in files)
                        {
                            Console.WriteLine("Excel file path: " + file);
                            File.Delete(file);
                        }
                    }
                }
    */
    /*
                public static string BuildVoucherBulkOrderPath()
                {

                    string[] dirs = Directory.GetFiles(@KortextGlobals.pathDownloads, "vouchers-" + "*.xlsx");
                    Assert.IsTrue(dirs.Length > 0, "No Excel bulk order file was generated");
                    if (dirs.Length > 0)
                    {
                        Console.WriteLine("Excel file path: " + dirs[0]);
                        return dirs[0];
                    }
                    else
                        return null;
                }

            }
        }*/
}





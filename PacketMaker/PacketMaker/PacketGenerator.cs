
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Linq.Expressions;

namespace PacketMaker
{
    class PacketGenerator
    {

        public static PacketGenerator instance = new PacketGenerator();

        public string ExcelPath = Directory.GetCurrentDirectory();
        public string ExcelName = "\\PacketDefinition.xlsx";

        public string DefinitionProjectPath = "C:/Users/kaios/Desktop/CSharp_Network/CSharp Server/CSharp Server";
        public string DefinitionFileName = "/Packet";

        
        public Application app = new Application();
        public Workbook workBook = null;




        List<string> keywords = new List<string>();
        public Dictionary<string, string> keyData = new Dictionary<string, string>();
        public PacketStruct[] packetList = new PacketStruct[ushort.MaxValue];


        private PacketGenerator() {
            ReadExcel();
        }

        public void OpenWorkBook()
        {
            workBook = app.Workbooks.Open(ExcelPath + ExcelName, 0, false);
        }

        public void ReadKeyword(Range range)
        {
            for (int i = 2; i <= range.Columns.Count; i++)
            {
                var value = (range[1, i] as Range).Value2;
                if (value != null)
                {
                    keywords.Add(value.ToString());
                }
            }

        }

        public void ReadKeyData(Range range)
        {

            for (int i = 1; i <= 30; ++i)
            {
                if (keywords.Count < keyData.Count)
                {
                    System.Console.WriteLine("Not matching !!! keyData's data was more then keywords");
                    return;
                }

                Range cell = range.Cells[i, 1] as Range;
                if (cell.Value2 != null)
                {
                    var key = cell.Value2.ToString();
                    if (keywords.Contains(key))
                    {
                        keyData[key] = (range.Cells[i, 2] as Range).Value2.ToString();
                    }

                }
            }

        }

        private void Ready2ReadSheet(Range range, out int start_row)
        {
            ReadKeyword(range);
            ReadKeyData(range);
            System.Int32.TryParse(keyData["DataRow"], out start_row);
        }

        public void ReadSheet(string sheetName)
        {
            Worksheet workSheet = workBook.Worksheets[sheetName] as Worksheet;
            if(workSheet == null)
            {
                System.Console.WriteLine("Sheet is Not exist!");
                return;
            }
            Range range = workSheet.UsedRange;


            Ready2ReadSheet(range, out var row);


            for (; row <= range.Rows.Count; ++row)
            {

                PacketStruct packetStruct = new PacketStruct();
                //read protocol 

                Range cell = null;
                
                cell = range.Cells[row, 1] as Range;
                packetStruct.Classification = cell.Value2.ToString();

                cell = range.Cells[row, 2] as Range;
                packetStruct.ProtocolName = cell.Value2.ToString();

                cell = range.Cells[row, 3] as Range;
                packetStruct.ProtocolNumber = cell.Value2.ToString();

                //read fields
                int column = 4;
                while(column <= range.Columns.Count)
                {
                    DataStruct dataStruct = new DataStruct();
                    try
                    {
                        cell = range.Cells[row, column] as Range;
                        dataStruct.Data_Name = cell.Value2.ToString();

                        cell = range.Cells[row, column + 1] as Range;
                        dataStruct.Data_Type = cell.Value2.ToString();

                        column += 2;
                    }
                    catch(System.NullReferenceException e)
                    {
                        System.Console.WriteLine("Warning! Null Exception Error! you don't have to process this error");
                        break;
                    }

                    packetStruct.DataList.Add(dataStruct);
                }
                System.Int32.TryParse(packetStruct.ProtocolNumber, out var index);
                if(packetList[index] != null)
                {
                    System.Console.WriteLine("Protocol Number is overlapped!!! Checking Excel have required");
                }
                packetList[index] = packetStruct;
                
            }

            
        }

        private void CloseExcel()
        {
            

            
            foreach(Worksheet workSheet in workBook.Sheets)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet.UsedRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook.Sheets);
            
            workBook.Save();
            workBook.Close(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app.Workbooks);

            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

        }

        public void ReadExcel()
        {
            OpenWorkBook();
            ReadSheet("ProtocolDefinition");
            Clear();
        }

        private void Clear()
        {
            CloseExcel();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }

    }
}

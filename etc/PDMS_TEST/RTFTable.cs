using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDMS_TEST
{
    class RTFTable
    {
        #region Field                 
        public int InternalMargin = 180; public int RowCount; 
        public int ColumnCount; 
        public int[] ColumnWidthArray = null;
        public string[,] ContentArray = null;
        #endregion

        public RTFTable(int rowCount, int colunmnCount, int internalMargin) 
        { 
            RowCount = rowCount;
            ColumnCount = colunmnCount;
            InternalMargin = internalMargin;
            ColumnWidthArray = Enumerable.Repeat(1440, ColumnCount).ToArray(); 
            ContentArray = new string[RowCount, ColumnCount]; 
            for (int row = 0; row < RowCount; row++)
            { 
                for (int column = 0; column < ColumnCount; column++) 
                { 
                    ContentArray[row, column] = string.Empty; 
                } 
            } 
        }

        public void SetColumnWidthArray(params int[] columnWidthArray)
        { 
            for (int column = 0; column < ColumnCount; column++)
            { 
                ColumnWidthArray[column] = columnWidthArray[column]; 
            } 
        }

        public override string ToString() 
        {
            StringBuilder stringBuilder = new StringBuilder(); 
            string columnWidthString = GetColumnWidthString();
            for (int row = 0; row < RowCount; row++) 
            {
                stringBuilder.Append(@"\trowd");
                stringBuilder.Append(@"\trgaph" + InternalMargin.ToString());
                stringBuilder.Append(columnWidthString);
                for (int c = 0; c < ColumnCount; c++)
                { 
                    stringBuilder.Append(@"\pard\intbl{" + ContentArray[row, c].Replace(@"\", @"\\") + @"}\cell");
                }
                stringBuilder.Append(@"\row");
            } 
            return stringBuilder.ToString(); 
        }

        private string GetColumnWidthString() 
        {
            StringBuilder stringBuilder = new StringBuilder(); 
            int totalWidth = 0;
            for (int column = 0; column < ColumnCount; column++) 
            {
                totalWidth += ColumnWidthArray[column]; 
                stringBuilder.Append(@"\cellx" + totalWidth.ToString());
            }
            return stringBuilder.ToString(); 
        }                              
    }
}


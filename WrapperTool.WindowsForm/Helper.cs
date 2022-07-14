using System.Windows.Forms;


namespace WrapperTool.WindowsForm
{
    static class Helper
    {
        public static int GetWidthFromPercent(string columnName, DataGridView grid)
		{
			return GetWidthFromPercent(columnName, grid, string.Empty);
		}
		
		public static int GetWidthFromPercent(string columnName
            , DataGridView grid
			, string lastFieldName)
		{
			int result      = 0;
			if (grid == null)
			{
				return result;
			}
			int width       = grid.Width; 
			int intercept   = 0;
			int lastGridCol = 0;
				intercept = 0;
			if (string.Compare(columnName, lastFieldName) == 0)
			{
				return LastColumnWidth(columnName, grid, intercept);
			}
			else
			{
				if (grid.Columns[columnName].Width==0 || !grid.Columns[columnName].Visible)
					return 0;
				result = GetPercent(columnName) + GetFreePercent(grid);
			}
			// One of these will always be zero:
			return (result * (width) / 100) + lastGridCol;
		}
		
		#region Private Methods
        private static int GetFreePercent(DataGridView grid)
		{
			int result = 0;
			int count = grid.Columns.Count;
			foreach(DataGridViewColumn column in grid.Columns)
			{
				result += (!column.Visible ? GetPercent(column.Name): 0);
				count -= (!column.Visible ? 1 : 0);
			}
			return (count) >0 ? result / (count) : 0;
		}

		private static int GetPercent(string columnName)
		{
			switch (columnName)
			{
					case "Name":	
						return 70;
					default:
						return  20;
			}
		}
		private static int LastColumnWidth(string columnName
            , DataGridView grid
			, int Padding)
		{
			int   result    = 0;
			int   sumWidths = 0;
			const int limit = 15;
            foreach (DataGridViewColumn col in grid.Columns)
			{
				if (col.Name != columnName && col.Visible)
				{
					sumWidths += col.Width;
				}
			}
			result = grid.Width - sumWidths + Padding;
			return (result > limit ? result : limit);
		}
		#endregion
        public static void ResizeGrid(DataGridView listViewGrid)
        {

            if (listViewGrid != null)
            {
                foreach (DataGridViewColumn col in listViewGrid.Columns)
                {
                    int width = GetWidthFromPercent
                        (
                        col.Name
                        , listViewGrid
                        , null);
                    if (width > 0)
                        col.Width = width;
                    else
                        col.Visible = false;
                }
            }
        }

    }
}

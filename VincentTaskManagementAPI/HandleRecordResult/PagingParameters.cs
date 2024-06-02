namespace VincentTaskManagementAPI.HandleRecordResult
{
		public class PagingParameters
		{
				const int maxPageSize = 50;
				public int PageNumber { get; set; } = 1;
				private int _pageSize = 2;
				public int PageSize
				{
						get
						{
								return _pageSize;
						}
						set
						{
								_pageSize = (value > maxPageSize) ? maxPageSize : value;
						}
				}
				public string? SearchTitle { get; set; }
				public string? OrderBy { get; set; } = "DESC";
		}
}

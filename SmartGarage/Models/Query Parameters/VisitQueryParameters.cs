namespace SmartGarage.Models.QueryParameters
{
    public class VisitQueryParameters
    {
        public string? User { get; set; } 
        public string? LicensePlate { get; set; } 
        public string? Brand { get; set; } 
        public string? Model { get; set; } 
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }
}
 
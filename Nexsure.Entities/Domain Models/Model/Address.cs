namespace Nexsure.Entities.Domain_Models.Model
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Landmark { get; set; }
        public string Region { get; set; }
        public string AddressType { get; set; }
        public string FullAddress { get; set; }
        public string Subdistrict { get; set; }
        public string BuildingName { get; set; }
        public string POBox { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string TimeZone { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
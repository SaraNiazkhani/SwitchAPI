namespace SwitchAPI.Models
{
    public class FilterModel
    {
        public string brandname { get; set; }
        public string modelname { get; set; }
        public int year { get; set; }
        public int usagekilometere { get; set; }
        public string gearboxtype { get; set; }
        public string bodystatus { get; set; }
        public double price { get; set; }
        public string fueltype { get; set; }

        public bool IsEmpty()
        {
            return (string.IsNullOrEmpty(brandname) && string.IsNullOrEmpty(modelname) &&
                string.IsNullOrEmpty(fueltype) && string.IsNullOrEmpty(bodystatus) &&
                string.IsNullOrEmpty(gearboxtype) &&
                year == 0 && usagekilometere == 0 && price == 0);
        }
    }
}

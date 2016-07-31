using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manny.Bots.InsuranceBot
{

    [Serializable]
    public class InsuranceProduct
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public TypeOfInsuranceOptions Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0} Policy - {1}", this.Name, this.Description);
        }

        public List<InsuranceProduct> GetInsuranceProducts(TypeOfInsuranceOptions? type)
        {
            var products= new List<InsuranceProduct>()
            {
                new InsuranceProduct() { Name="Health Ninja", Description="Health Ninja covers you when you are overseas and you get 10% discount on family deals", Type=TypeOfInsuranceOptions.Health },
                new InsuranceProduct() { Name="Health Volt", Description="Health volt supercharges your travel plans by offering free travel cover in certain countries and you get a free SIM pack too", Type=TypeOfInsuranceOptions.Health  },
                new InsuranceProduct() { Name="Travel Ninja", Description="Travel Ninja covers you when you are overseas and you get 10% discount on family deals", Type=TypeOfInsuranceOptions.Travel },
                new InsuranceProduct() { Name="Travel Volt", Description="Travel volt supercharges your travel plans by offering free medical care in certain countries and you get a free SIM pack too", Type=TypeOfInsuranceOptions.Travel },
                new InsuranceProduct() { Name="Home Ninja", Description="Home Ninja looks after your home and you get 10% discount on family deals", Type=TypeOfInsuranceOptions.Home },
                new InsuranceProduct() { Name="Home Volt", Description="Home Volt supercharges your home cover by offering free contents cover in certain scenarios and you get movie tickets too", Type=TypeOfInsuranceOptions.Home  },
                new InsuranceProduct() { Name="Car Ninja", Description="Car Ninja looks after your car and you get 10% discount batteries and service", Type=TypeOfInsuranceOptions.Car },
                new InsuranceProduct() { Name="Car Volt", Description="Car Volt supercharges your car by offering free cover on unlimited number of drivers and you a free tank of petrol too", Type=TypeOfInsuranceOptions.Car  }
            };

            if (type.HasValue)
                return products.Where(s => s.Type == type.Value).ToList();
            else
                return products;
        }
    }
}
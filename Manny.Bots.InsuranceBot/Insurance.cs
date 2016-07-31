using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace Manny.Bots.InsuranceBot
{
    public enum TypeOfInsuranceOptions { Home, Health, Travel, Car };

    [Serializable]
    public class Insurance
    {
        public TypeOfInsuranceOptions? TypeOfInsurance;

        public static IForm<Insurance> BuildForm()
        {
            return new FormBuilder<Insurance>()
                    .Message("Let us know what are you interested in?")
                    .Build();
        }
    }
}
namespace API_Wallet.Providers
{
    public enum ProductEnum
    {
        Default,// 0
        Bus,    // 1
        Ferry,  // 2
        Car,    // 3
        Tour,   // 4
        Train,   // 5 
        TrainPass, // 6
        TourCoachPackage, // 7
        Charter, //8
        BusDayPass, //9
        TPM //10
    }

    public static class ProductEnumExtension
    {
        public static int ToInt(this ProductEnum product)
        {
            return (int)product;
        }

        public static string ToUpperString(this ProductEnum product)
        {
            return product.ToString().ToUpper();
        }

        public static string ToLowerString(this ProductEnum product)
        {
            return product.ToString().ToLower();
        }

        public static string ToReadableString(this ProductEnum product)
        {
            switch (product)
            {
                case ProductEnum.Bus:
                    return "Bus";
                case ProductEnum.Ferry:
                    return "Ferry";
                case ProductEnum.Car:
                    return "Car";
                case ProductEnum.Tour:
                    return "Tour";
                case ProductEnum.Train:
                    return "Train";
                case ProductEnum.TrainPass:
                    return "Train Pass";
                case ProductEnum.TourCoachPackage:
                    return "Tour Coach Package";
                case ProductEnum.Charter:
                    return "Charter";
                case ProductEnum.BusDayPass:
                    return "Bus Day Pass";
                case ProductEnum.TPM:
                    return "Trip Package";
                default:
                    return product.ToString();
            }
        }
    }
}
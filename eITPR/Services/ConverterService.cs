namespace eITPR.Services {
    public  class ConverterService {

        public  DateTime ProcessDates(string data) {
            DateTime default_date;

            if (!DateTime.TryParse(data, out default_date)) {
                default_date = DateTime.Parse("1980-01-01T00:00:00");
            }

            return default_date;
        }
        public  double ProcessAmount(string data) {
            double outAmount;
            if (!double.TryParse(data, out outAmount)) {
                outAmount = 0.00;
            }

            return outAmount;
        }
        public  decimal ProcessDecimal(string data) {
            decimal outDecimal;
            if (!decimal.TryParse(data, out outDecimal)) {
                outDecimal = 0.00m;
            }
            return outDecimal;
        }
        public  int ProcessInteger(string data) {
            int outInt;
            if (!int.TryParse(data, out outInt)) {
                outInt = 0;
            }

            return outInt;
        }
        public  bool ProcessBool(string data) {
            switch (data) {
                case "1":
                case "Yes":
                case "yes": {
                        return true;
                    }
                default: {
                        return false;
                    }
            }
        }
    }
}

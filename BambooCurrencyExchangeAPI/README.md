1. **Get Latest Exchange Rates**
   - **URL:** `/api/currencyexchange/latest`
   - **Method:** `GET`
   - **Query Parameters:** `basecurrency`

2. **Convert Currency**
   - **URL:** `/api/currencyexchange/convert`
   - **Method:** `GET`
   - **Query Parameters:** `baseCurrency`, `targetCurrency`, `amount`
   - **Note:** Returns bad response for TRY, PLN, THB, and MXN currencies.

3. **Get Historical Rates**
   - **URL:** `/api/exchangerate/`
   - **Method:** `GET`
   - **Query Parameters:** `baseCurrency`, `startDate`, `endDate`
## Running the Application

1. Ensure you have [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.
2. Clone the repository:
   ```bash
   git clone https://github.com/sameer755c/BambooCurrencyExcange.git
   cd BambooCardAPI


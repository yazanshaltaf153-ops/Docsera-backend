using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace Docsera.Helper
{
    public class GoogleSheetsHelper
    {
        private readonly SheetsService _service;
        private readonly string _spreadsheetId;

        public GoogleSheetsHelper(string credentialsPath, string spreadsheetId)
        {
            GoogleCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(SheetsService.Scope.Spreadsheets);
            }

            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Docsera App",
            });

            _spreadsheetId = spreadsheetId;
        }

        public async Task AppendDoctorAsync(
            int id,
            string name,
            string specialty,
            string workingHours,
            string description)
        {
            var range = "Sheet1!A:E"; // assuming headers are in A1:E1
            var values = new ValueRange
            {
                Values = new List<IList<object>> {
                new List<object> { id, name, specialty, workingHours, description }
            }
            };

            var appendRequest = _service.Spreadsheets.Values.Append(values, _spreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            await appendRequest.ExecuteAsync();
        }
    }

}
using System;
using System.Collections.Generic;
using Com.Gridly.Client;
using Com.Gridly.Api;
using Com.Gridly.Model;

namespace gridly_csharp_sample
{

    class Program
    {
        // Load environment variables
        private static string apiKeyValue = System.Configuration.ConfigurationManager.AppSettings.Get("apiKey");
        private static string viewId = System.Configuration.ConfigurationManager.AppSettings.Get("viewId");

        static void Main(string[] args)
        {
            Console.WriteLine("Start SDK!");
            Configuration config = new Configuration();
            config.AddApiKey("confidential_key", apiKeyValue.ToString());
            config.DateTimeFormat = DateTime.Now.ToString();
            config.AddDefaultHeader("Authorization", $"ApiKey {config.ApiKey["confidential_key"]}");
            // Create a list of records 
            var cell1 = new List<SetCell>()
            {
                new SetCell("enUs_lang", null, "How to Play"),
                new SetCell("legal_bool", null, true),
                new SetCell("frFr_lang", null, "Comment jouer"),
                new SetCell("itIT_lang", null, "")
            };
            var cell2 = new List<SetCell>()
            {
                new SetCell("enUs_lang", null, "Instructions"),
                new SetCell("legal_bool", null, true),
                new SetCell("frFr_lang", null, "Instructions"),
                new SetCell("itIT_lang", null, "")
            };
            var cell3 = new List<SetCell>()
            {
                new SetCell("enUs_lang", null, "Other Features"),
                new SetCell("legal_bool", null, false),
                new SetCell("frFr_lang", null, "Autres caractéristiques"),
                new SetCell("itIT_lang", null, "Altre caratteristiche")
            };
            var lstNewRecords = new List<SetRecord>()
            {
                new SetRecord(cell1,"STR_HOW_TO_PLAY"),
                new SetRecord(cell2,"STR_INSTRUCTIONS"),
                new SetRecord(cell3,"STR_OTHER_FEATURES")
            };
            var recordApi = new RecordApi(config);
            recordApi.Create(viewId, lstNewRecords);
            printAllRecordsToConsole(lstNewRecords);
            // Update existing records
            var updateCell1 = new List<SetCell>()
            {
                 new SetCell("enUs_lang", null, "How to Play"),
                new SetCell("legal_bool", null, true),
                new SetCell("frFr_lang", null, "Comment jouer"),
                new SetCell("itIT_lang", null, "Come giocare")
            };
            var updateCell2 = new List<SetCell>()
            {
                 new SetCell("enUs_lang", null, "Instructions"),
                new SetCell("legal_bool", null, true),
                new SetCell("frFr_lang", null, "Instructions"),
                new SetCell("itIT_lang", null, "Istruzioni")
            };
            var lstUpdateRecords = new List<SetRecord>()
            {
                new SetRecord(updateCell1,"STR_HOW_TO_PLAY"),
                new SetRecord(updateCell2,"STR_INSTRUCTIONS"),
            };
            recordApi.Update(viewId, lstUpdateRecords);
            Console.WriteLine("Updating list of records:");
            printAllRecordsToConsole(lstUpdateRecords);
            // Delete existing records by list of IDs
            var recordIds = new List<String>() { "STR_HOW_TO_PLAY", "STR_INSTRUCTIONS", "STR_OTHER_FEATURES" };
            recordApi.Delete(viewId, new DeleteRecord(recordIds));
            Console.WriteLine("Deleting list of records by ID:");
            recordIds.ForEach(recordId => Console.WriteLine(recordId));
        }

        public static void printAllRecordsToConsole(List<SetRecord> listRecords)
        {
            foreach (var records in listRecords)
            {
                Console.WriteLine("ID: " + records.Id);
                records.Cells.ForEach(cell => Console.WriteLine(cell));
            }
        }
    }
}

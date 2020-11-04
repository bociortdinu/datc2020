using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace console_app
{
    class Program
    {
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;
        static void Main(string[] args)
        {
            Task.Run(async () => { await Initialize(); }).GetAwaiter().GetResult();
        }

        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=datc2020dinu;AccountKey=hqracVmDI9gNDfrSina4zJ7jFsb4m76hTw43rcfNHojobDgNJWR9BXtAti5UH5XmaCBXxvVCFMfF9Vg3XO7zTw==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("studenti");

            await studentsTable.CreateIfNotExistsAsync();

            await AddNewStudent();
            await EditStudent();
            await GetAllStudents();
        }

        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universitate\tCNP\tEmail\tNumar telefon\tAn");
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>();

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2} {3}\t{4}\t{5}\t{6}", entity.PartitionKey, entity.RowKey, entity.FirstName, entity.LastName,
                    entity.Email, entity.PhoneNumber, entity.Year);
                }
            } while (token != null);

        }

        private static async Task AddNewStudent()
        {
            var student = new StudentEntity("UPT", "9872731921937");
            student.FirstName = "Dinu";
            student.LastName = "Bociort";
            student.Year = 4;
            student.PhoneNumber = "0741223553";
            student.Email = "bociortdinu@gmail.com";
            student.Faculty = "AC";

            var insertOperation = TableOperation.Insert(student);

            await studentsTable.ExecuteAsync(insertOperation);

        }

        private static async Task EditStudent()
        {
            var student = new StudentEntity("UPT", "92130817283214");
            student.FirstName = "Marius";
            student.Year = 4;
            student.ETag = "*";
            var editOperation = TableOperation.Merge(student);

            await studentsTable.ExecuteAsync(editOperation);
        }

    }
}

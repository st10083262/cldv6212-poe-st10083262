using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Get Storage Information
                var accountName = "azjhbrsgvckndnst1008bf3";
                var creds = "w5e1CCry9j6++YHK5C4gj9fBqtlBxy9+6Din3gh8/6IOOxsGgQLeJeuUsbKQNJhItIwFIzvxQpXA+AStfIN5pw==";

                // Set Auth
                var cred = new StorageCredentials(accountName, creds);
                var account = new CloudStorageAccount(cred, useHttps: true);

                // Connect to Storage
                var client = account.CreateCloudTableClient();
                var table = client.GetTableReference("table1");

                //User Input
                Console.WriteLine("Vaccination Queue");
                Console.WriteLine("Enter your ID number");
                string id = Console.ReadLine();

                Console.WriteLine("Enter the vaccination centre that you were administered at");
                string vc = Console.ReadLine();

                Console.WriteLine("Enter the vaccination date (DD/MM/YYYY)");
                string vd = Console.ReadLine();

                Console.WriteLine("Enter the vaccination serial number");
                string vsn = Console.ReadLine();

                Console.WriteLine("Enter the vaccination barcode");
                string vb = Console.ReadLine();

                //Connects to the Entity
                var obj = new Entity()
                {
                    PartitionKey = id, // Must be unique
                    RowKey = Guid.NewGuid().ToString(), // Must be unique
                    Date = vd,
                    Centre = vc,
                    SerialNumber = vsn,
                    Barcode = vb
                };
                var insertOperation = TableOperation.Insert(obj);
                table.Execute(insertOperation);
            }
            //If an error is to occur, the application will continue to run and print the error message
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public class Entity : TableEntity
        {
            public Entity(string id, string row)
            {
                this.PartitionKey = id; this.RowKey = row;
            }
            public Entity() { }
            public string Date { get; set; }
            public string Centre { get; set; }
            public string SerialNumber { get; set; }
            public string Barcode { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using ASNA.DataGateHelper;


namespace Demo_DataGateHelper_CS
{
    class Program
    {
        static void Main(string[] args)
        {
            Tester t1 = new Tester();
            TesterWithClass t2 = new TesterWithClass();
            t1.Run();
            t2.Run();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    class Tester
    {
        public void Run()
        {
            ASNA.VisualRPG.Runtime.Database DGDB;

            DataGateDB DGDBManager = new DataGateDB("*Public/DG NET Local");
            DGDB = DGDBManager.GetConnectionForAVR();

            DGFileReader dgfr = new DGFileReader(DGDB);

            dgfr.AfterRowRead += OnAfterRowRead;

            dgfr.ReadEntireFile("Examples", "CMastNewL2");
        }

        // AfterRowRead event handler.
        private void OnAfterRowRead(object sender, AfterRowReadArgs e)
        {
            // Properties passed in through the e parameter:
            //    e.DataRow -- a System.Data.DataRow representing the row read.
            //    e.FieldNames -- an array of field names in the DataRow.
            //    e.CurrentRowCounter -- The current row number. 
            //    e.CancelRead -- Set to false to cancel the remaining read.
            //    e.CustomClassInstance -- Instance of custom class. 

            Console.WriteLine(e.DataRow["CMName"].ToString());
        }
    }

    class TesterWithClass
    {
        List<Customer> Customers = new List<Customer>();

        public void Run()
        {
            ASNA.VisualRPG.Runtime.Database DGDB;

            DataGateDB DGDBManager = new DataGateDB("*Public/DG NET Local");
            DGDB = DGDBManager.GetConnectionForAVR();

            DGFileReader dgfr = new DGFileReader(DGDB);
            dgfr.CustomClassType = typeof(Customer);

            dgfr.AfterRowRead += OnAfterRowRead;

            dgfr.ReadEntireFile("Examples", "CMastNewL2");

            foreach (Customer cust in Customers)
            {
                Console.WriteLine(cust.CMName);
            }
        }

        // AfterRowRead event handler.
        private void OnAfterRowRead(object sender, AfterRowReadArgs e)
        {
            Customer cust = (Customer)e.CustomClassInstance;

            // Add this row's Cust instance to the collection.
            Customers.Add(cust);
        }
    }

    public class Customer
    {
        public System.String CMName { get; set; }
        public System.Decimal CMCustNo { get; set; }

        public System.String CMAddr1 { get; set; }
        public System.String CMCity { get; set; }
        public System.String CMState { get; set; }
        public System.String CMCntry { get; set; }
        public System.String CMPostCode { get; set; }
        public System.String CMActive { get; set; }
        public System.Decimal CMFax { get; set; }
        public System.String CMPhone { get; set; }
    }
}

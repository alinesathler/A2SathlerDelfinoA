using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Aline Sathler Delfino - Assignment 2
//Name of Project: Electricity Bill
//Purpose: C# console application to calculate and print the electricity bill of a customer
//Revision History: REV00 - 2023/09/27 - Initial version


namespace A2SathlerDelfinoA {
    class Program {
        static string ReadString(string prompt) {
            Console.Write(prompt);

            return Console.ReadLine();
        }

        static int ReadInt(string prompt, int minimun, int maximun) {
            string stringValue = ReadString(prompt);
            do {
                Console.Write("Please, enter your 5-digits ID: ");
                customerIDString = Console.ReadLine();

                //Check if customerID is a 5-digit integer
                if (int.TryParse(customerIDString, out customerID) && (0 <= customerID) && (99999 >= customerID)) {
                    isIDOk = true;
                } else {
                    Console.Write("Invalid ID. Try again.\n");
                }
            } while (isIDOk != true);
        }

        static void Main() {
            //Constants
            //If bill exceeds $50 then a surcharge of 15% will be charged
            const int SURCHARGE = 15, EXCEEDS = 50;

            //Tax percentage is 13%
            const int TAX = 13;

            //Unit ranges
            const int UNIT1 = 199, UNIT2 = 500, UNIT3 = 800;
            const double CHARGE1 = 0.20, CHARGE2 = 0.50, CHARGE3 = 0.80, CHARGE4 = 1.00;

            //Government subsidy if applicable @0.1635
            const double GOVSUBSIDY = 0.1635;
            //*****

            //Variables
            string customerName, customerIDString, unitConsumedString, subsidyString;
            int customerID, unitConsumed, subsidy;
            bool isIDOk = false, isUnitConsumedOk = false, isSubsidyOk = false;
            double charge, amountCharges, surchargeAmount = 0, subsidyAmount = 0, taxAmount, totalAmount;

            Console.WriteLine($"Calculate Electricity Bill");
            Console.WriteLine($"----------------------------");

            //Customer enters its name: customerName
            customerName = ReadString("Please, enter your name: ");

            //Customer enters its ID: customerID
            do {
                Console.Write("Please, enter your 5-digits ID: ");
                customerIDString = Console.ReadLine();

                //Check if customerID is a 5-digit integer
                if (int.TryParse(customerIDString, out customerID) && (0 <= customerID) && (99999 >= customerID)) {
                    isIDOk = true;
                } else {
                    Console.Write("Invalid ID. Try again.\n");
                }
            } while (isIDOk != true);

            //Customer enters its unit consumed: unitConsumed
            do {
                Console.Write("Please, enter your unit consumed: ");
                unitConsumedString = Console.ReadLine();

                //Check if unitConsumed is an integer
                if (int.TryParse(unitConsumedString, out unitConsumed) && (0 <= unitConsumed)) {
                    isUnitConsumedOk = true;
                } else {
                    Console.Write("Invalid unit consumed. Try again.\n");
                }
            } while (isUnitConsumedOk != true);

            //Customer enters if subsidy is applicable: subsidy
            do {
                Console.Write("Do you have a subsidy (1: Yes, 0: No)? ");
                subsidyString = Console.ReadLine();

                //Check if subsidy is 0 or 1
                if (int.TryParse(subsidyString, out subsidy) && ((subsidy == 0) || (subsidy == 1))) {
                    isSubsidyOk = true;
                } else {
                    Console.Write("Invalid input. Try again.\n");
                }
            } while (isSubsidyOk != true);

            Console.Clear();

            //Defining the charge/unit
            if (unitConsumed <= UNIT1) {
                charge = CHARGE1;
            } else if (unitConsumed < UNIT2) {
                charge = CHARGE2;
            } else if (unitConsumed < UNIT3) {
                charge = CHARGE3;
            } else {
                charge = CHARGE4;
            }

            //Calculating amount charges
            amountCharges = unitConsumed * charge;

            //Calculating surcharge amount
            if (amountCharges > EXCEEDS) {
                surchargeAmount = amountCharges * SURCHARGE / 100;
            }

            //Calculating subsidy amount
            if (subsidy == 1) {
                subsidyAmount = unitConsumed * GOVSUBSIDY;
            }

            //Calculating tax amount
            taxAmount = (amountCharges + surchargeAmount - subsidyAmount) * TAX / 100;

            //Calculating total amount
            totalAmount = amountCharges + surchargeAmount - subsidyAmount + taxAmount;

            //Showing Bill
            Console.WriteLine($"Electricity Bill");
            Console.WriteLine($"Customer IDNO:\t\t\t\t{customerID.ToString("D5")}");
            Console.WriteLine($"Customer Name:\t\t\t\t{customerName}");
            Console.WriteLine($"Unit Consumed:\t\t\t\t{unitConsumed}");
            Console.WriteLine($"Amount Charges @{charge} per unit:\t\t{amountCharges.ToString("F")}");
            Console.WriteLine($"Surcharge Amount:\t\t\t{surchargeAmount.ToString("F")}");
            Console.WriteLine($"Subsidy Amount:\t\t\t\t{subsidyAmount.ToString("F")}");
            Console.WriteLine($"Tax Amount:\t\t\t\t{taxAmount.ToString("F")}");
            Console.WriteLine($"Net Amount Paid By the Customer:\t{totalAmount.ToString("F")}");
        }
    }
}

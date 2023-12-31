﻿using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Aline Sathler Delfino - Assignment 2
//Name of Project: Electricity Bill
//Purpose: C# console application to calculate and print the electricity bill of a customer
//Revision History:
                    //REV00 - 2023/09/27 - Initial version
                    //REV01 - 2023/10/04 - Refactoring the code using methods
                    //REV02 - 2023/10/07 - Refactoring the code using loops (do...while)


namespace A2SathlerDelfinoA {
    class Program {
        //Method to read a string
        static string ReadString(string prompt) {
            Console.Write(prompt);

            return Console.ReadLine();
        }

        //Method to read a integer between two predefined values, if isShowValue is true enable the show the range to the user
        static int ReadInt(string prompt, int minimun, int maximun, bool isShowValue = true) {
            string valueString;
            int valueInt;
            bool isValueOk = false;

            do {
                if(isShowValue) { 
                    Console.Write($"{prompt} (between {minimun} and {maximun}): ");
                } else {
                    Console.Write(prompt);
                }
                valueString = Console.ReadLine();

                //Check if the input is a integer between minimun and maximun
                if (int.TryParse(valueString, out valueInt) && (minimun <= valueInt) && (maximun >= valueInt)) {
                    isValueOk = true;
                } else {
                    Console.Write("Invalid input. Try again.\n");
                }
            } while (isValueOk != true);

            return valueInt;
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
            //---------------------

            //Variables
            string customerName;
            int customerID, unitConsumed, subsidy;
            double charge, amountCharges, surchargeAmount = 0, subsidyAmount = 0, taxAmount, totalAmount;

            //Change the color to cyan
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine($"Calculate Electricity Bill");
            Console.WriteLine($"----------------------------");

            //Customer enters its name: customerName
            customerName = ReadString("Please, enter your name: ");

            //Customer enters its ID: customerID
            customerID = ReadInt("Please, enter your 5-digits ID", 0, 99999);

            //Customer enters its unit consumed: unitConsumed
            unitConsumed = ReadInt("Please, enter your unit consumed: ", 0, 999999999, false);

            //Customer enters if subsidy is applicable: subsidy
            subsidy = ReadInt("Do you have a subsidy (1: Yes, 0: No)? ", 0, 1, false);

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
            Console.WriteLine($"Amount Charges @{charge} per unit:\t\t{amountCharges.ToString("F", CultureInfo.CreateSpecificCulture("en-CA"))}");
            Console.WriteLine($"Surcharge Amount:\t\t\t{surchargeAmount.ToString("F", CultureInfo.CreateSpecificCulture("en-CA"))}");
            Console.WriteLine($"Subsidy Amount:\t\t\t\t{subsidyAmount.ToString("F", CultureInfo.CreateSpecificCulture("en-CA"))}");
            Console.WriteLine($"Tax Amount:\t\t\t\t{taxAmount.ToString("F", CultureInfo.CreateSpecificCulture("en-CA"))}");
            Console.WriteLine($"Net Amount Paid By the Customer:\t{totalAmount.ToString("F", CultureInfo.CreateSpecificCulture("en-CA"))}");
        }
    }
}

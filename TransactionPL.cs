using System;
using System.Collections.Generic;
using Pecunia.BusinessLayer;
using Pecunia.Exceptions;
using Pecunia.Entities;

namespace Pecunia.PresentationLayer
{
    class MainClass
    {
        public static void Main()
        {
            TransactionPL trans = new TransactionPL();
            trans.MenuOfTransaction();
        }
    }
    class TransactionPL
    {
        public bool MenuOfTransaction()
        {
            int input;
            do
            {
                Console.Clear();
                Console.WriteLine("--------- Your Transactions Here ---------");
                Console.WriteLine("1. Credit");
                Console.WriteLine("2. Debit");
                Console.WriteLine("3. Show Transaction Records");
                Console.WriteLine("4. Back");
                Console.WriteLine("Enter your choice:");
                input = Convert.ToInt16(Console.ReadLine());

                if (input != 4)
                {
                    switch (input)
                    {
                        case 1:
                            MenuOfCredit();
                            break;

                        case 2:
                            MenuOfDebit();
                            break;

                        case 3:
                            MenuOfShowTransactionRecords();
                            break;

                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }

            } while (input != 4);
            return true;
        }


        public bool MenuOfCredit()
        {
            int input;

            do
            {
                Console.Clear();
                Console.WriteLine("-------- Select Mode for Credit Transaction --------");
                Console.WriteLine("1. By Cheque");
                Console.WriteLine("2. By Deposit Slilp");
                Console.WriteLine("3. Back");
                input = Convert.ToInt32(Console.ReadLine());

                if (input != 3)
                {
                    switch (input)
                    {
                        case 1:
                            if (CreditTransactionByChequePL() == true)
                                return true;
                            else
                                MenuOfCredit();

                            break;
                            
                        case 2:
                            if (CreditTransactionByDepositSlipPL() == true)
                                return true;
                            else
                                MenuOfCredit();
                            break;

                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }
            } while (input != 3);
            return true;
        }


        public bool MenuOfDebit()
        {
            int input;

            do
            {
                Console.Clear();
                Console.WriteLine("-------- Select Mode for Debit Transaction --------");
                Console.WriteLine("1. By Cheque");
                Console.WriteLine("2. By Withdrawal Slip");
                Console.WriteLine("3. Back");
                input = Convert.ToInt32(Console.ReadLine());

                if (input != 3)
                {
                    switch (input)
                    {
                        case 1:
                            if (DebitTransactionByChequePL() == true)
                                return true;
                            else
                                MenuOfDebit();
                            break;
                            
                        case 2:
                            if (DebitTransactionByWithdrawalSlipPL() == true)
                                return true;
                            else
                                MenuOfDebit();
                            break;
                        
                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }
            } while (input != 3);
            return true;
        }


        public bool MenuOfShowTransactionRecords()
        {
            int input;
            
            do
            {
                Console.Clear();
                Console.WriteLine("------- Your Transaction Records Here -------");
                Console.WriteLine("1. Show All Transactions");
                Console.WriteLine("2. Show Debit Transactions");
                Console.WriteLine("3. Show Credit Transactions");
                Console.WriteLine("4. Show By Transaction ID");
                Console.WriteLine("5. Show By Customer ID");
                Console.WriteLine("6. Show By Account No");
                Console.WriteLine("7. Back");
                input = Convert.ToInt32(Console.ReadLine());

                if (input != 7)
                {
                    switch (input)
                    {
                        case 1:
                            if (GetAllTransactionsPL() == true)
                                return true;
                            else
                                MenuOfShowTransactionRecords();
                            break;

                        case 2:
                            if (MenuOfShowDebitTransactions() == true)
                                return true;
                            else
                                MenuOfShowTransactionRecords();
                            break;

                        case 3:
                            if (MenuOfShowCreditTransactions() == true)
                                return true;
                            else
                                MenuOfShowTransactionRecords();
                            break;

                        case 4:
                            if (ListTransactionByTransactionID_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionRecords();
                            break;

                        case 5:
                            if (MenuOfShowTransactionsByCustomerID() == true)
                                return true;
                            else
                                MenuOfShowTransactionRecords();
                            break;

                        case 6:
                            if (MenuOfShowTransactionsByAccountNo() == true)
                                return true;
                            else
                                MenuOfShowTransactionRecords();
                            break;

                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }
            } while (input != 7);

            return true;
        }

        public bool MenuOfShowDebitTransactions()
        {
            int input;
            
            do
            {
                Console.Clear();
                Console.WriteLine("-------- Debit Transactions --------");
                Console.WriteLine("1. List Transactions by Date Range");
                Console.WriteLine("2. List Transactions of Specific Date");
                Console.WriteLine("3. List All Debit Transactions");
                Console.WriteLine("4. Back");
                input = Convert.ToInt32(Console.ReadLine());

                if (input != 4)
                {
                    switch (input)
                    {
                        case 1:
                            if (ListDebitTransactionsByDateRangePL() == true)
                                return true;
                            else
                                MenuOfShowDebitTransactions();
                            break;

                        case 2:
                            if (ListDebitTransactionsOfSpecificDatePL() == true)
                                return true;
                            else
                                MenuOfShowDebitTransactions();
                            break;

                        case 3:
                            if (ListAllDebitTransactionsPL() == true)
                                return true;
                            else
                                MenuOfShowDebitTransactions();
                            break;

                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }
            } while (input != 4);

            return true;
        }

        public bool MenuOfShowCreditTransactions()
        {
            int input;
            
            do
            {
                Console.Clear();
                Console.WriteLine("-------- Creit Transactions --------");
                Console.WriteLine("1. List Transactions by Date Range");
                Console.WriteLine("2. List Transactions of Specific Date");
                Console.WriteLine("3. List All Creit Transactions");
                Console.WriteLine("4. Back");
                input = Convert.ToInt32(Console.ReadLine());

                if (input != 4)
                {
                    switch (input)
                    {
                        case 1:
                            if (ListCreditTransactionsByDateRangePL() == true)
                                return true;
                            else
                                MenuOfShowCreditTransactions();
                            break;

                        case 2:
                            if (ListCreditTransactionsOfSpecificDatePL() == true)
                                return true;
                            else
                                MenuOfShowCreditTransactions();
                            break;

                        case 3:
                            if (ListAllCreditTransactionsPL() == true)
                                return true;
                            else
                                MenuOfShowCreditTransactions();
                            break;

                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }
            } while (input != 4);

            return true;
        }

        public bool MenuOfShowTransactionsByCustomerID()
        {
            int input;
            

            do
            {
                Console.Clear();
                Console.WriteLine("-------- Customer's Transactions --------");
                Console.WriteLine("1. List Transactions by Date Range");
                Console.WriteLine("2. List Transactions of Specific Date");
                Console.WriteLine("3. List All Debit Transactions");
                Console.WriteLine("4. List All Creit Transactions");
                Console.WriteLine("5. List All Transactions");
                Console.WriteLine("6. Back");
                input = Convert.ToInt32(Console.ReadLine());

                if (input != 6)
                {
                    switch (input)
                    {
                        case 1:
                            if (ListTransactionsByDateRangeOfCustomerID_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByCustomerID();
                            break;

                        case 2:
                            if (ListTransactionsOfSpecificDateOfCustomerID_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByCustomerID();
                            break;

                        case 3:
                            if (ListAllDebitTransactionsOfCustomerID_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByCustomerID();
                            break;

                        case 4:
                            if (ListAllCreditTransactionsOfCustomerID_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByCustomerID();
                            break;

                        case 5:
                            if (ListTransactionByCustomerID_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByCustomerID();
                            break;

                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }
            } while (input != 6);

            return true;
        }


        public bool MenuOfShowTransactionsByAccountNo()
        {
            int input;
            do
            {
                Console.Clear();
                Console.WriteLine("-------- Account's Transactions --------");
                Console.WriteLine("1. List Transactions by Date Range");
                Console.WriteLine("2. List Transactions of Specific Date");
                Console.WriteLine("3. List All Debit Transactions");
                Console.WriteLine("4. List All Creit Transactions");
                Console.WriteLine("5. List All Transactions");
                Console.WriteLine("6. Back");
                input = Convert.ToInt32(Console.ReadLine());

                if (input != 6)
                {
                    switch (input)
                    {
                        case 1:
                            if (ListTransactionsByDateRangeOfAccountNo_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByAccountNo();
                            break;

                        case 2:
                            if (ListTransactionsOfSpecificDateOfAccountNo_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByAccountNo();
                            break;

                        case 3:
                            if (ListAllDebitTransactionsOfAccountNo_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByAccountNo();
                            break;

                        case 4:
                            if (ListAllCreditTransactionsOfAccountNo_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByAccountNo();
                            break;

                        case 5:
                            if (ListTransactionByAccountNo_PL() == true)
                                return true;
                            else
                                MenuOfShowTransactionsByAccountNo();
                            break;

                        default:
                            Console.WriteLine("Invalid Input\nPress Any Key -> Try Again");
                            Console.ReadKey();
                            break;
                    }
                }
            } while (input != 6);

            return true;

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool CreditTransactionByChequePL()
        {
            long accountNo;
            double amount;
            string chequeNo;
            try
            {
                Console.Clear();
                Console.WriteLine("------- Credit Transaction By Cheque -------");
                Console.Write("Enter Account No:");
                accountNo = long.Parse(Console.ReadLine());
                Console.WriteLine("Maximum Allowed Amount Rs.50000");
                Console.Write("Enter Amount (Rs.):");
                amount = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Cheque No:");
                chequeNo = Console.ReadLine();

                TransactionsBL transBL = new TransactionsBL();
                return transBL.CreditTransactionByChequeBL(accountNo, amount, chequeNo);
            }
            catch (CreditChequeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }
        }

        public bool CreditTransactionByDepositSlipPL()
        {
            Console.Clear();
            long accountNo;
            double amount;

            try
            {
                Console.WriteLine("--------- Credit Transaction By Deposit Slip ---------");
                Console.Write("Enter Account No:");
                accountNo = long.Parse(Console.ReadLine());
                Console.WriteLine("Maximum Allowed Amount is Rs.50000");
                Console.Write("Enter Amount (Rs.):");
                amount = Convert.ToDouble(Console.ReadLine());

                TransactionsBL transBL = new TransactionsBL();
                return transBL.CreditTransactionByDepositSlipBL(accountNo, amount);
            }
            catch (CreditSlipException e)
            {
                Console.WriteLine(e.Message);
                
                return false;
            }
        }

        public bool DebitTransactionByChequePL()
        {
            long accountNo;
            double amount;
            string chequeNo;
            try
            {
                Console.Clear();
                Console.WriteLine("------- Debit Transaction By Cheque -------");
                Console.Write("Enter Account No:");
                accountNo = long.Parse(Console.ReadLine());
                Console.WriteLine("Maximum Allowed Amount Rs.50000");
                Console.Write("Enter Amount (Rs.):");
                amount = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Cheque No:");
                chequeNo = Console.ReadLine();

                TransactionsBL transBL = new TransactionsBL();
                return transBL.DebitTransactionByChequeBL(accountNo, amount, chequeNo);
            }
            catch (DebitChequeException e)
            {
                Console.WriteLine(e.Message+"\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }
            catch(InsufficientBalanceException e)
            {
                Console.WriteLine(e.Message + "\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }
            
        }

        public bool DebitTransactionByWithdrawalSlipPL()
        {
            Console.Clear();
            long accountNo;
            double amount;

            try
            {
                Console.WriteLine("--------- Debit Transaction By Withdrawal Slip ---------");
                Console.Write("Enter Account No:");
                accountNo = long.Parse(Console.ReadLine());
                Console.WriteLine("Maximum Allowed Amount is Rs.50000");
                Console.Write("Enter Amount (Rs.):");
                amount = Convert.ToDouble(Console.ReadLine());

                TransactionsBL transBL = new TransactionsBL();
                return transBL.DebitTransactionByWithdrawalSlipBL(accountNo, amount);
            }
            catch (CreditSlipException e)
            {
                Console.WriteLine(e.Message + "\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }
            catch (InsufficientBalanceException e)
            {
                Console.WriteLine(e.Message + "\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }
            catch (DebitSlipException e)
            {
                Console.WriteLine(e.Message + "\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }
        }

        public bool GetAllTransactionsPL()
        {
            Console.Clear();
            Console.WriteLine("------ All Transactions ------");

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();
            
            foreach(var transaction in transactions)
            {
                BusinessLogicUtil.ShowTransactionDetails(transaction);
            }

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListTransactionByTransactionID_PL()
        {
            Console.Clear();
            Console.WriteLine("------ Transaction Details ------");
            
            Console.Write("Enter Transaction ID:");
            string transactionID = Console.ReadLine();

            try
            {
                TransactionsBL transBL = new TransactionsBL();
                TransactionEntities transaction = transBL.DisplayTransactionByTransactionID_BL(transactionID);
                if (transaction == null)
                {
                    Console.WriteLine($"No Records found for {transactionID}");
                    return false;
                }
                else
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    Console.WriteLine("Press Any Key -> Previous Menu");
                    return true;
                }
            }
            catch(TransactionDetailsException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key -> Try Again");
                return false;
            }
        }

        public bool ListAllDebitTransactionsPL()
        {
            Console.Clear();
            Console.WriteLine("------ All Debit Transactions ------");

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            foreach (var transaction in transactions)
            {
                // Enum for Debit is 1
                if (transaction.Type == (TypeOfTranscation)1)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                }
            }

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListDebitTransactionsOfSpecificDatePL()
        {
            Console.Clear();
            Console.WriteLine("------ Debit Transactions ------");

            Console.Write("Enter Date (in dd-MM-yyyy format only):");
            string inputDateStr = Console.ReadLine();
            DateTime inputDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", inputDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (inputDate == default(DateTime))
            {
                Console.WriteLine("Wrong Date Format\nInput date needs to be in dd-MM-yyyy format");
                Console.WriteLine("Press any key -> Try Again");
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            Console.WriteLine($"------ All Debit Transactions of {inputDate.Date} ------");
            foreach (var transaction in transactions)
            {
                // Enum for Debit is 1
                if (transaction.Type == (TypeOfTranscation)1 && DateTime.Compare(inputDate.Date, transaction.DateOfTransaction.Date) == 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if(transactionsFound == false)
                Console.WriteLine($"No Debit Transactions found on {inputDate.Date}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListDebitTransactionsByDateRangePL()
        {
            Console.Clear();
            Console.WriteLine("------. All Debit Transaction ------");
            string FromDateStr, ToDateStr;
            
            Console.WriteLine("All dates must be in mm-dd-yyyy format");
            Console.Write("Enter Start Date:");
            FromDateStr = Console.ReadLine();
            DateTime FromDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", FromDateStr);

            Console.Write("Enter End Date:");
            ToDateStr = Console.ReadLine();
            DateTime ToDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", ToDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (FromDate == default(DateTime) || ToDate == default(DateTime))
            {
                Console.WriteLine("One or more dates are not in specified format\nSpecified Format mm-dd-yyyy");
                Console.WriteLine("Press Any Key -> Try Again");
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach(var transaction in transactions)
            {
                // Enum for Debit is 1
                if (transaction.Type == (TypeOfTranscation)1 && DateTime.Compare(FromDate.Date, transaction.DateOfTransaction.Date) < 0 && DateTime.Compare(transaction.DateOfTransaction.Date, ToDate.Date) < 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if(transactionsFound == false)
                Console.WriteLine($"No Debit Transactions found between {FromDate.Date} and {ToDate.Date}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListCreditTransactionsByDateRangePL()
        {
            Console.Clear();
            Console.WriteLine("------. All Credit Transaction ------");
            string FromDateStr, ToDateStr;

            Console.WriteLine("All dates must be in mm-dd-yyyy format");
            Console.Write("Enter Start Date:");
            FromDateStr = Console.ReadLine();
            DateTime FromDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", FromDateStr);

            Console.Write("Enter End Date:");
            ToDateStr = Console.ReadLine();
            DateTime ToDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", ToDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (FromDate == default(DateTime) || ToDate == default(DateTime))
            {
                Console.WriteLine("One or more dates are not in specified format\nSpecified Format mm-dd-yyyy");
                Console.WriteLine("Press Any Key -> Try Again");
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                // Enum for Credit is 0
                if (transaction.Type == (TypeOfTranscation)0 && DateTime.Compare(FromDate.Date, transaction.DateOfTransaction.Date) < 0 && DateTime.Compare(transaction.DateOfTransaction.Date, ToDate.Date) < 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Credit Transactions found between {FromDate.Date} and {ToDate.Date}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListCreditTransactionsOfSpecificDatePL()
        {
            Console.Clear();
            Console.WriteLine("------ Credit Transactions ------");

            Console.Write("Enter Date (in dd-MM-yyyy format only):");
            string inputDateStr = Console.ReadLine();
            DateTime inputDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", inputDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (inputDate == default(DateTime))
            {
                Console.WriteLine("Wrong Date Format\nInput date needs to be in dd-MM-yyyy format");
                Console.WriteLine("Press any key -> Try Again");
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            Console.WriteLine($"------ All Credit Transactions of {inputDate.Date} ------");
            foreach (var transaction in transactions)
            {
                // Enum for Credit is 0
                if (transaction.Type == (TypeOfTranscation)0 && DateTime.Compare(inputDate.Date, transaction.DateOfTransaction.Date) == 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Credit Transactions found on {inputDate.Date}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListAllCreditTransactionsPL()
        {
            Console.Clear();
            Console.WriteLine("------ All Credit Transactions ------");

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            foreach (var transaction in transactions)
            {
                // Enum for Credit is 0
                if (transaction.Type == (TypeOfTranscation)0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                }
            }

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListTransactionsByDateRangeOfCustomerID_PL()
        {
            Console.Clear();
            Console.Write("Enter Customer ID");
            string customerID = Console.ReadLine();
            if(BusinessLogicUtil.validate(customerID) == false)
            {
                Console.WriteLine("Not a valid customer ID\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            string FromDateStr, ToDateStr;

            Console.WriteLine("All dates must be in mm-dd-yyyy format");
            Console.Write("Enter Start Date:");
            FromDateStr = Console.ReadLine();
            DateTime FromDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", FromDateStr);

            Console.Write("Enter End Date:");
            ToDateStr = Console.ReadLine();
            DateTime ToDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", ToDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (FromDate == default(DateTime) || ToDate == default(DateTime))
            {
                Console.WriteLine("One or more dates are not in specified format\nSpecified Format mm-dd-yyyy");
                Console.WriteLine("Press Any Key -> Try Again");
                return false;
            }

            // required transactions will be filered out in foreach loop from all transactions
            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                if (transaction.CustomerID == customerID && DateTime.Compare(FromDate.Date, transaction.DateOfTransaction.Date) < 0 && DateTime.Compare(transaction.DateOfTransaction.Date, ToDate.Date) < 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Transactions found between {FromDate.Date} and {ToDate.Date} of {customerID}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListTransactionsOfSpecificDateOfCustomerID_PL()
        {
            Console.Clear();
            Console.Write("Enter Customer ID");
            string customerID = Console.ReadLine();
            if (BusinessLogicUtil.validate(customerID) == false)
            {
                Console.WriteLine("Not a valid customer ID\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            Console.Write("Enter Date (in dd-MM-yyyy format only):");
            string inputDateStr = Console.ReadLine();
            DateTime inputDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", inputDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (inputDate == default(DateTime))
            {
                Console.WriteLine("Wrong Date Format\nInput date needs to be in dd-MM-yyyy format");
                Console.WriteLine("Press any key -> Try Again");
                return false;
            }

            // required transaction will be filtered in foreach loop from all transaction
            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            Console.WriteLine($"------ All Debit Transactions of {inputDate.Date} ------");
            foreach (var transaction in transactions)
            {
                if (transaction.CustomerID == customerID && DateTime.Compare(inputDate.Date, transaction.DateOfTransaction.Date) == 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Debit Transactions found on {inputDate.Date}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListAllDebitTransactionsOfCustomerID_PL()
        {
            Console.Clear();
            Console.Write("Enter Customer ID");
            string customerID = Console.ReadLine();
            if (BusinessLogicUtil.validate(customerID) == false)
            {
                Console.WriteLine("Not a valid customer ID\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                // Enum for Debit is 1
                if (transaction.Type == (TypeOfTranscation)1 && transaction.CustomerID == customerID)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if(transactionsFound ==  false)
                Console.WriteLine($"No Debit transactions founds in records of {customerID}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListAllCreditTransactionsOfCustomerID_PL()
        {
            Console.Clear();
            Console.Write("Enter Customer ID");
            string customerID = Console.ReadLine();
            if (BusinessLogicUtil.validate(customerID) == false)
            {
                Console.WriteLine("Not a valid customer ID\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                // Enum for Credit is 0
                if (transaction.Type == (TypeOfTranscation)0 && transaction.CustomerID == customerID)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Credit transactions founds in records of {customerID}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }

        public bool ListTransactionByCustomerID_PL()
        {
            Console.Clear();
            Console.Write("Enter Customer ID");
            string customerID = Console.ReadLine();
            if (BusinessLogicUtil.validate(customerID) == false)
            {
                Console.WriteLine("Not a valid customer ID\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                if (transaction.CustomerID == customerID)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Transactions founds in records of {customerID}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            return true;
        }


        public bool ListTransactionsByDateRangeOfAccountNo_PL()
        {
            Console.Clear();
            Console.Write("Enter Account No");
            string accountNo = Console.ReadLine();
            if (BusinessLogicUtil.validateAccountNo(accountNo) == false)
            {
                Console.WriteLine("Not a valid Account No.\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            string FromDateStr, ToDateStr;

            Console.WriteLine("All dates must be in mm-dd-yyyy format");
            Console.Write("Enter Start Date:");
            FromDateStr = Console.ReadLine();
            DateTime FromDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", FromDateStr);

            Console.Write("Enter End Date:");
            ToDateStr = Console.ReadLine();
            DateTime ToDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", ToDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (FromDate == default(DateTime) || ToDate == default(DateTime))
            {
                Console.WriteLine("One or more dates are not in specified format\nSpecified Format mm-dd-yyyy");
                Console.WriteLine("Press Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            // required transactions will be filered out in foreach loop from all transactions
            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                if (transaction.AccountNo == long.Parse(accountNo) && DateTime.Compare(FromDate.Date, transaction.DateOfTransaction.Date) < 0 && DateTime.Compare(transaction.DateOfTransaction.Date, ToDate.Date) < 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Transactions found between {FromDate.Date} and {ToDate.Date} of account:{accountNo}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            Console.ReadKey();
            return true;
        }

        public bool ListTransactionsOfSpecificDateOfAccountNo_PL()
        {
            Console.Clear();
            Console.Write("Enter Account No");
            string accountNo = Console.ReadLine();
            if (BusinessLogicUtil.validateAccountNo(accountNo) == false)
            {
                Console.WriteLine("Not a valid account No.\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            Console.Write("Enter Date (in dd-MM-yyyy format only):");
            string inputDateStr = Console.ReadLine();
            DateTime inputDate = BusinessLogicUtil.validateDate("dd-MM-yyyy", inputDateStr);

            // validateDate returns default(DateTime) if date is not in valid format because DateTime is non nullable
            if (inputDate == default(DateTime))
            {
                Console.WriteLine("Wrong Date Format\nInput date needs to be in dd-MM-yyyy format");
                Console.WriteLine("Press any key -> Try Again");
                Console.ReadKey();
                return false;
            }

            // required transaction will be filtered in foreach loop from all transaction
            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            Console.WriteLine($"------ All Debit Transactions of {inputDate.Date} ------");
            foreach (var transaction in transactions)
            {
                if (transaction.AccountNo == long.Parse(accountNo) && DateTime.Compare(inputDate.Date, transaction.DateOfTransaction.Date) == 0)
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Transactions found on {inputDate.Date} for account {accountNo}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            Console.ReadKey();

            return true;
        }

        public bool ListAllDebitTransactionsOfAccountNo_PL()
        {
            Console.Clear();
            Console.Write("Enter account no.");
            string accountNo = Console.ReadLine();
            if (BusinessLogicUtil.validateAccountNo(accountNo) == false)
            {
                Console.WriteLine("Not a valid Account No\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                // Enum for Debit is 1
                if (transaction.Type == (TypeOfTranscation)1 && transaction.AccountNo == long.Parse(accountNo))
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Debit transactions founds in records of {accountNo}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            Console.ReadKey();

            return true;
        }


        public bool ListAllCreditTransactionsOfAccountNo_PL()
        {
            Console.Clear();
            Console.Write("Enter Account No:");
            string accountNo = Console.ReadLine();
            if (BusinessLogicUtil.validateAccountNo(accountNo) == false)
            {
                Console.WriteLine("Not a valid customer ID\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                // Enum for Credit is 0
                if (transaction.Type == (TypeOfTranscation)0 && transaction.AccountNo == long.Parse(accountNo))
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Credit transactions founds in account No {accountNo}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            Console.ReadKey();

            return true;
        }

        public bool ListTransactionByAccountNo_PL()
        {
            Console.Clear();
            Console.Write("Enter Account No:");
            string accountNo = Console.ReadLine();
            if (BusinessLogicUtil.validateAccountNo(accountNo) == false)
            {
                Console.WriteLine("Not a valid account No.\nPress Any Key -> Try Again");
                Console.ReadKey();
                return false;
            }

            TransactionsBL transBL = new TransactionsBL();
            List<TransactionEntities> transactions = transBL.GetAllTransactionBL();

            bool transactionsFound = false;
            foreach (var transaction in transactions)
            {
                if (transaction.AccountNo == long.Parse(accountNo))
                {
                    BusinessLogicUtil.ShowTransactionDetails(transaction);
                    transactionsFound = true;
                }
            }

            if (transactionsFound == false)
                Console.WriteLine($"No Transactions founds in account {accountNo}");

            Console.WriteLine("Press Any Key -> Previous Menu");
            Console.ReadKey();
            return true;
        }

    }

}

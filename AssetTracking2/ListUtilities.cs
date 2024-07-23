using AssetTracking2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssetTracking2
{
    public class ListUtilities
    {

        public AssetRepository AssetRepo { get; set; }


        //public List<Project> Projects { get; set; }


        public ListUtilities()
        {

            AssetRepo = new AssetRepository();
        }


        // HelpClass which let the user input an answer-string to a question
        // The answer-string is returned
        private string ReadDataFromUser(string userAction)
        {
            Console.Write(userAction + ": ");    //example  userAction = "Enter a ProjectName"
            string? data = Console.ReadLine();

            if (data != null)
            {
                if (data.Trim().ToLower() != "q")   // data Ok
                {
                    return data.Trim();    // data is returned trimmed
                }
                else if (data.Trim().ToLower() == "q")
                {
                    return "q";
                }
            }
            return "";
        }


        // Writes the Welcome-note when the program starts
        //public void WriteWelcomeHeader(List<ProjectTask> tasks)
        //{
        //    int done = tasks.FindAll(item => item.Status == TaskStatus.Done).Count();  // done
        //    int notDone = tasks.Count - done;   // "notstarted" and "started"

        //    string nd_tsk = (notDone != 1) ? "tasks" : "task";
        //    string d_tsk = (done != 1) ? "tasks" : "task";

        //    Console.WriteLine();
        //    Console.WriteLine("Welcome to Todoly you have " + notDone.ToString() + " " + nd_tsk + " todo and " + done.ToString() + " " + d_tsk+ " are done.");
        //    Console.WriteLine("Pick an option:");

        //}


        // Writes the 4 main choices for the user
        public void WriteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
           
            Console.WriteLine();
            Console.WriteLine("(1) Show Asset List");
            Console.WriteLine("(2) Add New Asset");
            Console.WriteLine("(3) Edit Asset (update, remove)");
            Console.WriteLine("(4) Quit");

            Console.ResetColor();
        }


        // Message when an action is completed
        public void SuccessMessage(string action)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("The Asset was successfully " + action);   // action= "added" / "edited" etc
            Console.ResetColor();
            Console.WriteLine("-------------------------------------");
        }

        // Errorhandling message
        public void FailMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Something went wrong when " + msg + ". Contact dev if problem persist");   // msg= "" / "edited" etc
            Console.ResetColor();
            Console.WriteLine("-------------------------------------");
        }


        // Lets the user decide if he wants to goahead with the removal of a Asset with a certain id
        // If 'Y/y': goes ahead with removal . if 'A/a' or 'Q/q': the process is aborted  
        private void RemoveAsset(List<Asset> assets, int id)
        {
            bool dataDeleteOk = false;
            string dataDelete = "";

            Console.WriteLine("");

            while (!dataDeleteOk)
            {
                dataDelete = ReadDataFromUser("Are You sure you want to Delete the asset with id = " + id + "?  YES 'Y'/'y'. ABORT 'A'/'a'");
                dataDelete = dataDelete.ToLower();

                if (dataDelete == "y" || dataDelete == "a" || dataDelete == "q")
                {
                    dataDeleteOk = true;
                }
            }
            if (dataDelete != "a" && dataDelete != "q")   // Do the Edit
            {

                try
                {
                    // Asset pT = assets.Find(item => item.Id == id);    //get task to remove. We know id exists here from ChangeTask()

                    // bool ok = tasks.Remove(pT);
                    bool ok = true;
                    if (ok)
                    {
                        // bool ok2 = TaskRepository.SaveTasksToFile(tasks);   // List has been changed. Save new List to file.
                        bool ok2 = true;
                        if (ok2)
                        {
                            SuccessMessage("removed");     // The maxId stays the same even if we have erased this id here. (Simulate DB)                   
                        }
                        else
                        {
                            FailMessage("removing an asset");
                        }
                    }
                    else
                    {
                        FailMessage("removing an asset");
                    }
                }
                catch (Exception e)
                {
                    FailMessage("removing an asset");
                    Console.WriteLine(e.Message);
                }
            }
        }


        // Lets the user decide what param to edit and then does the editing
        private void EditAsset(List<Asset> assets, int id)
        {

            string dataEditParameter = "";
            bool dataEditParameterOk = false;

            Console.WriteLine("");

            while (!dataEditParameterOk)
            {
                dataEditParameter = ReadDataFromUser("Do you want to edit Due Date 'dd' , Project 'p' , Title 't' or Status 's' ");
                dataEditParameter = dataEditParameter.ToLower();

                if (dataEditParameter == "dd" || dataEditParameter == "p" || dataEditParameter == "t" || dataEditParameter == "s" || dataEditParameter == "q")
                {
                    dataEditParameterOk = true;
                }
            }

            if (dataEditParameter.ToLower() != "q")   // Do the Edit
            {

                try
                {
                    //Asset pT = assets.Find(item => item.Id == id);    //get task to edit, we know id exists here from ChangeLIst

                    if (dataEditParameter == "dd")
                    {
                        string dataDueDate = "";
                        bool dateTimeOk = false;

                        while (!dateTimeOk)
                        {
                            dataDueDate = ReadDataFromUser("Enter a new value for DueDate in format YYYY-MM-DD");
                            if (ValidateDate(dataDueDate))
                            {
                                dateTimeOk = true;
                            }
                        }
                        DateTime dueDt = Convert.ToDateTime(dataDueDate);
                       // pT.DueDate = dueDt;                       // edit attribute

                    }
                    else if (dataEditParameter == "p")
                    {
                        string projName = "";
                        bool projNameOk = false;

                        while (!projNameOk)
                        {
                            projName = ReadDataFromUser("Give a new value for projectName");
                            if (projName!="")
                            {
                                projNameOk = true;
                            }
                        }

                       // pT.Project.Name = projName;  // we have a non-blank-value
                    }
                    else if (dataEditParameter == "t")
                    {

                        string taskTitle = "";
                        bool taskTitleOk = false;

                        while (!taskTitleOk)
                        {
                            taskTitle = ReadDataFromUser("Give a new value for taskTitle");
                            if (taskTitle!= "")
                            {
                                taskTitleOk = true;
                            }
                        }

                       // pT.TaskTitle = taskTitle;
                    }
                    else   // "s"
                    {
                        bool statusOk = false;
                        string status = "";

                        while (!statusOk)
                        {
                            status = ReadDataFromUser("Give new value for status. Not Started 'ns', Started 's' or Done 'd'");
                            status = status.ToLower();

                            if (status == "ns" || status == "s" || status == "d")
                            {
                                statusOk = true;
                            }
                        }
                        if (status == "ns")
                        {
                          //  pT.Status = TaskStatus.NotStarted;
                        }
                        else if (status == "s")
                        {
                          //  pT.Status = TaskStatus.Started;
                        }
                        else if (status == "d")
                        {
                          //  pT.Status = TaskStatus.Done;
                        }
                    }

                    // write to file
                    //  bool ok = TaskRepository.SaveTasksToFile(tasks);   // List has been changed. Save new List to file.
                    bool ok = false;

                    if (ok)
                    {
                        SuccessMessage("edited");
                    }
                    else
                    {
                        FailMessage("editing an asset");
                    }
                }
                catch (Exception e)
                {
                    FailMessage("editing an asset");
                    Console.WriteLine(e.Message);
                }

            }

        }


        // Start method accessible from program-class for adding a new task.
        public void AddAsset()
        {
            ReadAllDataForAddAsset();
        }



        // Reads user input for an Asset: type,brand,purchasedDate       
        private void ReadAllDataForAddAsset()
        {

            string dataType = "";
            string dataBrandName = "";
            string dataModelName = "";
            string dataPurchasedDate = "";
            string dataPrice = "";
            
            //string dataOfficeCountry = "";


            while (true)
            {
                bool typeOk = false;

                Console.WriteLine();               
                QuitCue();   //Console.WriteLine("Write q to quit");

                while (!typeOk)
                {
                    dataType = ReadDataFromUser("Enter a Type ('Computer' or 'Phone'). Write q to quit");
                    if (dataType.Trim().ToLower() == "computer" || dataType.Trim().ToLower() == "phone" || dataType.Trim().ToLower() == "q")
                    {
                        typeOk = true;
                    }
                }
                if (dataType.ToLower() == "q")
                {
                    break;
                }

                bool brandNameOk = false;
                while (!brandNameOk)
                {
                    dataBrandName = ReadDataFromUser("Enter a Brand Name. Write q to quit");
                    if (dataBrandName != "")
                    {
                        brandNameOk = true;
                    }
                }
                if (dataBrandName.Trim().ToLower() == "q")
                {
                    break;
                }

                bool modelNameOk = false;
                while (!modelNameOk)
                {
                    dataModelName = ReadDataFromUser("Enter a Model Name. Write q to quit");
                    if (dataModelName != "")
                    {
                        modelNameOk = true;
                    }
                }
                if (dataModelName.Trim().ToLower() == "q")
                {
                    break;
                }

                bool dateTimeOk = false;
                while (!dateTimeOk)
                {
                    dataPurchasedDate = ReadDataFromUser("Enter a PurchaseDate in format YYYY-MM-DD. Write q to quit");
                    if (ValidateDate(dataPurchasedDate.Trim()) || dataPurchasedDate.Trim().ToLower() == "q")
                    {
                        dateTimeOk = true;
                    }
                }
                if (dataPurchasedDate.Trim().ToLower() == "q")
                {
                    break;
                }

                bool priceOk = false;
                double price = 0;
                while (!priceOk)
                {
                    dataPrice = ReadDataFromUser("Enter a Price in USD.  Write q to quit");
                    if (dataPrice != "")
                    {
                        try
                        {
                            price = Convert.ToDouble(dataPrice);
                            priceOk = true;
                        }
                        catch (Exception e)  //.......
                        {
                            if (dataPrice.ToLower() == "q")
                            {
                                priceOk = true;
                            }
                            else
                            {
                                priceOk = false;
                            }
                        }
                    }
                }
                if (dataPrice.ToLower() == "q")
                {
                    break;
                }



                // All datas here because no break has been performed

                DateTime purDt = Convert.ToDateTime(dataPurchasedDate);
           
            
                try
                {
                    bool okStf = false;

                    if (dataType.Trim().ToLower() == "computer")
                    {
                        Computer c = new Computer
                        {
                            Type = "Computer",
                            Brand = dataBrandName,
                            Model = dataModelName,
                            PriceInDollar = price,
                            PurchaseDate = purDt
                        };

                        okStf = AssetRepo.CreateComputer(c);

                    }
                    else if (dataType.Trim().ToLower() == "phone")
                    {
                        Phone p = new Phone
                        {
                            Type = "Phone",
                            Brand = dataBrandName,
                            Model = dataModelName,
                            PriceInDollar = price,
                            PurchaseDate = purDt
                        };

                        okStf = AssetRepo.CreatePhone(p);
                    }

                    if (okStf)
                    {
                        SuccessMessage("added");
                    }
                    else
                    {
                        FailMessage("adding an asset");
                    }
                }
                catch (Exception e)
                {
                    FailMessage("adding an asset");
                    Console.WriteLine(e.Message);
                }               
            }
        }


        // Informs the user of the possibility to quit
        private void QuitCue()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Write q to quit");
            Console.ResetColor();
        }


        // Lets the user input the preferred sorting
        // Returns the string associated with the particular sort: 'd' ,'p' ,'t','s' or 'q' (if user quits)
        public string UserSortsList()
        {
            string dataSort = "";
            bool dataSortOk = false;

            Console.WriteLine();

            //Console.WriteLine("Write q to quit");
            QuitCue();

            //Console.WriteLine();

            while (!dataSortOk)
            {
                dataSort = ReadDataFromUser("Write 'd' to sort by Date ,'p' by Project, " +
                  "'t' by Tasktitle or 's' by Status");
                dataSort = dataSort.ToLower();

                if (dataSort == "d" || dataSort == "p" || dataSort == "t" || dataSort == "s" || dataSort == "q")
                {
                    dataSortOk = true;
                }
            }
            return dataSort;
        }


        // Start method accessible from program-class for editing / deleting a task
        public void ChangeAsset(List<Asset> assets)
        {
            Console.WriteLine();

            while (true)
            {
                string dataId = "";
                bool dataIdOk = false;

                int id = 0;

                while (!dataIdOk)
                {
                    Console.WriteLine();
             
                    QuitCue();
                   
                    dataId = ReadDataFromUser("Write the Id-number for the asset you want to change");
                    dataId = dataId.ToLower();

                    if (dataId != "q")
                    {

                        bool intOk = int.TryParse(dataId, out id);      // sends out int id if user entered an int

                        if (intOk)
                        {
                            // int index = assets.FindIndex(item => item.Id == id);  // FindIndex Only used to check if id exists  
                            int index = 0;


                            if (index >= 0)    // id exists
                            {
                                dataIdOk = true;
                            }
                            else  // -1
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The ID was not found");
                                Console.ResetColor();
                            }
                        }
                        // else : not recognized as id
                    }
                    else
                    {
                        dataIdOk = true;
                    }
                }

                if (dataId == "q")
                {
                    break;
                }


                string dataEditOrRemove = "";
                bool dataEditOrRemoveOk = false;

                while (!dataEditOrRemoveOk)
                {
                    dataEditOrRemove = ReadDataFromUser("Do you want to Edit 'E' or Remove 'X' the Asset");
                    dataEditOrRemove = dataEditOrRemove.ToLower();

                    if (dataEditOrRemove == "e" || dataEditOrRemove == "x" || dataEditOrRemove == "q")
                    {
                        dataEditOrRemoveOk = true;
                    }
                }
                if (dataEditOrRemove == "q")
                {
                    break;
                }

                // here dataEditOrRemove is either "e"  or "x" 
                if (dataEditOrRemove == "e")   // edit task
                {

                    EditAsset(assets, id);

                }
                else   // remove task  "x"
                {
                    RemoveAsset(assets, id);

                    break;
                }
            }
        }

        private double GetLocalPrice(Office office, double price)
        {
            double fact = 0;
            
            // currency values from 2024-07-23 , googled values

            if (office.Currency == "SEK")
            {
                fact = 10.75;                          
            }
            else if (office.Currency == "USD")
            {
                fact = 1;
            }
            else     // office.Currency == "EUR"
            {
                fact = 0.92;                     
            }
            return fact * price;
        }

        // Checks if a datetime-string of format "yyyy-MM-dd" is a valid date.
        // For instance: YYYY-04-30 is valid but YYYY-04-31 is not
        private bool ValidateDate(string str)
        {
            DateTime dt;
            string[] formats = { "yyyy-MM-dd" };
            if (DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        // Sort by ascending Title
        //private List<ProjectTask> TitleSort(List<ProjectTask> tasks)
        //{
        //    return tasks.OrderBy(item => item.TaskTitle).ToList();
        //}


        //// Sort by ascending ProjectName
        //private List<ProjectTask> ProjectSort(List<ProjectTask> tasks)
        //{
        //    return tasks.OrderBy(item => item.Project.Name).ToList();
        //}


        //// Sort by ascending date
        //private List<ProjectTask> DateSort(List<ProjectTask> tasks)
        //{
        //    return tasks.OrderBy(item => item.DueDate).ToList();
        //}


        //// Sort by alphabetical status:  Done,NotStarted,Started
        //private List<ProjectTask> StatusSort(List<ProjectTask> tasks)
        //{
        //    return tasks.OrderBy(item => item.Status.ToString()).ToList();
        //}


        // Help-Method to PrintAllTasks who returns the sort wanted 
        //private List<ProjectTask> GetSortedTasks(List<ProjectTask> tasks, string sort)
        //{
        //    if (sort == "p")
        //    {
        //        return ProjectSort(tasks);
        //    }
        //    else if (sort == "d")
        //    {
        //        return DateSort(tasks);
        //    }
        //    else if (sort == "s")
        //    {
        //        return StatusSort(tasks);
        //    }
        //    else       //   sort=="t"
        //    {
        //        return TitleSort(tasks);
        //    }
        //}

        // Writes the Header of the main-alternative 1-4
        public void UserChoiceHeader(string choice)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine();
            Console.WriteLine(choice);

            //Console.ResetColor();
        }


        // Writes the headers for the different task-params
        private void ListHeader(string sort="")
        {
            string typeString = "Type";
            string brandString = "Brand";
            string modelString = "Model";
            string priceDollarString = "Price dollar";
            //string localPriceString = "Local Price";
            string purchasedString = "Purchased Date";
            string officeString = "Office";


            //if (sort == "p")
            //{
            //    projString = "Project'";
            //}
            //else if (sort == "d")
            //{
            //    dueString = "DueDate'";
            //}
            //else if (sort == "t")
            //{
            //    taskString = "Task'";
            //}
            //else if (sort == "s")
            //{
            //    statusString = "Status'";
            //}

            Console.WriteLine();
            Console.WriteLine("Id".ToString().PadLeft(3).PadRight(7) + typeString.PadRight(10) + brandString.PadRight(18) + modelString.PadRight(18) + officeString.PadRight(10) + priceDollarString.PadRight(15) +
                purchasedString); ;
            
            Console.WriteLine("--".ToString().PadLeft(3).PadRight(7) + "----".PadRight(10) + "-----".PadRight(18) + "-----".ToString().PadRight(18) + "------".ToString().PadRight(10)  + "----------".ToString().PadRight(15) +
                "-------------");
        }


       
        //public void PrintAllAssets(string sort)

        public async void PrintAllAssets()
        {

              
            List<Asset> assets = await AssetRepo.GetAllAssets();

            // List<Asset> sorted = GetSortedAssets(assets, sort);

            ListHeader();        // show the sort-method by quotation-mark

            if (assets.Any())
            {
                //int cmpTime = 10;    //  days
                foreach (var asset in assets.ToList()) // Show List
                {

                    string dt = asset.PurchaseDate.Value.ToString("yyyy-MM-dd");
                 
                    Console.WriteLine(asset.Id.ToString().PadLeft(2).PadRight(7) + asset.Type.PadRight(10) + asset.Brand.PadRight(18) + asset.Model.PadRight(18) + asset.Office.Country.PadRight(10) + asset.PriceInDollar.ToString().PadRight(15) 
                     + dt);

                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(" No items here yet");
            }
            
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------");
        }

        //private int GetTimeSpanInDays(DateTime dt)
        //{
        //    TimeSpan ts = dt - DateTime.Now;
        //    return ts.Days;
        //}
    }
}

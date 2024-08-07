﻿using AssetTracking2.Data;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssetTracking2
{
    public class ListUtilities
    {

        public AssetRepository AssetRepo { get; set; }    //  Repository holds methods of data-access


        public ListUtilities()
        {
            AssetRepo = new AssetRepository();
        }


        // HelpFunction which lets the user input an answer (string) to a question (userAction)
        // The answer-string is returned
        private string ReadDataFromUser(string userAction)
        {
            Console.Write(userAction + ": ");    //example  userAction = "Enter a Name"
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


        

        // Writes the 6 main choices for the user
        public void WriteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
           
            Console.WriteLine();
            Console.WriteLine("(1) Show Asset List");
            Console.WriteLine("(2) Show Computers Only List");
            Console.WriteLine("(3) Show Phones Only List");
            Console.WriteLine("(4) Add New Asset");
            Console.WriteLine("(5) Edit Asset (update, remove)");
            Console.WriteLine("(6) Quit");

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


        // Lets the user decide if he wants to goahead with the removal of an Asset with a certain id
        // If 'Y/y': goes ahead with removal . if 'A/a' or 'Q/q': the process is aborted  
        private void RemoveAsset(int id)
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
            if (dataDelete != "a" && dataDelete != "q")     // Do the Remove
            {

                try
                {
                            

                    bool ok = AssetRepo.DeleteAsset(id);   // Delete Asset. We know id exists here from ChangeAsset().

                    if (ok)
                    {
              
                        SuccessMessage("removed");                     
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
        private void EditAsset(int id)
        {

            string dataEditParameter = "";
            bool dataEditParameterOk = false;

            Console.WriteLine("");

            while (!dataEditParameterOk)
            {
                dataEditParameter = ReadDataFromUser("Do you want to edit  Brand 'b' , Model 'm' , Price ($) 'p', Office 'o'" +
                    " or PurchasedDate 'dp'");
                dataEditParameter = dataEditParameter.ToLower();

                if (dataEditParameter == "dp" || dataEditParameter == "b" || dataEditParameter == "m" || dataEditParameter == "p" ||
                    dataEditParameter == "o" || dataEditParameter == "q")
                {
                    dataEditParameterOk = true;
                }
            }

            if (dataEditParameter.ToLower() != "q")   // Do the Edit
            {

                try
                {

                    Asset a = AssetRepo.GetAsset(id);        // get asset to edit, we know id exists here from ChangeAsset

                    //if (dataEditParameter == "t")
                    //{
                        //string typeName = "";
                        //bool typeNameOk = false;

                        //while (!typeNameOk)
                        //{
                        //    typeName = ReadDataFromUser("Give a new value for Type : 'Computer' or 'Phone'");
                        //    if (typeName.ToLower() == "computer" || typeName.ToLower() == "phone")
                        //    {
                        //        typeNameOk = true;
                        //    }
                        //}

                        //string oldType = a.Type;

                        //string t = typeName.ToLower();
                        //typeName = $"{t[0].ToString().ToUpper()}{t.Substring(1)}";   // Sets first Letter : Big 'C' or big 'P'

                        //a.Type = typeName;


                        //Phone p = new Phone
                        //{
                        //    Id = 13,
                        //    Type = "Phone",
                        //    Brand = a.Brand,
                        //    Model = a.Model,
                        //    OfficeId = a.OfficeId,
                        //    PriceInDollar = a.PriceInDollar,
                        //    LocalPrice = a.LocalPrice,
                        //    PurchaseDate = a.PurchaseDate
                        //};

                        //bool ok1 = AssetRepo.DeleteComputer(12);

                        //bool ok2 = AssetRepo.CreatePhone(p);

                        //p.Id = 12;
                        //bool ok3 = AssetRepo.UpdatePhone(p);                                              
                   // }

                    if (dataEditParameter == "dp")
                    {
                        string dataPurchasedDate = "";
                        bool dateTimeOk = false;

                        while (!dateTimeOk)
                        {
                            dataPurchasedDate = ReadDataFromUser("Enter a new value for PurchasedDate in format YYYY-MM-DD");
                            if (ValidateDate(dataPurchasedDate))
                            {
                                dateTimeOk = true;
                            }
                        }

                        DateTime purDt = Convert.ToDateTime(dataPurchasedDate);
                        a.PurchaseDate = purDt;     // edit field

                    }
                    else if (dataEditParameter == "b")
                    {
                        string brandName = "";
                        bool brandNameOk = false;

                        while (!brandNameOk)
                        {
                            brandName = ReadDataFromUser("Give a new value for Brand");
                            if (brandName != "")
                            {
                                brandNameOk = true;
                            }
                        }

                        a.Brand = brandName;    // edit field
                                                // 
                    }
                    else if (dataEditParameter == "m")
                    {

                        string model = "";
                        bool modelOk = false;

                        while (!modelOk)
                        {
                            model = ReadDataFromUser("Give a new value for Model");
                            if (model != "")
                            {
                                modelOk = true;
                            }
                        }

                        a.Model = model;
                    }

                    else if (dataEditParameter == "p")
                    {
                        string dataPrice = "";
                        bool priceOk = false;
                        double price = 0;

                        while (!priceOk)
                        {
                            dataPrice = ReadDataFromUser("Enter a Price in USD");
                            if (dataPrice != "")
                            {
                                try
                                {
                                    price = Convert.ToDouble(dataPrice);
                                    priceOk = true;
                                }
                                catch (Exception e)  //.......
                                {

                                    priceOk = false;
                                }
                            }
                        }

                        double lp = GetLocalPrice(a.Office.CountryCode, price);

                        a.PriceInDollar = price;
                        a.LocalPrice = lp;
                    }

                    else if (dataEditParameter == "o")
                    {

                        string officeCountry = "";
                        bool officeCountryOk = false;

                        while (!officeCountryOk)
                        {
                            officeCountry = ReadDataFromUser("Give a new value for OfficeCountry, 'SWE','USA' or 'SPA'");
                            officeCountry = officeCountry.ToUpper();

                            if (officeCountry == "SWE" || officeCountry == "USA" || officeCountry == "SPA")
                            {
                                officeCountryOk = true;
                            }
                        }

                        int officeId = AssetRepo.GetOfficeId(officeCountry);
                        double lp = GetLocalPrice(officeCountry, a.PriceInDollar);

                        a = new Asset    // since we are updating OfficeId (FK) we need to create a new Asset-object to use when updating
                        {
                            Id = a.Id,
                            Type = a.Type,
                            Brand = a.Brand,
                            Model = a.Model,
                            OfficeId = officeId,
                            PriceInDollar = a.PriceInDollar,
                            LocalPrice = lp,
                            PurchaseDate = a.PurchaseDate
                        };
                    }


                    bool ok = AssetRepo.UpdateAsset(a);     // Do the update


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


        // Start method accessible from program-class for adding a new asset.
        public void AddAsset()
        {
            ReadAllDataForAddAsset();
        }



        // Reads user input for an Asset: type,brand,model,price,office and purchasedDate       
        private void ReadAllDataForAddAsset()
        {

            string dataType = "";
            string dataBrandName = "";
            string dataModelName = "";
            string dataPurchasedDate = "";
            string dataPrice = "";
            string dataOfficeCountry = "";
            
         
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
                    if (ValidateDate(dataPurchasedDate.Trim()) || dataPurchasedDate.Trim().ToLower() == "q")    // Test that Date exists
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

                bool officeOk = false;
                while (!officeOk)
                {
                    dataOfficeCountry = ReadDataFromUser("Enter Office Country 'SWE', 'USA' or 'SPA'. Write q to quit");
                    if (dataOfficeCountry.Trim().ToLower() == "swe" || dataOfficeCountry.Trim().ToLower() == "usa" ||
                       dataOfficeCountry.Trim().ToLower() == "spa" || dataOfficeCountry.Trim().ToLower() == "q")
                    {
                        officeOk = true;
                    }
                }
                if (dataOfficeCountry.Trim().ToLower() == "q")
                {
                    break;
                }

                int officeId = AssetRepo.GetOfficeId(dataOfficeCountry.Trim().ToUpper());

                double localPrice = GetLocalPrice(dataOfficeCountry.Trim().ToUpper(),price);
 
                DateTime purDt = Convert.ToDateTime(dataPurchasedDate);

                // All datas here because no break has been performed

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
                            OfficeId = officeId,
                            PriceInDollar = price,
                            LocalPrice = localPrice,
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
                            OfficeId = officeId,
                            PriceInDollar = price,
                            LocalPrice = localPrice,
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
        //public string UserSortsList()
        //{
        //    string dataSort = "";
        //    bool dataSortOk = false;

        //    Console.WriteLine();

        //    QuitCue();

        //    while (!dataSortOk)
        //    {
        //        dataSort = ReadDataFromUser("Write 'd' to sort by Date ,'p' by Project, " +
        //          "'t' by Tasktitle or 's' by Status");
        //        dataSort = dataSort.ToLower();

        //        if (dataSort == "d" || dataSort == "p" || dataSort == "t" || dataSort == "s" || dataSort == "q")
        //        {
        //            dataSortOk = true;
        //        }
        //    }
        //    return dataSort;
        //}


        // Start method accessible from program-class for editing / deleting an asset
        public void ChangeAsset()
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
                   
                    dataId = ReadDataFromUser("Write the Id-number for the asset you want to edit/remove");
                    dataId = dataId.ToLower();

                    if (dataId != "q")
                    {

                        bool intOk = int.TryParse(dataId, out id);      // returns int id if user entered an int

                        if (intOk)
                        {
                            bool ok_id = AssetRepo.ExistsId(id);  // Only used to check if id exists  
                            
                            if (ok_id)     // id exists
                            {
                                dataIdOk = true;
                            }
                            else  
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
                    Console.WriteLine();
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
                if (dataEditOrRemove == "e")     // edit asset
                {

                    EditAsset(id);

                }
                else     // remove asset  "x"
                {
                    RemoveAsset(id);

                    break;
                }
            }
        }

        //
        private double GetLocalPrice(string country_code, double? price)
        {
            if (price != null)
            {
                double fact = 0;

                // currency values from 2024-07-23 , googled values

                if (country_code == "SWE")
                {
                    fact = 10.75;           // 1 dollar = 10.75 SEK                 
                }
                else if (country_code == "USA")
                {
                    fact = 1;
                }
                else     // country_code= "SPA",  office.Currency == "EUR"
                {
                    fact = 0.92;              // 1 dollar = 0.92 Euros         
                }

                return Math.Round(fact *(double) price, 2);
            }
            else
            {
                FailMessage("when assigning a local price. Price in USD was not set");
                return 0;
            }
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


        private List<Asset> BrandSort(List<Asset> assets)
        {
            return assets.OrderBy(item => item.Brand).ThenBy(item => item.PurchaseDate).ToList();
        }

        private List<Asset> ModelSort(List<Asset> assets)
        {
            return assets.OrderBy(item => item.Model).ThenBy(item => item.PurchaseDate).ToList();
        }

        private List<Asset> OfficeSort(List<Asset> assets)
        {
            return assets.OrderBy(item => item.Office.Country).ThenBy(item => item.PurchaseDate).ToList();
        }

        private List<Asset> PriceDollarSort(List<Asset> assets)
        {
            return assets.OrderBy(item => item.PriceInDollar).ThenBy(item => item.PurchaseDate).ToList();
        }

        private List<Asset> PurchasedDateSort(List<Asset> assets)
        {
            return assets.OrderBy(item => item.PurchaseDate).ToList();
        }

        private List<Asset> DefaultSort(List<Asset> assets)
        {
            return assets.OrderBy(item => item.Type).ThenBy(item=>item.PurchaseDate).ToList();
        }


        //Help_Method to PrintAllAssets who returns the assets sorted according to 'sort'
        private List<Asset> GetSortedAssets(List<Asset> assets, string sort)
        {
            if (sort == "b")
            {
                return BrandSort(assets);
            }
            else if (sort == "m")
            {
                return ModelSort(assets);
            }
            else if (sort == "o")
            {
                return OfficeSort(assets);
            }
            else if (sort == "p")
            {
                return PriceDollarSort(assets);
            }
            else if (sort == "pd")
            {
                return PurchasedDateSort(assets);
            }

            return DefaultSort(assets);   // By Type , then by PurchaseDate
        }

        // Writes the Header of the main-alternative 1-4
        public void UserChoiceHeader(string choice)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine();
            Console.WriteLine(choice);

            //Console.ResetColor();
        }


        // Writes the headers for the Asset-list
        // Highlights the sort-method used with a single-quotation-mark '
        private void ListHeader(string sort="")
        {
            string typeString = "Type";
            string brandString = "Brand";
            string modelString = "Model";
            string priceDollarString = "Price ($)";
            string localPriceString = "Local Price";
            string currencyString = "Curr";
            string purchasedString = "Purchased Date";
            string officeString = "Office";


            if (sort == "b")
            {
                brandString = "Brand'";
            }
            else if (sort == "m")
            {
                modelString = "Model'";
            }
            else if (sort == "p")
            {
                priceDollarString = "Price ($)'";
            }
            else if (sort == "lp")
            { 
                localPriceString = "Local Price'";
            }
            else if (sort == "pd")
            {
                purchasedString = "Purchased Date'";
            }
            else if (sort == "o")
            {
                officeString = "Office'";
            }
            else
            {
                typeString = "Type'";
            }


            Console.WriteLine();
            Console.WriteLine("Id".ToString().PadLeft(3).PadRight(8) + typeString.PadRight(10) + brandString.PadRight(18) + modelString.PadRight(18) + officeString.PadRight(10) + priceDollarString.PadRight(12) +
               localPriceString.PadRight(14) + currencyString.PadRight(6) + purchasedString); ;
            
            Console.WriteLine("--".ToString().PadLeft(3).PadRight(8) + "----".PadRight(10) + "-----".PadRight(18)  +"-----".ToString().PadRight(18) + "------".ToString().PadRight(10)  + "---------".ToString().PadRight(12) +
               "-----------".ToString().PadRight(14) + "----".ToString().PadRight(6) + "-------------");
        }


        // Gets the Assets from the db and prints them in a list
        // type = empty string => show all assets, type = "c" show only Computers
        // type = "P" show only Phones
        public void PrintAllAssets(string type="")
        {
            List<Asset> assets;
            string sort;         // sort = "":  sort by type,  sort = "o": sort by office 

            switch(type)
            {
                case "c":
                    assets = AssetRepo.GetAllComputers();
                    sort = "o";
                    break;
                case "p":
                    assets = AssetRepo.GetAllPhones();
                    sort = "o";
                    break;
                default:
                    assets = AssetRepo.GetAllAssets();
                    //sort = "";        // Level 2 - DefaultSort 
                    sort = "o";       // Level 3 - "o"=> OfficeSort,
                    break;                // "b"=> BrandSort, "m"=> ModelSort , "p" => PriceDollarSort                
            }    

            List<Asset> sorted = GetSortedAssets(assets,sort);        

            ListHeader(sort);                                          // show the sort-method by single-quotation-mark

            int cmpTime = 1096;   // 1096 days approx. 3 years
            if (assets.Any())
            {
                int cntRed = 0;
                int cntYellow = 0;

                foreach (var asset in sorted) // Show List
                {
                    int ts = GetTimeSpanInDays(asset.PurchaseDate);   // ts = Number of days since it was purchased
                                       

                    if (cmpTime - ts <= 91 && (cmpTime - ts >= 0))   // Level2 - assets between 2 years 9 months and 3 years old will be colored red 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        cntRed++;
                    }
                    else if (cmpTime - ts <= 183 && (cmpTime - ts >= 0))   // Level3 - assets between 2 years 6 months and 2,9 years old will be colored yellow 
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        cntYellow++;
                    }

                    string dt = asset.PurchaseDate.Value.ToString("yyyy-MM-dd");
                    string locPrice = Math.Round((double)asset.LocalPrice, 2).ToString();
                 
                    Console.WriteLine(" " + asset.Id.ToString().PadRight(7) + asset.Type.PadRight(10) + asset.Brand.PadRight(18) + asset.Model.PadRight(18) + asset.Office.Country.PadRight(10) + asset.PriceInDollar.ToString().PadRight(12) +
                      locPrice.PadRight(14) + asset.Office.Currency.PadRight(6) + dt);

                    Console.ResetColor();
                }

                Console.WriteLine();
               
                if (cntRed>0)
                {
                    string red_assets = (cntRed > 1) ? "assets" : "asset";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 3 months or less from end-of-life:  " + cntRed + " " + red_assets);
                }

                if (cntYellow > 0)
                {
                    string yellow_assets = (cntYellow > 1) ? "assets" : "asset";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" 6 months or less from end-of-life:  " + cntYellow + " " + yellow_assets);
                }
                
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(" No items here yet");
            }
            
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }

        private int GetTimeSpanInDays(DateTime? dt)
        {
            if (dt != null)
            {
                TimeSpan ts = (TimeSpan)(DateTime.Now - dt);
                return ts.Days;
            }
            return 0;
        }
    }
}

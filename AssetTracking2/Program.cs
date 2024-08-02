// See https://aka.ms/new-console-template for more information

using AssetTracking2;
using AssetTracking2.Models;



void Main()
{
    ListUtilities lu = new ListUtilities();

    string choice = "";

    while (true)
    {
        lu.WriteMenu();       // 1-4 choices
        choice = Console.ReadLine();

        if (choice.Trim().ToLower() == "1")       // 1 - Show List
        {
            lu.UserChoiceHeader("Show List Of Assets");
            lu.PrintAllAssets();

        }
        else if (choice.Trim().ToLower() == "2")    // 2 - Show computer list only
        {
            lu.UserChoiceHeader("Show List Of Computers Only");
            lu.PrintAllAssets("c");  
        }
        else if (choice.Trim().ToLower() == "3")   // 3 - Show phones list only
        {
            lu.UserChoiceHeader("Show List Of Phones Only");
            lu.PrintAllAssets("p");
        }
        else if (choice.Trim().ToLower() == "4")    // 4 - Add new Asset
        {
            lu.UserChoiceHeader("Add a new Asset");
            lu.AddAsset();
        }
        else if (choice.Trim().ToLower() == "5")     // 5 - Edit Asset (update,remove)
        {
            lu.UserChoiceHeader("Edit asset / Remove Asset");
            lu.PrintAllAssets(); 
            lu.ChangeAsset();
        }
        else if (choice.Trim().ToLower() == "6")     // 6 - Quit
        {
            break;
        }
    }
}

Main();
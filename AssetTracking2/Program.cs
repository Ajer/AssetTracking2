// See https://aka.ms/new-console-template for more information

using AssetTracking2;



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

            //string sort = lu.UserSortsList();
            //if (sort != "q")
            //{

             lu.PrintAllAssets();

            //}
        }
        else if (choice.Trim().ToLower() == "2")    // 2 - 
        {
            lu.UserChoiceHeader("Show List Of Computers Only");
            lu.PrintAllAssets("c");
        }
        else if (choice.Trim().ToLower() == "3")    // 2 - 
        {
            lu.UserChoiceHeader("Show List Of Phones Only");
            lu.PrintAllAssets("p");
        }
        else if (choice.Trim().ToLower() == "4")    // 2 - Add new Asset
        {
            lu.UserChoiceHeader("Add a new Asset");
            lu.AddAsset();
        }
        else if (choice.Trim().ToLower() == "5")     // 3 - Edit Asset (update,remove)
        {
            lu.UserChoiceHeader("Edit asset / Remove Asset");
            lu.PrintAllAssets(); 
            lu.ChangeAsset();
        }
        else if (choice.Trim().ToLower() == "6")     // 4 - Quit
        {
            break;
        }
    }
}

Main();
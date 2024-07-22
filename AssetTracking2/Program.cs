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
        else if (choice.Trim().ToLower() == "2")    // 2 - Add new Asset
        {
            lu.UserChoiceHeader("Add a new Asset");
           // lu.AddAsset();

        }
        else if (choice.Trim().ToLower() == "3")     // 3 - Edit Task (update, mark as done, remove)
        {
            lu.UserChoiceHeader("Edit asset / Remove Asset");
           // lu.PrintAllAssets();  // p
           // lu.ChangeAsset();

        }
        else if (choice.Trim().ToLower() == "4")     // 4 = Quit  (Everything already saved in TaskListUtilities)
        {

            break;
        }
    }
}

Main();
using DataAccessLayerCore.Models;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayerCore.Data
{
    class TravelListContextInitializer // : DropCreateDatabaseIfModelChanges<TravelListContext>
    {
        //protected override void Seed(TravelListContext context)
        //{
        //    base.Seed(context);

        //    TravelList travelList1 = new TravelList { Name = "Italy" };
        //    TravelList travelList2 = new TravelList { Name = "France" };
        //    TravelList travelList3 = new TravelList { Name = "Uk" };
        //    context.TravelLists.Add(travelList1);
        //    context.TravelLists.Add(travelList2);
        //    context.TravelLists.Add(travelList3);

        //    context.Items.AddRange(new[] {
        //    new Item { Name = "First", TravelList = travelList1 },
        //    new Item { Name = "Second", TravelList = travelList1 },
        //    new Item { Name = "Third", TravelList = travelList1 },

        //    new Item { Name = "First", TravelList = travelList2 },
        //    new Item { Name = "Second", TravelList = travelList2 },
        //    new Item { Name = "Third", TravelList = travelList2 },

        //    new Item { Name = "First", TravelList = travelList3 },
        //    new Item { Name = "Second", TravelList = travelList3 },
        //    new Item { Name = "Third", TravelList = travelList3 },
        //});

        //    context.SaveChanges();
        //}
    }
}

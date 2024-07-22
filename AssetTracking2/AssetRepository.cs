using AssetTracking2.Data;
using AssetTracking2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2
{
    public class AssetRepository
    {
        public async Task<List<Asset>> GetAllAssets()
        {
            List<Asset> list;
            using (var context = new AssetContext())
            {
                list = context.Assets.ToList();
            }     
            return list;
        }


    }
}

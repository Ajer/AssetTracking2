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
                list = context.Assets.Include(a=>a.Office).ToList();
            }
            return list;
        }

        public bool CreateComputer(Computer c)
        {
            bool createOk = false;
            using (var context = new AssetContext())
            {
                try
                {
                    context.Computers.Add(c);
                    context.SaveChanges();
                    createOk = true;
                }
                catch (Exception)
                {
                    createOk = false;
                }          
            }
            return createOk;
        }

        public bool CreatePhone(Phone p)
        {
            bool createOk = false;
            using (var context = new AssetContext())
            {
                try
                {
                    context.Phones.Add(p);
                    context.SaveChanges();
                    createOk = true;
                }
                catch (Exception)
                {
                    createOk = false;
                }
            }
            return createOk;
        }
    }
}

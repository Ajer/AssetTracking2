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
        public List<Asset> GetAllAssets()
        {
           List<Asset> list;
           using (var context = new AssetContext())
           {
             list = context.Assets.Include(a=>a.Office).AsNoTracking().ToList();  // OBS: AsNoTracking can only be used on reading queries
           }
           return list;
        }

        public List<Asset> GetAllComputers()
        {
            List<Asset> list;
            using (var context = new AssetContext())
            {
                list = context.Assets.Include(a => a.Office).Where(a => a.Type == "Computer").ToList();  // OBS: AsNoTracking can only be used on reading queries
            }
            return list;
        }


        public List<Asset> GetAllPhones()
        {
            List<Asset> list;
            using (var context = new AssetContext())
            {
                list = context.Assets.Include(a => a.Office).Where(a => a.Type == "Phone").ToList();  // OBS: AsNoTracking can only be used on reading queries
            }
            return list;
        }


        public Asset GetAsset(int id)
        {
            Asset a;
            using (var context = new AssetContext())
            {
                a = context.Assets.Include(a=>a.Office).Where(elem=>elem.Id==id).FirstOrDefault();
            }
            return a;
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

        public bool UpdateAsset(Asset a)
        {
            bool updateOk = false;
            using (var context = new AssetContext())
            {
                try
                {

                    if (a != null)
                    {
                        context.Assets.Update(a);
                        context.SaveChanges();
                        updateOk = true;
                    }
                }
                catch (Exception)
                {
                    updateOk = false;
                }
            }
            return updateOk;
        }

        

        public bool DeleteAsset(int id)
        {
            bool delOk = false;
            using (var context = new AssetContext())
            {
                try
                {
                    var a = context.Assets.Where(elem => elem.Id == id).FirstOrDefault();
                    if (a != null)
                    {
                        context.Assets.Remove(a);
                        context.SaveChanges();
                        delOk = true;
                    }
                }
                catch (Exception)
                {
                    delOk = false;
                }
            }
            return delOk;
        }

        

        public int GetOfficeId(string officeCountryCode)
        {
            
            using (var context = new AssetContext())
            {
                Office o =  context.Offices.Where(t => t.CountryCode == officeCountryCode).FirstOrDefault();

                //var  o =  (from off in context.Offices
                //           where off.CountryCode == dataOfficeCountry
                //           select off).FirstOrDefault();

                if (o != null)
                {
                    return o.Id;
                }

                return 0;
            }
        }

        public bool ExistsId(int id)
        {
            bool found = false;
            using (var context = new AssetContext())
            {
                var a = context.Assets.Where(item => item.Id == id).FirstOrDefault();
                if (a!=null)
                {
                    found = true;
                }
            }
            return found;
        }

        
    }
}

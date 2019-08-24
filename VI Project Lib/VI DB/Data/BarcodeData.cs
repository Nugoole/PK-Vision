using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VI_DB.Data
{
    public class BarCodeData : EntityData <Barcode>
    {
        public bool InsertOrUpdate(Barcode barcode)
        {
            using (PCBVIEntities context = new PCBVIEntities())
            {
                context.Entry(barcode).State =
                    barcode.BarcodeId != 0 ? EntityState.Modified : EntityState.Added;

                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    if (e != null)
                        return false;
                }
                return true;
            }
        }
    }
}

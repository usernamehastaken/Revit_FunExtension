using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revit_FunExtension
{
    public class Basic_Funs
    {
        public static void SetProjectUnits(Document doc, UnitType unitType, DisplayUnitType displayUnitType)
        {
            Transaction transaction = new Transaction(doc, "Change Units:"+displayUnitType.ToString());
            transaction.Start();
            TaskDialog.Show("Promt", "Change to "+displayUnitType.ToString());
            Units units = doc.GetUnits();
            FormatOptions formatOptions = new FormatOptions(displayUnitType);
            units.SetFormatOptions(unitType, formatOptions);
            doc.SetUnits(units);
            transaction.Commit();
        }
        
        public void n()
        {
            int init1 = 0;
            
        }
    }
}

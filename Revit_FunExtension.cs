using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

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
        
        public static void SetParametersToElement(ElementId elementId)
        {
            
        }

        public static bool SetNewParameterToInstanceWall(UIApplication app, DefinitionFile myDefinitionFile)
        {
            // create a new group in the shared parameters file
            DefinitionGroups myGroups = myDefinitionFile.Groups;
            DefinitionGroup myGroup = myGroups.Create("MyParameters1");

            // create an instance definition in definition group MyParameters
            ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions("Instance_ProductDate", ParameterType.Text);
            // Don't let the user modify the value, only the API
            option.UserModifiable = false;
            // Set tooltip
            option.Description = "Wall product date";
            Definition myDefinition_ProductDate = myGroup.Definitions.Create(option);

            // create a category set and insert category of wall to it
            CategorySet myCategories = app.Application.Create.NewCategorySet();
            // use BuiltInCategory to get category of wall
            Category myCategory = Category.GetCategory(app.ActiveUIDocument.Document, BuiltInCategory.OST_Walls);


            myCategories.Insert(myCategory);

            //Create an instance of InstanceBinding
            InstanceBinding instanceBinding = app.Application.Create.NewInstanceBinding(myCategories);

            // Get the BingdingMap of current document.
            BindingMap bindingMap = app.ActiveUIDocument.Document.ParameterBindings;

            // Bind the definitions to the document
            bool instanceBindOK = bindingMap.Insert(myDefinition_ProductDate,
                                            instanceBinding, BuiltInParameterGroup.PG_TEXT);
            return instanceBindOK;
        }

    }
}

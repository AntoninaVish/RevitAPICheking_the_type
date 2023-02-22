using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPICheking_the_type
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, "Выберите элементы");
            var wallist = new List<Wall>();//список стен

            foreach (var selectedElement in selectedElementRefList)
            {
                Element element = doc.GetElement(selectedElement);
                if (element is Wall)//проверяем: является ли элемент, который открыли стеной
                {
                    
                    Wall oWall = (Wall)element;//если элемент является стеной, то создаем новую переменную и преобразуем элемент в стену
                    wallist.Add(oWall);//добавляем данную переменную в список стен

                }

            }

            TaskDialog.Show("Selection", $"Количество: {wallist.Count}");//выводим список всех стен

            return Result.Succeeded;
        }
    }
}

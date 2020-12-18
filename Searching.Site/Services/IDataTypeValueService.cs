using System.Collections.Generic;
using System.Web.Mvc;

namespace Searching.Site.Services
{
    public interface IDataTypeValueService
    {
        IEnumerable<SelectListItem> GetItemsFromValueListDataType(string dataTypeName, 
            string[] selectedValues);
    }
}
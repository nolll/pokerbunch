using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models.FormModels{

	public class TimezoneFieldModel : SelectFieldModel{

		public TimezoneFieldModel(string fieldName, string fieldId, string selectedValue, IEnumerable<TimeZoneInfo> timezones, string firstItemText = null)
            : base(fieldName, fieldId, selectedValue, GetSelectItems(timezones), firstItemText)
        {
		}

        private static IEnumerable<SelectFieldItem> GetSelectItems(IEnumerable<TimeZoneInfo> timezones)
        {
			var items = new List<SelectFieldItem>();
			if(timezones != null){
			    items.AddRange(timezones.Select(timezone => new SelectFieldItem(timezone.DisplayName, timezone.Id)));
			}
			return items;
		}

	}

}
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Resources;
using Microsoft.AspNetCore.Components;
using System;

namespace MoneyManagerUi.Shared
{
    public class DatePickerComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<DateTimeOffset?> OnDateSelected { get; set; }

        [Parameter]
        public string Title { get; set; } = Resource.Date;

        public DateTimeOffset? SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnDateSelected.InvokeAsync(selectedDate);
            }
        }

        protected DateTimeOffset? selectedDate;
        protected string DatePickerClasses => isDateChanged ?
            $"{CssConstants.Form} {CssConstants.Valid}" : CssConstants.Form;
        protected bool isDateChanged;

        protected override void OnInitialized()
        {
            selectedDate = DateTime.Today;
            base.OnInitialized();
        }
    }
}

/*
    File name: EmailValidator.cs
    Purpose:   Behavior to ensure entry contains a
               valid email pattern.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace RedlineApp.Behaviors
{
    class EmailValidator : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var emailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
            + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            bool isValid = Regex.IsMatch(e.NewTextValue, emailPattern);

            ((Entry)sender).TextColor = isValid ? Color.FromHex("#000") : Color.FromHex("#ff9c96");
        }
    }
}

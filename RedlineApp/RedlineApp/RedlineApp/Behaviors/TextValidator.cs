/*
    File name: TextValidator.cs
    Purpose:   Behavior to ensure entry doesn't contain
               numeric values.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace RedlineApp.Behaviors
{
    public class TextValidator : Behavior<Entry>
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

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Regex reg = new Regex("[0-9]");
            bool isValid = reg.IsMatch(args.NewTextValue);
            ((Entry)sender).TextColor = isValid ? Color.Red : Color.Default;
        }
    }
}


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
        // Create EntryField property on entry behavior.
        public static readonly BindableProperty EntryFieldProperty =
            BindableProperty.Create("entryField", typeof(string),
                typeof(EmailValidator), default(string));

        public string EntryField
        {
            get { return (string)GetValue(EntryFieldProperty); }
            set { SetValue(EntryFieldProperty, value); }
        }

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

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            // Get error label.
            Label errorLabel = ((Entry)sender).FindByName<Label>(EntryField);

            string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
              + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            bool isEmail = Regex.IsMatch(args.NewTextValue, emailRegex, RegexOptions.IgnoreCase);

            if (isEmail)
            {
                ((Entry)sender).TextColor = Color.Default;
                errorLabel.Text = "";
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
                errorLabel.Text = "Invalid email address.";
            }
        }
    }
}

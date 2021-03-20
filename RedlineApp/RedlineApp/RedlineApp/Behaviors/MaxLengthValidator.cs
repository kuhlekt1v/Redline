/*
    File name: MaxLengthValidator.cs
    Purpose:   Behavior to ensure entry doesn't exceed
               specified maximum length and display error.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedlineApp.Behaviors
{
    class MaxLengthValidator : Behavior<Entry>
    {
        // Create MaxLength property on entry behavior
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("maxLength", typeof(int),
            typeof(MaxLengthValidator), 0);

        // Create EntryField property on entry behevaior
        public static readonly BindableProperty EntryFieldProperty =
            BindableProperty.Create("entryField", typeof(string),
                typeof(MaxLengthValidator), default(string));

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public string EntryField
        {
            get { return (string)GetValue(EntryFieldProperty); }
            set { SetValue(EntryFieldProperty, value); }
        }

        // Watch entry for text being added.
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        // Watch entry for text being removed.
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            // Get error label.
            Label errorLabel = ((Entry)sender).FindByName<Label>(EntryField);
            // Format EntryField for display in error message.
            string fieldName = string.Join(" ", EntryField.Split('_'));
            bool isValid = args.NewTextValue.Length <= MaxLength;

            if (isValid)
            {
                ((Entry)sender).TextColor = Color.Default;
                errorLabel.Text = "";
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
                errorLabel.Text = $"{fieldName} cannot exceed {MaxLength} characters.";
            }
        }
    }
}

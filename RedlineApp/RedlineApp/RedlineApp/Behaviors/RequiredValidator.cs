using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedlineApp.Behaviors
{
    class RequiredValidator : Behavior<Entry>
    {
        // Create Required property on entry behevaior
        public static readonly BindableProperty RequiredProperty =
            BindableProperty.Create("required", typeof(bool),
                typeof(RequiredValidator), true);

        // Create EntryField property on entry behevaior
        public static readonly BindableProperty EntryFieldProperty =
            BindableProperty.Create("entryField", typeof(string),
                typeof(RequiredValidator), default(string));

        public bool Required
        {
            get { return (bool)GetValue(RequiredProperty); }
            set { SetValue(RequiredProperty, value); }
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
            string fieldName = string.Join(" ", EntryField.Split('_'));

            if (String.IsNullOrEmpty(args.NewTextValue))
            {
                ((Entry)sender).PlaceholderColor = Color.FromHex("#eb1f10");
                ((Entry)sender).Placeholder = $"{fieldName} Required";
                Required = true;
            }
            else
            {
                Required = false;
            }
        }
    }
}

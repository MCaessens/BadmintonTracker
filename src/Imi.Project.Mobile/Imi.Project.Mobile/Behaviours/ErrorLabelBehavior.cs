using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Imi.Project.Mobile.Behaviours
{
    public class ErrorLabelBehavior : Behavior<Label>
    {
        protected override void OnAttachedTo(Label label)
        {
            label.PropertyChanged += OnEntryTextChanged;
            base.OnAttachedTo(label);
        }

        protected override void OnDetachingFrom(Label label)
        {
            label.PropertyChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(label);
        }
        void OnEntryTextChanged(object sender, PropertyChangedEventArgs e)
        {
            var label = (Label) sender;
            label.IsVisible = label.Text != null;
        }
    }
}

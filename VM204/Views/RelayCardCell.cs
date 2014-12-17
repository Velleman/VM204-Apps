using System;
using Xamarin.Forms;

namespace VM204
{
	public class RelayCardCell : ViewCell
	{
		public RelayCardCell ()
		{
			var label = new Label{ 
				YAlign = TextAlignment.Center
			};
			label.SetBinding(Label.TextProperty, "Name");

			var layout = new StackLayout{ 
				Padding = new Thickness(20,0,0,0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {label}
			};
			View = layout;

		}

		protected override void OnBindingContextChanged ()
		{
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}


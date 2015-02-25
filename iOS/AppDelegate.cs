using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin;

namespace VM204.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			Insights.Initialize ("debc89672cad1c2f0b12316c57e0af418b2b7504");

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}


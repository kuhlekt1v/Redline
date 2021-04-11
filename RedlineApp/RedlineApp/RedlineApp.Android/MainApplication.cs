using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



[MetaData("com.google.android.maps.v2.API_KEY",
              Value = "AIzaSyC7W4MIjUl-83OZv1Coz7gfzxNEiipTipM")]
public class MainApplication : Application
{
    public MainApplication(IntPtr handle, JniHandleOwnership transer)
      : base(handle, transer)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        CrossCurrentActivity.Current.Init(this);
    }
}
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using BookReader.CustomeView;
using BookReader.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FullSrceenPage), typeof(FullSrceenRenderer))]
namespace BookReader.Droid.Renderer
{
    public class FullSrceenRenderer : PageRenderer
    {

        public FullSrceenRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                return;
            }
            if (!(Context is Activity activity) || !(e.NewElement is FullSrceenPage page))
            {
                return;
            }

            DisplayMetrics metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetRealMetrics(metrics);
            page.ScreenHeight = metrics.HeightPixels / metrics.Density;
            page.ScreenWidth = metrics.WidthPixels / metrics.Density;
        }

        protected override void OnWindowVisibilityChanged(ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);
            
            if (!(Context is Activity activity) || !(Element is FullSrceenPage page))
            {
                return;
            }
            if (visibility == ViewStates.Visible && page.EnterOnAppear)
            {
//                activity.Window.SetFlags(
//                    WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
                var uiOptions = SystemUiFlags.HideNavigation | SystemUiFlags.ImmersiveSticky | SystemUiFlags.Fullscreen;
                activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                page.IsFullSrceen = true;
            }
            else if(page.IsFullSrceen && visibility == ViewStates.Gone && page.ExitOnDisAppear)
            {
                //activity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
                activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.Visible;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == FullSrceenPage.IsFullSrceenProperty.PropertyName)
            {
                if (!(Context is Activity activity))
                {
                    return;
                }
                var isFullSrceen = ((FullSrceenPage) sender).IsFullSrceen;
                if (isFullSrceen)
                {
//                    activity.Window.SetFlags(
//                        WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
                    var uiOptions = SystemUiFlags.HideNavigation | SystemUiFlags.ImmersiveSticky | SystemUiFlags.Fullscreen| SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutHideNavigation;
                    activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                }
                else
                {
                    //activity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
                    activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutFullscreen|SystemUiFlags.LayoutHideNavigation);
                }
            }
        }
    }
}
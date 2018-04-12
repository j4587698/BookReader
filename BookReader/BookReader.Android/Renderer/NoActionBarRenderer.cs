using Android.App;
using Android.Content;
using BookReader.Droid.Renderer;
using BookReader.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(NavigationPage), typeof(NoActionBarRenderer))]
namespace BookReader.Droid.Renderer
{ 
    public class NoActionBarRenderer : NavigationRenderer
    { 
        public NoActionBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            ((Activity)Context).ActionBar.Hide();
        }
    }
}
using Windows.UI.Xaml;
using BookReader.CustomeView;
using BookReader.UWP.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(FullSrceenPage), typeof(FullSrceenRenderer))]
namespace BookReader.UWP.Renderer
{
    public class FullSrceenRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                return;
            }
            if (!(e.NewElement is FullSrceenPage page))
            {
                return;
            }

            page.ScreenHeight = (float)Window.Current.Bounds.Height;
            page.ScreenWidth = (float) Window.Current.Bounds.Width;
        }
    }
}
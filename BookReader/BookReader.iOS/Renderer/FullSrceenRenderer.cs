using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;

namespace BookReader.iOS.Renderer
{
    public class FullSrceenRenderer : PageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Element.PropertyChanged += OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        
    }
}
using Xamarin.Forms;

namespace BookReader.CustomeView
{
    public class FullSrceenPage : ContentPage
    {
        /// <summary>
        /// 当前是否全屏
        /// </summary>
        public static readonly BindableProperty IsFullSrceenProperty =
            BindableProperty.Create("IsFullSrceen", typeof(bool), typeof(FullSrceenPage), false);


        public bool IsFullSrceen
        {
            get => (bool) GetValue(IsFullSrceenProperty);
            set => SetValue(IsFullSrceenProperty, value);
        }

        public static readonly BindableProperty EnterOnAppearProperty = BindableProperty.Create("EnterOnAppear", typeof(bool), typeof(FullSrceenPage), true);

        public bool EnterOnAppear
        {
            get => (bool)GetValue(EnterOnAppearProperty);
            set => SetValue(EnterOnAppearProperty, value);
        }

        public static readonly BindableProperty ExitOnDisAppearProperty = BindableProperty.Create("ExitOnDisAppear", typeof(bool), typeof(FullSrceenPage), true);

        public bool ExitOnDisAppear
        {
            get => (bool)GetValue(ExitOnDisAppearProperty);
            set => SetValue(ExitOnDisAppearProperty, value);
        }
    }
}
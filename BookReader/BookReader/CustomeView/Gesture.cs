using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookReader.CustomeView
{
    public static class Gesture
    {
        public static readonly BindableProperty TapCommandProperty = BindableProperty.CreateAttached("TapCommand", typeof(ICommand), typeof(Gesture), null, propertyChanged: CommandChanged);
        public static readonly BindableProperty PanCommandProperty = BindableProperty.CreateAttached("PanCommand", typeof(ICommand), typeof(Gesture), null, propertyChanged: CommandChanged);

        public static ICommand GetTapCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(TapCommandProperty);
        }

        public static void SetTapCommand(BindableObject view, ICommand value)
        {
            view.SetValue(TapCommandProperty, value);
        }

        public static ICommand GetPanCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(PanCommandProperty);
        }

        public static void SetPanCommand(BindableObject view, ICommand value)
        {
            view.SetValue(PanCommandProperty, value);
        }

        private static void GetOrCreateEffect(Xamarin.Forms.View view)
        {
            var effect = (GestureEffect)view.Effects.FirstOrDefault(e => e is GestureEffect);
            if (effect == null)
            {
                effect = new GestureEffect();
                view.Effects.Add(effect);
            }
        }

        private static void CommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Xamarin.Forms.View view)
            {
                GetOrCreateEffect(view);
            }
        }

        class GestureEffect : RoutingEffect
        {
            public GestureEffect() : base("BookReader.PlatformGestureEffect")
            {
            }
        }

        public class PanEventArgs
        {
            public Point StartPosition { get; set; }

            public Point CurrentPosition { get; set; }

            public Point TotalMove { get; set; }

            public GestureStatus StatusType { get; set; }
        }
    }
}
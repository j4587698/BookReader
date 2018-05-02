using System;
using System.ComponentModel;
using System.Windows.Input;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using BookReader.CustomeView;
using BookReader.Droid.Effect;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("BookReader")]
[assembly: ExportEffect(typeof(PlatformGestureEffect), nameof(PlatformGestureEffect))]
namespace BookReader.Droid.Effect
{
    public class PlatformGestureEffect : PlatformEffect
    {
        private GestureDetectorCompat _gestureRecognizer;
        private readonly InternalGestureDetector _tapDetector;
        private ICommand _tapCommand;
        private ICommand _panCommand;
        private Action<Point> _tappedAction;
        private Action<Gesture.PanEventArgs> _panUpdateAction;
        private DisplayMetrics _displayMetrics;

        private Point _panStartPoint;
         

        public PlatformGestureEffect()
        {
            _tapDetector = new InternalGestureDetector
            {
                TapAction = motionEvent =>
                {
                    var x = motionEvent.GetX();
                    var y = motionEvent.GetY();
                    var point = PxToDp(new Point(x, y));
                    _tappedAction?.Invoke(point);
                    if (_tapCommand?.CanExecute(point) == true)
                    {
                        _tapCommand.Execute(point);
                    }
                },
                StartPanAction = initialDown =>
                {
                    _panStartPoint = new Point(initialDown.GetX(), initialDown.GetY());
                    Gesture.PanEventArgs arg = new Gesture.PanEventArgs
                    {
                        StartPosition = PxToDp(_panStartPoint),
                        CurrentPosition = PxToDp(_panStartPoint),
                        StatusType = GestureStatus.Started,
                        TotalMove = new Point(0, 0)
                    };
                    _panUpdateAction?.Invoke(arg);
                    if (_panCommand?.CanExecute(arg) == true)
                        _panCommand.Execute(arg);
                },
                PanAction = (currentMove) =>
                {
                    Gesture.PanEventArgs arg = new Gesture.PanEventArgs
                    {
                        StartPosition = PxToDp(_panStartPoint),
                        CurrentPosition = PxToDp(new Point(currentMove.GetX(), currentMove.GetY())),
                        TotalMove = PxToDp(new Point(currentMove.GetX() - _panStartPoint.X,
                            currentMove.GetY() - _panStartPoint.Y)),
                        StatusType = GestureStatus.Running
                    };
                    _panUpdateAction?.Invoke(arg);
                    if (_panCommand?.CanExecute(arg) == true)
                        _panCommand.Execute(arg);
                },
                EndPanAction = currentMove =>
                {
                    Gesture.PanEventArgs arg = new Gesture.PanEventArgs
                    {
                        StartPosition = PxToDp(_panStartPoint),
                        CurrentPosition = PxToDp(new Point(currentMove.GetX(), currentMove.GetY())),
                        TotalMove = PxToDp(new Point(currentMove.GetX() - _panStartPoint.X,
                            currentMove.GetY() - _panStartPoint.Y)),
                        StatusType = GestureStatus.Completed
                    };
                    _panUpdateAction?.Invoke(arg);
                    if (_panCommand?.CanExecute(arg) == true)
                        _panCommand.Execute(arg);
                }
            };
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            _tapCommand = Gesture.GetTapCommand(Element);
            _panCommand = Gesture.GetPanCommand(Element);
            _tappedAction = Gesture.GetTappedAction(Element);
            _panUpdateAction = Gesture.GetPanUpdateAction(Element);
        }

        protected override void OnAttached()
        {
            var control = Control ?? Container;

            var context = control.Context;
            _displayMetrics = context.Resources.DisplayMetrics;
            _tapDetector.Density = _displayMetrics.Density;

            if (_gestureRecognizer == null)
                _gestureRecognizer = new GestureDetectorCompat(context, _tapDetector);
            control.Touch += ControlOnTouch;

            OnElementPropertyChanged(new PropertyChangedEventArgs(String.Empty));
        }

        private void ControlOnTouch(object sender, Android.Views.View.TouchEventArgs touchEventArgs)
        {
            switch (touchEventArgs.Event.Action)
            {
                case MotionEventActions.Up:
                    if (_tapDetector.Srcolling)
                        _tapDetector.Srcolling = false;
                        _tapDetector.EndPanAction?.Invoke(touchEventArgs.Event);
                    break;
            }
            _gestureRecognizer?.OnTouchEvent(touchEventArgs.Event);
        }

        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.Touch -= ControlOnTouch;
        }

        private Point PxToDp(Point point)
        {
            point.X = point.X / _displayMetrics.Density;
            point.Y = point.Y / _displayMetrics.Density;
            return point;
        }

        private sealed class InternalGestureDetector : GestureDetector.SimpleOnGestureListener
        {
            public bool Srcolling = false;

            public Action<MotionEvent> TapAction { private get; set; }

            public Action<MotionEvent> StartPanAction { private get; set; }

            public Action<MotionEvent> PanAction { private get; set; }

            public Action<MotionEvent> EndPanAction { get; set; }

            public float Density { get; set; }

            public override bool OnSingleTapUp(MotionEvent e)
            {
                TapAction?.Invoke(e);
                return true;
            }

            public override bool OnScroll(MotionEvent initialDown, MotionEvent currentMove, float distanceX, float distanceY)
            {
                if (!Srcolling)
                {
                    StartPanAction?.Invoke(initialDown);
                }

                Srcolling = true;
                PanAction?.Invoke(currentMove);
                return base.OnScroll(initialDown, currentMove, distanceX, distanceY);
                //return true;
            }
        }
    }
}
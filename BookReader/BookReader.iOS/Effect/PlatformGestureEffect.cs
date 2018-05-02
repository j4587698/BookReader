using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using BookReader.CustomeView;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace BookReader.iOS.Effect
{
    public class PlatformGestureEffect : PlatformEffect
    {
        private readonly UITapGestureRecognizer _tapDetector;
        private readonly UIPanGestureRecognizer _panDetector;
        private ICommand _tapCommand;
        private ICommand _panCommand;

        private readonly List<UIGestureRecognizer> _recognizers;

        private CGPoint _panStartPoint;

        public PlatformGestureEffect()
        {
            //if (!allSubviews)
            //    tapDetector.ShouldReceiveTouch = (s, args) => args.View != null && (args.View == view || view.Subviews.Any(v => v == args.View));
            //else
            //    tapDetector.ShouldReceiveTouch = (s, args) => true;

            _tapDetector = CreateTapRecognizer(() => _tapCommand);
            _panDetector = CreatePanRecognizer(() => _panCommand);

            _recognizers = new List<UIGestureRecognizer>
            {
                _tapDetector,
                _panDetector
            };
        }

        private UIPanGestureRecognizer CreatePanRecognizer(Func<ICommand> getCommand)
        {
            return new UIPanGestureRecognizer(recognizer =>
            {
                var handler = getCommand();
                if (handler != null)
                {
                    var control = Control ?? Container;
                    Gesture.PanEventArgs arg = new Gesture.PanEventArgs();
                    switch (recognizer.State)
                    {
                        case UIGestureRecognizerState.Possible:
                            arg.StatusType = GestureStatus.Canceled;
                            break;
                        case UIGestureRecognizerState.Began:
                            arg.StatusType = GestureStatus.Started;
                            _panStartPoint = recognizer.LocationInView(control);
                            break;
                        case UIGestureRecognizerState.Changed:
                            arg.StatusType = GestureStatus.Running;
                            break;
                        case UIGestureRecognizerState.Ended:
                            arg.StatusType = GestureStatus.Completed;
                            break;
                        case UIGestureRecognizerState.Cancelled:
                            arg.StatusType = GestureStatus.Canceled;
                            break;
                        case UIGestureRecognizerState.Failed:
                            arg.StatusType = GestureStatus.Canceled;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    var totalCgPoint = recognizer.TranslationInView(control);
                    var totalPoint = new Point(totalCgPoint.X, totalCgPoint.Y);
                    var cgPositon = recognizer.LocationInView(control);
                    var potision = new Point(cgPositon.X, cgPositon.Y);
                    arg.CurrentPosition = potision;
                    arg.StartPosition = new Point(_panStartPoint.X, _panStartPoint.Y);
                    arg.TotalMove = totalPoint;
                    if (handler.CanExecute(arg))
                        handler.Execute(arg);
                }
            })
            {
                Enabled = false,
                ShouldRecognizeSimultaneously = (recognizer, gestureRecognizer) => true,
                MaximumNumberOfTouches = 1,
            };
        }

        private UITapGestureRecognizer CreateTapRecognizer(Func<ICommand> getCommand)
        {
            return new UITapGestureRecognizer(recognizer =>
            {
                var handler = getCommand();
                if (handler != null)
                {
                    var control = Control ?? Container;
                    var point = recognizer.LocationInView(control);
                    var pt = new Point(point.X, point.Y);
                    if (handler.CanExecute(pt))
                        handler.Execute(pt);
                }
            })
            {
                Enabled = false,
                ShouldRecognizeSimultaneously = (recognizer, gestureRecognizer) => true,
                //ShouldReceiveTouch = (recognizer, touch) => true,
            };
        }

        protected override void OnAttached()
        {
            var control = Control ?? Container;

            foreach (var recognizer in _recognizers)
            {
                control.AddGestureRecognizer(recognizer);
                recognizer.Enabled = true;
            }

            OnElementPropertyChanged(new PropertyChangedEventArgs(String.Empty));
        }

        protected override void OnDetached()
        {
            var control = Control ?? Container;
            foreach (var recognizer in _recognizers)
            {
                recognizer.Enabled = false;
                control.RemoveGestureRecognizer(recognizer);
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            _tapCommand = Gesture.GetTapCommand(Element);
            _panCommand = Gesture.GetPanCommand(Element);
        }
    }
}
using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace BookReader.CustomeView
{
    public class TurnImageView : SKCanvasView
    {
        #region Field

        private int _currentIndex;
        private float _left, _right, _top, _buttom;

        #endregion


        #region Property

        public static readonly BindableProperty ImagesProperty = BindableProperty.Create("Images",
            typeof(IList<SKBitmap>), typeof(TurnImageView), null, propertyChanged: ImagesChange);

        public IList<SKBitmap> ImageSource
        {
            get => (IList<SKBitmap>) GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

        private static void ImagesChange(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is TurnImageView view && oldvalue is IList<SKBitmap> oldList &&
                  newvalue is IList<SKBitmap> newList))
            {
                return;
            }

            var index = newList.IndexOf(oldList[view._currentIndex]);
            view._currentIndex = index == -1 ? 0 : index;
        }

        public static readonly BindableProperty TurnTypeProperty = BindableProperty.Create("TurnType",
            typeof(TurnTypes), typeof(TurnImageView), TurnTypes.BookTurn);

        public TurnTypes TurnType
        {
            get => (TurnTypes) GetValue(TurnTypeProperty);
            set => SetValue(ImagesProperty, value);
        }

        public static readonly BindableProperty LeftPercentProperty = BindableProperty.Create("LeftPercent",
            typeof(int), typeof(TurnImageView), 33, validateValue: PercentValidate, propertyChanged: PercentChanged);

        public int LeftPercent
        {
            get => (int) GetValue(LeftPercentProperty);
            set => SetValue(LeftPercentProperty, value);
        }

        public static readonly BindableProperty RightPercentProperty = BindableProperty.Create("RightPercent",
            typeof(int), typeof(TurnImageView), 33, validateValue: PercentValidate, propertyChanged: PercentChanged);

        public int RightPercent
        {
            get => (int) GetValue(RightPercentProperty);
            set => SetValue(RightPercentProperty, value);
        }

        public static readonly BindableProperty TopPercentProperty = BindableProperty.Create("TopPercent",
            typeof(int), typeof(TurnImageView), 33, validateValue: PercentValidate, propertyChanged: PercentChanged);

        public int TopPercent
        {
            get => (int) GetValue(TopPercentProperty);
            set => SetValue(TopPercentProperty, value);
        }

        public static readonly BindableProperty ButtomPercentProperty = BindableProperty.Create("ButtomPercent",
            typeof(int), typeof(TurnImageView), 33, validateValue: PercentValidate, propertyChanged: PercentChanged);

        public int ButtomPercent
        {
            get => (int) GetValue(ButtomPercentProperty);
            set => SetValue(ButtomPercentProperty, value);
        }

        private static void PercentChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is TurnImageView view))
            {
                return;
                ;
            }

        }

        private static bool PercentValidate(BindableObject bindable, object value)
        {
            if (!(value is int percent))
            {
                return false;
            }

            return percent >= 0 && percent <= 100;
        }

        /// <summary>
        /// The aspect property.
        /// </summary>
        public static readonly BindableProperty AspectProperty =
            BindableProperty.Create(nameof(Aspect), typeof(Aspect), typeof(TurnImageView), Aspect.AspectFit);

        /// <summary>
        /// Gets or sets the aspect.
        /// </summary>
        /// <value>The aspect.</value>
        public Aspect Aspect
        {
            get => (Aspect) GetValue(AspectProperty);
            set => SetValue(AspectProperty, value);
        }


        #endregion

        #region Override

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            SizeRequest desiredSize = base.OnMeasure(double.PositiveInfinity, double.PositiveInfinity);

            double desiredWidth = desiredSize.Request.Width;
            double desiredHeight = desiredSize.Request.Height;

            if (desiredWidth == 0 || desiredHeight == 0)
                return new SizeRequest(new Size(0, 0));

            double desiredAspect = desiredSize.Request.Width / desiredSize.Request.Height;
            double constraintAspect = widthConstraint / heightConstraint;

            double width = desiredWidth;
            double height = desiredHeight;
            if (constraintAspect > desiredAspect)
            {
                // constraint area is proportionally wider than image
                switch (Aspect)
                {
                    case Aspect.AspectFit:
                    case Aspect.AspectFill:
                        height = Math.Min(desiredHeight, heightConstraint);
                        width = desiredWidth * (height / desiredHeight);
                        break;
                    case Aspect.Fill:
                        width = Math.Min(desiredWidth, widthConstraint);
                        height = desiredHeight * (width / desiredWidth);
                        break;
                }
            }
            else if (constraintAspect < desiredAspect)
            {
                // constraint area is proportionally taller than image
                switch (Aspect)
                {
                    case Aspect.AspectFit:
                    case Aspect.AspectFill:
                        width = Math.Min(desiredWidth, widthConstraint);
                        height = desiredHeight * (width / desiredWidth);
                        break;
                    case Aspect.Fill:
                        height = Math.Min(desiredHeight, heightConstraint);
                        width = desiredWidth * (height / desiredHeight);
                        break;
                }
            }
            else
            {
                // constraint area is same aspect as image
                width = Math.Min(desiredWidth, widthConstraint);
                height = desiredHeight * (width / desiredWidth);
            }

            return new SizeRequest(new Size(width, height));
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            base.OnTouch(e);

        }

        #endregion

    }

    public enum TurnTypes
    {
        BookTurn,
        LandscapeLamination,
        PortraitLamination,
        LandscapeTranslation,
        PortraitTranslation,
    }
}
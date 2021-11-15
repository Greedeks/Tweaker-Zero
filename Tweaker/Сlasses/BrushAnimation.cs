using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Tweaker.Сlasses
{
    internal class BrushAnimation : AnimationTimeline
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BrushAnimation();
        }

        public override Type TargetPropertyType
        {
            get { return typeof(Brush); }
        }

        static BrushAnimation()
        {
            FromProperty = DependencyProperty.Register("From", typeof(Brush),
              typeof(BrushAnimation));

            ToProperty = DependencyProperty.Register("To", typeof(Brush),
              typeof(BrushAnimation));
        }

        internal static readonly DependencyProperty FromProperty;
        internal Brush From
        {
            get
            {
                return (Brush)GetValue(BrushAnimation.FromProperty);
            }
            set
            {
                SetValue(BrushAnimation.FromProperty, value);
            }
        }

        internal static readonly DependencyProperty ToProperty;
        internal Brush To
        {
            get
            {
                return (Brush)GetValue(BrushAnimation.ToProperty);
            }
            set
            {
                SetValue(BrushAnimation.ToProperty, value);
            }
        }

        public override object GetCurrentValue(object defaultOriginValue,
        object defaultDestinationValue, AnimationClock animationClock)
        {
            Brush _fromVal = ((Brush)GetValue(BrushAnimation.FromProperty)).CloneCurrentValue();
            Brush _toVal = ((Brush)GetValue(BrushAnimation.ToProperty)).CloneCurrentValue();

            if ((double)animationClock.CurrentProgress == 0.0)
                return _fromVal;

            if ((double)animationClock.CurrentProgress == 1.0)
                return _toVal;

            _toVal.Opacity = (double)animationClock.CurrentProgress;


            Border _Bd = new Border();
            Border _Bdr = new Border();

            _Bd.Width = 1.0;
            _Bd.Height = 1.0;

            _Bd.Background = _fromVal;
            _Bdr.Background = _toVal;

            _Bd.Visibility = Visibility.Visible;
            _Bdr.Visibility = Visibility.Visible;
            _Bd.Child = _Bdr;

            Brush VB = new VisualBrush(_Bd);
            return VB;

        }
    }
}

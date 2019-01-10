using System;
using Xamarin.Forms;

namespace UniqueColor.Controls
{
    public class CustomEntry : Entry
    {
        #region Properties

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(
                propertyName: nameof(BorderColor),
                returnType: typeof(Color),
                declaringType: typeof(CustomEntry),
                defaultValue: Color.Transparent);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static BindableProperty BorderWidthProperty =
            BindableProperty.Create(
                propertyName: nameof(BorderWidth),
                returnType: typeof(float),
                declaringType: typeof(CustomEntry),
                defaultValue: 0f);

        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static BindableProperty BorderRadiusProperty =
            BindableProperty.Create(
            propertyName: nameof(BorderRadius),
            returnType: typeof(float),
            declaringType: typeof(CustomEntry),
            defaultValue: 0f);

        public float BorderRadius
        {
            get { return (float)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static BindableProperty LeftPaddingProperty =
            BindableProperty.Create(
            propertyName: nameof(LeftPadding),
            returnType: typeof(int),
            declaringType: typeof(CustomEntry),
            defaultValue: 5);

        public int LeftPadding
        {
            get { return (int)GetValue(LeftPaddingProperty); }
            set { SetValue(LeftPaddingProperty, value); }
        }

        public static BindableProperty RightPaddingProperty =
            BindableProperty.Create(
            propertyName: nameof(RightPadding),
            returnType: typeof(int),
            declaringType: typeof(CustomEntry),
            defaultValue: 5);

        public int RightPadding
        {
            get { return (int)GetValue(RightPaddingProperty); }
            set { SetValue(RightPaddingProperty, value); }
        }

        public static BindableProperty LowercaseProperty =
            BindableProperty.Create(
            propertyName: nameof(Lowercase),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false);

        public bool Lowercase
        {
            get { return (bool)GetValue(LowercaseProperty); }
            set { SetValue(LowercaseProperty, value); }
        }

        #endregion
    }
}

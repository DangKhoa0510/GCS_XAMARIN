using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using GCS_CPC_EMEC.Droid.Library;
using GCS_CPC_EMEC.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
//[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace GCS_CPC_EMEC.Droid.Library
{
   
    public class ImageEntryRenderer : EntryRenderer
    {
        ImageEntry element;
        public ImageEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (ImageEntry)this.Element;
            this.Control.KeyListener = DigitsKeyListener.GetInstance("1234567890,");           
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        Control.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                        break;
                    case ImageAlignment.Right:
                        Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
                        break;
                }
            }
            
            Control.CompoundDrawablePadding = 25;
            var gradienDrawble = new GradientDrawable();
            gradienDrawble.SetCornerRadius(60f);
            gradienDrawble.SetStroke(5, Android.Graphics.Color.DeepPink);
            gradienDrawble.SetColor(Android.Graphics.Color.LightGray);
            Control.SetBackground(gradienDrawble);
            Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);

        }


        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * element.ImageSize, element.ImageHeight * element.ImageSize, true));
        }
    }
}
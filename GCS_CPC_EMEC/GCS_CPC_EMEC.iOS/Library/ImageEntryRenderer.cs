using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Foundation;
using GCS_CPC_EMEC.Interface;
using GCS_CPC_EMEC.iOS.Library;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace GCS_CPC_EMEC.iOS.Library
{
    public class ImageEntryRenderer : EntryRenderer
    {

        public ImageEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ImageEntry)this.Element;

            var textField = this.Control;

            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        textField.LeftViewMode = UITextFieldViewMode.Always;
                        textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                    case ImageAlignment.Right:
                        textField.RightViewMode = UITextFieldViewMode.Always;
                        textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                }
            }
            else
            {
                textField.LeftViewMode = UITextFieldViewMode.Always;
                textField.LeftView = new UIView(new System.Drawing.RectangleF(20, 0, 10, 0));
            }
            

            //textField.BorderStyle = UITextBorderStyle.None;
            //CALayer bottomBorder = new CALayer
            //{
            //    Frame = new CGRect(0.0f, element.HeightRequest - 1, this.Frame.Width, 1.0f),
            //    BorderWidth = 2.0f,
            //    BorderColor = element.LineColor.ToCGColor()
            //};

            textField.Layer.CornerRadius = 20;
            textField.Layer.BorderWidth = 2f;
            textField.Layer.BorderColor = Xamarin.Forms.Color.DeepPink.ToCGColor();
            textField.Layer.BackgroundColor = Xamarin.Forms.Color.LightGray.ToCGColor();

            // textField.Layer.AddSublayer(bottomBorder);
            //textField.Layer.MasksToBounds = true;

        }

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(10, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.RectangleF(10, 0, width + 10, height + 5));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }
    }
}
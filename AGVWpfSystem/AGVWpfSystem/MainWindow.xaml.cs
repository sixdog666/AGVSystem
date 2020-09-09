using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors.Settings;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using DevExpress.Xpf.Ribbon;


namespace AGVWpfSystem {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
           // bFileName.Content = "Document 1";
            ((ComboBoxEditSettings)eFontSize.EditSettings).ItemsSource = (new FontSizes()).Items;
            InitializeFontFamilyGallery();

        }


        void InitializeFontFamilyGallery() {
            foreach (FontFamily fontFamily in (new DecimatedFontFamilies()).Items) {
                ImageSource src = CreateImage(fontFamily);
                FontFamilyGalleryGroup.Items.Add(CreateItem(fontFamily, src));
                FontFamilyDropDownGalleryGroup.Items.Add(CreateItem(fontFamily, src));
            }
        }

        FormattedText fmtText = null;
        FormattedText createFormattedText(FontFamily fontFamily) {
            return new FormattedText("Aa", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), 18, Brushes.Black, null, TextFormattingMode.Ideal);
        }

        ImageSource CreateImage(FontFamily fontFamily) {
            const double DimensionSize = 32;
            const double HalfDimensionSize = DimensionSize / 2d;
            DrawingVisual v = new DrawingVisual();
            DrawingContext c = v.RenderOpen();
            c.DrawRectangle(Brushes.White, null, new Rect(0, 0, DimensionSize, DimensionSize));
            if (fmtText == null)
                fmtText = createFormattedText(fontFamily);
            fmtText.SetFontFamily(fontFamily);
            fmtText.TextAlignment = TextAlignment.Center;
            double verticalOffset = (DimensionSize - fmtText.Baseline) / 2d;
            c.DrawText(fmtText, new Point(HalfDimensionSize, verticalOffset));
            c.Close();

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)DimensionSize, (int)DimensionSize, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(v);
            return rtb;
        }

        GalleryItem CreateItem(FontFamily fontFamily, ImageSource image) {
            GalleryItem item = new GalleryItem();
            item.Glyph = image;
            item.Caption = fontFamily.ToString();
            item.Tag = fontFamily;
            return item;
        }

        void textEditor_SelectionChanged(object sender, RoutedEventArgs e) {
            ShowHideSelectionCategory();
            InvokeUpdateFormat();
        }

        bool isInvokePending = false;

        void InvokeUpdateFormat() {
            if (!isInvokePending) {
               // Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(UpdateFormat));
                isInvokePending = true;
            }
            UpdateFormat();
        }

        protected void UpdateFormat() {
        }

        void ShowHideSelectionCategory() {
        }


        void FontFamilyGallery_ItemChecked(object sender, GalleryItemEventArgs e) {
            FontFamily newFontFamily = (FontFamily)e.Item.Tag;
        }

        void eFontSize_EditValueChanged(object sender, RoutedEventArgs e) {
        }


        void OptionsButton_Click(object sender, RoutedEventArgs e) {
            (RibbonControl.ApplicationMenu as ApplicationMenu).ClosePopup();
        }
        void ExitButton_Click(object sender, RoutedEventArgs e) {
            (RibbonControl.ApplicationMenu as ApplicationMenu).ClosePopup();
        }



        void groupEdit_CaptionButtonClick(object sender, EventArgs e) {
            MessageBox.Show("DevExpress Ribbon Control", "Edit Settings Dialog");
        }

        private void bAbout_ItemClick(object sender, ItemClickEventArgs e) {
            MessageBox.Show("DevExpress Ribbon Control", "About Window");
        }

        private void groupFile_CaptionButtonClick(object sender, RibbonCaptionButtonClickEventArgs e) {
            MessageBox.Show("DevExpress Ribbon Control", "File Settings Dialog");

        }

        private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Point pointClicked = e.GetPosition(drawingSurface);
          //  Shape shape = drawingSurface.get


        }

        private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e) {

        }
    }

    public class RecentItem {
        public int Number { get; set; }
        public string FileName { get; set; }
    }

    public class ButtonWithImageContent {
        public string ImageSource { get; set; }
        public object Content { get; set; }
    }

    public class FontSizes {
        public double[] Items {
            get {
                return new double[] {
            3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5,
            10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
            16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
            32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
            80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
            };
            }
        }
    }

    public class DecimatedFontFamilies : FontFamilies {
        const int DecimationFactor = 5;

        public override ObservableCollection<FontFamily> Items {
            get {
                ObservableCollection<FontFamily> res = new ObservableCollection<FontFamily>();
                for (int i = 0; i < ItemsCore.Count; i++) {
                    if (i % DecimationFactor == 0)
                        res.Add(ItemsCore[i]);
                }
                return res;
            }
        }
    }

    public class FontFamilies {
        static ObservableCollection<FontFamily> items;
        protected static ObservableCollection<FontFamily> ItemsCore {
            get {
                if (items == null) {
                    items = new ObservableCollection<FontFamily>();
                    foreach (FontFamily fam in Fonts.SystemFontFamilies) {
                        if (!IsValidFamily(fam)) continue;
                        items.Add(fam);
                    }
                }
                return items;
            }
        }
        public static bool IsValidFamily(FontFamily fam) {
            foreach (Typeface f in fam.GetTypefaces()) {
                GlyphTypeface g = null;
                try {
                    if (f.TryGetGlyphTypeface(out g))
                        if (g.Symbol) return false;
                }
                catch (Exception) {
                    return false;
                }
            }
            return true;
        }
        public virtual ObservableCollection<FontFamily> Items {
            get {
                ObservableCollection<FontFamily> res = new ObservableCollection<FontFamily>();
                foreach (FontFamily fm in ItemsCore) {
                    res.Add(fm);
                }
                return res;
            }
        }
    }

}

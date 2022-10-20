using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragAndDropLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetInitialListData();
            LabelDrop.AllowDrop = true;
            ListDragTarget.AllowDrop = true;
            this.LabelDrag.MouseDown += new MouseButtonEventHandler(this.LabelDrag_MouseDown);
            this.LabelDrop.DragEnter += new DragEventHandler(this.LabelDrop_DragEnter);
            this.LabelDrop.Drop += new DragEventHandler(this.LabelDrop_DragDrop);
            this.LabelToDrag1.MouseDown += new MouseButtonEventHandler(this.Labels_MouseDown);
            this.LabelToDrag2.MouseDown += new MouseButtonEventHandler(this.Labels_MouseDown);
            this.LabelToDrag3.MouseDown += new MouseButtonEventHandler(this.Labels_MouseDown);
            this.ListDragTarget.DragEnter += new DragEventHandler(this.ListDragTarget_DragEnter);
            this.ListDragTarget.Drop += new DragEventHandler(this.ListDragTarget_DragDrop);
        }

        private void LabelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(lbl, lbl.Content.ToString(), DragDropEffects.Copy);
            }
        }
        private void Labels_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dragSource = sender as Label;
            Random rnd = new Random();
            var data = new MyItem()
            {
                Name = dragSource.Name,
                Number = rnd.Next(),
                ImageFile = "default.jpg"
            };

            var dataObj = new DataObject(data);
            dataObj.SetData("DragSource", dragSource);
            DragDrop.DoDragDrop(dragSource, dataObj, DragDropEffects.Copy);
        }

        private void LabelDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void LabelDrop_DragDrop(object sender, DragEventArgs e)
        {
            LabelDrop.Content = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void ListDragTarget_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void ListDragTarget_DragDrop(object sender, DragEventArgs e)
        {
            ListDragTarget.Items.Add(e.Data.GetData(DataFormats.Text).ToString()); 
        }

        private void SetInitialListData()
        {
            List<MyItem> items = new List<MyItem>();
            items.Add(new MyItem()
            {
                Name = "Default Item",
                Number = 39547,
                ImageFile = "default.jpg"
            });
            ListDragTarget.ItemsSource = items;
        }
    }
}

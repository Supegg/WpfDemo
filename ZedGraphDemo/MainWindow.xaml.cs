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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ZedGraph;
using System.Drawing;

namespace ZedGraphDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.SizeChanged += MainWindow_SizeChanged;
            draw();
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetSize();
        }

        private void draw()
        {
            // Get a reference to the GraphPane instance in the ZedGraphControl
            GraphPane myPane = zg1.GraphPane;
            
            // Set the titles and axis labels
            myPane.Title.Text = "Demonstration of Dual Y Graph";
            myPane.XAxis.Title.Text = "Time, Days";
            myPane.YAxis.Title.Text = "Parameter A";
            myPane.Y2Axis.Title.Text = "Parameter B";

            // Make up some data points based on the Sine function
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 36; i++)
            {
                double x = (double)i * 5.0;
                double y = Math.Sin((double)i * Math.PI / 15.0) * 16.0;
                double y2 = y * 13.5;
                list.Add(x, y);
                list2.Add(x, y2);
            }

            // Generate a red curve with diamond symbols, and "Alpha" in the legend
            LineItem curve = myPane.AddCurve("Alpha", list, Color.Red, SymbolType.Diamond);
            // Fill the symbols with white
            curve.Symbol.Fill = new Fill(Color.White);
            // Generate a blue curve with circle symbols, and "Beta" in the legend
            LineItem curve2 = myPane.AddCurve("Beta", list2, Color.Blue, SymbolType.Circle);
            // Fill the symbols with white
            curve2.Symbol.Fill = new Fill(Color.White);
            // Associate this curve with the Y2 axis
            curve2.IsY2Axis = true;
            
            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            myPane.YAxis.Scale.IsReverse = false;
            // 设置坐标范围
            myPane.YAxis.Scale.Min = -20;
            myPane.YAxis.Scale.Max = 20;
            
            // Enable the Y2 axis display
            myPane.Y2Axis.IsVisible = true;
            // Make the Y2 axis scale blue
            myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
            myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            myPane.Y2Axis.MajorTic.IsOpposite = true;
            myPane.Y2Axis.MinorTic.IsOpposite = true;
            // Display the Y2 axis grid lines
            myPane.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.Gray, 45.0f);

            // Add a text box with instructions
            TextObj text = new TextObj(
                "Zoom: left mouse & drag\nPan: middle mouse & drag\nContext Menu: right mouse",
                0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom);
            text.FontSpec.StringAlignment = StringAlignment.Near;
            myPane.GraphObjList.Add(text);

            // Enable scrollbars if needed
            zg1.IsShowHScrollBar = false;
            zg1.IsShowVScrollBar = false;
            zg1.IsAutoScrollRange = true;
            zg1.IsScrollY2 = true;

            // OPTIONAL: Show tooltips when the mouse hovers over a point
            zg1.IsShowPointValues = true;
            zg1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);//自定义tooltip显示内容

            // OPTIONAL: Handle the Zoom Event,缩放
            zg1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(MyZoomEvent);

            //添加右键菜单
            System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem item = new System.Windows.Forms.MenuItem();
            item.Text = "Add an alpha point";
            item.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            item.Click += item_Click;
            menu.MenuItems.Add(item);
            zg1.ContextMenu = menu;
            
            
            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zg1.AxisChange();
            // Make sure the Graph gets redrawn
            zg1.Invalidate();
        }

        void item_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MenuItem item = (System.Windows.Forms.MenuItem)sender;
            item.Checked = !item.Checked;
            AddBetaPoint();
        }

        private void SetSize()
        {
            host.Margin = new Thickness(10);
            // Leave a small margin around the outside of the control
            host.Width = this.ActualWidth - 30;
            host.Height = this.ActualHeight - 40;

            zg1.Width = (int)host.Width;
            zg1.Height = (int)host.Height;
        }

        /// <summary>
        /// Display customized tooltips when the mouse hovers over a point
        /// </summary>
        private string MyPointValueHandler(ZedGraphControl control, GraphPane pane,
                        CurveItem curve, int iPt)
        {
            // Get the PointPair that is under the mouse
            PointPair pt = curve[iPt];

            return curve.Label.Text + " is " + pt.Y.ToString("f2") + " units at " + pt.X.ToString("f1") + " days";
        }

        /// <summary>
        /// Beta曲线加一个点
        /// </summary>
        private void AddBetaPoint()
        {
            // Get a reference to the "Beta" curve IPointListEdit
            IPointListEdit ip = zg1.GraphPane.CurveList["Beta"].Points as IPointListEdit;
            if (ip != null)
            {
                double x = ip.Count * 5.0;
                double y = Math.Sin(ip.Count * Math.PI / 15.0) * 16.0 * 13.5;
                ip.Add(x, y);
                zg1.AxisChange();//坐标系自适应
                zg1.Refresh();
            }
        }

        // Respond to a Zoom Event
        private void MyZoomEvent(ZedGraphControl control, ZoomState oldState,
                    ZoomState newState)
        {
            // Here we get notification everytime the user zooms
        }

    }
}

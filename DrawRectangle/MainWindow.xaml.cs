using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawRectangle
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// マウス押下時の座標
        /// </summary>
        private Point clickPoint = new Point(0, 0);

        /// <summary>
        /// 描画中のオブジェクト
        /// </summary>
        private Rectangle currentRect = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // マウス押下時の座標を保持
            this.clickPoint = e.GetPosition(this.drawCanvas);

            // 描画オブジェクトの生成
            this.currentRect = new Rectangle
            {
                Stroke = Brushes.Green,
                StrokeThickness = 1
            };
            Canvas.SetLeft(this.currentRect, clickPoint.X);
            Canvas.SetTop(this.currentRect, clickPoint.X);

            // オブジェクトをキャンバスに追加
            this.drawCanvas.Children.Add(this.currentRect);
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // 「マウスを左クリックしていないとき、描画中のオブジェクトが無いとき」は何もしない
            if (e.LeftButton == MouseButtonState.Released || this.currentRect == null)
            {
                return;
            }

            // クリック座標とマウス座標から、長方形の位置とサイズを求める
            // ※小さい座標が左上位置、大きい座標がサイズ※
            Point mousePoint = e.GetPosition(drawCanvas);
            double x = Math.Min(mousePoint.X, this.clickPoint.X);
            double y = Math.Min(mousePoint.Y, this.clickPoint.Y);
            double width = Math.Max(mousePoint.X, this.clickPoint.X) - x;
            double height = Math.Max(mousePoint.Y, this.clickPoint.Y) - y;

            // 描画中オブジェクトの情報を更新
            this.currentRect.Width = width;
            this.currentRect.Height = height;
            Canvas.SetLeft(this.currentRect, x);
            Canvas.SetTop(this.currentRect, y);
        }

        private void drawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 描画中オブジェクトの参照を削除
            this.currentRect = null;
        }
    }
}


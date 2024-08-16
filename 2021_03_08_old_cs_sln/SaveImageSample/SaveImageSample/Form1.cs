using ImageViewer.Controls;
using ImageViewer.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveImageSample
{
    public partial class Form1 : Form
    {
        ErrorLog.IErrorLog errorLog;
        IViewImageControl viewImageControl;
        IViewInnerControl viewInnerControl;
        IViewImage viewImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                errorLog = new ErrorLog.ErrorLog();
                // picturebox
                viewImageControl = new ViewImageControl(
                    errorLog,this.panel1,this.pictureBox1,new ViewImageSettings(),new ViewControlState());
                // panel
                viewInnerControl = new ViewInnerControl(
                    errorLog, this, panel1, viewImageControl.Settings, viewImageControl.State);
                // viewimage
                viewImage = new ViewImage();

            } catch (Exception ex)
            {
                MessageBox.Show("Initialize Failed.\n" + ex.Message);
            }
        }

        private void PaintImage(string path)
        {
            try
            {
                Control frameControl = this;
                int ret;
                // イメージを読み込む
                ret = viewImage.SetPath(path);
                if (ret < 1)
                {
                    errorLog.AddException(new Exception(this.ToString()), "ViewImageDefault setPath"); return;
                }
                // ImageSize
                Size ImageSize = viewImage.GetImage().Size;
                // CalcObjectSet
                SizeLocationCalc Calclator = new SizeLocationCalc();
                // Assert
                Calclator.SizeIsNull(frameControl.Size, viewImage.GetImage().Size);
                // サイズ計算 Panel にフィットさせる
                Size newSize = Calclator.GetSizeFitFrame(frameControl.Size, viewImage.GetImage().Size);

                // Location
                if (viewInnerControl.Settings.IsViewAlwaysCenter)
                {
                    // 常に中央に表示する場合
                    // サイズ変更、中央計算
                }
                else
                {
                    // PictureBox 非固定

                }
                // ちらつき防止
                viewInnerControl.SetVisible(false);

                // イメージをセット
                viewInnerControl.SetImageWithDispose(viewImage.GetImage());
                // SetSize
                viewInnerControl.ChangeSize(frameControl.Size);
                // visible
                viewInnerControl.SetVisible(true);
                // save size and location
                viewInnerControl.SaveRaitoSizeAndPositionFromFrameControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PaintImage Failed.\n" + ex.Message);
            }
        }

        // 計算用クラス
        public class SizeLocationCalc
        {
            ErrorLog.IErrorLog _errorLog;
            public void SetErrorLog(ErrorLog.IErrorLog errorlog) { _errorLog = errorlog; }
            // Image を Frameに合わせる
            public Size GetSizeFitFrame(Size frameSize, Size imageSize)
            {
                try
                {
                    // ※ sizeIsNull を外部で実行しておく
                    //'PictureBoxのサイズをPanelサイズにFitさせる
                    //'＝PictureBoxのサイズをPanelサイズからはみ出さないようにする

                    //'サイズを合わせるための基準 Criteria for matching sizes
                    //'Public Overloads Sub setFlagCriteriaForMatchSize(argFlag As Integer)
                    //'0 Image OuterFit
                    //'1 Inner_Horizontal 幅はInnerで高さにOuterの比率をかける
                    //'2 Inner_Vertical　高さはInnerで幅にOuterの比率をかける
                    //'FlagCriteriaForMatchSize = argFlag
                    // 縦横比 1:AspectRaito
                    double AspectRaito = (double)imageSize.Width / imageSize.Height;
                    // Frame と Image の縦比
                    double VerticalRaito = (double)frameSize.Height / imageSize.Height;
                    // Frame と Image の縦比
                    double HorizontalRaito = (double)frameSize.Width / imageSize.Width;

                    double retRaito = 0, NewHeight = 0, NewWidth = 0;
                    // 倍率が小さいほうを設定する
                    if (VerticalRaito < HorizontalRaito)
                    {
                        // Frame の縦に合わせる
                        //NewHeight = frameSize.Height * 1;
                        //retRaito = NewHeight / imageSize.Height; // 倍率を計算する
                        //NewWidth = imageSize.Width * retRaito; //* AspectRaito; // 上記倍率から縦横比を掛けて Width を決定

                        // 縦のほうが倍率が小さい→横の余白が長い→縦に合わせる
                        NewHeight = frameSize.Height * 1;
                        // 縦の倍率を求める imageSize が frameSize になる
                        retRaito = (double)frameSize.Height / imageSize.Height; // VerticalRaito
                        // 倍率を Width にかける
                        NewWidth = imageSize.Width * retRaito;
                    }
                    else
                    {
                        // Frame の横に合わせる
                        NewWidth = frameSize.Width * 1;
                        retRaito = NewWidth / frameSize.Width; // 倍率を計算する
                        NewHeight = imageSize.Height * retRaito; // 上記倍率から縦横比を掛けて Height を決定
                    }
                    return new Size((int)NewWidth, (int)NewHeight);
                }
                catch (Exception ex)
                {
                    _errorLog.AddException(ex, this.ToString() + " getSizeFitFrame");
                    return new Size();
                }
            }

            public bool SizeIsNull(Size frameSize, Size imageSize)
            {
                try
                {
                    if (frameSize == null)
                    {
                        _errorLog.AddErrorNotException(this.ToString() + " getPointLocationCenter ,frameSize null");
                        return false;
                    }
                    if (imageSize == null)
                    {
                        _errorLog.AddErrorNotException(this.ToString() + " getPointLocationCenter ,imageSize null");
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    _errorLog.AddException(ex, this.ToString() + " getPointLocationCenter");
                    return false;
                }
            }

            // 中央に表示
            public Point GetPointLocationCenter(Size frameSize, Size imageSize)
            {
                try
                {
                    // sizeIsNull を外部で実行しておく
                    double width = (frameSize.Width - imageSize.Width) / 2;
                    double height = (frameSize.Height - imageSize.Height) / 2;
                    return new Point((int)width, (int)height);
                }
                catch (Exception ex)
                {
                    _errorLog.AddException(ex, this.ToString() + " getPointLocationCenter");
                    return new Point();
                }
            }
        }
    }
}

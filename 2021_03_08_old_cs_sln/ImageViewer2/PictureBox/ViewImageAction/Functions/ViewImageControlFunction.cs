using System;
using ImageViewer2;
using System.Drawing;
using System.Diagnostics;
using System.Windows;

namespace ViewImageAction
{
    public class ViewImageControlFunction
    {
        ErrorLog.IErrorLog _errorlog;
        public IViewImageControl ViewImageControl;
        public IViewInnerControl ViewInnerControl;
        public ViewControl.IViewFrameControl ViewFrameControl;
        public ViewImageObjects ViewImageObjects;

        public ViewImageControlFunction(
            IViewImageControl pictureBoxControl,IViewInnerControl viewInnerControl, ViewControl.IViewFrameControl viewFrameControl)
        {
            ViewImageControl = pictureBoxControl;
            ViewInnerControl = viewInnerControl;
            ViewFrameControl = viewFrameControl;
        }

        public void SetErrorLog(ErrorLog.IErrorLog errorLog) { _errorlog = errorLog; }


        // 画像表示時、マウスドラッグでコントロール移動時、拡大縮小時に記録する
        // saveDifferenceSizeAndPositionFromFramecControl
        public void SaveDifferenceSizeAndPositionInnerControlFromFramecControl()
        {
            ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();
        }
        // FrameControlとInnerControlのサイズと位置比率を維持する
        // Maintain the System.Drawing.Size and position ratio of Frame Control and Inner Control
        public void MaintainSizeAndPositionRatioFrameAndInner(object sender, EventArgs e)
        {
            try
            {
                
                
                Debug.WriteLine("MaintainSizeAndPositionRatioFrameAndInner");
                // 今の FrameControl サイズ
                System.Drawing.Size nowSize = ViewFrameControl.getSize();
                //System.Drawing.Point differentSize = ViewInnerControl.State.DifferenceSizeInnerFromFrame;
                //System.Drawing.Size afterSize = new System.Drawing.Size(
                //    nowSize.Width - differentSize.X, nowSize.Height - differentSize.Y);

                if (ViewImageObjects.ViewFrameControl.State.RatioSizeInnerFromFrame.X <= 0)
                {
                    _errorlog.addErrorNotException( this.ToString(), "MaintainSizeAndPositionRatioFrameAndInner Failed");
                }
                if (ViewImageObjects.ViewFrameControl.State.RatioSizeInnerFromFrame.Y <= 0)
                {
                    _errorlog.addErrorNotException( this.ToString(), "MaintainSizeAndPositionRatioFrameAndInner Failed");
                }


                // 記録した比率から算出
                double ratioW = (double)nowSize.Width * (double)ViewImageObjects.ViewFrameControl.State.RatioSizeInnerFromFrame.X;
                double ratioH = (double)nowSize.Height * (double)ViewImageObjects.ViewFrameControl.State.RatioSizeInnerFromFrame.Y;
                System.Drawing.Size afterSize = new System.Drawing.Size((int)ratioW, (int)ratioH);
                    

                System.Drawing.Point nowPos = ViewInnerControl.getLocation();
                System.Drawing.Point differentPos = ViewInnerControl.State.DifferencePositionInnerInFrame;
                double newX = ViewImageObjects.ViewFrameControl.State.RatioLocationInnerFromFrameX
                    * (double)ViewFrameControl.getSize().Width;
                double newY = ViewImageObjects.ViewFrameControl.State.RatioLocationInnerFromFrameY
                    * (double)ViewFrameControl.getSize().Height;

                System.Drawing.Point newPos = new System.Drawing.Point((int)newX,(int)newY);


                Debug.WriteLine("before Size = " + nowSize.Width + ", " + nowSize.Height);
                Debug.WriteLine("before pos  = " + nowPos.X + ", " + nowPos.Y);

                Debug.WriteLine("new Size = " + afterSize.Width + ", " + afterSize.Height);
                Debug.WriteLine("new pos  = " + newPos.X + ", " + newPos.Y);
                //Debug.WriteLine("pos  = " + newX + ", " + newY);

                this.ChangeSizeAndLocationForInnerConrol(afterSize, newPos);

            } catch (Exception ex)
            {
                _errorlog.addException(ex, this.ToString(), "MaintainSizeAndPositionRatioFrameAndInner Failed");
            }
        }

        // Frame コントロールのサイズ変更、位置変更
        public void ChangeSizeFrameControl(System.Drawing.Size size)
        {
            try
            {
                ViewFrameControl.changeSize(size);
                ViewFrameControl.saveRatioFromContentscControl();
                //ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();
            } catch (Exception ex)
            {
                _errorlog.addException(ex, this.ToString(), "ChangeSizeFrameControl Failed");
            }
        }

        public void SaveRatioFromContentscControl()
        {
            ViewFrameControl.saveRatioFromContentscControl();
        }

        // サイズ変更
        public void ChangeSizeViewControl(System.Drawing.Size newSize)
        {
            try
            {
                //ViewImageBasicFunction.SizeLocationCalc calc = new ViewImageBasicFunction.SizeLocationCalc();

                //System.Drawing.Size System.Drawing.Size = ViewInnerControl.getSize();
                //System.Drawing.Size = new System.Drawing.Size((int)(System.Drawing.Size.Width * raito), (int)(System.Drawing.Size.Height * raito));
                // 拡大縮小時にポジションを変更のための計算
                //System.Drawing.Point newLocation = ChangeLocationWhenChangeSize(ViewInnerControl.getSize(), System.Drawing.Size);
                // Control の描画を停止
                ViewInnerControl.setVisible(false);
                //ViewInnerControl.PausePaint(true);
                // サイズ変更
                ViewInnerControl.changeSize(newSize);
                // ポジション変更
                //ViewInnerControl.changeLocation(newLocation);
                // Control の描画を再開
                //ViewInnerControl.PausePaint(false);
                ViewInnerControl.setVisible(true);
            }
            catch (Exception ex)
            {
                _errorlog.addException(ex, this.ToString(), "ChangeSizeViewControl Failed");
            }
        }

        // コントロールのサイズと位置変更
        public void ChangeSizeAndLocationForInnerConrol(System.Drawing.Size size,System.Drawing.Point location)
        {
            try
            {
                //System.Drawing.Size System.Drawing.Size = ViewInnerControl.getSize();
                //System.Drawing.Size = new System.Drawing.Size((int)(System.Drawing.Size.Width * raito), (int)(System.Drawing.Size.Height * raito));
                // 拡大縮小時にポジションを変更のための計算
                //System.Drawing.Point newLocation = ChangeLocationWhenChangeSize(ViewInnerControl.getSize(), System.Drawing.Size);
                // Control の描画を停止
                ViewInnerControl.setVisible(false);
                //ViewInnerControl.PausePaint(true);
                // サイズ変更
                ViewInnerControl.changeSize(size);
                // ポジション変更
                ViewInnerControl.changeLocation(location);
                // Control の描画を再開
                //ViewInnerControl.PausePaint(false);
                ViewInnerControl.setVisible(true);
                // 位置とサイズを記憶する
                ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();
            }
            catch (Exception ex)
            {
                _errorlog.addException(ex, this.ToString(), "ChangeSizeViewControl Failed");
            }
        }

        // コントロールの拡大
        public void ChangeSizeViewControl(double raito)
        {
            try
            {
                System.Drawing.Size size = ViewInnerControl.getSize();
                size = new System.Drawing.Size((int)(size.Width * raito), (int)(size.Height * raito));

                // 拡大縮小時にポジションを変更のための計算
                System.Drawing.Point newLocation = ChangeLocationWhenChangeSize(ViewInnerControl.getSize(), size);
                // Control の描画を停止
                ViewInnerControl.setVisible(false);
                //ViewInnerControl.PausePaint(true);
                // サイズ変更
                ViewInnerControl.changeSize(size);
                // ポジション変更
                ViewInnerControl.changeLocation(newLocation);
                // Control の描画を再開
                //ViewInnerControl.PausePaint(false);
                ViewInnerControl.setVisible(true);
                // 位置とサイズを記憶する
                ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();
            } catch (Exception ex)
            {
                _errorlog.addException(ex, this.ToString(), "ChangeSizeViewControl Failed");
            }
        }

        // コントロールの位置変更
        public System.Drawing.Point ChangeLocationWhenChangeSize(System.Drawing.Size beforeSize,System.Drawing.Size afterSize)
        {
            try
            {
                int x = (int)(beforeSize.Width - afterSize.Width)/2;
                int y = (int)(beforeSize.Height - afterSize.Height)/2;
                x += ViewInnerControl.getLocation().X;
                y += ViewInnerControl.getLocation().Y;
                return new System.Drawing.Point(x, y);
            } catch (Exception ex)
            {
                _errorlog.addException(ex, this.ToString(), "ChangeLocationWhenChangeSize Failed");
                return new System.Drawing.Point();
            }
        }
    }
}

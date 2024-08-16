using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer2;
using ErrorLog;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ViewImageAction
{
    public class ViewImageBasicFunction
    {
        private IViewInnerControl _viewImageControl;
        
        public IViewImage ViewImage;
        public FileList.FileListForUse FileList;
        public FileList.FileList FileRegister;
        ErrorLog.IErrorLog _errorLog;
        private Control _parentControl;

        //public ViewImageObjects ViewImageObjects;
        public ViewImageManager ViewImageManager;

        public ViewImageBasicFunction()
        {
        }

        // コンストラクタ
        public IViewInnerControl ViewImageControl
        {
            get { return _viewImageControl; }
            set {
            try
                {
                    _viewImageControl = value;
                    _parentControl = _viewImageControl.getParentControl();
                } catch (Exception ex)
                {
                    _errorLog.addException(ex, this.ToString() + " ViewImageControl");
                }
            }
        }

        public void setErrorLog(ErrorLog.IErrorLog errorLog) { _errorLog = errorLog; }

        public void ViewImageNowIndex()
        {
            try
            {
                ViewImageManager.doFunction(ViewImageNowIndex);

                //List<ViewImageObjects> list = ViewImageManager.getActiveControl();
                //if (!((list is null)|(list.Count < 1)))
                //{
                //    foreach (ViewImageObjects value in list)
                //    {
                //        ViewImageNowIndex(value);
                //    }
                //} else
                //{
                //    _errorLog.addErrorNotException( this.ToString() + " ViewImgeNowIndex");
                //}
            } catch(Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewImgeNowIndex");
                return;
            }
        }

        public int ViewImageNowIndex(ViewImageObjects viewImageObjects)
        {
            try
            {
                int ret = 0;
                // パス取得
                string path = viewImageObjects.FileList.GetNowValue();
                Debug.WriteLine("path (" + viewImageObjects.FileList.NowIndex + ")= " + path);
                // イメージ取得
                ret = viewImageObjects.ViewImage.setPath(path);
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString() + "setPath");
                    return -1;
                }
                // イメージをセット
                viewImageObjects.ViewFrameControl.ViewInnerControl.setImageWithDispose(
                    viewImageObjects.ViewImage.getImage());

                // InnerControl の位置とサイズを記憶する
                viewImageObjects.ViewFrameControl.ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewImage");
                return 0;
            }
        }

        public void ViewNext(Object sender, int s) { ViewNext(); }
        public void ViewNext()
        {
            try
            {
                List<ViewImageObjects> list = ViewImageManager.getActiveControl();
                if (!((list is null) | (list.Count < 1)))
                {
                    foreach (ViewImageObjects value in list)
                    {
                        NextView(value);
                        //ViewImageNowIndex(value); 
                    }
                }
            }
            catch (Exception ex) { _errorLog.addException(ex, this.ToString() + " ViewNext"); }
        }
        private void NextView(ViewImageObjects viewImageObjects)
        {
            try
            {
                viewImageObjects.FileList.MoveNext();
                if (_errorLog.haveError())
                {
                    _errorLog.ShowErrorMessageAndClearError();
                    return;
                }
                ViewImageNowIndex(viewImageObjects);
                if (_errorLog.haveError())
                {
                    _errorLog.ShowErrorMessageAndClearError();
                }
                viewImageObjects.ViewFrameControl.ViewInnerControl.RefreshPaint();
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewNext");
            }
        }


        public void ViewPrevious() { ViewImageManager.doFunction(ViewPrevious); }
        public void ViewPrevious(Object sender, int s) { ViewImageManager.doFunction(ViewPrevious); }
        private int ViewPrevious(ViewImageObjects viewImageObjects)
        {
            try
            {
                viewImageObjects.FileList.MovePrevious();
                if (_errorLog.haveError())
                {
                    _errorLog.ShowErrorMessageAndClearError();
                    return -1;
                }
                ViewImageNowIndex(viewImageObjects);
                if (_errorLog.haveError())
                {
                    _errorLog.ShowErrorMessageAndClearError();
                }
                viewImageObjects.ViewFrameControl.ViewInnerControl.RefreshPaint();
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewNext");
                return 0;
            }
        }

        // 拡大縮小

        // SetPath + ViewImage
        public void ViewImageAfterSetPath(ViewImageObjects viewImageObjects,List<string> list)
        {
            try
            {
                SetPaths(viewImageObjects,list);
                ViewImageDefault(viewImageObjects);

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewImageAfterSetPath");
            }
        }

        public void SetPaths(ViewImageObjects viewImageObjects,List<string> list)
        {
            int ret = viewImageObjects.FileRegister.setFileListWithApplyConditions(list);
            if (ret < 1)
            {
                MessageBox.Show("Control_DragDrop.setFileListWithApplyConditions Failed");
            }
            viewImageObjects.FileList.FileList = viewImageObjects.FileRegister.getList();
            if (_errorLog.haveError())
            {
                _errorLog.ShowErrorMessageAndClearError();
            }
        }


        // サイズ Panel.Size、Location00
        // 画像を表示
        public void ViewImageDefault(ViewImageObjects viewImageObjects)
        {
            try
            {
                int ret = 1;
                Panel frameControl = (Panel)viewImageObjects.ViewFrameControl.getControl();
                // 外側のサイズ
                if (frameControl == null)
                {
                    _errorLog.addErrorNotException(this.ToString(), ".ViewImageDefault frameControl is null"); return;
                }
                Size frameSize = frameControl.Size;
                // SetPath
                string NowPath = viewImageObjects.FileList.GetNowValue();
                if (!(((NowPath == null)|(viewImageObjects.ViewImage.getPath() == null))))
                {
                    // これからの Path と以前の Path のどちらかが空 null ではない 
                    // イメージの読み込みはしない
                } else if (NowPath.CompareTo(viewImageObjects.ViewImage.getPath()) == 0)
                {
                    // 両方とも値がある
                    // 今表示しているものと同じ、この場合イメージの読み込みはしない
                }
                else
                {
                    // 今表示しているものと違う、イメージを読み込みなおす
                    ret = viewImageObjects.ViewImage.setPath(NowPath);
                }

                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString() + "ViewImageDefault setPath"); return;
                }

                if (viewImageObjects.ViewImage.getImage() == null)
                {
                    _errorLog.addErrorNotException(this.ToString() + "ViewImageDefault ViewImage is null"); return;
                }

                // ImageSize
                Size ImageSize = viewImageObjects.ViewImage.getImage().Size;

                // CalcObjectSet
                SizeLocationCalc Calclator = new SizeLocationCalc();
                // Assert
                Calclator.sizeIsNull(frameControl.Size, viewImageObjects.ViewImage.getImage().Size);
                // サイズ計算 Panel にフィットさせる
                Size newSize = Calclator.getSizeFitFrame(frameControl.Size, viewImageObjects.ViewImage.getImage().Size);
                //Size newSize = Calclator.getSizeFitFrame(_parentControl.Size, ViewImageControl.getSize());

                // Location
                if (viewImageObjects.ViewFrameControl.ViewInnerControl.Settings.IsViewAlwaysCenter)
                {
                    // 常に中央に表示する場合
                    // サイズ変更、中央計算
                } else
                {
                    // PictureBox 非固定

                }
                // Default は00
                //Point location = new Point(0, 0);

                viewImageObjects.ViewFrameControl.ViewInnerControl.setVisible(false);

                // イメージをセット
                viewImageObjects.ViewFrameControl.ViewInnerControl.setImageWithDispose(viewImageObjects.ViewImage.getImage());
                // SetSize
                //_viewImageControl.changeSize(newSize);
                viewImageObjects.ViewFrameControl.ViewInnerControl.changeSize(frameControl.Size);
                // SetLocation
                //_viewImageControl.changeLocation(location);

                // visible
                viewImageObjects.ViewFrameControl.ViewInnerControl.setVisible(true);

                // save size and location
                viewImageObjects.ViewFrameControl.ViewInnerControl.saveDifferenceSizeAndPositionFromFramecControl();

            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewImageDefault");
            }
        }

        private Size getViewSizeByCalc()
        {
            try
            {
                return new Size();
            } catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " getViewSizeByCalc");
                return new Size();
            }
        }
        public class SizeLocationCalc
        {
            ErrorLog.IErrorLog _errorLog;
            public void setErrorLog(IErrorLog errorlog) { _errorLog = errorlog; }
            // Image を Frameに合わせる
            public Size getSizeFitFrame(Size frameSize, Size imageSize)
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
                    _errorLog.addException(ex, this.ToString() + " getSizeFitFrame");
                    return new Size();
                }
            }

            public bool sizeIsNull(Size frameSize, Size imageSize)
            {
                try
                {
                    if (frameSize == null)
                    {
                        _errorLog.addErrorNotException(this.ToString() + " getPointLocationCenter ,frameSize null");
                        return false;
                    }
                    if (imageSize == null)
                    {
                        _errorLog.addErrorNotException(this.ToString() + " getPointLocationCenter ,imageSize null");
                        return false;
                    }
                    return true;
                } catch (Exception ex)
                {
                    _errorLog.addException(ex, this.ToString() + " getPointLocationCenter");
                    return false;
                }
            }

            // 中央に表示
            public Point getPointLocationCenter(Size frameSize,Size imageSize)
            {
                try
                {
                    // sizeIsNull を外部で実行しておく
                    double width = (frameSize.Width - imageSize.Width) / 2;
                    double height = (frameSize.Height - imageSize.Height) / 2;
                    return new Point((int)width, (int)height);
                } catch (Exception ex)
                {
                    _errorLog.addException(ex, this.ToString() + " getPointLocationCenter");
                    return new Point();
                }
            }
        }

    }
}
